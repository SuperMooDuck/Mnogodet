using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Xml;

namespace MnogodetLiteDB
{
    public static class Export
    {
        public static void ExportPfrExcel()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файлы Excel (*.xlsx)|*.xlsx";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            if (System.IO.File.Exists(saveFileDialog.FileName))
                System.IO.File.Delete(saveFileDialog.FileName);
            //OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var excelFile = new OfficeOpenXml.ExcelPackage(saveFileDialog.FileName);
            var workbook = excelFile.Workbook;
            var sheet = workbook.Worksheets.Add("Сведения о многодетных семьях");

            FormExportProgress exportForm = new FormExportProgress();
            exportForm.Show();

            int totalColumns = 15;
            string[] columnCaptions = { "", "Родители", "Дата рождения", "Документ", "Номер", "Дата выдачи", "Место выдачи", "Дети", "Дата рождения", "Документ", "Номер", "Дата выдачи", "Место выдачи", "Дата начала статуса", "Дата окончания", "Адрес" };
            for (int i = 1; i <= totalColumns; i++)
            {
                var cell = sheet.Cells[1, i];
                cell.Value = columnCaptions[i];
                cell.Style.Font.Bold = true;
                cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);
            }

            int familyCellY = 2, familyProcessed = 0, familyExported = 0;
            var familyList = Database.FindFamiliesAll();

            foreach (var f in familyList)
            {
                familyProcessed++;
                exportForm.progressText = familyProcessed.ToString() + " / " + familyList.Count.ToString();
                exportForm.Update();
                if (!f.IsValidByUdostExpireDate()) continue;
                //if (!f.IsValidByUdostExpireDate(DateTime.Now) && !f.IsValidByUdostExpireDate(new DateTime(2024, 04, 16))) continue;
                //if (f.GetProblemText() != null) continue;
                //if (f.udostoverenie.number == "" || f.udostoverenie.number == null) continue;

                int parentCellY = familyCellY, childCellY = familyCellY;
                foreach (var p in f.persons)
                {
                    int cellX, cellY;
                    if (p.type < 2)
                    {
                        cellX = 1;
                        cellY = parentCellY++;
                    }
                    else
                    {
                        //if (p.Age() >= 18) continue;
                        cellX = 7;
                        cellY = childCellY++;
                    }
                    sheet.Cells[cellY, cellX].Value = $"{p.f} {p.i} {p.o}";
                    sheet.Cells[cellY, cellX + 1].Value = p.birthDate.ToString("d");

                    Database.Document document = null;
                    foreach (var d in p.documents)
                    {
                        if (d.typeID == 0)
                        {
                            document = d;
                            break;
                        }
                    }
                    if (document == null) document = p.documents[0];
                    sheet.Cells[cellY, cellX + 2].Value = Database.dictDocuments.values[document.typeID];
                    sheet.Cells[cellY, cellX + 3].Value = document.number;
                    if (document.issuedDate != new DateTime())
                        sheet.Cells[cellY, cellX + 4].Value = document.issuedDate.ToString("d");
                    sheet.Cells[cellY, cellX + 5].Value = document.issuedPlace;
                }
                sheet.Cells[familyCellY, 13].Value = f.StatusStartByChildrenBirthdate().ToString("d");
                sheet.Cells[familyCellY, 14].Value = f.StatusExpirationByChildrenBirthdate().ToString("d");
                sheet.Cells[familyCellY, 15].Value = f.address;
                var familyRange = sheet.Cells[familyCellY, 1, childCellY - 1, totalColumns];
                familyRange.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                familyCellY = childCellY;
                familyExported++;
                f.pfrExportDate = DateTime.Now;
                f.Update();
            }
            sheet.Columns.AutoFit();

