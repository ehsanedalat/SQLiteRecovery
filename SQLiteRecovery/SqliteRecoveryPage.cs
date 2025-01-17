﻿using System;
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
using System.Diagnostics;

namespace SQLiteRecovery
{
    public partial class SqliteRecoveryPage : Form
    {
        private Dictionary<string, ArrayList> appsInfo;
        
        private Form MainPage;
        private string dbFilePath;
        private string journalFilePath;
        private Dictionary<string, ArrayList> tabsPointers = new Dictionary<string, ArrayList>();
        private Dictionary<string, ArrayList> journalResult;
        private Dictionary<string, ArrayList> unallocatedResult;
        private SQLiteLibrary sqliteLibrary;
        private Button showButton;

        internal SqliteRecoveryPage(Form MainPage, Dictionary<string, ArrayList> appsInfo)
        {
            FormClosed += new FormClosedEventHandler(mainFrame_onClose);
            InitializeComponent();

            this.appsInfo = appsInfo;
            this.MainPage = MainPage;

            BuildingAppsRecoverdDataTabs();
        }

        internal SqliteRecoveryPage(Form MainPage, string dbFilePath, string journalFilePath)
        {
            FormClosed += new FormClosedEventHandler(mainFrame_onClose);
            InitializeComponent();

            this.MainPage = MainPage;
            

            BuildingRecoverDataTabs(dbFilePath, journalFilePath,true);


        }

        private void dbTabsControl_SelectedIndexChanged(Object sender, EventArgs e)
        {
            string tabName = ((TabControl)sender).SelectedTab.Name;
            sqliteLibrary = (SQLiteLibrary)((ArrayList)tabsPointers[tabName])[1];
            this.dbFilePath = (string)((ArrayList)tabsPointers[tabName])[2];
            this.journalFilePath = (string)((ArrayList)tabsPointers[tabName])[3];

            if ((bool)((ArrayList)tabsPointers[tabName])[0])
            {
                unallocatedResult = (Dictionary<string,ArrayList>)((ArrayList)tabsPointers[tabName])[4];
                journalResult = (Dictionary<string, ArrayList>)((ArrayList)tabsPointers[tabName])[5];
                
            }
            else
            {
                showButton = (Button)((TabControl)sender).SelectedTab.Controls["propertisesGroupBox"].Controls["showButton"];
                showButton.Enabled = false;
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork +=new DoWorkEventHandler(worker_DoWork);
                worker.ProgressChanged +=new ProgressChangedEventHandler(worker_ProgressChanged);
                worker.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                if(worker.IsBusy==false)
                    worker.RunWorkerAsync((ArrayList)tabsPointers[tabName]);

            }
        }

        void  worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
 	        
        }

