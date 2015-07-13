using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLiteRecovery
{
    public partial class SqliteRecoveryPage : Form
    {
        private List<CheckBox> checkedBoxes;
        private Form MainPage;

        public SqliteRecoveryPage(List<CheckBox> checkedBoxes, Form MainPage)
        {
            InitializeComponent();

            this.checkedBoxes = checkedBoxes;
            this.MainPage = MainPage;
        }
    }
}
