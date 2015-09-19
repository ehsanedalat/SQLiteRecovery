using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevicePluginInterface;
using System.Diagnostics;
using PluginGenerator;
using MySQLLibrary;
using System.IO;
using System.Collections;
using System.Reflection;

namespace SQLiteRecovery
{
    public partial class MainUI : Form
    {
        private object plugin;
        private static string storedDBsPath = @"..\Databases\";
        private Dictionary<string, Dictionary<string, string>> tabs;
        private SQLUtils utils;
        private string DBFilePath;
        private string journalFilePath;
        private ErrorProvider error;
        private string assemblyPath;
        public string updatedTab { get; set; }
        /// <summary>
        /// constructor
        /// </summary>
        public MainUI()
        {
            InitializeComponent();
            error = new ErrorProvider();
            utils = new SQLUtils("sqlite_recovery_plugins");
            
            storedDBsPath = Path.GetFullPath(storedDBsPath);
            tabs=new Dictionary<string,Dictionary<string,string>>();
            getPluginData();
            OSTabsControl.SelectedIndexChanged += new EventHandler(OSTabsControl_SelectedIndexChanged);
        }

        void OSTabsControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage current = ((TabControl)sender).SelectedTab;
            if (current.Name == "directTabPage")
            {
                editButton.Enabled = false;
                deleteButton.Enabled = false;
            }
            else
            {
                editButton.Enabled = true;
                deleteButton.Enabled = true;
                assemblyPath = OSTabsControl.SelectedTab.Controls["dllAddressLabel"].Text;
                if (!isAssemblyLoaded(assemblyPath))
                {
                    BackgroundWorker assemblyWorker = new BackgroundWorker();
                    assemblyWorker.WorkerReportsProgress = true;
                    assemblyWorker.DoWork += new DoWorkEventHandler(assemblyWorker_DoWork);
                    assemblyWorker.ProgressChanged += new ProgressChangedEventHandler(assemblyWorker_ProgressChanged);
                    assemblyWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(assemblyWorker_RunWorkerCompleted);
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = "Loading plugin...";
                    ((Button)OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"]).Visible = false;
                    ((Button)OSTabsControl.SelectedTab.Controls["recoverButton"]).Enabled = false;
                    if (assemblyWorker.IsBusy == false)
                        assemblyWorker.RunWorkerAsync();
                }
                else
                {

                    string status = "";
                    if (PluginServices.isDeviceConnected(plugin))
                    {
                        if (PluginServices.isDeviceRoot(plugin))
                        {
                            status = "Root";
                        }
                        else
                        {
                            status = "not Root";
                        }
                    }
                    else
                    {
                        status = "Offline";
                    }
                    Button statusButton = (Button)OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"];
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = status;
                    if (status == "Offline")
                        statusButton.Text = "Refresh";
                    else if (status == "not Root")
                        statusButton.Text = "Root";
                    else
                        statusButton.Visible = false;
                }
            }
        }

        void assemblyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ((Button)OSTabsControl.SelectedTab.Controls["recoverButton"]).Enabled = true;
        }

