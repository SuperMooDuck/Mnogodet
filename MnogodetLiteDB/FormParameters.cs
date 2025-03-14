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
    public partial class FormParameters : Form {
        static FormParameters openedForm;
        public static void Open() {
            if (openedForm == null)
                openedForm = new FormParameters();
            openedForm.Show();
            openedForm.Activate();
        }
        public FormParameters() {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            Dictionary<string, string> newValues = new Dictionary<string, string>();
            foreach (DataGridViewRow row in dataGrid.Rows) {
                if (row.Cells[0].Value == null && row.Cells[1].Value == null) continue;
                if (row.Cells[0].Value == null || row.Cells[1].Value == null) {
                    MessageBox.Show($"Пустое поле в строке {row.Index + 1}");
                    return;
                }
                if (newValues.ContainsKey((string)row.Cells[0].Value)) {
                    MessageBox.Show($"Повтор имени параметра в строке {row.Index + 1}");
                    return;
                }
                newValues.Add((string)row.Cells[0].Value, (string)row.Cells[1].Value);
            }
            Database.ReplaceParameters(newValues);
        }

        private void FormParameters_Load(object sender, EventArgs e) {
            dataGrid.Rows.Clear();
            var parameters = Database.FindParametersAll();
            foreach (var value in parameters) {
                dataGrid.Rows.Add(new object[] {
                    value.Name.ToString(),
                    value.Value
                });
            }
        }

        private void FormParameters_FormClosed(object sender, FormClosedEventArgs e) {
            openedForm = null;
        }
    }
}
