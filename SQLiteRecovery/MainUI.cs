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

namespace SQLiteRecovery
{
    public partial class MainUI : Form
    {
        private ICollection<DeviceRecoveryPluginInterface> plugins;
        private TabPage tabPage;
        /// <summary>
        /// constructor
        /// </summary>
        public MainUI()
        {
            InitializeComponent();
            
            plugins = PluginServices.LoadPlugins("Plugins");
            
            //buildPluginUI();
        }
        /// <summary>
        /// Add each plugin to main UI.
        /// add action listener to recover button.
        /// </summary>
        private void buildPluginUI()
        {
            foreach(DeviceRecoveryPluginInterface plugin in plugins){
                tabPage=plugin.osTabPage;
                OSTabsControl.Controls.Add(tabPage);
                if (tabPage.Controls.ContainsKey(plugin.recoverButtonName))
                {
                    Button recover = (Button)tabPage.Controls[plugin.recoverButtonName];
                    recover.Click += new EventHandler((sender,e)=>recoverOnClickListener(sender,e,tabPage));
                    tabPage.Controls.Add(recover);
                }
                else
                {
                    Debug.Fail("MainUI/there is no button in "+plugin.pluginName+" tabPage with name of "+plugin.pluginName+"!!");
                }

            }
        }
        /// <summary>
        /// listener for each tab page, adding to main UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="tabPage"> tab page, created in each plugin</param>
        void recoverOnClickListener(object sender, EventArgs e, TabPage tabPage)
        {
            var checkedBoxes = tabPage.Controls.OfType<CheckBox>().Where(c => c.Checked).ToList<CheckBox>();
            SqliteRecoveryPage page = new SqliteRecoveryPage(checkedBoxes, this);
            page.Show();
            this.Hide();
        }

        private void generatePluginButton_Click(object sender, EventArgs e)
        {
            MainFormPluginGenerator generatePluginUI = new MainFormPluginGenerator(this);
            generatePluginUI.Show();
            this.Hide();
        }

       
    }
}
