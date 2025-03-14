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
    public partial class FormUdostoverenie : Form {
        bool dataChanged;
        Database.Family family;
        public FormUdostoverenie(Database.Family family) {
            InitializeComponent();
            this.family = family;
            editNumber.Text = family.udostoverenie.number;
            if (family.udostoverenie.issuedDate >= DateTimePicker.MinimumDateTime && family.udostoverenie.issuedDate <= DateTimePicker.MaximumDateTime)
                editDateIssue.Value = family.udostoverenie.issuedDate;
            if (family.udostoverenieExpirationDate >= DateTimePicker.MinimumDateTime && family.udostoverenieExpirationDate <= DateTimePicker.MaximumDateTime)
                editDateExpire.Value = family.udostoverenieExpirationDate;
            this.Text = "Удостоверение " + family.familyName;
            dataChanged = false;
        }

        private void Save() {
            DialogResult = DialogResult.OK;
            family.udostoverenie.number = editNumber.Text;
            family.udostoverenie.issuedDate = editDateIssue.Value;
            family.udostoverenieExpirationDate = editDateExpire.Value;
            family.Update();
            dataChanged = false;
            
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            Save();
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            dataChanged = false;
            Close();
        }

        private void DataChanged(object sender, EventArgs e) {
            dataChanged = true;
        }

        private void FormUdostoverenie_FormClosing(object sender, FormClosingEventArgs e) {
            if (!dataChanged) return;
            DialogResult result = MessageBox.Show("Сохранить изменения?", "Закрыть", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) {
                Save();
            }
            else if (result == DialogResult.Cancel)
                e.Cancel = true;
        }
    }
}
