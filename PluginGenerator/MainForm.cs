using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PluginGenerator
{
    public partial class MainForm : Form
    {
        private static string PATH = "Path: ";
        private static int HEIGHT=20;
        private static int WIDTH=65;
        public Dictionary<string, string> apps { get; set; }

        public MainForm()
        {
            InitializeComponent();
            FormClosed += new FormClosedEventHandler(mainFrame_onClose);
            apps = new Dictionary<string, string>();
            apps.Add("SMS", "");
            apps.Add("Browser", "");

            updateAppsCheckBoxes();
        }

        private void browse_button_Click(object sender, EventArgs e)
        {
            solutionDialog.ShowDialog();
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
                checkBoxPanel.Controls.Add(checkBox);
                row++;
                if (row == 9)
                {
                    row = 0;
                    column++;
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveDllFileDialog.ShowDialog();
        }
        private void mainFrame_onClose(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        
    }
}
