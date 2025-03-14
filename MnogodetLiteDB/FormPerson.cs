using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MnogodetLiteDB {
    public partial class FormPerson : Form {
        Database.Person person;
        Database.Family family;
        bool newPerson = false, dataChanged = false;
        public FormPerson(Database.Person person) {
            InitializeComponent();
            this.person = person;
            LoadPerson();
            dataChanged = false;
        }

        public FormPerson(Database.Family family) {
            InitializeComponent();
            this.family = family;
            this.Text = "Новый член семьи " + family.familyName;
            person = new Database.Person() { familyId = family.Id };
            newPerson = true;
            buttonDelete.Enabled = false;
            dataChanged = false;
        }

        void LoadPerson() {
            this.Text = $"{person.f} {person.i} {person.o}";
            editType.SelectedIndex = person.type;
            editF.Text = person.f;
            editI.Text = person.i;
            editO.Text = person.o;
            editGender.SelectedIndex = (int)person.gender;
            editBirthDate.Value = person.birthDate > editBirthDate.MinDate ? person.birthDate : editBirthDate.MinDate;
            foreach (var document in person.documents) {
                gridDocuments.Rows.Add(new object[] {
                    document.typeID,
                    document.number,
                    document.issuedDate == new DateTime() ? "" : document.issuedDate.ToShortDateString(),
                    document.issuedPlace,
                    document.issuedCode
                });
            }
        }

        bool Save() {
            if (!newPerson && !dataChanged) {
                Close();
                return false;
            }
            if (editType.SelectedIndex < 0) {
                MessageBox.Show("Не выбран род.\nИзменения не сохранены.");
                return false;
            }
            if (editGender.SelectedIndex < 0) {
                MessageBox.Show("Не выбран пол.\nИзменения не сохранены.");
                return false;
            }
            person.type = editType.SelectedIndex;
            person.f = editF.Text.Trim();
            person.i = editI.Text.Trim();
            person.o = editO.Text.Trim();
            person.gender = (Database.Person.Gender)editGender.SelectedIndex;
            person.birthDate = editBirthDate.Value;
            person.documents.Clear();
            foreach (DataGridViewRow row in gridDocuments.Rows) {
                if (row.Cells["type"].Value == null) continue;
                DateTime issuedDate = new DateTime();
                string issuedDateString = row.Cells["issuedDate"].Value as string;
                if (issuedDateString != null && issuedDateString != "" && !DateTime.TryParse(issuedDateString, out issuedDate)) {
                    MessageBox.Show("Неверная дата выдачи.\nИзменения не сохранены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                var document = new Database.Document() {
                    typeID = (int)row.Cells["type"].Value,
                    number = row.Cells["number"].Value as string,
                    issuedDate = issuedDate,
                    issuedPlace = row.Cells["issuedPlace"].Value as string,
                    issuedCode = row.Cells["issuedCode"].Value as string
                };
                if (!document.IsValid()) {
                    MessageBox.Show("Неверно заполнен документ.\nИзменения не сохранены.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                person.documents.Add(document);
            }
            if (newPerson) {
                person.Insert();
                family.persons.Add(person);
            }
            else
                person.Update();

            DialogResult = DialogResult.OK;
            dataChanged = false;
            return true;
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            if (Save()) Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            dataChanged = false;
            Close();
        }

        private void FormPerson_Load(object sender, EventArgs e) {
            DataGridViewComboBoxColumn cbc = gridDocuments.Columns["type"] as DataGridViewComboBoxColumn;
            cbc.DataSource = Database.dictDocuments.GetMenuList();
            cbc.ValueMember = "Value";
            cbc.DisplayMember = "Name";
        }

        private void buttonDelete_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Вы хотите удалить члена семьи из базы?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                DialogResult = DialogResult.Abort;
                dataChanged = false;
                person.Delete();
                Close();
            }

        }

        private void FormPerson_FormClosing(object sender, FormClosingEventArgs e) {
            if (!dataChanged) return;
            DialogResult result = MessageBox.Show("Сохранить изменения?", "Закрыть", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) {
                if (!Save()) e.Cancel = true;
            }
            else if (result == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void DataChanged(object sender, EventArgs e) {
            dataChanged = true;
        }

        private void gridDocuments_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            DataChanged(sender, e);
        }

        private void gridDocuments_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e) {
            DataChanged(sender, e);
        }

        private void gridDocuments_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            DataChanged(sender, e);
        }
    }
}
