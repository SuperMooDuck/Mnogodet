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
    public partial class FormDictionaries : Form {
        static FormDictionaries openedForm;
        public static void Open() {
            if (openedForm == null)
                openedForm = new FormDictionaries();
            openedForm.Show();
            openedForm.Activate();
        }


        List<Database.MenuListDictionary> dbDicts;
        public FormDictionaries() {
            InitializeComponent();
        }

        private void FormDictionaries_Load(object sender, EventArgs e) {
            dbDicts = Database.FindDictsAll();
            foreach (var dict in dbDicts) {
                listDictionaries.Items.Add(dict.displayName);
            }
            
        }

        private void listDictionaries_SelectedIndexChanged(object sender, EventArgs e) {
            dataGrid.Rows.Clear();
            var dict = dbDicts[listDictionaries.SelectedIndex];
            foreach (var value in dict.values) {
                dataGrid.Rows.Add(new object[] {
                    value.Key.ToString(),
                    value.Value
                });
            }
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            Dictionary<int, string> newValues = new Dictionary<int, string>();
            foreach (DataGridViewRow row in dataGrid.Rows) {
                if (row.Cells[0].Value == null && row.Cells[1].Value == null) continue;
                if (row.Cells[0].Value == null || row.Cells[1].Value == null) {
                    MessageBox.Show($"Пустое поле в строке {row.Index + 1}");
                    return;
                }
                try {
                    newValues.Add(int.Parse((string)row.Cells[0].Value), (string)row.Cells[1].Value);
                }
                catch (System.Exception exception) {
                    MessageBox.Show($"Ошибка в строке {row.Index + 1}: " + exception.Message);
                    return;
                }
            }

            var dict = dbDicts[listDictionaries.SelectedIndex];
            dict.values = newValues;
            Database.UpdateDictionary(dict);
        }

        private void FormDictionaries_FormClosed(object sender, FormClosedEventArgs e) {
            openedForm = null;
        }
    }
}
