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
    public partial class FormFamiliesProblems : Form {
        static FormFamiliesProblems openedForm;

        public static void Open(DateTime toDate = new DateTime()) {
            if (openedForm == null)
                openedForm = new FormFamiliesProblems(toDate);
            openedForm.Show();
            openedForm.Activate();
        }

        FormFamiliesProblems(DateTime toDate = new DateTime()) {
            InitializeComponent();
            toDate = new DateTime(DateTime.Now.Year, 1, 1);
            editDate.Text = toDate.ToShortDateString();
        }

        void RefreshList() {
            DateTime toDate;
            if (!DateTime.TryParse(editDate.Text, out toDate)) {
                MessageBox.Show("Неверная дата проверки");
                return;
            }
            gridFamilies.Rows.Clear();
            gridPersons.Rows.Clear();
            foreach (var f in Database.FindFamiliesAll()) {
                if (f.GetProblemText() == null) continue;
                gridFamilies.Rows.Add(new object[] {
                    f.Id,
                    f.familyName,
                    f.GetProblemText()
                });
                foreach (var p in f.persons) {
                    if (p.GetProblemText() == null) continue;
                    gridPersons.Rows.Add(new object[] {
                        p.Id, p.f, p.i, p.o, p.birthDate.ToShortDateString(),
                        p.GetProblemText()
                    });
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e) {
            RefreshList();
        }

        private void gridPersons_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e) {
            new FormPerson(Database.FindPersonById((int)gridPersons.CurrentRow.Cells["pId"].Value)).Show();
        }

        private void gridFamilies_CellMouseDoubleClick_1(object sender, DataGridViewCellMouseEventArgs e) {
            new FormFamily(Database.FindFamilyById((int)gridFamilies.CurrentRow.Cells["id"].Value)).Show();
        }

        private void FormFamiliesProblems_Shown(object sender, EventArgs e) {
            RefreshList();
        }

        private void FormFamiliesProblems_FormClosed(object sender, FormClosedEventArgs e) {
            openedForm = null;
        }
    }
}