        void assemblyWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string status = "";
            if (PluginServices.isDeviceConnected(plugin))
            {
                if (PluginServices.isDeviceRoot(plugin))
                {
                    status = "Root";
                }
                else
                {
                    status = "not Root";
                }
            }
            else
            {
                status = "Offline";
            }
            Button statusButton = (Button)OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"];
            OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = status;
            if (status == "Offline")
                statusButton.Text = "Refresh";
            else if (status == "not Root")
                statusButton.Text = "Root";
            else
                statusButton.Visible = false;
        }

        void assemblyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            plugin = PluginServices.loadPlugin(assemblyPath);
            ((BackgroundWorker)sender).ReportProgress(100);

        }
        private bool isAssemblyLoaded(string location)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                if (assembly.Location == location)
                {        
                    return true;
                }
            }

            return false;
        }

        
        /// <summary>
        /// get plugin data from DB
        /// </summary>
        private void getPluginData()
        {
            Dictionary<int, Dictionary<string, string>> pluginsData = utils.Select("plugins", true, null, null);

            for (int i = 0; pluginsData.ContainsKey(i);i++ )
            {
               Dictionary<int,Dictionary<string, string>> appsData=
                   utils.Select("plugins join apps", false, new string[] { "apps.name", "apps.path" }, "plugins.name='" + pluginsData[i]["name"] + "' and plugins.name=apps.plugin_name");
               buildPluginUI(pluginsData[i]["name"], pluginsData[i]["os"], pluginsData[i]["dll_address"], appsData);
               tabs.Add(pluginsData[i]["name"], pluginsData[i]);
            }
        }
        /// <summary>
        /// build UI for each plugin.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="os"></param>
        /// <param name="dll_address"></param>
        /// <param name="appsData"></param>
        private void buildPluginUI(string name, string os, string dll_address, Dictionary<int, Dictionary<string, string>> appsData)
        {
            // 
            // initialization
            // 
            Button recoverButton = new Button();
            Button rootButton = new Button();
            GroupBox groupBox2=new GroupBox();
            GroupBox groupBox3 = new GroupBox();
            Label dllAddressLabel = new Label();
            Label label2 = new Label();
            Label label1 = new Label();
            Label label3 = new Label();
            Label rootLabel = new Label();
            Label osLabel = new Label();
            TabPage tabPage = new TabPage();
            FlowLayoutPanel appsPannel = new FlowLayoutPanel();
            //
            //tabPage 
            //
            tabPage.Controls.Add(recoverButton);
            tabPage.Controls.Add(groupBox2);
            tabPage.Controls.Add(groupBox3);
            tabPage.Controls.Add(dllAddressLabel);
            tabPage.Controls.Add(label2);
            tabPage.Controls.Add(osLabel);
            tabPage.Controls.Add(label1);
            tabPage.Location = new System.Drawing.Point(4, 22);
            tabPage.Name = name;
            tabPage.Padding = new System.Windows.Forms.Padding(3);
            tabPage.Size = new System.Drawing.Size(583, 413);
            tabPage.TabIndex = 0;
            tabPage.Text = name;
            tabPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 12);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(77, 13);
            label1.TabIndex = 0;
            label1.Text = "Supported OS:";
            // 
            // osLabel
            // 
            osLabel.AutoSize = true;
            osLabel.Location = new System.Drawing.Point(90, 12);
            osLabel.Name = "osLabel";
            osLabel.Size = new System.Drawing.Size(0, 13);
            osLabel.TabIndex = 1;
            osLabel.Text = os;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 35);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(82, 13);
            label2.TabIndex = 2;
            label2.Text = "Dll File Address:";
            // 
            // dllAddressLabel
            // 
            dllAddressLabel.AutoSize = true;
            dllAddressLabel.Location = new System.Drawing.Point(95, 35);
            dllAddressLabel.Name = "dllAddressLabel";
            dllAddressLabel.Size = new System.Drawing.Size(0, 13);
            dllAddressLabel.TabIndex = 3;
            dllAddressLabel.Text = dll_address;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(appsPannel);
            groupBox2.Location = new System.Drawing.Point(4, 52);
            groupBox2.Name = "appsGroupBox";
            groupBox2.Size = new System.Drawing.Size(576, 225);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Select Witch App to recover Data";
            // 
            // appsPannel
            // 
            appsPannel.Dock = System.Windows.Forms.DockStyle.Fill;
            appsPannel.Location = new System.Drawing.Point(3, 16);
            appsPannel.Name = "appsPannel";
            appsPannel.Size = new System.Drawing.Size(570, 296);
            appsPannel.TabIndex = 0;
            // 
            // checkBoxs
            // 
            Dictionary<string,string> item=new Dictionary<string,string>();
            for (int i = 0; appsData.ContainsKey(i); i++)
            {
                CheckBox checkBox1 = new CheckBox();
                checkBox1.AutoSize = true;
                checkBox1.Location = new System.Drawing.Point(3, 3);
                checkBox1.Name = appsData[i]["name"]+"_checkBox";
                checkBox1.Size = new System.Drawing.Size(49, 17);
                checkBox1.TabIndex = 0;
                checkBox1.Text = appsData[i]["name"];
                checkBox1.UseVisualStyleBackColor = true;
                checkBox1.CheckStateChanged += new EventHandler(checkBox1_CheckStateChanged);
                appsPannel.Controls.Add(checkBox1);
                item.Add(appsData[i]["name"], appsData[i]["path"]);
            }
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(rootButton);
            groupBox3.Controls.Add(rootLabel);
            groupBox3.Controls.Add(label3);
            groupBox3.Location = new System.Drawing.Point(4, 279);
            groupBox3.Name = "statusGroupBox";
            groupBox3.Size = new System.Drawing.Size(576, 68);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Device Status";
            // 
            // label1
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(7, 32);
            label3.Name = "label1";
            label3.Size = new System.Drawing.Size(75, 13);
            label3.TabIndex = 0;
            label3.Text = "Status: ";
            
            // 
            // rootLabel
            // 
            rootLabel.AutoSize = true;
            rootLabel.Location = new System.Drawing.Point(80, 32);
            rootLabel.Name = "statusLabel";
            rootLabel.Size = new System.Drawing.Size(38, 13);
            rootLabel.TabIndex = 1;
            rootLabel.Text = ".";
            // 
            // rootButton
            // 
            rootButton.Location = new System.Drawing.Point(156, 27);
            rootButton.Name = "statusButton";
            rootButton.Size = new System.Drawing.Size(104, 23);
            rootButton.TabIndex = 2;
            rootButton.UseVisualStyleBackColor = true;
            rootButton.Click += new EventHandler(rootButton_Click);
            // 
            // recoverButton
            // 
            recoverButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            recoverButton.Location = new System.Drawing.Point(188, 378);
            recoverButton.Name = "recoverButton";
            recoverButton.Size = new System.Drawing.Size(181, 29);
            recoverButton.TabIndex = 0;
            recoverButton.Text = "Recover selected Apps data";
            recoverButton.UseVisualStyleBackColor = true;
            recoverButton.Click += new EventHandler((sender, e) => recoverButtonPlugin_Click(sender, e, item));

            OSTabsControl.Controls.Add(tabPage);
            appsData.Clear();
        }

        void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            error.Clear();
        }

        void rootButton_Click(object sender, EventArgs e)
        {
            string status = ((Button)sender).Text;
            if (status == "Refresh")
            {
                if (PluginServices.isDeviceConnected(plugin))
                {
                    if (PluginServices.isDeviceRoot(plugin))
                    {
                        OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Visible = false;
                        OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = "Root";
                    }
                    else
                    {
                        OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Visible = true;
                        OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Text = "Root";
                        OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = "not Root";
                    }
                }
            }
            else
            {
                if (PluginServices.isDeviceConnected(plugin))
                {
                    if (PluginServices.isDeviceRoot(plugin))
                    {
                        OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Visible = false;
                        OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Text = "Refresh";
                        OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = "Root";
                    }
                    else
                    {
                        if (PluginServices.rootDevice(plugin))
                        {
                            OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Visible = false;
                            OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = "Root";
                        }
                        else
                        {
                            OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Visible = true;
                            OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Text = "Root";
                            OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = "not Root";
                            PluginServices.refreshDeviceList(plugin);
                        }
                    }
                }
                else
                {
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Visible = true;
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Text = "Refresh";
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = "Offline";
                    PluginServices.refreshDeviceList(plugin);
                }
            }
        }

        private void recoverButtonPlugin_Click(object sender, EventArgs e, Dictionary<string,string> apps)
        {
            ArrayList checkedApps = getAppChecked();
            error.Clear();
            if (checkedApps.Count > 0 && PluginServices.isDeviceConnected(plugin) && PluginServices.isDeviceRoot(plugin))
            {
                Dictionary<string, string> temp = new Dictionary<string, string>();
                Dictionary<string, ArrayList> appsInfo = new Dictionary<string, ArrayList>();
                foreach (string name in checkedApps)
                {
                    string n = name.Split(new char[] { '_' })[0];
                    temp.Add(n, apps[n]);
                    if (apps[n].Contains(@"\"))
                        apps[n] = apps[n].Replace(@"\", "/");
                    string path=storedDBsPath+n+@"\"+apps[n].Substring(apps[n].LastIndexOf("/")).Replace("/", "");
                    string journalPath=path+"-journal";
                    ArrayList pathes=new ArrayList();
                    pathes.Add(path);
                    pathes.Add(journalPath);
                    //pathes.Add(path + "-shm");
                    //pathes.Add(path + "-wal");
                    appsInfo.Add(n, pathes);
                }
                
                CopyFilesProgress copy = new CopyFilesProgress(this, temp, plugin, storedDBsPath,appsInfo);
                copy.Show();
                this.Hide();
                
            }
            else
            {
                if (checkedApps.Count == 0)
                {
                    error.SetError(OSTabsControl.SelectedTab.Controls["appsGroupBox"].Controls["appsPannel"].Controls[0], "Please select at least one app!!");
                    
                }
                if (!PluginServices.isDeviceConnected(plugin))
                {
                    MessageBox.Show("Please connect your device!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Visible = true;
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Text = "Refresh";
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = "Offline";
                    PluginServices.refreshDeviceList(plugin);
                }
                else if (!PluginServices.isDeviceRoot(plugin))
                {
                    MessageBox.Show("Connected device has not root access. please root your device!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Visible = true;
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusButton"].Text = "Root";
                    OSTabsControl.SelectedTab.Controls["statusGroupBox"].Controls["statusLabel"].Text = "not Root";
                }
            }
        }
        private ArrayList getAppChecked()
        {
            CheckBox[] apps = OSTabsControl.SelectedTab.Controls["appsGroupBox"].Controls["appsPannel"].Controls.OfType<CheckBox>().ToArray();
            ArrayList result = new ArrayList();
            foreach (CheckBox box in apps)
                if (box.Checked)
                    result.Add(box.Name);
            return result;
        }

        private void generatePluginButton_Click(object sender, EventArgs e)
        {
            MainFormPluginGenerator generatePluginUI = new MainFormPluginGenerator(this,null);
            generatePluginUI.Show();
            this.Hide();
        }

        public void updateTabs()
        {
            Dictionary<int, Dictionary<string, string>> pluginsData = utils.Select("plugins", true, null, null);
            Dictionary<string, Dictionary<string, string>> newTabsData = new Dictionary<string, Dictionary<string, string>>();

            for (int i = 0; pluginsData.ContainsKey(i); i++)
            {
                newTabsData.Add(pluginsData[i]["name"], pluginsData[i]);
            }
            //add new tabs
            foreach (string key in newTabsData.Keys)
            {
                if (!tabs.ContainsKey(key) || newTabsData[key]["name"] == updatedTab)
                {
                    if (newTabsData[key]["name"] == updatedTab)
                    {
                        tabs.Remove(updatedTab);
                        OSTabsControl.TabPages.RemoveByKey(updatedTab);
                    }
                   buildPluginUI(newTabsData[key]["name"], newTabsData[key]["os"], newTabsData[key]["dll_address"],
                   utils.Select("plugins join apps", false, new string[] { "apps.name", "apps.path" }, "plugins.name='" + newTabsData[key]["name"] + "' and plugins.name=apps.plugin_name"));
                }
                else
                    tabs.Remove(key);
            }
            //remove deleted tabs
            foreach (string key in tabs.Keys)
            {
                OSTabsControl.TabPages.RemoveByKey(key);
            }
            tabs.Clear();
            //////mabe bug!!
            tabs = newTabsData;
            
        }

        public void updateTabData(string name)
        {
            tabs.Remove(name);
            updateTabs();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure, you want delete this plugin(" + OSTabsControl.SelectedTab.Name + ") ??",
                                     "Delete Confirmation !!",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {
                if (OSTabsControl.TabCount > 0)
                {
                    utils.Delete("delete from plugins where name='" + OSTabsControl.SelectedTab.Name + "'");
                    updateTabs();
                }
            }
            
            
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            MainFormPluginGenerator generatePluginUI = new MainFormPluginGenerator(this, OSTabsControl.SelectedTab.Name);
            generatePluginUI.Show();
            this.Hide();
        }

        private void DBPathButton_Click(object sender, EventArgs e)
        {
            if (DBOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                DBFilePath = DBOpenFileDialog.FileName;
                DBFileTextBox.Text = DBFilePath;
            }
        }

        private void journalPathButton_Click(object sender, EventArgs e)
        {
            if (journalOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                journalFilePath = journalOpenFileDialog.FileName;
                journalFileTextBox.Text = journalFilePath;
            }
        }

        private void recoverButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(DBFilePath))
            {
                SqliteRecoveryPage recoveryPage = null;
                try
                {
                    recoveryPage = new SqliteRecoveryPage(this, DBFilePath, journalFilePath);
                    recoveryPage.Show();
                    this.Hide();
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("There is no siutable file in given path!!!( "+ex.FileName+" )", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                error.SetError(DBFileTextBox, "Empty Box !!");
            }
        }

        private void DBFileTextBox_TextChanged(object sender, EventArgs e)
        {
            DBFilePath = DBFileTextBox.Text;
            error.Clear();
        }
        
    }
}
