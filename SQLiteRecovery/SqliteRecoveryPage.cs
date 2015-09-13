using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLiteParser;
using System.IO;
using System.Collections;

namespace SQLiteRecovery
{
    public partial class SqliteRecoveryPage : Form
    {
        private List<CheckBox> checkedBoxes;
        private Form MainPage;

        internal SqliteRecoveryPage(List<CheckBox> checkedBoxes, Form MainPage)
        {
            FormClosed += new FormClosedEventHandler(mainFrame_onClose);
            InitializeComponent();

            this.checkedBoxes = checkedBoxes;
            this.MainPage = MainPage;

            BuildingAppsRecoverdDataTabs();
        }

        internal SqliteRecoveryPage(Form MainPage, string dbFilePath, string journalFilePath)
        {
            InitializeComponent();

            this.MainPage = MainPage;

            BuildingRecoverDataTabs(dbFilePath, journalFilePath);

        }

        private void BuildingRecoverDataTabs(string dbFilePath, string journalFilePath)
        {
            SQLiteLibrary sqlite= buildSqliteConnection(dbFilePath);
            TabPage tab = BuildNewTabForDBData("DB NAME",sqlite);
            this.tabsControl.Controls.Add(tab);
        }

        private void BuildingAppsRecoverdDataTabs()
        {
            foreach (CheckBox checkbox in checkedBoxes)
            {
                //TODO FILE AND JOURNAL PATH
                //recoverDataFromDB(checkbox.Name);
                //TabPage tab = BuildNewTabForDBData(checkbox.Name);
                //this.tabsControl.Controls.Add(tab);
            }
        }

        private TabPage BuildNewTabForDBData(string name, SQLiteLibrary sqlite)
        {
            TabPage tabPage = new TabPage();
            GroupBox RecordsGroupBox = new GroupBox();
            GroupBox groupBox2 = new GroupBox();
            ComboBox tableComboBox = new ComboBox();
            Button showButton = new Button();
            Label label1 = new Label();
            Label label2 = new Label();
            TextBox filterTextBox = new TextBox();
            TabControl recoverTabControl = new TabControl();
            TabPage currentTabPage = new TabPage();
            TabPage unallocatedTabPage = new TabPage();
            TabPage journalTabPage = new TabPage();

            ArrayList tables = sqlite.getAllTableNames();

            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = name;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(568, 403);
            tabPage.TabIndex = 0;
            tabPage.Text = name;
            tabPage.UseVisualStyleBackColor = true;
            tabPage.Controls.Add(RecordsGroupBox);
            tabPage.Controls.Add(groupBox2);
            // 
            // RecordsGroupBox
            // 
            RecordsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            RecordsGroupBox.Location = new System.Drawing.Point(3, 61);
            RecordsGroupBox.Name = "RecordsGroupBox";
            RecordsGroupBox.Size = new System.Drawing.Size(562, 340);
            RecordsGroupBox.TabIndex = 6;
            RecordsGroupBox.TabStop = false;
            RecordsGroupBox.Text = "Records";
            RecordsGroupBox.Controls.Add(recoverTabControl);
            // 
            // recoverTabControl
            // 
            recoverTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            recoverTabControl.Controls.Add(currentTabPage);
            recoverTabControl.Controls.Add(unallocatedTabPage);
            recoverTabControl.Controls.Add(journalTabPage);
            recoverTabControl.Location = new System.Drawing.Point(6, 16);
            recoverTabControl.Name = "recoverTabControl";
            recoverTabControl.SelectedIndex = 0;
            recoverTabControl.Size = new System.Drawing.Size(550, 320);
            recoverTabControl.TabIndex = 0;
            // 
            // currentTabPage
            // 
            currentTabPage.Location = new System.Drawing.Point(4, 22);
            currentTabPage.Name = "currentTabPage";
            currentTabPage.Padding = new System.Windows.Forms.Padding(3);
            currentTabPage.Size = new System.Drawing.Size(548, 276);
            currentTabPage.TabIndex = 0;
            currentTabPage.Text = "Current Records";
            currentTabPage.UseVisualStyleBackColor = true;
            // 
            // unallocatedTabPage
            // 
            unallocatedTabPage.Location = new System.Drawing.Point(4, 22);
            unallocatedTabPage.Name = "unallocatedTabPage";
            unallocatedTabPage.Padding = new System.Windows.Forms.Padding(3);
            unallocatedTabPage.Size = new System.Drawing.Size(192, 74);
            unallocatedTabPage.TabIndex = 1;
            unallocatedTabPage.Text = "UnAllocated";
            unallocatedTabPage.UseVisualStyleBackColor = true;
            // 
            // journalTabPage
            // 
            journalTabPage.Location = new System.Drawing.Point(4, 22);
            journalTabPage.Name = "journalTabPage";
            journalTabPage.Padding = new System.Windows.Forms.Padding(3);
            journalTabPage.Size = new System.Drawing.Size(192, 74);
            journalTabPage.TabIndex = 2;
            journalTabPage.Text = "Journal Records";
            journalTabPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            groupBox2.Controls.Add(tableComboBox);
            groupBox2.Controls.Add(showButton);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(filterTextBox);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new System.Drawing.Point(3, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(562, 52);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Propertises";
            // 
            // tableComboBox
            // 
            tableComboBox.FormattingEnabled = true;
            tableComboBox.Location = new System.Drawing.Point(51, 19);
            tableComboBox.Name = "tableComboBox";
            tableComboBox.Size = new System.Drawing.Size(155, 21);
            tableComboBox.TabIndex = 0;
            tableComboBox.Items.AddRange(tables.ToArray());
            tableComboBox.SelectedIndex = 0;
            // 
            // showButton
            // 
            showButton.Location = new System.Drawing.Point(454, 17);
            showButton.Name = "showButton";
            showButton.Size = new System.Drawing.Size(90, 23);
            showButton.TabIndex = 4;
            showButton.Text = "Show Records";
            showButton.UseVisualStyleBackColor = true;
            showButton.Click += new EventHandler((sender, e) => showButton_Click(sender, e, sqlite));
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 23);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(40, 13);
            label1.TabIndex = 1;
            label1.Text = "Table: ";
            // 
            // filterTextBox
            // 
            filterTextBox.Location = new System.Drawing.Point(259, 19);
            filterTextBox.Name = "filterTextBox";
            filterTextBox.Size = new System.Drawing.Size(178, 20);
            filterTextBox.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(218, 22);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 13);
            label2.TabIndex = 2;
            label2.Text = "Filter: ";
            return tabPage;
        }

        private void showButton_Click(object sender, EventArgs e, SQLiteLibrary sqlite)
        {
            
        }

        private SQLiteLibrary buildSqliteConnection(string dbFilePath)
        {
            SQLiteLibrary sqlite = new SQLiteLibrary(@"..\workspace", dbFilePath);
            
            return sqlite;
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainPage.Show();
        }

        private void mainFrame_onClose(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
