using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace MnogodetLiteDB {
    public static class Database {
        static LiteDB.LiteDatabase db;
        static LiteDB.ILiteCollection<Person> persons;
        static LiteDB.ILiteCollection<Family> families;
        static LiteDB.ILiteCollection<MenuListDictionary> dictionaries;
        static LiteDB.ILiteCollection<Parameter> parameters;

        public static MenuListDictionary dictDocuments, dictCancelReasons, dictSettlements;

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
            dictSettlements = dictionaries.FindById("settlements");

            /*int deleted = 0;
            foreach (var f in FindFamiliesAll())
            {
                
                if (f.persons.Count == 0)
                {
                    deleted++;
                    families.Delete(f.Id);
                }
            }
            MessageBox.Show($"Удалено {deleted}");
            dictSettlements = new MenuListDictionary();
            dictSettlements.Id = "settlements";
            dictSettlements.displayName = "Населенные пункты";
            dictSettlements.values = new Dictionary<int, string> {
                {0, "Нет"},
                {1000000, "Еврейская автономная область"},
                {1010000, "Муниципальное образование «Город Биробиджан»"},
                {1010100, "Муниципальное образование «Город Биробиджан»"},
                {1010101, "Биробиджан"},
                {1020000, "Биробиджанский район"},
                {1020100, "Бирофельдское сельское поселение"},
                {1020101, "Бирофельд"},
                {1020102, "Алексеевка"},
                {1020103, "Димитрово"},
                {1020104, "Красивое"},
                {1020105, "Опытное Поле"},
                {1020200, "Валдгеймское сельское поселение"},
                {1020201, "Валдгейм"},
                {1020202, "Аэропорт"},
                {1020203, "Жёлтый Яр"},
                {1020204, "Красный Восток"},
                {1020205, "Пронькино"},
                {1020300, "Дубовское сельское поселение"},
                {1020301, "Дубовое"},
                {1020302, "Казанка"},
                {1020400, "Надеждинское сельское поселение"},
                {1020401, "Надеждинское"},
                {1020402, "Головино"},
                {1020500, "Найфельдское сельское поселение"},
                {1020501, "Найфельд"},
                {1020502, "Петровка"},
                {1020503, "Русская Поляна"},
                {1020600, "Птичнинское сельское поселение"},
                {1020601, "Птичник"},
                {1020602, "Кирга"},
                {1020603, "Раздольное"},
                {1030000, "Облученский район"},
                {1030100, "Бираканское городское поселение"},
                {1030101, "Биракан"},
                {1030102, "Новый"},
                {1030103, "Тёплые Ключи"},
                {1030200, "Бирское городское поселение"},
                {1030201, "Бира"},
                {1030202, "Будукан"},
                {1030203, "Семисточный"},
                {1030204, "Трек"},
                {1030300, "Известковское городское поселение"},
                {1030301, "Абрамовка"},
                {1030302, "Двуречье"},
                {1030303, "Известковый"},
                {1030304, "Кимкан"},
                {1030305, "Рудное"},
                {1030306, "Снарский"},
                {1030400, "Кульдурское городское поселение"},
                {1030401, "Кульдур"},
                {1030500, "Облученское городское поселение"},
                {1030501, "Облучье"},
                {1030502, "Лагар-Аул"},
                {1030503, "Соловьёвка"},
                {1030504, "Сутара"},
                {1030505, "Ударный"},
                {1030506, "Хинганск"},
                {1030600, "Теплоозерское городское поселение"},
                {1030601, "Теплоозёрск"},
                {1030602, "Лондоко"},
                {1030603, "Лондоко-завод"},
                {1030700, "Пашковское сельское поселение"},
                {1030701, "Башурово"},
                {1030702, "Заречное"},
                {1030703, "Пашково"},
                {1030704, "Радде"},
                {1040000, "Смидовичский район"},
                {1040100, "Волочаевское городское поселение"},
                {1040101, "Волочаевка-2"},
                {1040102, "Соцгородок"},
                {1040200, "Николаевское городское поселение"},
                {1040201, "Николаевка"},
                {1040202, "Дежнёвка"},
                {1040203, "Ключевое"},
                {1040300, "Приамурское городское поселение"},
                {1040301, "Приамурский"},
                {1040302, "Владимировка"},
                {1040303, "Имени Тельмана"},
                {1040304, "Осиновка"},
                {1040400, "Смидовичское городское поселение"},
                {1040401, "Смидович"},
                {1040402, "Аур"},
                {1040403, "Белгородское"},
                {1040404, "Икура"},
                {1040405, "Оль"},
                {1040406, "Песчаное"},
                {1040407, "Урми"},
                {1040408, "Усов Балаган"},
                {1040500, "Волочаевское сельское поселение"},
                {1040501, "Волочаевка-1"},
                {1040502, "Лумку-Корань"},
                {1040503, "Ольгохта"},
                {1040504, "Партизанское"},
                {1040600, "Камышовское сельское поселение"},
                {1040601, "Камышовка"},
                {1040602, "Даниловка"},
                {1040603, "Дежнёвка"},
                {1040604, "Нижнеспасское"},
                {1050000, "Ленинский район"},
                {1050100, "Бабстовское сельское поселение"},
                {1050101, "Бабстово"},
                {1050102, "Бабстово ЖД станция"},
                {1050103, "Горное"},
                {1050104, "Октябрьское"},
                {1050105, "Целинное"},
                {1050200, "Биджанское сельское поселение"},
                {1050201, "Биджан"},
                {1050202, "Башмак"},
                {1050203, "Венцелево"},
                {1050204, "Кирово"},
                {1050205, "Новотроицкое"},
                {1050206, "Преображеновка"},
                {1050207, "Степное"},
                {1050300, "Дежнёвское сельское поселение"},
                {1050301, "Дежнёво"},
                {1050302, "Квашнино"},
                {1050303, "Новое"},
                {1050400, "Лазаревское сельское поселение"},
                {1050401, "Лазарево"},
                {1050402, "Унгун"},
                {1050500, "Ленинское сельское поселение"},
                {1050501, "Ленинское"},
                {1050502, "Воскресеновка"},
                {1050503, "Калинино"},
                {1050504, "Кукелево"},
                {1050505, "Ленинск ЖД Станция"},
                {1050506, "Нижнеленинское"},
                {1050507, "Чурки"},
                {1060000, "Октябрьский район"},
                {1060100, "Амурзетское сельское поселение"},
                {1060101, "Амурзет"},
                {1060102, "Екатерино-Никольское"},
                {1060103, "Озёрное"},
                {1060104, "Помпеевка"},
                {1060105, "Пузино"},
                {1060106, "Союзное"},
                {1060200, "Нагибовское сельское поселение"},
                {1060201, "Благословенное"},
                {1060202, "Доброе"},
                {1060203, "Нагибово"},
                {1060204, "Ручейки"},
                {1060205, "Садовое"},
                {1060300, "Полевское сельское поселение"},
                {1060301, "Полевое"},
                {1060302, "Луговое"},
                {1060303, "Самара"},
                {1060304, "Столбовое"}
            };
            dictionaries.Insert(dictSettlements);*/
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
            public int settlementId { get; set; }
            public string settlementName
            {
                get { return dictSettlements.values[settlementId]; }
                private set { }
            }
            public string settlementMunObr
            {
                get { return dictSettlements.values[settlementId / 100 * 100]; }
                private set { }
            }
            public string settlementRaion
            {
                get { return dictSettlements.values[settlementId / 10000 * 10000]; }
                private set { }
            }
            public int Id { get; set; }

            public string address { get; set; } = "";
            public string comment { get; set; } = "";

            public List<Person> persons = new List<Person>();
            public string familyName = "";

            public bool IsValidByUdostExpireDate(DateTime date)
            {
                if (GetProblemText() != null) return false;
                if (cancelReason != 0) return false;
                if (udostoverenieExpirationDate < date) return false;
                return true;
            }

            public bool IsValidByUdostExpireDate()
            {
                return IsValidByUdostExpireDate(DateTime.Now);
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

            public List<ListMenuItem> GetMenuListRaion()
            {
                List<ListMenuItem> list = new List<ListMenuItem>();
                foreach (int key in values.Keys)
                {
                    if (key % 10000 != 0) continue;
                    if (key % 1000000 == 0 && key != 0) continue;
                    list.Add(new ListMenuItem(key, values[key]));
                }
                return list;
            }

            public List<ListMenuItem> GetMenuListMunObrByRaion(int raionId)
            {
                List<ListMenuItem> list = new List<ListMenuItem>();
                foreach (int key in values.Keys)
                {
                    if (key != 0 && key == raionId) continue;
                    if (key != 0 && (key / 10000 * 10000 != raionId || key % 100 != 0) ) continue;
                    list.Add(new ListMenuItem(key, values[key]));
                }
                return list;
            }

            public List<ListMenuItem> GetMenuListSettlementsByMunObr(int munObrId)
            {
                List<ListMenuItem> list = new List<ListMenuItem>();
                foreach (int key in values.Keys)
                {
                    if (key != 0 && key == munObrId) continue;
                    if (key != 0 && key / 100 * 100 != munObrId) continue;
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
