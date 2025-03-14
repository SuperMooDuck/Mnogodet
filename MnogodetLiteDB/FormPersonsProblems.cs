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
    public partial class FormPersonsProblems : Form {
        static FormPersonsProblems openedForm;
        public static void Open() {
            if (openedForm == null)
                openedForm = new FormPersonsProblems();
            openedForm.Show();
            openedForm.Activate();
        }
        public FormPersonsProblems() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            foreach (var p in Database.FindPersonsAll()) {
                if (p.GetProblemText() == null) continue;
                grid.Rows.Add(new object[] {
                    p.Id, p.f, p.i, p.o, p.birthDate.ToShortDateString()
                }) ;

            }
        }

        private void grid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e) {
            new FormPerson(Database.FindPersonById((int)grid.CurrentRow.Cells["id"].Value)).Show();
        }

        private void FormPersonsProblems_FormClosed(object sender, FormClosedEventArgs e) {
            openedForm = null;
        }
    }
}
