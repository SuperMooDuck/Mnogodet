using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MnogodetLiteDB
{
    public partial class FormQueries : Form
    {
        static FormQueries openedForm;
        List<Action> queryDelegates = new List<Action>();
        public FormQueries()
        {
            InitializeComponent();
        }

        public static void Open()
        {
            if (openedForm == null)
                openedForm = new FormQueries();

            openedForm.listBox.Items.Clear();
            openedForm.queryDelegates = new List<Action>();
            openedForm.AddQuery("Ежегодная выгрузка многодетных в ФНС", Export.ExportFNSXML);
            openedForm.AddQuery("Выгрузка семей с действующими удостоверениями", Export.ExportPfrExcel);
            openedForm.AddQuery("Выгрузка граждан по СНИЛСам из списка", Export.FindPeopleFromExcelSNILS);

            openedForm.Show();
            openedForm.Activate();
        }

        private void AddQuery(string name, Action query)
        { 
            listBox.Items.Add(name);
            queryDelegates.Add(query);
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {

        }

        private void listBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox.SelectedIndex > -1)
                queryDelegates[listBox.SelectedIndex]();
        }
    }
}