            excelFile.Save();
            excelFile.Dispose();
            exportForm.Close();
            MessageBox.Show($"Выгружено {familyExported} семей");
        }
        public static void ExportFNSXML()
        { //DateTime toDate
            int yearToExport = DateTime.Now.Year - 1;
            DateTime yearToExportStart = new DateTime(yearToExport, 1, 1);
            DateTime yearToExportEnd = new DateTime(yearToExport, 12, 31);
            if (MessageBox.Show($"Будут выгружены сведения за {yearToExport} год", "Экспорт в ФНС", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            string filename = $"VO_SVMNDET_7900_7901008992790101001_{DateTime.Now.ToString("yyyyMMdd")}_001";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.DefaultExt = "xml";
            saveFileDialog.Filter = "Файлы XML (*.xml)|*.xml";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = filename;
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            filename = System.IO.Path.GetFileNameWithoutExtension(saveFileDialog.FileName);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "windows-1251", null));

            XmlElement xmlParentNode = xmlDoc.CreateElement("Файл");
            xmlParentNode.SetAttribute("ИдФайл", filename);
            xmlParentNode.SetAttribute("ВерсФорм", "4.01");
            xmlParentNode.SetAttribute("ТипИнф", "СВЕД_МНОГОДЕТ");
            xmlDoc.AppendChild(xmlParentNode);

            XmlElement xmlNode = xmlDoc.CreateElement("СвОргРег");
            xmlNode.SetAttribute("НаимОрг", "Департамент социальной защиты населения правительства Еврейской автономной области");
            xmlNode.SetAttribute("ИННЮЛ", "7901008992");
            xmlNode.SetAttribute("КПП", "790101001");
            xmlNode.SetAttribute("ОГРН", "1027900511726");
            xmlParentNode.AppendChild(xmlNode);

            xmlNode = xmlDoc.CreateElement("НО");
            xmlNode.SetAttribute("КодНО", "7900");
            xmlNode.SetAttribute("НаимНО", "Управление ФНС России по Еврейской автономной области");
            xmlParentNode.AppendChild(xmlNode);

            /*XmlElement newDocsNode = null, changeDocsNode = null;
            for (int i = 0; i <= 1; i++) {
                xmlNode = xmlDoc.CreateElement("Документ");
                xmlNode.SetAttribute("ИдДок", Guid.NewGuid().ToString());
                xmlNode.SetAttribute("КНД", "1114014");
                xmlNode.SetAttribute("ДатаДок", DateTime.Now.ToShortDateString());
                xmlNode.SetAttribute("ТипДок", i == 0 ? "01" : "02");
                if (i == 0) newDocsNode = xmlNode;
                else changeDocsNode = xmlNode;
            }*/


            int exportedFamilies = 0, exportedParents = 0, exportedChildren = 0, newRecords = 0, changedRecords = 0, familyProcessed = 0;
            var familyList = Database.FindFamiliesAll();
            FormExportProgress exportForm = new FormExportProgress();
            exportForm.Show();
            foreach (var f in familyList)
            {
                familyProcessed++;
                exportForm.progressText = familyProcessed.ToString() + " / " + familyList.Count.ToString();
                exportForm.Update();

                if (f.fnsStatus == Database.Family.ExportStatus.Exported)
                    continue;

                if (f.GetProblemText() != null) continue;
                if (!f.IsValidByChildrenNumberAndAge(yearToExportStart) && !f.IsValidByChildrenNumberAndAge(yearToExportEnd)) continue;

                List<Database.Person> children = new List<Database.Person>(), parents = new List<Database.Person>();
                foreach (var p in f.persons)
                {
                    if (p.type < 2) parents.Add(p);
                    else if (p.birthDate <= yearToExportEnd && p.birthDate.AddYears(18) > yearToExportStart) children.Add(p);
                }
                exportedFamilies++;
                exportedParents += parents.Count;
                exportedChildren += children.Count;

                xmlNode = xmlDoc.CreateElement("Документ");
                xmlNode.SetAttribute("ИдДок", Guid.NewGuid().ToString());
                xmlNode.SetAttribute("КНД", "1114014");
                xmlNode.SetAttribute("ДатаДок", DateTime.Now.ToShortDateString());
                xmlNode.SetAttribute("ТипДок", f.fnsStatus == Database.Family.ExportStatus.New ? "01" : "02");

                foreach (var p in parents)
                {
                    XmlElement xmlFamily = xmlDoc.CreateElement("СодСвед");
                    xmlFamily.SetAttribute("КолДет", children.Count.ToString());

                    for (int i = -1; i < children.Count; i++)
                    {
                        Database.Person expPer = i == -1 ? p : children[i];
                        XmlElement xmlPerson = xmlDoc.CreateElement(i == -1 ? "СведФЛ" : "СведДет");
                        xmlPerson.SetAttribute("ДатаРожд", expPer.birthDate.ToShortDateString());
                        XmlElement xmlFio = xmlDoc.CreateElement("ФИО");
                        xmlFio.SetAttribute("Фамилия", expPer.f);
                        xmlFio.SetAttribute("Имя", expPer.i);
                        if (expPer.o != null && expPer.o != "")
                            xmlFio.SetAttribute("Отчество", expPer.o);
                        xmlPerson.AppendChild(xmlFio);

                        bool singleDocExported = false;
                        foreach (var document in expPer.documents)
                        {
                            switch (document.typeID)
                            {
                                case 0: xmlPerson.SetAttribute("СНИЛС", document.number.Replace('-', ' ')); break;
                                case 1: xmlPerson.SetAttribute("ИННФЛ", document.number); break;
                                default:
                                    if (singleDocExported) break;
                                    singleDocExported = true;
                                    XmlElement xmlOtherDoc = xmlDoc.CreateElement("УдЛичнФЛ");
                                    xmlOtherDoc.SetAttribute("КодВидДок", document.typeID.ToString("D2"));
                                    xmlOtherDoc.SetAttribute("СерНомДок", document.number);
                                    xmlOtherDoc.SetAttribute("ДатаДок", document.issuedDate.ToShortDateString());
                                    xmlOtherDoc.SetAttribute("ВыдДок", document.issuedPlace);
                                    if (document.issuedCode != null && document.issuedCode != "")
                                        xmlOtherDoc.SetAttribute("КодВыдДок", document.issuedCode);
                                    xmlPerson.AppendChild(xmlOtherDoc);
                                    break;
                            }
                        }
                        xmlFamily.AppendChild(xmlPerson);
                    }
                    xmlNode.AppendChild(xmlFamily);


                    /*if (f.fnsStatus == Database.Family.ExportStatus.New) {
                        newDocsNode.AppendChild(xmlFamily);
                        newRecords++;
                    }
                    else {
                        changeDocsNode.AppendChild(xmlFamily);
                        changedRecords++;
                    }*/
                }
                if (f.fnsStatus == Database.Family.ExportStatus.New)
                    newRecords++;
                else
                    changedRecords++;
                xmlParentNode.AppendChild(xmlNode);
                f.fnsStatus = Database.Family.ExportStatus.Exported;
                f.exportDate = DateTime.Now;
                f.Update();
            }
            //int docsNum = 0;
            /*if (newDocsNode.ChildNodes.Count > 0) {
                xmlParentNode.AppendChild(newDocsNode);
                docsNum++;
            }
            if (changeDocsNode.ChildNodes.Count > 0) {
                xmlParentNode.AppendChild(changeDocsNode);
                docsNum++;
            }*/
            xmlParentNode.SetAttribute("КолДок", (newRecords + changedRecords).ToString());
            xmlDoc.Save(saveFileDialog.FileName);
            exportForm.Close();
            MessageBox.Show($"Выгружено семей: {exportedFamilies}, родителей: {exportedParents}, детей: {exportedChildren}\n" +
                $"Новых записей: {newRecords}, измененных: {changedRecords}");
        }

        public static void QueryMothers()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файлы Excel (*.xlsx)|*.xlsx";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            if (System.IO.File.Exists(saveFileDialog.FileName))
                System.IO.File.Delete(saveFileDialog.FileName);
            //OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var excelFile = new OfficeOpenXml.ExcelPackage(saveFileDialog.FileName);
            var workbook = excelFile.Workbook;
            var sheet = workbook.Worksheets.Add("Сведения о многодетных семьях");

            FormExportProgress exportForm = new FormExportProgress();
            exportForm.Show();

            int totalColumns = 7;
            for (int i = 1; i <= totalColumns; i++)
            {
                string text = "";
                switch (i)
                {
                    case 1: text = "СНИЛС"; break;
                    case 2: text = "ФИО"; break;
                    case 3: text = "Дата рождения"; break;
                    case 4: text = "Адрес"; break; //3
                    case 5: text = "Кол-во детей"; break;
                    case 6: text = "Даты рождения детей"; break;
                    case 7: text = "Комментарий"; break;
                }
                var cell = sheet.Cells[1, i];
                cell.Value = text;
                cell.Style.Font.Bold = true;
                cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);
            }

            var familyList = Database.FindFamiliesAll();
            int totalFamilies = familyList.Count;
            int familiesProcessed = 0;
            int row = 2;
            foreach (var f in familyList)
            {
                familiesProcessed++;

                if (!f.IsValidByChildrenNumberAndAge(DateTime.Now)) continue;
                Database.Person mother = null;
                foreach (var p in f.persons)
                    if (p.type == 0)
                    {
                        mother = p;
                        break;
                    }
                if (mother == null) continue;

                if (mother.birthDate.Year < 1971 || mother.birthDate.Year > 1975)
                    continue;

                sheet.Cells[row, 2].Value = mother.f + " " + mother.i + " " + mother.o;
                sheet.Cells[row, 3].Value = mother.birthDate.ToShortDateString();
                sheet.Cells[row, 4].Value = f.address;
                sheet.Cells[row, 7].Value = f.comment;

                foreach (var d in mother.documents)
                {
                    if (d.typeID != 0) continue;
                    sheet.Cells[row, 1].Value = d.number;
                }

                int childrenNum = 0;
                string childrenBirthDates = "";
                foreach (var p in f.persons)
                {
                    if (p.type != 2) continue;
                    if (childrenNum > 0) childrenBirthDates += ", ";
                    childrenBirthDates += p.birthDate.ToShortDateString();
                    childrenNum++;
                }
                sheet.Cells[row, 5].Value = childrenNum.ToString();
                sheet.Cells[row, 6].Value = childrenBirthDates;

                row++;
                exportForm.progressText = familiesProcessed.ToString() + " / " + totalFamilies.ToString();
                exportForm.Update();
            }

            excelFile.Save();
            excelFile.Dispose();
            exportForm.Close();
        }

        public static void FullFamiliesChildrenNum()
        {
            var familyList = Database.FindFamiliesAll();
            int[] familiesNum = new int[13];

            foreach (var f in familyList)
            {
                if (f.IsValidByChildrenNumberAndAge(DateTime.Parse("01.01.2021"))) continue;
                int cNum = 0, pNum = 0;
                foreach (var p in f.persons)
                {
                    if (p.type == 2) cNum++;
                    else pNum++;
                }
                if (pNum < 2) continue;
                familiesNum[cNum]++;
            }

            string s = "";
            for (int i = 1; i < 13; i++) s += i.ToString() + " - " + familiesNum[i].ToString() + ", ";
            MessageBox.Show(s);
        }

        public static void QueryChildrenNum()
        {
            DateTime toDate = DateTime.Parse("01.05.2023");
            var familyList = Database.FindFamiliesAll();
            int[] childrenNum = new int[2];

            foreach (var f in familyList)
            {
                if (!f.IsValidByChildrenNumberAndAge(toDate)) continue;
                foreach (var p in f.persons)
                {
                    if (p.type != 2) continue;
                    TimeSpan dateDiff = toDate - p.birthDate;
                    if (dateDiff.TotalDays < 0) continue;
                    else if (dateDiff.TotalDays < 548) childrenNum[0]++;
                    else if (dateDiff.TotalDays < 1095) childrenNum[1]++;
                }
            }
            MessageBox.Show("0-1.5: " + childrenNum[0] + ", 1.5-3: " + childrenNum[1]);
        }

        public static void FillGender()
        {
            var personsList = Database.FindPersonsAll();
            int processed = 0;
            List<List<string>> names = new List<List<string>>(2);
            names.Add(new List<string>());
            names.Add(new List<string>());
            foreach (var p in personsList)
            {
                processed++;
                if (p.type < 2)
                {
                    p.gender = (Database.Person.Gender)p.type;
                    p.Update();
                    if (!names[p.type].Contains(p.i))
                        names[p.type].Add(p.i);
                    continue;
                }

                bool nameFound = false;
                for (int i = 0; i < 2; i++)
                {
                    foreach (var name in names[i])
                    {
                        if (name == p.i)
                        {
                            p.gender = (Database.Person.Gender)i;
                            p.Update();
                            nameFound = true;
                            i = 2;
                            break;
                        }
                    }
                }
                if (nameFound) continue;

                DialogResult dialogResult;
                do
                {
                    dialogResult = MessageBox.Show(processed.ToString() + "/" + personsList.Count + "\n" + p.f + " " + p.i + " " + p.o + "\n Да - мужской, Нет - женский", "Выберите пол", MessageBoxButtons.YesNo);
                } while (dialogResult != DialogResult.Yes && dialogResult != DialogResult.No);

                if (dialogResult == DialogResult.Yes)
                {
                    p.gender = Database.Person.Gender.Male;
                    names[1].Add(p.i);
                }
                else
                {
                    p.gender = Database.Person.Gender.Female;
                    names[0].Add(p.i);
                }
                p.Update();
            }
        }

        public static void ExportPfrCsv()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файлы CSV (*.csv)|*.csv";
            saveFileDialog.AddExtension = true;

            saveFileDialog.Title = "Основной файл";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            if (System.IO.File.Exists(saveFileDialog.FileName))
                System.IO.File.Delete(saveFileDialog.FileName);
            var mainStreamWriter = new System.IO.StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8); //System.Text.Encoding.GetEncoding("CP866")

            saveFileDialog.Title = "Файл с семьями без снилсов";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            if (System.IO.File.Exists(saveFileDialog.FileName))
                System.IO.File.Delete(saveFileDialog.FileName);
            var secondStreamWriter = new System.IO.StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8); //System.Text.Encoding.GetEncoding("CP866")

            FormExportProgress exportForm = new FormExportProgress();
            exportForm.Show();
            int familyProcessed = 0, familyExported = 0;

            string subjectRF = "79";
            string organization = "Департамент социальной защиты населения правительства ЕАО;7901008992;790101001";
            string NPA = "757-ОЗ";
            string workerFio = "Галактионова;Наталья;Георгиевна;Главный специалист-эксперт";


            var familyList = Database.FindFamiliesAll();

            foreach (var f in familyList)
            {
                familyProcessed++;
                exportForm.progressText = familyProcessed.ToString() + " / " + familyList.Count.ToString();
                exportForm.Update();
                if (!f.IsValidByUdostExpireDate()) continue;

                string familyToWrite = "";
                bool snilsNotPresent = false;

                foreach (var p in f.persons)
                {
                    familyToWrite += f.Id.ToString() + ";1;000;" + subjectRF + ";";
                    Database.Document snils = null;
                    foreach (var d in p.documents)
                        if (d.typeID == 0)
                        {
                            snils = d;
                            break;
                        }
                    if (snils != null) familyToWrite += snils.number;
                    else snilsNotPresent = true;
                    familyToWrite += $";{p.f};{p.i};{p.o};{p.birthDate.ToShortDateString()};{(p.type == 0 ? true : false)};";
                    if (p.type < 2)
                        familyToWrite += (p.type + 1).ToString();
                    else
                        familyToWrite += (p.gender == Database.Person.Gender.Male ? "3" : "4");
                    if (f.udostoverenie.number != "")
                    {
                        familyToWrite += $";{f.udostoverenie.issuedDate.ToShortDateString()};{f.udostoverenieExpirationDate.ToShortDateString()};{f.udostoverenie.number};{f.udostoverenie.issuedDate.ToShortDateString()};";
                        familyToWrite += $"{organization};{NPA};{f.udostoverenie.issuedDate.ToShortDateString()};{f.udostoverenieExpirationDate.ToShortDateString()};{workerFio};\n";
                    }
                    else
                    {
                        familyToWrite += $";{f.StatusStartByChildrenBirthdate().ToShortDateString()};{f.StatusExpirationByChildrenBirthdate().ToShortDateString()};0;{f.StatusStartByChildrenBirthdate().ToShortDateString()};";
                        familyToWrite += $"{organization};{NPA};{f.StatusStartByChildrenBirthdate().ToShortDateString()};{f.StatusExpirationByChildrenBirthdate().ToShortDateString()};{workerFio};\n";
                    }

                }
                if (!snilsNotPresent)
                    mainStreamWriter.Write(familyToWrite);
                else
                    secondStreamWriter.Write(familyToWrite);
                familyExported++;
            }

            mainStreamWriter.Close();
            secondStreamWriter.Close();
            exportForm.Close();
            MessageBox.Show($"Выгружено {familyExported} семей");
        }

        public static void AddSnilsFromFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы Excel (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            var excelFile = new OfficeOpenXml.ExcelPackage(openFileDialog.FileName);
            var table = excelFile.Workbook.Worksheets[0];

            var exportForm = new FormExportProgress();
            exportForm.Show();

            int i = 1;
            while (table.Cells[i, 1].Value != null)
            {
                foreach (var p in Database.FindPersonsAll())
                {
                    if (p.f.ToLower().Replace("ё", "е") != ((string)table.Cells[i, 2].Value).ToLower().Replace("ё", "е") || 
                    p.i.ToLower().Replace("ё", "е") != ((string)table.Cells[i, 3].Value).ToLower().Replace("ё", "е") ||
                    p.o.ToLower().Replace("ё", "е") != ((string)table.Cells[i, 4].Value).ToLower().Replace("ё", "е") || 
                    p.birthDate != (DateTime)table.Cells[i, 5].Value)
                    {
                        table.Cells[i, 6].Value = "Не найден";
                        continue;
                    }
                    if (p.SNILS != null)
                    {
                        table.Cells[i, 6].Value = "СНИЛС уже есть";
                        break;
                    }

                    p.documents.Add(new Database.Document
                    {
                        typeID = 0,
                        number = (string)table.Cells[i, 1].Value
                    });
                    p.Update();
                    table.Cells[i, 6].Value = "СНИЛС внесен";

                    break;
                }

                exportForm.progressText = (i).ToString();
                exportForm.Update();
                i++;
            }
            excelFile.Save();
            excelFile.Dispose();
            exportForm.Close();
        }

        public static void QueryChildrenBornInPeriod(DateTime periodStart, DateTime periodEnd, DateTime forValidToDate)
        {
            //DateTime periodStart = DateTime.Parse("01.01.2019");
            //DateTime periodEnd = DateTime.Parse("31.12.2023");
            var familyList = Database.FindFamiliesAll();
            int childrenNum = 0;

            foreach (var f in familyList)
            {
                if (f.GetProblemText() != null) continue;
                if (!f.IsValidByChildrenNumberAndAge(forValidToDate)) continue;
                foreach (var p in f.persons)
                {
                    if (p.type != 2) continue;
                    if (p.birthDate >= periodStart && p.birthDate <= periodEnd)
                    {
                        childrenNum++;
                    }
                }
            }
            MessageBox.Show("Детей : " + childrenNum);

        }

        public static void QueryChildrenOfAge(int ageStart, int ageEnd, DateTime forValidToDate)
        {
            var familyList = Database.FindFamiliesAll();
            int childrenNum = 0;

            foreach (var f in familyList)
            {
                if (f.GetProblemText() != null) continue;
                if (!f.IsValidByChildrenNumberAndAge(forValidToDate)) continue;
                foreach (var p in f.persons)
                {
                    if (p.type != 2) continue;
                    if (p.AgeToDate(forValidToDate) >= ageStart && p.AgeToDate(forValidToDate) <= ageEnd)
                    {
                        childrenNum++;
                    }
                }
            }
            MessageBox.Show("Детей : " + childrenNum);
        }

        public static void QueryNumberOfChildrenBornInYear()
        {
            var familyList = Database.FindFamiliesAll();
            for (int year = 2018; year <= 2024; year++)
            {
                int childrenNumber = 0;
                foreach (var f in familyList)
                {
                    if (f.GetProblemText() != null) continue;
                    foreach (var p in f.persons)
                    {
                        if (p.type != 2) continue;
                        if (p.birthDate > new DateTime(year, 1, 1) && p.birthDate < new DateTime(year, 12, 31)) childrenNumber++;
                    }
                }
                MessageBox.Show(year.ToString() + ": " + childrenNumber.ToString());
            }
        }

        public static void QueryNumberOfChildrenSingleParent(string dateString)
        {
            FormExportProgress exportForm = new FormExportProgress();
            exportForm.progressText = "Загрузка";
            exportForm.Show();
            if (!DateTime.TryParse(dateString, out DateTime date))
            {
                exportForm.Close();
                MessageBox.Show("Неверная дата");
                return;
            }
            var familyList = Database.FindFamiliesAll();
            var familyNumberByChildrenNumber = new int[20];
            int counter = 0;
            foreach (var f in familyList)
            {
                if (!f.IsValidByChildrenNumberAndAge(date)) continue;
                int parents = 0, children = 0;
                foreach (var p in f.persons)
                    if (p.type != 2) parents++; else children++;
                if (parents >= 2) continue;
                familyNumberByChildrenNumber[children]++;
                exportForm.progressText = counter++.ToString();
                exportForm.Update();
            }
            exportForm.Close();
            string text = "";
            int totalFamilies = 0, totalChildren = 0;
            for (int childrenNumber = 3; childrenNumber < 20; childrenNumber++)
            {
                int familiesNum = familyNumberByChildrenNumber[childrenNumber];
                if (familiesNum == 0) continue;
                totalFamilies += familiesNum;
                totalChildren += childrenNumber * familiesNum;
                text += $"{childrenNumber} детей в семье * {familiesNum} семей = {childrenNumber * familiesNum} детей всего\n";
            }
            text += $"Всего семей: {totalFamilies}\nВсего детей:{totalChildren}";
            MessageBox.Show(text);
        }

        public static void FindPeopleFromExcelFIO()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы Excel (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            var excelFile = new OfficeOpenXml.ExcelPackage(openFileDialog.FileName);
            var table = excelFile.Workbook.Worksheets[0];
            var persons = Database.FindPersonsAll();

            var exportForm = new FormExportProgress();
            exportForm.Show();

            int i = 2;
            while (table.Cells[i, 1].Value != null)
            {
                /*string s = ((string)table.Cells[i, 1].Value).ToLower().Replace("ё", "е");
                string[] fio = s.Split(' ');
                if (fio.Length != 3) {
                    table.Cells[i, 7].Value = "Пропущен";
                    i++;
                    continue;
                }*/
                string fio = ((string)table.Cells[i, 1].Value).ToLower().Replace("ё", "е");

                bool personFound = false;
                foreach (var p in persons)
                {
                    /*if (p.f.ToLower().Replace("ё", "е") != fio[0]) continue;
                    if (p.i.ToLower().Replace("ё", "е") != fio[1]) continue;
                    if (p.o.ToLower().Replace("ё", "е") != fio[2]) continue;*/
                    if ((p.f + " " + p.i + " " + p.o).ToLower().Replace("ё", "е") != fio) continue;
                    personFound = true;
                }
                table.Cells[i, 7].Value = personFound ? "Найден" : "Не найден";

                exportForm.progressText = (i).ToString();
                exportForm.Update();
                i++;
            }

            exportForm.Close();
            excelFile.Save();
            excelFile.Dispose();
        }

        public static void FindPeopleFromExcelSNILS()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы Excel (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            var excelFile = new OfficeOpenXml.ExcelPackage(openFileDialog.FileName);
            var table = excelFile.Workbook.Worksheets[0];
            var persons = Database.FindPersonsAll();

            var exportForm = new FormExportProgress();
            exportForm.Show();

            int i = 1;
            while (table.Cells[i, 1].Value != null)
            {
                string SNILS = ((string)table.Cells[i, 1].Value);

                bool personFound = false;
                foreach (var p in persons)
                {
                    foreach (var d in p.documents)
                    {
                        if (d.typeID != 0) continue;
                        if (d.number != SNILS) continue;
                        personFound = true;
                        break;
                    }
                    if (personFound)
                    {
                        table.Cells[i, 2].Value = p.f;
                        table.Cells[i, 3].Value = p.i;
                        table.Cells[i, 4].Value = p.o;
                        table.Cells[i, 5].Value = p.birthDate.ToShortDateString();
                        table.Cells[i, 6].Value = p.type;
                        break;
                    }
                }

                exportForm.progressText = (i++).ToString();
                exportForm.Update();
            }

            exportForm.Close();
            excelFile.Save();
            excelFile.Dispose();
        }
        
        public static void ParseAddresses()
        {
            var exportForm = new FormExportProgress();
            exportForm.Show();
            FormMain.formObject.gridPersons.Rows.Clear();

            var families = Database.FindFamiliesAll();
            var settlements = new Dictionary<int, string>();
            foreach (var s in Database.dictSettlements.values)
                if (s.Key % 100 != 0) settlements.Add(s.Key, s.Value);
            int i = 0;

            foreach (var f in families)
            {
                foreach (var s in settlements)
                    if (f.address.ToLower().Replace("ё", "е").Contains(s.Value.ToLower().Replace("ё", "е")))
                    {
                        f.settlementId = s.Key;
                        f.Update();
                        break;
                    }
                if (f.settlementId == 0)
                {
                    var p = f.persons[0];
                    FormMain.formObject.gridPersons.Rows.Add(new object[] { f.Id, p.f, p.i, p.o, p.birthDate.ToShortDateString() });
                }
                exportForm.progressText = (i++).ToString() + " / " + families.Count.ToString();
                exportForm.Update();
            }
            exportForm.Close();
        }

        public static void MonthlyReport()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файлы Excel (*.xlsx)|*.xlsx";
            saveFileDialog.AddExtension = true;
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            const int calcsSize = 12;
            var calcs = new Dictionary<int, Dictionary<int, int[]>>();

            foreach (var s in Database.dictSettlements.values)
            {
                int id = s.Key;
                if (id % 1000000 == 0) continue;
                if (id % 10000 == 0) calcs.Add(id, new Dictionary<int, int[]>());
                else if (id % 100 == 0) calcs[id / 10000 * 10000].Add(id, new int[calcsSize]);
            }

            var exportForm = new FormExportProgress();
            exportForm.Show();

            int progress = 0;
            var families = Database.FindFamiliesAll();
            foreach (var f in families)
            {
                if (f.GetProblemText() != null) continue;
                if (f.settlementId == 0) continue;

                var munObrList = calcs[f.settlementId / 10000 * 10000][f.settlementId / 100 * 100];
                int childrenNum = 0;
                foreach (var p in f.persons)
                    if (p.type == 2) childrenNum++;
                munObrList[0]++;
                munObrList[1] += childrenNum;
                
                int listIndex = 0;
                if (childrenNum >= 11) listIndex = 10;
                else if (childrenNum >= 8) listIndex = 8;
                else if (childrenNum >= 5) listIndex = 6;
                else if (childrenNum == 4) listIndex = 4;
                else if (childrenNum == 3) listIndex = 2;
                munObrList[listIndex]++;
                munObrList[listIndex+1] += childrenNum;

                exportForm.progressText = (progress++).ToString() + " / " + families.Count.ToString();
                exportForm.Update();
            }

            exportForm.Close();

            if (System.IO.File.Exists(saveFileDialog.FileName))
                System.IO.File.Delete(saveFileDialog.FileName);
            var excelFile = new OfficeOpenXml.ExcelPackage(saveFileDialog.FileName);
            var workbook = excelFile.Workbook;
            var sheet = workbook.Worksheets.Add("Сведения о многодетных семьях");

            var captions = new Dictionary<string, string>()
            {
                {"A1:N1", "Информация о численности многодетных семей в Еврейской автономной области по состоянию на " + DateTime.Now.ToShortDateString()},
                {"A2:A4", "№" },
                {"B2:B4", "Наименование поселения" },
                {"C2:C4", "Общая численность многодетных семей (всего)" },
                {"D2:D4", "Численность в них детей (всего)" },
                {"E2:F2", "Семей с 3 детьми" }, {"E3:F3", "Численность" }, {"E4", "семей" }, {"F4", "в них детей" },
                {"G2:H2", "Семей с 4 детьми" }, {"G3:H3", "Численность" }, {"G4", "семей" }, {"H4", "в них детей" },
                {"I2:J2", "Семей с 5-7 детьми" }, {"I3:J3", "Численность" }, {"I4", "семей" }, {"J4", "в них детей" },
                {"K2:L2", "Семей с 8-10 детьми" }, {"K3:L3", "Численность" }, {"K4", "семей" }, {"L4", "в них детей" },
                {"M2:N2", "Семей с 11 детьми и более" }, {"M3:N3", "Численность" }, {"M4", "семей" }, {"N4", "в них детей" }
            };

            foreach (var c in captions)
            {
                var cell = sheet.Cells[c.Key];
                cell.Merge = true;
                cell.Value = c.Value;
                var style = cell.Style;
                style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                style.WrapText = true;
                style.Font.Bold = true;
                style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            }
            sheet.Columns[2].Width = 50;

            int curRow = 5;
            int num = 1;
            foreach (var raion in calcs)
            {
                var captionCell = sheet.Cells[$"A{curRow}:N{curRow}"];
                captionCell.Merge = true;
                captionCell.Value = Database.dictSettlements.values[raion.Key];
                var captionStyle = captionCell.Style;
                captionStyle.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                captionStyle.Font.Bold = true;
                captionStyle.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                curRow++;

                var totalsList = new int[calcsSize];
                foreach (var munObr in raion.Value)
                {
                    sheet.Cells[curRow, 1].Value = num++;
                    sheet.Cells[curRow, 2].Value = Database.dictSettlements.values[munObr.Key];
                    for (int i = 1; i < calcsSize + 3; i++)
                    {
                        var cell = sheet.Cells[curRow, i];

                        if (i == 1) cell.Value = num++;
                        else if (i == 2) cell.Value = Database.dictSettlements.values[munObr.Key];
                        else
                        {
                            totalsList[i - 3] += munObr.Value[i - 3];
                            cell.Value = munObr.Value[i - 3];
                        }
                        cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                    }
                    curRow++;
                }

                for (int i = 1; i < calcsSize + 3; i++)
                {
                    var cell = sheet.Cells[curRow, i];
                    if (i == 2) cell.Value = "Итого:";
                    else if (i > 2) cell.Value = totalsList[i - 3];
                    cell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                }
                curRow++;
            }

            excelFile.Save();
            excelFile.Dispose();
        }
    }
}