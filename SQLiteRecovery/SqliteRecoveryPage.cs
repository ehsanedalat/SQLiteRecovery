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

            BuildingAppsRecoverdDataTabs();
        }

        private void BuildingAppsRecoverdDataTabs()
        {
            foreach (CheckBox checkbox in checkedBoxes)
            {
                recoverDataFromDB(checkbox.Name);
                TabPage tab = BuildNewTabForDBData(checkbox.Name);
                this.tabsControl.Controls.Add(tab);
            }
        }

        private TabPage BuildNewTabForDBData(string name)
        {
            TabPage tabPage = new TabPage();
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = name;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(568, 403);
            tabPage.TabIndex = 0;
            tabPage.Text = name;
            tabPage.UseVisualStyleBackColor = true;
            return tabPage;
        }

        private void recoverDataFromDB(string dbName)
        {
            throw new NotImplementedException();
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainPage.Show();
        }
    }
}
