﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace MnogodetLiteDB {
    public static class Export {

        public static void CompareList2() {
            FormExportProgress exportForm = new FormExportProgress();
            exportForm.Show();

            var dbFamilies = Database.FindFamiliesAll();

            var MfcFile = new OfficeOpenXml.ExcelPackage("Многодетные из МФЦ.xlsx");
            var MfcTable = MfcFile.Workbook.Worksheets[0];
            var MfcFamilies = new List<Database.Family>();

            var filename = "У нас есть, в МФЦ нет.xlsx";
            if (System.IO.File.Exists(filename)) System.IO.File.Delete(filename);
            var exportAssFile = new OfficeOpenXml.ExcelPackage(filename);
            var exportAssTable = exportAssFile.Workbook.Worksheets.Add("1");
            int assCount = 1;

            int row = 1;
            while (MfcTable.Cells[row, 1].Value != null) {

                var f = new Database.Family();
                
                while (true) {
                    var p = new Database.Person() {
                        f = (string)MfcTable.Cells[row, 2].Value,
                        i = (string)MfcTable.Cells[row, 3].Value,
                        o = (string)MfcTable.Cells[row, 4].Value,
                        birthDate = DateTime.Parse((string)MfcTable.Cells[row, 5].Value)
                    };
                    if (p.Age() < 18) p.type = 2;
                    f.persons.Add(p);
                    MfcFamilies.Add(f);
                    row++;
                    if ((MfcTable.Cells[row, 1].Value != null && MfcTable.Cells[row, 1].Value != "") || MfcTable.Cells[row, 2].Value == null) break;
                }
            }
            MfcFile.Dispose();

            int i = 0;
            foreach (var dbF in dbFamilies) {
                
                exportForm.progressText = (++i).ToString();
                if (!dbF.IsValidByChildrenNumberAndAge(DateTime.Now))
                    continue;
                exportForm.Update();

                bool personFound = false;

                foreach (var dbP in dbF.persons) {                    
                    foreach (var mfcF in MfcFamilies) {                        
                        foreach (var mfcP in mfcF.persons) {

                            if (mfcP.IsFioDateEqualsTo(dbP)) {
                                personFound = true;
                                break;
                            }

                        }
                        if (personFound) break;
                    }
                    if (personFound) break;
                }

                if (!personFound)
                    WriteToXl(dbF, exportAssTable, ref assCount);
            }

            void WriteToXl(Database.Family family, OfficeOpenXml.ExcelWorksheet table, ref int counter) {
                table.Cells[counter, 1].Value = family.address;
                foreach (var person in family.persons) {
                    table.Cells[counter, 2].Value = person.f;
                    table.Cells[counter, 3].Value = person.i;
                    table.Cells[counter, 4].Value = person.o;
                    table.Cells[counter, 5].Value = person.birthDate.ToShortDateString();
                    counter++;
                }
            }

            exportAssFile.Save();
            exportAssFile.Dispose();
            exportForm.Close();
        }

        public static void CompareList() {
            FormExportProgress exportForm = new FormExportProgress();
            exportForm.Show();

            var dbFamilies = Database.FindFamiliesAll();

            var MfcFile = new OfficeOpenXml.ExcelPackage("Многодетные из МФЦ.xlsx");
            var MfcTable = MfcFile.Workbook.Worksheets[0];

            string filename = "Совпадают.xlsx";
            if (System.IO.File.Exists(filename)) System.IO.File.Delete(filename);
            var exportFoundFile = new OfficeOpenXml.ExcelPackage(filename);
            var exportFoundTable = exportFoundFile.Workbook.Worksheets.Add("1");
            int foundCount = 1;

            filename = "Не найдены у нас.xlsx";
            if (System.IO.File.Exists(filename)) System.IO.File.Delete(filename);
            var exportNotFoundFile = new OfficeOpenXml.ExcelPackage(filename);
            var exportNotFoundTable = exportNotFoundFile.Workbook.Worksheets.Add("1");
            int notFoundCount = 1;

            filename = "У нас неактуальны, в мфц актуальны.xlsx";
            if (System.IO.File.Exists(filename)) System.IO.File.Delete(filename);
            var exportNeedCheckFile = new OfficeOpenXml.ExcelPackage(filename);
            var exportNeedCheckTable = exportNeedCheckFile.Workbook.Worksheets.Add("1");
            int needCheckCount = 1;

            filename = "Не совсем совпадают.xlsx";
            if (System.IO.File.Exists(filename)) System.IO.File.Delete(filename);
            var exportPartFoundFile = new OfficeOpenXml.ExcelPackage(filename);
            var exportPartFoundTable = exportPartFoundFile.Workbook.Worksheets.Add("1");
            int partFoundCount = 1;

            filename = "Опечатки.xlsx";
            if (System.IO.File.Exists(filename)) System.IO.File.Delete(filename);
            var exportTypeErrorFile = new OfficeOpenXml.ExcelPackage(filename);
            var exportTypeErrorTable = exportTypeErrorFile.Workbook.Worksheets.Add("1");
            int typeErrorCount = 1;

            filename = "Неактуальные.xlsx";
            if (System.IO.File.Exists(filename)) System.IO.File.Delete(filename);
            var exportOutdatedFile = new OfficeOpenXml.ExcelPackage(filename);
            var exportOutdatedTable = exportOutdatedFile.Workbook.Worksheets.Add("1");
            int outdatedCount = 1;

            int row = 1;
            while (MfcTable.Cells[row, 1].Value != null) {
                
                var f = new Database.Family();
                f.address = (string)MfcTable.Cells[row, 1].Value;

                while (true) {
                    var p = new Database.Person() {
                        f = (string)MfcTable.Cells[row, 2].Value,
                        i = (string)MfcTable.Cells[row, 3].Value,
                        o = (string)MfcTable.Cells[row, 4].Value,
                        birthDate = DateTime.Parse((string)MfcTable.Cells[row, 5].Value)
                    };
                    if (p.Age() < 18) p.type = 2;                    
                    f.persons.Add(p);
                    row++;
                    if ((MfcTable.Cells[row, 1].Value != null && MfcTable.Cells[row, 1].Value != "") || MfcTable.Cells[row, 2].Value == null) break;
                }

                if (!f.IsValidByChildrenNumberAndAge(DateTime.Now)) {
                    WriteToXl(f, exportOutdatedTable, ref outdatedCount);
                    continue;
                }

                int personsFound = 0;
                foreach (var dbF in dbFamilies) {

                    foreach (var p in f.persons) {
                        foreach (var dbP in dbF.persons) {

                            if (p.IsFioDateEqualsTo(dbP)) {
                                personsFound++;
                                break;
                            }
                            else {
                                int groupsEquals = 0;
                                if (p.f.ToLower().Replace("ё", "е") == dbP.f.ToLower().Replace("ё", "е")) groupsEquals++;
                                if (p.i.ToLower().Replace("ё", "е") == dbP.i.ToLower().Replace("ё", "е")) groupsEquals++;
                                if (p.o.ToLower().Replace("ё", "е") == dbP.o.ToLower().Replace("ё", "е")) groupsEquals++;
                                if (p.birthDate == dbP.birthDate) groupsEquals++;
                                if (groupsEquals >= 3) {
                                    //p.i = dbP.i; p.f = dbP.f; p.o = dbP.o; p.birthDate = dbP.birthDate;
                                    //p.Update();
                                    exportTypeErrorTable.Cells[typeErrorCount, 1].Value = p.f;
                                    exportTypeErrorTable.Cells[typeErrorCount, 2].Value = p.i;
                                    exportTypeErrorTable.Cells[typeErrorCount, 3].Value = p.o;
                                    exportTypeErrorTable.Cells[typeErrorCount, 4].Value = p.birthDate.ToShortDateString();
                                    exportTypeErrorTable.Cells[typeErrorCount, 5].Value = dbP.f;
                                    exportTypeErrorTable.Cells[typeErrorCount, 6].Value = dbP.i;
                                    exportTypeErrorTable.Cells[typeErrorCount, 7].Value = dbP.o;
                                    exportTypeErrorTable.Cells[typeErrorCount, 8].Value = dbP.birthDate.ToShortDateString();
                                    typeErrorCount++;
                                    personsFound++;
                                    break;
                                }
                            }

                        }
                    }

                    if (personsFound > 0) {
                        dbF.address = f.address;
                        dbF.Update();
                        if (personsFound == f.persons.Count && personsFound == dbF.persons.Count) 
                            WriteToXl(f, exportFoundTable, ref foundCount);
                        else if (dbF.IsValidByChildrenNumberAndAge(DateTime.Now))
                            WriteToXl(f, exportPartFoundTable, ref partFoundCount);
                        else
                            WriteToXl(f, exportNeedCheckTable, ref needCheckCount);
                        break;
                    }

                }

                if (personsFound == 0)
                    WriteToXl(f, exportNotFoundTable, ref notFoundCount);

                void WriteToXl(Database.Family family, OfficeOpenXml.ExcelWorksheet table, ref int counter) {
                    table.Cells[counter, 1].Value = family.address;
                    foreach (var person in family.persons) {
                        table.Cells[counter, 2].Value = person.f;
                        table.Cells[counter, 3].Value = person.i;
                        table.Cells[counter, 4].Value = person.o;
                        table.Cells[counter, 5].Value = person.birthDate.ToShortDateString();
                        counter++;
                    }
                }

                exportForm.progressText = row.ToString();
                exportForm.Update();
            }
            MfcFile.Dispose();

            exportFoundFile.Save();
            exportFoundFile.Dispose();
            exportNotFoundFile.Save();
            exportNotFoundFile.Dispose();
            exportPartFoundFile.Save();
            exportPartFoundFile.Dispose();
            exportOutdatedFile.Save();
            exportOutdatedFile.Dispose();
            exportTypeErrorFile.Save();
            exportTypeErrorFile.Dispose();
            exportNeedCheckFile.Save();
            exportNeedCheckFile.Dispose();
            exportForm.Close();
        }

        public static void ExportPfrExcel() {
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
            string[] columnCaptions = { "", "Родители", "Дата рождения", "Документ", "Номер", "Дата выдачи", "Место выдачи", "Дети", "Дата рождения", "Документ", "Номер", "Дата выдачи", "Место выдачи", "Дата начала статуса", "Дата окончания", "Адрес"};
            for (int i = 1; i <= totalColumns; i++) {
                var cell = sheet.Cells[1, i];
                cell.Value = columnCaptions[i];
                cell.Style.Font.Bold = true;
                cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thick);
            }

            int familyCellY = 2, familyProcessed = 0, familyExported = 0;
            var familyList = Database.FindFamiliesAll();

            foreach (var f in familyList) {
                familyProcessed++;
                exportForm.progressText = familyProcessed.ToString() + " / " + familyList.Count.ToString();
                exportForm.Update();
                if (!f.IsValidByUdostExpireDate()) continue;
                //if (!f.IsValidByUdostExpireDate(DateTime.Now) && !f.IsValidByUdostExpireDate(new DateTime(2024, 04, 16))) continue;
                //if (f.GetProblemText() != null) continue;
                //if (f.udostoverenie.number == "" || f.udostoverenie.number == null) continue;

                    int parentCellY = familyCellY, childCellY = familyCellY;
                foreach (var p in f.persons) {
                    int cellX, cellY;
                    if (p.type < 2) {
                        cellX = 1;
                        cellY = parentCellY++;
                    }
                    else {
                        //if (p.Age() >= 18) continue;
                        cellX = 7;
                        cellY = childCellY++;
                    }
                    sheet.Cells[cellY, cellX].Value = $"{p.f} {p.i} {p.o}";
                    sheet.Cells[cellY, cellX + 1].Value = p.birthDate.ToString("d");

                    Database.Document document = null;
                    foreach (var d in p.documents) {
                        if (d.typeID == 0) {
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
        public static void ExportFNSXML() { //DateTime toDate
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
            foreach (var f in familyList) {
                familyProcessed++;
                exportForm.progressText = familyProcessed.ToString() + " / " + familyList.Count.ToString();
                exportForm.Update();

                if (f.fnsStatus == Database.Family.ExportStatus.Exported)
                    continue;

                if (f.GetProblemText() != null) continue;
                if (!f.IsValidByChildrenNumberAndAge(yearToExportStart) && !f.IsValidByChildrenNumberAndAge(yearToExportEnd)) continue;
                
                List<Database.Person> children = new List<Database.Person>(), parents = new List<Database.Person>();
                foreach (var p in f.persons) {
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

                foreach (var p in parents) {
                    XmlElement xmlFamily = xmlDoc.CreateElement("СодСвед");
                    xmlFamily.SetAttribute("КолДет", children.Count.ToString());

                    for (int i = -1; i < children.Count; i++) {
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
                        foreach (var document in expPer.documents) {
                            switch (document.typeID) {
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

        public static void QueryMothers() {
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
            for (int i = 1; i <= totalColumns; i++) {
                string text = "";
                switch (i) {
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
            foreach (var f in familyList) {
                familiesProcessed++;

                if (!f.IsValidByChildrenNumberAndAge(DateTime.Now)) continue;
                Database.Person mother = null;
                foreach (var p in f.persons)
                    if (p.type == 0) {
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

                foreach (var d in mother.documents) {
                    if (d.typeID != 0) continue;
                    sheet.Cells[row, 1].Value = d.number;
                }

                int childrenNum = 0;
                string childrenBirthDates = "";
                foreach (var p in f.persons) {
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

        public static void FullFamiliesChildrenNum() {
            var familyList = Database.FindFamiliesAll();
            int[] familiesNum = new int[13];

            foreach (var f in familyList) {
                if (f.IsValidByChildrenNumberAndAge(DateTime.Parse("01.01.2021"))) continue;
                int cNum = 0, pNum = 0;
                foreach (var p in f.persons) {
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

        public static void QueryChildrenNum() {
            DateTime toDate = DateTime.Parse("01.05.2023");
            var familyList = Database.FindFamiliesAll();
            int[] childrenNum = new int[2];

            foreach (var f in familyList) {
                if (!f.IsValidByChildrenNumberAndAge(toDate)) continue;
                foreach (var p in f.persons) {
                    if (p.type != 2) continue;
                    TimeSpan dateDiff = toDate - p.birthDate;
                    if (dateDiff.TotalDays < 0) continue;
                    else if (dateDiff.TotalDays < 548) childrenNum[0]++;
                    else if (dateDiff.TotalDays < 1095) childrenNum[1]++;
                }
            }
            MessageBox.Show("0-1.5: " + childrenNum[0] + ", 1.5-3: " + childrenNum[1]);
        }

        public static void FillGender() {
            var personsList = Database.FindPersonsAll();
            int processed = 0;
            List<List<string>> names = new List<List<string>>(2);
            names.Add(new List<string>());
            names.Add(new List<string>());
            foreach (var p in personsList) {
                processed++;
                if (p.type < 2) {
                    p.gender = (Database.Person.Gender) p.type;
                    p.Update();
                    if (!names[p.type].Contains(p.i ))
                        names[p.type].Add(p.i);
                    continue;
                }

                bool nameFound = false;
                for (int i=0; i<2; i++) {
                    foreach (var name in names[i]) {
                        if (name == p.i) {
                            p.gender = (Database.Person.Gender) i;
                            p.Update();
                            nameFound = true;
                            i = 2;
                            break;
                        }
                    }
                }
                if (nameFound) continue;

                DialogResult dialogResult;
                do {
                    dialogResult = MessageBox.Show(processed.ToString() + "/" + personsList.Count + "\n" + p.f + " " + p.i + " " + p.o + "\n Да - мужской, Нет - женский", "Выберите пол", MessageBoxButtons.YesNo);
                } while (dialogResult != DialogResult.Yes && dialogResult != DialogResult.No);

                if (dialogResult == DialogResult.Yes) {
                    p.gender = Database.Person.Gender.Male;
                    names[1].Add(p.i);
                } else {
                    p.gender = Database.Person.Gender.Female;
                    names[0].Add(p.i);
                }
                p.Update();
            }
        }
    
        public static void ExportPfrCsv() {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файлы CSV (*.csv)|*.csv";
            saveFileDialog.AddExtension = true;

            saveFileDialog.Title = "Основной файл";
            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;
            if (System.IO.File.Exists(saveFileDialog.FileName))
                System.IO.File.Delete(saveFileDialog.FileName);
            var mainStreamWriter = new System.IO.StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8 ); //System.Text.Encoding.GetEncoding("CP866")

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

            foreach (var f in familyList) {
                familyProcessed++;
                exportForm.progressText = familyProcessed.ToString() + " / " + familyList.Count.ToString();
                exportForm.Update();
                if (!f.IsValidByUdostExpireDate()) continue;

                string familyToWrite = "";
                bool snilsNotPresent = false;

                foreach (var p in f.persons) {
                    familyToWrite += f.Id.ToString() + ";1;000;" + subjectRF + ";";
                    Database.Document snils = null;
                    foreach (var d in p.documents)
                        if (d.typeID == 0) {
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
                    if (f.udostoverenie.number != "") {
                        familyToWrite += $";{f.udostoverenie.issuedDate.ToShortDateString()};{f.udostoverenieExpirationDate.ToShortDateString()};{f.udostoverenie.number};{f.udostoverenie.issuedDate.ToShortDateString()};";
                        familyToWrite += $"{organization};{NPA};{ f.udostoverenie.issuedDate.ToShortDateString()};{f.udostoverenieExpirationDate.ToShortDateString()};{workerFio};\n";
                    }
                    else {
                        familyToWrite += $";{f.StatusStartByChildrenBirthdate().ToShortDateString()};{f.StatusExpirationByChildrenBirthdate().ToShortDateString()};0;{f.StatusStartByChildrenBirthdate().ToShortDateString()};";
                        familyToWrite += $"{organization};{NPA};{ f.StatusStartByChildrenBirthdate().ToShortDateString()};{f.StatusExpirationByChildrenBirthdate().ToShortDateString()};{workerFio};\n";
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
    
        public static void AddSnilsFromFile() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы Excel (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            var excelFile = new OfficeOpenXml.ExcelPackage(openFileDialog.FileName);
            var table = excelFile.Workbook.Worksheets[0];

            var exportForm = new FormExportProgress();
            exportForm.Show();

            int i = 2;
            while (table.Cells[i, 1].Value != null)
            {
                foreach (var p in Database.FindPersonsAll()) {
                    if (p.f.ToLower().Replace("ё", "е") != ((string)table.Cells[i,2].Value).ToLower().Replace("ё", "е")) continue;
                    if (p.i.ToLower().Replace("ё", "е") != ((string)table.Cells[i,3].Value).ToLower().Replace("ё", "е")) continue;
                    if (p.o.ToLower().Replace("ё", "е") != ((string)table.Cells[i,4].Value).ToLower().Replace("ё", "е")) continue;
                    if (p.birthDate != (DateTime)table.Cells[i, 5].Value) continue;
                    if (p.SNILS != null) continue;

                    p.documents.Add(new Database.Document {
                        typeID = 0,
                        number = (string)table.Cells[i, 1].Value
                    });
                    p.Update();

                    break;
                }
                exportForm.progressText = (i).ToString();
                exportForm.Update();
                i++;
            }
            
            exportForm.Close();
        }

        public static void QueryChildrenBornInPeriod(DateTime periodStart, DateTime periodEnd, DateTime forValidToDate) {
            //DateTime periodStart = DateTime.Parse("01.01.2019");
            //DateTime periodEnd = DateTime.Parse("31.12.2023");
            var familyList = Database.FindFamiliesAll();
            int childrenNum = 0;

            foreach (var f in familyList) {
                if (f.GetProblemText() != null) continue;
                if (!f.IsValidByChildrenNumberAndAge(forValidToDate)) continue;
                foreach (var p in f.persons) {
                    if (p.type != 2) continue;
                    if (p.birthDate >= periodStart && p.birthDate <= periodEnd) {
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

        public static void QueryNumberOfChildrenBornInYear() {
            var familyList = Database.FindFamiliesAll();
            for (int year = 2018; year <= 2024; year++) {
                int childrenNumber = 0;
                foreach (var f in familyList) {
                    if (f.GetProblemText() != null) continue;
                    foreach (var p in f.persons) {
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
            if (!DateTime.TryParse(dateString, out DateTime date)) {
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
            for (int childrenNumber = 3; childrenNumber < 20; childrenNumber++) {
                int familiesNum = familyNumberByChildrenNumber[childrenNumber];
                if (familiesNum == 0) continue;
                totalFamilies += familiesNum;
                totalChildren += childrenNumber * familiesNum;
                text += $"{childrenNumber} детей в семье * {familiesNum} семей = {childrenNumber * familiesNum} детей всего\n";
            }
            text += $"Всего семей: {totalFamilies}\nВсего детей:{totalChildren}";
            MessageBox.Show(text);
        }
    
        public static void FindPeopleFromExcel() {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы Excel (*.xlsx)|*.xlsx";
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            var excelFile = new OfficeOpenXml.ExcelPackage(openFileDialog.FileName);
            var table = excelFile.Workbook.Worksheets[0];
            var persons = Database.FindPersonsAll();

            var exportForm = new FormExportProgress();
            exportForm.Show();

            int i = 2;
            while (table.Cells[i, 1].Value != null ) {
                /*string s = ((string)table.Cells[i, 1].Value).ToLower().Replace("ё", "е");
                string[] fio = s.Split(' ');
                if (fio.Length != 3) {
                    table.Cells[i, 7].Value = "Пропущен";
                    i++;
                    continue;
                }*/
                string fio = ((string)table.Cells[i, 1].Value).ToLower().Replace("ё", "е");

                bool personFound = false;
                foreach (var p in persons) {
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
    }
}
