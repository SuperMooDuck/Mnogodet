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
    public partial class FormDoubles : Form {
        public FormDoubles() {
            InitializeComponent();
        }

        static FormDoubles openedForm;
        public static void Open() {
            if (openedForm == null)
                openedForm = new FormDoubles();
            openedForm.Show();
            openedForm.Activate();
        }
        private void buttonRefresh_Click(object sender, EventArgs e) {
            FormExportProgress exportForm = new FormExportProgress();
            exportForm.Show();
            var allPersons = Database.FindPersonsAll();
            var addedIds = new List<int>();
            int totalPersons = allPersons.Count, checkedPersons = 0;
            foreach (var p1 in allPersons) {
                checkedPersons++;
                if (addedIds.Contains(p1.Id)) continue;
                exportForm.progressText = checkedPersons.ToString() + "/" + totalPersons.ToString();
                exportForm.Update();
                foreach (var p2 in allPersons) {
                    bool p1Added = false;
                    if (!p1.Equals(p2) && p1.IsFioDateEqualsTo(p2)) {
                        if (!p1Added) {
                            addedIds.Add(p1.Id);
                            grid.Rows.Add(new object[] {
                            p1.Id, p1.f, p1.i, p1.o, p1.birthDate.ToShortDateString()
                        });
                        }
                        addedIds.Add(p2.Id);
                        grid.Rows.Add(new object[] {
                            p2.Id, p2.f, p2.i, p2.o, p2.birthDate.ToShortDateString()
                        });
                    }
                }
            }
            exportForm.Close();
        }

        private void grid_MouseDoubleClick(object sender, MouseEventArgs e) {
            int personId = (int)grid.CurrentRow.Cells["id"].Value;
            var person = Database.FindPersonById(personId);
            if (person == null) {
                MessageBox.Show("Человек удален");
                return;
            }
            var family = Database.FindFamilyById(person.familyId);
            new FormFamily(family).Show();
        }
    }
}
