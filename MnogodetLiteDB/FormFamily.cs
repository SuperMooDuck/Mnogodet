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
    public partial class FormFamily : Form {
        Database.Family family;
        bool dataChanged, newFamily;
        //List<Database.ListMenuItem> cancelReasonMenuItemsList;

        void LoadMenuList() {
            //cancelReasonMenuItemsList = Database.dictCancelReasons.GetMenuList();
            listCancelReason.DataSource = Database.dictCancelReasons.GetMenuList();
            listCancelReason.ValueMember = "Value";
            listCancelReason.DisplayMember = "Name";
            comboBoxRaion.ValueMember = "Value";
            comboBoxRaion.DisplayMember = "Name";
            comboBoxRaion.DataSource = Database.dictSettlements.GetMenuListRaion();

        }
        public FormFamily(Database.Family family) {
            InitializeComponent();
            LoadMenuList();
            newFamily = false;
            this.family = family;
            RefreshPersons();
            RefreshData();
            dataChanged = false;
        }

        public FormFamily() {
            InitializeComponent();
            LoadMenuList();
            newFamily = true;
            family = new Database.Family();
            family.creationDate = DateTime.Now;
            family.udostoverenie = new Database.Document();
            family.udostoverenie.issuedDate = new DateTime(2000, 1, 1);
            family.udostoverenieExpirationDate = new DateTime(2000, 1, 1);
            this.Text = "Новая семья";
            buttonAddPerson.Enabled = false;
            buttonSaveAndClose.Text = "Сохранить";
            dataChanged = false;
        }

        void RefreshPersons() {
            gridPersons.Rows.Clear();
            foreach (var p in family.persons) {
                string type = "";
                switch (p.type) {
                    case 0: type = "Мать"; break;
                    case 1: type = "Отец"; break;
                    case 2: type = "Ребенок"; break;
                }
                gridPersons.Rows.Add(new object[] {
                    p.Id,
                    type,
                    p.f,
                    p.i,
                    p.o,
                    p.birthDate.ToShortDateString(),
                    p.GetProblemText() ?? "OK"
                });
            }
        }

        void RefreshData() {
            this.Text = "Семья " + family.familyName;
            editAddress.Text = family.address;
            editComment.Text = family.comment;
            labelId.Text = family.Id.ToString();
            labelCreationDate.Text = family.creationDate.ToString("d");

            listCancelReason.SelectedValue = family.cancelReason;
            labelCancelReasonDate.Text = family.cancelReason == 0 ? "" : family.cancelReasonDate.ToString("d");

            labelUdostNumber.Text = family.udostoverenie.number;
            labelUdostDateIssue.Text = family.udostoverenie.issuedDate.ToShortDateString();
            labelUdostDateExpire.Text = family.udostoverenieExpirationDate.ToShortDateString();
            labelUdostDateExpireStatus.Text = family.udostoverenieExpirationDate < DateTime.Now ? "Истекло:" : "Истекает:";
            labelUdostDateExpireStatus.ForeColor = family.udostoverenieExpirationDate < DateTime.Now ? Color.Red : Color.Black;

            if (family.settlementId == 0)
            {
                comboBoxRaion.SelectedIndex = 0;
                comboBoxMunObr.SelectedIndex = 0;
                comboBoxSettlement.SelectedIndex = 0;
            } else
            {
                comboBoxRaion.SelectedValue = family.settlementId / 10000 * 10000;
                comboBoxMunObr.SelectedValue = family.settlementId / 100 * 100;
                comboBoxSettlement.SelectedValue = family.settlementId;
            }

            string status = family.GetProblemText();
            if (status == null) {
                switch (family.fnsStatus) {
                    case Database.Family.ExportStatus.New: status = "Не выгружен в ФНС"; break;
                    case Database.Family.ExportStatus.Exported: status = "Выгружен в ФНС " + family.exportDate.ToString("d"); break;
                    case Database.Family.ExportStatus.Changed: status = "Изменен после выгрузки в ФНС " + family.exportDate.ToString("d"); break;
                }
                status += "\n";
                switch (family.pfrStatus) {
                    case Database.Family.ExportStatus.New: status += "Не выгружен в ПФР"; break;
                    case Database.Family.ExportStatus.Exported: status += "Выгружен в ПФР " + family.pfrExportDate.ToString("d"); break;
                    case Database.Family.ExportStatus.Changed: status += "Изменен после выгрузки в ПФР " + family.pfrExportDate.ToString("d"); break;
                }
                DateTime expireDate = family.StatusExpirationByChildrenBirthdate();
                string s = (expireDate < DateTime.Now ? "истек " : "") + expireDate.ToShortDateString();
                labelExpireDate.ForeColor = expireDate < DateTime.Now ? Color.Red : Color.Black;
                labelExpireDate.Text = s;
            } else {
                labelExpireDate.Text = "Семья не действительна";
                labelExpireDate.ForeColor = Color.Red;
            }
            labelStatus.Text = status;
        }

        void SaveAndClose() {
            family.address = editAddress.Text;
            family.comment = editComment.Text;
            family.settlementId = (int)comboBoxSettlement.SelectedValue;
            if (family.cancelReason != (int)listCancelReason.SelectedValue) {
                family.cancelReason = (int)listCancelReason.SelectedValue;
                family.cancelReasonDate = DateTime.Now;
            }
            /*family.udostoverenie = new Database.Document {
                number = editUdostNumber.Text,
                issuedDate = editUdostDate.Value
            };
            family.udostoverenieExpirationDate = editUdostExpireDate.Value;*/

            dataChanged = false;
            if (newFamily)
                family.Insert();
            else
                family.Update();
            DialogResult = DialogResult.OK;
            if (!newFamily) Close();
            else {
                newFamily = false;
                dataChanged = false;
                buttonAddPerson.Enabled = true;
                buttonSaveAndClose.Text = "Сохранить и закрыть";
            }
        }

        private void gridPersons_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            var person = family.FindPersonById((int)gridPersons.CurrentRow.Cells["id"].Value);
            switch (new FormPerson(person).ShowDialog()) {
                case DialogResult.OK:
                    DialogResult = DialogResult.OK;
                    PersonsDataChanged();
                    break;
                case DialogResult.Abort:
                    family.persons.Remove(person);
                    PersonsDataChanged();
                    break;
            }
        }

        private void FormFamily_FormClosing(object sender, FormClosingEventArgs e) {
            if (!dataChanged) return;
            DialogResult result = MessageBox.Show("Сохранить изменения?", "Закрыть", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes) {
                e.Cancel = true;
                SaveAndClose();
            }
            else if (result == DialogResult.Cancel)
                e.Cancel = true;
        }

        private void buttonSaveAndClose_Click(object sender, EventArgs e) {
            SaveAndClose();
        }

        private void buttonDeleteFamily_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Удалить семью?", "Удалить", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                foreach (var p in family.persons)
                    p.Delete();
                family.Delete();
                dataChanged = false;
                DialogResult = DialogResult.Abort;
                Close();
            }
        }

        private void buttonAddPerson_Click(object sender, EventArgs e) {
            if (new FormPerson(family).ShowDialog() == DialogResult.OK) {
                DialogResult = DialogResult.OK;
                
                PersonsDataChanged();
            }
        }

        private void DataChanged(object sender, EventArgs e) {
            dataChanged = true;
        }

        private void buttonUdostEdit_Click(object sender, EventArgs e) {
            if ((new FormUdostoverenie(family).ShowDialog() == DialogResult.OK)) {
                RefreshData();
            }
        }

        private void comboBoxRaion_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMunObr.ValueMember = "Value";
            comboBoxMunObr.DisplayMember = "Name";
            comboBoxMunObr.DataSource = Database.dictSettlements.GetMenuListMunObrByRaion((int)comboBoxRaion.SelectedValue);
            DataChanged(sender, e);
        }

        private void comboBoxMunObr_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSettlement.ValueMember = "Value";
            comboBoxSettlement.DisplayMember = "Name";
            comboBoxSettlement.DataSource = Database.dictSettlements.GetMenuListSettlementsByMunObr((int)comboBoxMunObr.SelectedValue);
            DataChanged(sender, e);
        }

        private void PersonsDataChanged() {
            RefreshPersons();
            if (family.fnsStatus == Database.Family.ExportStatus.Exported) 
                family.fnsStatus = Database.Family.ExportStatus.Changed;
            if (family.pfrStatus == Database.Family.ExportStatus.Exported)
                family.pfrStatus = Database.Family.ExportStatus.Changed;
            family.Update();
            RefreshData();
        }
    }
}
