using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using MySQLLibrary;
using System.Diagnostics;
using SQLiteRecovery;

namespace PluginGenerator
{
    public partial class MainFormPluginGenerator : Form
    {
        private static string PATH = "Path: ";
        private static int HEIGHT=20;
        private static int WIDTH=65;
        private string dllFileName;
        public Dictionary<string, string> apps { get; set; }
        private ErrorProvider error;
        private MainUI parent;
        public string tabName { get; set; }
        private SQLUtils utils;

        public MainFormPluginGenerator(MainUI parent,string tabName)
        {
            InitializeComponent();
            FormClosed += new FormClosedEventHandler(mainFrame_onClose);
            this.parent = parent;
            error = new ErrorProvider();
            apps = new Dictionary<string, string>();
            this.tabName = tabName;
            if (!String.IsNullOrEmpty(tabName))
            {
                utils = new SQLUtils("sqlite_recovery_plugins");
                Dictionary<int, Dictionary<string, string>> result =
                    utils.Select("plugins", false, new string[] { "os", "dll_address" }, "plugins.name='" + tabName + "'");
                pluginNameTextBox.Text = tabName;
                pluginNameTextBox.Enabled = false;
                osComboBox.SelectedIndex = osComboBox.Items.IndexOf(result[0]["os"]);
                addressTextBox.Text = result[0]["dll_address"];
                result = utils.Select("plugins,apps", false, new string[] { "apps.name", "apps.path" }, "plugins.name=apps.plugin_name and plugins.name='" + tabName + "'");
                for (int i = 0; result.ContainsKey(i); i++)
                {
                    apps.Add(result[i]["name"], result[i]["path"]);
                }
                this.Text = "Update "+tabName+" Information";
                build_button.Text = "Update Plugin Data";
            }
            else
            {
                apps.Add("SMS", "/data/data/");
                apps.Add("Browser", "/data/data/");
            }

            updateAppsCheckBoxes();
        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            if (solutionDialog.ShowDialog() == DialogResult.OK)
            {
                dllFileName=solutionDialog.FileName;
                addressTextBox.Text = dllFileName;
                dllFileName=dllFileName.Replace(@"\",@"\\");
            }
            
        }

        private void edit_button_Click(object sender, EventArgs e)
        {
            Apps_and_pathes appsAndPathes = new Apps_and_pathes(apps, this);
            appsAndPathes.Show();
            this.Hide();
        }

        public void updateAppsCheckBoxes()
        {
            checkBoxPanel.Controls.Clear();
            //var grid = new Grid();
            int row = 0;
            int column = 0;
            foreach (var item in apps)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.AutoSize = true;
                checkBox.Location = new System.Drawing.Point(3+WIDTH*column, 3+HEIGHT*row);
                checkBox.Name = item.Key+"_checkBox";
                checkBox.Size = new System.Drawing.Size(WIDTH, HEIGHT);
                checkBox.TabIndex = 1;
                checkBox.Text = item.Key;
                checkBox.UseVisualStyleBackColor = true;
                Utils.ToolTip(PATH+item.Value, checkBox);
                checkBox.Checked = true;
                checkBoxPanel.Controls.Add(checkBox);
                row++;
                if (row == 9)
                {
                    row = 0;
                    column++;
                }

            }
        }

        private void mainFrame_onClose(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void build_button_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(osComboBox.Text)&&!string.IsNullOrEmpty(pluginNameTextBox.Text)&&!string.IsNullOrEmpty(addressTextBox.Text) && !noAppChecked())
            {
                if (String.IsNullOrEmpty(tabName) && addRecordToDB())
                {
                    parent.Show();
                    parent.updateTabs();
                    this.Hide();
                }
                else if (!String.IsNullOrEmpty(tabName)&& updateRecordToDB())
                {
                    parent.updatedTab = tabName;
                    parent.Show();
                    parent.updateTabs();
                    this.Hide();
                }
            }
            else
            {
                if (string.IsNullOrEmpty(pluginNameTextBox.Text))
                    error.SetError(pluginNameTextBox, "Empty Box !!!");
                if (string.IsNullOrEmpty(osComboBox.Text))
                    error.SetError(osComboBox, "Empty Box !!!");
                if (string.IsNullOrEmpty(addressTextBox.Text))
                    error.SetError(addressTextBox, "Empty Box!!!");
                if (noAppChecked())
                    error.SetError(checkBoxPanel,"None of items are checked!!!");
            }
        }

        private bool updateRecordToDB()
        {
            try
            {
                utils.Update("UPDATE plugins SET os='"+osComboBox.SelectedItem+"', dll_address='"+addressTextBox.Text.Replace(@"\",@"\\")+"' WHERE name='"+tabName+"'");
                utils.Delete("DELETE FROM apps WHERE plugin_name='"+tabName+"'");
                CheckBox[] appsBox = this.checkBoxPanel.Controls.OfType<CheckBox>().ToArray();
                foreach (CheckBox box in appsBox)
                    if (box.Checked)
                        utils.Insert("insert into apps (name,path,plugin_name)values('" + box.Text + "','" + apps[box.Text] + "','" + tabName + "');");
            }
            catch (MySQLException e)
            {
                Debug.Fail(e.getMessage());
                return false;
            }
            return true;
        }

        private bool addRecordToDB()
        {
            
            try
            {
                SQLUtils utils = new SQLUtils("sqlite_recovery_plugins");
                Dictionary<int,Dictionary<string,string>> result=utils.Select("plugins", false, new string[] { "name" }, "plugins.name='" + pluginNameTextBox.Text + "'");
                if (!result.ContainsKey(0) || !result[0].ContainsValue(pluginNameTextBox.Text))
                {
                    utils.Insert("insert into plugins (name,os,dll_address) values ('" + pluginNameTextBox.Text + "','" + osComboBox.SelectedItem + "','" + dllFileName + "');");
                    string name = utils.Select("plugins", false, new string[] { "name" }, "name='" + pluginNameTextBox.Text + "' and os='" + osComboBox.SelectedItem + "' and dll_address='" + dllFileName + "'")[0]["name"];
                    CheckBox[] appsBox = this.checkBoxPanel.Controls.OfType<CheckBox>().ToArray();
                    foreach (CheckBox box in appsBox)
                        if (box.Checked)
                            utils.Insert("insert into apps (name,path,plugin_name)values('" + box.Text + "','" + apps[box.Text] + "','" + name + "');");
                    return true;
                }
                else
                {
                    error.SetError(pluginNameTextBox, "This name current exist!");
                    return false;
                }
            }
            catch (MySQLException e)
            {
                Debug.Fail(e.getMessage());
                return false;
            }
            
        }
        /*
        private void loadDllFile()
        {
            importedDllfile = Assembly.LoadFile(addressTextBox.Text);
        }
        
        private void root()
        {
            var rootType=importedDllfile.GetType("root");
            var root=Activator.CreateInstance(rootType);
            rootType.InvokeMember("root", BindingFlags.InvokeMethod, null, root, null);
           
        }*/

        private bool noAppChecked()
        {
            CheckBox[] apps = this.checkBoxPanel.Controls.OfType<CheckBox>().ToArray();
            foreach (CheckBox box in apps)
                if (box.Checked)
                    return false;
            return true;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            parent.Show();
        }

        
    }
}
