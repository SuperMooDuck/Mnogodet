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
    public partial class FormExportProgress : Form {
        public FormExportProgress() {
            InitializeComponent();
        }

        public string progressText { get { return labelProgress.Text; } set { labelProgress.Text = value; } }
    }
}
