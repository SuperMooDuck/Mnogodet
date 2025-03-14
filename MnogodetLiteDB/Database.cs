using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MnogodetLiteDB {
    public static class Database {
        static LiteDB.LiteDatabase db;
        static LiteDB.ILiteCollection<Person> persons;
        static LiteDB.ILiteCollection<Family> families;
        static LiteDB.ILiteCollection<MenuListDictionary> dictionaries;
        static LiteDB.ILiteCollection<Parameter> parameters;

        public static MenuListDictionary dictDocuments, dictCancelReasons;

        static LiteDB.ILiteCollection<T> GetCollection<T>() {
            if (typeof(T) == typeof(Person)) return (LiteDB.ILiteCollection<T>)persons;
            if (typeof(T) == typeof(Family)) return (LiteDB.ILiteCollection<T>)families;
            if (typeof(T) == typeof(MenuListDictionary)) return (LiteDB.ILiteCollection<T>)dictionaries;
            throw new Exception();
        }

        public static void Open() {
            LiteDB.BsonMapper.Global.EmptyStringToNull = false;
            db = new LiteDB.LiteDatabase("filename = database.db; connection = direct");
            persons = db.GetCollection<Person>("persons");
            families = db.GetCollection<Family>("families");
            parameters = db.GetCollection<Parameter>("parameters");
            dictionaries = db.GetCollection<MenuListDictionary>("dictionaries");
            dictDocuments = dictionaries.FindById("documents");
            dictCancelReasons = dictionaries.FindById("cancelReasons");
        }

        public static void EnsureIndex<T, K>(System.Linq.Expressions.Expression<Func<T, K>> expression) {
            GetCollection<T>().EnsureIndex<K>(expression);
        }

        public static Person FindPerson(System.Linq.Expressions.Expression<Func<Person, bool>> expression) {
            return persons.FindOne(expression);
        }

        public static Person FindPersonById(int personId) {
            return persons.FindById(personId);
        }

        public static List<Person> FindPersonsAll() {
            return persons.FindAll().ToList();
        }

        public static Family FindFamilyById(int familyId) {
            Family f = families.FindById(familyId);
            if (f == null) return null;
            f.persons.Clear();
            int familyNameType = 3;
            foreach (Person person in persons.Find(p => p.familyId == familyId)) {
                f.persons.Add(person);
                if (person.type < familyNameType) {
                    familyNameType = person.type;
                    f.familyName = person.f;
                }
            }
            return f;
        }

        public static List<Family> FindFamiliesAll() {
            var list = families.FindAll().ToList();
            foreach (Family f in list) {
                foreach (Person person in persons.Find(p => p.familyId == f.Id)) {
                    f.persons.Add(person);
                }
            }
            return list;
        }

        public static List<MenuListDictionary> FindDictsAll() {
            return dictionaries.FindAll().ToList();
        }

        public static void UpdateDictionary(MenuListDictionary dictionary) {
            dictionaries.Update(dictionary);
        }

        public static void UpdateParameter(string name, string value) {
            var parameter = parameters.FindOne(p => p.Name == name);
            if (parameter == null) {
                parameters.Insert(new Parameter { Name = name, Value = value });
                return;
            }
            parameter.Value = value;
            parameters.Update(parameter);
        }

        public static void ReplaceParameters(Dictionary<string,string> newParameters) {
            parameters.DeleteAll();
            foreach (var p in newParameters) {
                parameters.Insert(new Parameter { Name = p.Key, Value = p.Value });
            }
        }

        public static List<Parameter> FindParametersAll() {
            return parameters.FindAll().ToList();
        }

        public static string FindParameter(string name) {
            return parameters.FindOne(p => p.Name == name).Value;
        }

        public class Family {
            public enum ExportStatus { New, Exported, Changed };
            public ExportStatus fnsStatus { get; set; }
            public DateTime exportDate { get; set; }
            public ExportStatus pfrStatus { get; set; }
            public DateTime pfrExportDate { get; set; }
            public int cancelReason { get; set; }
            public DateTime cancelReasonDate { get; set; }
            public DateTime creationDate { get; set; }
            public Document udostoverenie { get; set; }
            public DateTime udostoverenieExpirationDate { get; set; }
            public int Id { get; set; }

            public string address { get; set; } = "";
            public string comment { get; set; } = "";

            public List<Person> persons = new List<Person>();
            public string familyName = "";

            public bool IsValidByUdostExpireDate() {
                if (GetProblemText() != null) return false;
                if (cancelReason != 0) return false;
                if (udostoverenieExpirationDate < DateTime.Now) return false;
                return true;
            }

            public bool IsValidByUdostExpireDate(DateTime date)
            {
                if (GetProblemText() != null) return false;
                if (cancelReason != 0) return false;
                if (udostoverenieExpirationDate < date) return false;
                return true;
            }

            public bool IsValidForFNSOnDate(DateTime date) {
                if (GetProblemText() != null) return false;
                //if (cancelReason != 0) return false;
                return IsValidByChildrenNumberAndAge(date);
            }

            public bool IsValidByChildrenNumberAndAge(DateTime date) {
                int validChildrenNum = 0;
                foreach (Person p in persons) {
                    if (p.type != 2) continue;
                    if (p.birthDate > date) continue;
                    if (p.birthDate.AddYears(18) <= date) continue;
                    validChildrenNum++;
                }
                return validChildrenNum >= 3;
            }

            public string GetProblemText() {
                int parentsNum = 0, childrenNum = 0;
                string error = null;

                foreach (Person p in persons) {
                    if (p.type < 2) parentsNum++;
                    else childrenNum++;

                    string e = p.GetProblemText();
                    if (e != null)
                        switch (p.type) {
                            case 0: error = "Мать: " + e; break;
                            case 1: error = "Отец: " + e; break;
                            case 2: error = "Ребенок: " + e; break;
                        }
                }
                if (parentsNum == 0) return "Отсутствуют родители";
                if (childrenNum < 3) return "Меньше трех детей";
                return error;
            }

            public DateTime StatusExpirationByChildrenBirthdate() {
                int childrenNum = 0, validChildrenNum = 0;
                Person validOldestChild = null, invalidYoungestChild = null;
                foreach (Person p in persons) {
                    if (p.type != 2) continue;
                    childrenNum++;
                    if (p.birthDate > DateTime.Now || p.birthDate.AddYears(18) <= DateTime.Now) {
                        if (invalidYoungestChild == null || invalidYoungestChild.birthDate < p.birthDate)
                            invalidYoungestChild = p;
                        continue;
                    }
                    validChildrenNum++;
                    if (validOldestChild == null || validOldestChild.birthDate > p.birthDate)
                        validOldestChild = p;
                }
                if (childrenNum < 3) return new DateTime(1900, 1, 1);
                if (validChildrenNum < 3) return invalidYoungestChild.birthDate.AddYears(18);

                return validOldestChild.birthDate.AddYears(18);
            }

            public DateTime StatusStartByChildrenBirthdate() {
                int childrenNum = 0, validChildrenNum = 0;
                Person validYoungestChild = null;
                foreach (Person p in persons) {
                    if (p.type != 2) continue;
                    childrenNum++;
                    if (p.birthDate > DateTime.Now) continue;
                    if (p.birthDate.AddYears(18) <= DateTime.Now)
                        continue;
                    validChildrenNum++;
                    if (validYoungestChild == null || validYoungestChild.birthDate < p.birthDate)
                        validYoungestChild = p;
                }
                if (childrenNum < 3 || validChildrenNum < 3) return new DateTime(3000, 1, 1);

                return validYoungestChild.birthDate;
            }

            public Person FindPersonById(int personId) {
                foreach (var p in persons)
                    if (p.Id == personId)
                        return p;
                return null;
            }

            public void Update() {
                families.Update(this);
            }

            public void Insert() {
                families.Insert(this);
            }

            public void Delete() {
                families.Delete(Id);
            }
        }
        public class Person {
            public int Id { get; set; }
            public int familyId { get; set; }
            public string f { get; set; } = "";
            public string i { get; set; } = "";
            public string o { get; set; } = "";

            public enum Gender { Female, Male};
            public Gender gender { get; set; }
            public DateTime birthDate { get; set; }
            public List<Document> documents { get; set; } = new List<Document>();
            public int type { get; set; } //0 - mother, 1 - father, 2 - child

            static Regex fioRule = new Regex(@"^[а-яА-ЯёЁ]+(?:[\s-][а-яА-ЯёЁ]+)*$");

            public Document SNILS {
                get {
                    foreach (var d in documents) {
                        if (d.typeID == 0) return d;
                    }
                    return null;
                }
                private set { }
            }

            public string GetProblemText() {
                if (String.IsNullOrEmpty(i)) return "Отсутствует имя";
                if (String.IsNullOrEmpty(f)) return "Отсутствует фамилия";
                if (!fioRule.IsMatch(i) || !fioRule.IsMatch(f) || (!String.IsNullOrEmpty(o) && !fioRule.IsMatch(o))) return "Неверное ФИО";
                if (birthDate.Year < 1900) return "Неверная дата рождения";
                if (birthDate > DateTime.Now) return "Дата рождения больше текущей";
                if (documents.Count == 0) return "Отсутствует документ";
                foreach (Document d in documents)
                    if (!d.IsValid()) return "Неверно заполнен документ";
                return null;
            }

            public int Age() {
                return AgeToDate(DateTime.Now);
            }

            public int AgeToDate(DateTime date) {
                int age = date.Year - birthDate.Year;
                if (birthDate.Month < date.Month || birthDate.Day < date.Day)
                    age--;
                return age;
            }

            public void Update() {
                persons.Update(this);
            }

            public void Insert() {

                persons.Insert(this);
                persons.EnsureIndex(p => p.familyId);
            }

            public void Delete() {
                persons.Delete(Id);
            }

            public bool IsFioDateEqualsTo(Person p) {
                return p.f.ToLower().Replace("ё","е") == f.ToLower().Replace("ё", "е") && p.i.ToLower().Replace("ё", "е") == i.ToLower().Replace("ё", "е") && p.o.ToLower().Replace("ё", "е") == o.ToLower().Replace("ё", "е") && p.birthDate == birthDate;
            }
        }
        public class Document {
            static Regex reSnils = new Regex(@"^(\d{3})-(\d{3})-(\d{3})\s(\d{2})$", RegexOptions.Compiled);
            static Regex reInn = new Regex(@"^\d{12}$", RegexOptions.Compiled);
            static Regex rePassportRF = new Regex(@"^\d{2} \d{2} \d{6}\d{0,1}$", RegexOptions.Compiled);
            static Regex reIssuedCode = new Regex(@"^\d{3}-\d{3}$", RegexOptions.Compiled);
            public int typeID { get; set; }
            //string number;
            public string issuedPlace { get; set; }
            public string issuedCode { get; set; }
            public DateTime issuedDate { get; set; }
            public string number { get; set; }

            public bool IsValid() {
                if (String.IsNullOrEmpty(number)) return false;
                if (!String.IsNullOrEmpty(issuedCode) && !reIssuedCode.IsMatch(issuedCode)) return false;
                    if (typeID > 1) {
                    if (issuedPlace == null || issuedPlace == "") return false;
                    if (issuedDate == new DateTime()) return false;
                    if (issuedDate > DateTime.Now) return false;
                }
                switch (typeID) {
                    case 0:
                        Match matchSNILS = reSnils.Match(number);
                        if (!matchSNILS.Success) return false;
                        break;
                    case 1:
                        if (!reInn.IsMatch(number)) return false;
                        break;
                    case 21:
                        if (!rePassportRF.IsMatch(number)) return false;
                        break;
                }
                return true;
            }

        }

        public class MenuListDictionary {
            public string Id { get; set; }
            public string displayName { get; set; }
            public Dictionary<int, string> values { get; set; }

            public List<ListMenuItem> GetMenuList() {
                List<ListMenuItem> list = new List<ListMenuItem>();
                foreach (int key in values.Keys) {
                    list.Add(new ListMenuItem(key, values[key]));
                }
                return list;
            }
        }

        public class ListMenuItem {
            public string Name { get; set; }
            public int Value { get; set; }
            public ListMenuItem(int value, string name) {
                Name = name;
                Value = value;
            }
        }

        public class Parameter {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }

}