        void  worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           showButton.Enabled = true;
        }

        void  worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ArrayList item = e.Argument as ArrayList;
            unallocatedResult = sqliteLibrary.unAllocatedSpases();
            item.Add(unallocatedResult);
            if (!string.IsNullOrEmpty(this.journalFilePath))
            {
                journalResult = sqliteLibrary.journalRecovery(this.journalFilePath);
                item.Add(journalResult);
            }
            else
            {
                item.Add(null);
            }

            item[0] = true;
            ((BackgroundWorker)sender).ReportProgress(100);
        }

        private void BuildingRecoverDataTabs(string dbFilePath, string journalFilePath, bool first)
        {
            string tabName = dbFilePath.Substring(dbFilePath.LastIndexOf(@"\")).Replace(@"\", "");
            SQLiteLibrary sqlite = buildSqliteConnection(dbFilePath, tabName);
            TabPage tab = BuildNewTabForDBData(tabName, sqlite.getAllTableNames());
            ArrayList items=new ArrayList();
            items.Add(false);
            items.Add(sqlite);
            items.Add(dbFilePath);
            items.Add(journalFilePath);
            tabsPointers.Add(tabName, items);
            this.dbTabsControl.SelectedIndexChanged += new EventHandler(dbTabsControl_SelectedIndexChanged);
            this.dbTabsControl.Controls.Add(tab);
            if(first)
                dbTabsControl_SelectedIndexChanged(dbTabsControl,null);
            
        }

        private void BuildingAppsRecoverdDataTabs()
        {
            bool first=true;
            foreach (string key in appsInfo.Keys)
            {
               BuildingRecoverDataTabs((string)((ArrayList)appsInfo[key])[0], (string)((ArrayList)appsInfo[key])[1], first);
               first = false;
            }
        }

        private TabPage BuildNewTabForDBData(string name, ArrayList tables)
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
            TabPage freeBlockTabPage = new TabPage();

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
            recoverTabControl.Controls.Add(freeBlockTabPage);
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
            currentTabPage.Text = "Current DB";
            currentTabPage.UseVisualStyleBackColor = true;
            // 
            // unallocatedTabPage
            // 
            unallocatedTabPage.Location = new System.Drawing.Point(4, 22);
            unallocatedTabPage.Name = "unallocatedTabPage";
            unallocatedTabPage.Padding = new System.Windows.Forms.Padding(3);
            unallocatedTabPage.Size = new System.Drawing.Size(192, 74);
            unallocatedTabPage.TabIndex = 1;
            unallocatedTabPage.Text = "Unallocated";
            unallocatedTabPage.UseVisualStyleBackColor = true;
            // 
            // freeblockTabPage
            // 
            freeBlockTabPage.Location = new System.Drawing.Point(4, 22);
            freeBlockTabPage.Name = "freeBlockTabPage";
            freeBlockTabPage.Padding = new System.Windows.Forms.Padding(3);
            freeBlockTabPage.Size = new System.Drawing.Size(192, 74);
            freeBlockTabPage.TabIndex = 2;
            freeBlockTabPage.Text = "Free Block";
            freeBlockTabPage.UseVisualStyleBackColor = true;
            // 
            // journalTabPage
            // 
            journalTabPage.Location = new System.Drawing.Point(4, 22);
            journalTabPage.Name = "journalTabPage";
            journalTabPage.Padding = new System.Windows.Forms.Padding(3);
            journalTabPage.Size = new System.Drawing.Size(192, 74);
            journalTabPage.TabIndex = 3;
            journalTabPage.Text = "Journal File";
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
            groupBox2.Name = "propertisesGroupBox";
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
            showButton.Click += new EventHandler((sender, e) => showButton_Click(sender, e, name));
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

        private void showButton_Click(object sender, EventArgs e, string name)
        {
            TabPage currentTab = getTabByName(name, dbTabsControl);
            TabControl recover = (TabControl)currentTab.Controls["RecordsGroupBox"].Controls["recoverTabControl"];
            TabPage current = getTabByName("currentTabPage", recover);
            TabPage unallocated = getTabByName("unallocatedTabPage", recover);
            TabPage freeBlock = getTabByName("freeBlockTabPage", recover);
            TabPage journal = getTabByName("journalTabPage", recover);

            string table=((ComboBox) currentTab.Controls["propertisesGroupBox"].Controls["tableComboBox"]).SelectedItem.ToString();
            string filter = ((TextBox)currentTab.Controls["propertisesGroupBox"].Controls["filterTextBox"]).Text;


            //Unallocated and freeblock records...

            if (unallocatedResult.ContainsKey(table)&&unallocatedResult!=null)
            {
                int WIDTH = 100;
                ArrayList result = unallocatedResult[table];
                ArrayList temp = new ArrayList();
                if (!String.IsNullOrEmpty(filter))
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        if (((string[])result[i])[2].IndexOf(filter, 0) != -1)
                        {
                            temp.Add(result[i]);
                        }
                    }
                    result.Clear();
                    result.AddRange(temp);
                }

                if (unallocated.Controls.ContainsKey("currentRecords"))
                {
                    unallocated.Controls.RemoveByKey("currentRecords");
                }
                DataGridView UnallocatedRecords = new DataGridView();
                UnallocatedRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
                UnallocatedRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                UnallocatedRecords.Location = new System.Drawing.Point(6, 6);
                UnallocatedRecords.Name = "currentRecords";
                UnallocatedRecords.Size = new System.Drawing.Size(unallocated.Width - 10, unallocated.Height - 10);
                UnallocatedRecords.TabIndex = 0;
                UnallocatedRecords.AutoGenerateColumns = true;
                UnallocatedRecords.ColumnCount = 2;
                UnallocatedRecords.Columns[0].Name = "";
                UnallocatedRecords.Columns[1].Name = "Data";
                UnallocatedRecords.Columns[1].Width = UnallocatedRecords.Columns[1].MinimumWidth + WIDTH;
                disableEditing(UnallocatedRecords);
                UnallocatedRecords.CellDoubleClick += new DataGridViewCellEventHandler(DataGridView1_CellDoubleClick);

                if (freeBlock.Controls.ContainsKey("currentRecords"))
                {
                    freeBlock.Controls.RemoveByKey("currentRecords");
                }
                DataGridView FreeBlockRecords = new DataGridView();
                FreeBlockRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
                FreeBlockRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                FreeBlockRecords.Location = new System.Drawing.Point(6, 6);
                FreeBlockRecords.Name = "currentRecords";
                FreeBlockRecords.Size = new System.Drawing.Size(freeBlock.Width - 10, freeBlock.Height - 10);
                FreeBlockRecords.TabIndex = 0;
                FreeBlockRecords.AutoGenerateColumns = true;
                FreeBlockRecords.ColumnCount = 2;
                FreeBlockRecords.Columns[0].Name = "";
                FreeBlockRecords.Columns[1].Name = "Data";
                FreeBlockRecords.Columns[1].Width = FreeBlockRecords.Columns[1].MinimumWidth + WIDTH;
                disableEditing(FreeBlockRecords);
                FreeBlockRecords.CellDoubleClick += new DataGridViewCellEventHandler(DataGridView1_CellDoubleClick);

                int index = 0;
                int indexf = 0;
                foreach (string[] item in result)
                {

                    if (item[1] == "FREE BLOCK")
                    {
                        indexf++;
                        FreeBlockRecords.Rows.Add(new string[] { indexf + "", item[2] });
                    }
                    else
                    {
                        index++;
                        UnallocatedRecords.Rows.Add(new string[] { index + "", item[2] });
                    }
                }

                unallocated.Controls.Add(UnallocatedRecords);
                freeBlock.Controls.Add(FreeBlockRecords);
            }
            else
            {
                if (unallocated.Controls.ContainsKey("currentRecords"))
                {
                    unallocated.Controls.RemoveByKey("currentRecords");
                }
                if (freeBlock.Controls.ContainsKey("currentRecords"))
                {
                    freeBlock.Controls.RemoveByKey("currentRecords");
                }
            }
            // All current records...

            DataTable tableRecords = sqliteLibrary.getAllTableRecords(table, filter);
            if (tableRecords != null)
            {
                if (current.Controls.ContainsKey("currentRecords"))
                {
                    current.Controls.RemoveByKey("currentRecords");
                }
                // 
                // currentRecords
                // 
                DataGridView records = new DataGridView();
                records.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
                records.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                records.Location = new System.Drawing.Point(6, 6);
                records.Name = "currentRecords";
                records.Size = new System.Drawing.Size(current.Width - 10, current.Height - 10);
                records.TabIndex = 0;
                records.AutoGenerateColumns = true;

                records.DataSource = tableRecords;
                records.DataError += new DataGridViewDataErrorEventHandler(records_DataError);
                records.CellDoubleClick += new DataGridViewCellEventHandler(DataGridView1_CellDoubleClick);

                current.Controls.Add(records);
                disableEditing(records);
            }
            //journal records...

            if (!String.IsNullOrEmpty(journalFilePath)&&journalResult!=null)
            {

                if (journalResult.ContainsKey(table))
                {
                    ArrayList list = journalResult[table];
                    if (!String.IsNullOrEmpty(filter))
                    {
                        ArrayList temp = new ArrayList();
                        temp.Add(list[0]);
                        foreach (ArrayList item in list)
                        {
                            foreach (object obj in item)
                            {
                                if (obj is string)
                                {
                                    if (((string)obj).Contains(filter))
                                    {
                                        if (temp[0] != item)
                                        {
                                            temp.Add(item);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        list.Clear();
                        list.AddRange(temp);
                    }
                    if (journal.Controls.ContainsKey("currentRecords"))
                    {
                        journal.Controls.RemoveByKey("currentRecords");
                    }
                    DataGridView JournalRecords = new DataGridView();
                    JournalRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                    JournalRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    JournalRecords.Location = new System.Drawing.Point(6, 6);
                    JournalRecords.Name = "currentRecords";
                    JournalRecords.Size = new System.Drawing.Size(journal.Width - 10, journal.Height - 10);
                    JournalRecords.TabIndex = 0;
                    JournalRecords.AutoGenerateColumns = true;
                    JournalRecords.ColumnCount = ((ArrayList)list[0]).Count;
                    for (int i = 0; i < ((ArrayList)list[0]).Count; i++)
                    {
                        JournalRecords.Columns[i].Name = (string)((ArrayList)list[0])[i];
                    }

                    disableEditing(JournalRecords);
                    JournalRecords.CellDoubleClick += new DataGridViewCellEventHandler(DataGridView1_CellDoubleClick);

                    for (int i = 1; i < list.Count; i++)
                    {
                        object[] item = new object[((ArrayList)list[i]).Count];
                        int index = 0;
                        foreach (object obj in (ArrayList)list[i])
                        {
                            if (obj is byte[])
                            {
                                item[index] =((byte[])obj);
                            }
                            else
                            {
                                item[index] = obj.ToString();
                            }
                            index++;
                        }
                        JournalRecords.Rows.Add(item);
                    }
                    journal.Controls.Add(JournalRecords);
                }
                else
                {
                    if (journal.Controls.ContainsKey("currentRecords"))
                    {
                        journal.Controls.RemoveByKey("currentRecords");
                    }
                }
            }
        }

        private void disableEditing(DataGridView dataGridView)
        {
            foreach (DataGridViewBand band in dataGridView.Columns)
            {
                band.ReadOnly = true;
            }
        }
        private void DataGridView1_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            //TODO MESSAGE FORM
            object cell=((DataGridView)sender).SelectedCells[0].Value;
            if (cell is byte[])
            {
                string res = "";
                foreach (byte b in (byte[])cell)
                {
                    res = res + Convert.ToString(b, 16) + " ";
                }
                MessageBox.Show(res);
            }
            else
            {
                try
                {
                    MessageBox.Show((string)cell);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(cell.ToString());
                }
            }
        }
        void records_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            DataGridView records = (DataGridView)sender;
            byte[] cell = (byte[])records.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            records.Rows[e.RowIndex].Cells[e.ColumnIndex].ValueType = typeof(String);
            //records.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "blob(" + cell.Length + ")";
            
        }

        private TabPage getTabByName(string name, TabControl dbTabsControl)
        {
            TabPage currentTab = null;
            foreach (TabPage tab in dbTabsControl.TabPages)
                if (tab.Name == name)
                    currentTab = tab;
            return currentTab;
        }

        private SQLiteLibrary buildSqliteConnection(string dbFilePath, string dbName)
        {
            string dbDirName = dbName.Split(new char[] { '.' })[0];

            SQLiteLibrary sqlite = new SQLiteLibrary(@"..\workspace\" + dbDirName, dbFilePath, dbName);
            
            return sqlite;
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            tabsPointers.Clear();
            this.Hide();
            MainPage.Show();
        }

        private void mainFrame_onClose(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
