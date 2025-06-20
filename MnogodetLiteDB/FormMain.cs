using OfficeOpenXml.ConditionalFormatting;
using System;
using System.Windows.Forms;

namespace MnogodetLiteDB {
    public partial class FormMain : Form {

        public static FormMain formObject;

        public FormMain() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            Database.Open();
            formObject = this;
        }

        private void buttonFamiliesProblems_Click(object sender, EventArgs e) {
            FormFamiliesProblems.Open();
        }

        private void button2_Click(object sender, EventArgs e) {
            FormPersonsProblems.Open();
        }


        void Search() {
            gridPersons.Rows.Clear();

            foreach (var p in Database.FindPersonsAll()) {
                Database.Family family;
                string s = editF.Text.ToLower();
                if (s != "" && !p.f.ToLower().Contains(s)) continue;
                s = editI.Text.ToLower();
                if (s != "" && !p.i.ToLower().Contains(s)) continue;
                s = editO.Text.ToLower();
                if (s != "" && !p.o.ToLower().Contains(s)) continue;
                s = editDocumentNumber.Text.ToLower();
                if (s != "") {
                    bool documentFound = false;
                    foreach (var document in p.documents) 
                        if (document.number.ToLower().Contains(s)) {
                            documentFound = true;
                            break;
                        }
                    if (!documentFound) {
                        family = Database.FindFamilyById(p.familyId);
                        if (family.udostoverenie.number == null || !family.udostoverenie.number.ToLower().Contains(s)) continue;
                    }

                }
                string[] addressStrings = editAddress.Text.ToLower().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (addressStrings.Length != 0) {
                    string familyAddress = Database.FindFamilyById(p.familyId).address.ToLower();
                    bool addressInvalid = false; 
                    foreach (string addressString in addressStrings) 
                        if (!familyAddress.Contains(addressString)) {
                            addressInvalid = true;
                            break;
                        }
                    if (addressInvalid) continue;
                }

                gridPersons.Rows.Add(new object[]{ p.familyId, p.f, p.i, p.o, p.birthDate.ToShortDateString() });
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e) {
            Search();
        }

        private void searchEdit_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                Search();
        }

        private void gridPersons_MouseDoubleClick(object sender, MouseEventArgs e) {
            int familyId = (int)gridPersons.CurrentRow.Cells["familyId"].Value;
            var family = Database.FindFamilyById(familyId);
            if (family == null)
                MessageBox.Show("Семья удалена");
            else 
                new FormFamily(family).Show();
        }

        
        private void buttonAddFamily_Click(object sender, EventArgs e) {
            new FormFamily().Show();
        }


        private void buttonDictionaries_Click(object sender, EventArgs e) {
            FormDictionaries.Open();
        }

        private void buttonDoubles_Click(object sender, EventArgs e) {
            FormDoubles.Open();
        }
// Запилить приличное окно прогресса, с автоматизацией Show и Update
// Запилить диалог выбора даты
        private void QueryBtn_Click(object sender, EventArgs e) {
            FormQueries.Open();
        }

        private void buttonParameters_Click(object sender, EventArgs e) {
            FormParameters.Open();
        }

    }
}
