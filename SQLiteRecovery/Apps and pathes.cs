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
    public partial class Apps_and_pathes : Form
    {
        private MainFormPluginGenerator main;
        private int MaxLength=7;
        public Dictionary<string, string> apps { get; set; }

        public Apps_and_pathes(Dictionary<string,string> apps, MainFormPluginGenerator main)
        {
            this.main = main;
            this.apps = apps;

            FormClosed += new FormClosedEventHandler(mainFrame_onClose);
            InitializeComponent();

            updateCheckBoxesInfo();
        }

        private void updateCheckBoxesInfo()
        {
            int row = 0;
            int column = 0;
            //this.flowLayoutPanel1.Controls.Clear();
            foreach (KeyValuePair<string, string> item in apps)
            {
                NewAppPannel(item.Key,item.Value);
                //row++;
            }
        }

        private void NewAppPannel(string name,string path)
        {
            Panel panel1 = new Panel();
            Label label1 = new Label();
            TextBox textBox1 = new TextBox();
            Button remove_button = new Button();
            
            // 
            // label1
            // 
            //label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 13);
            label1.Name = "label1";
            string textInLabel=name;
            if (name.Length > MaxLength)
            {
                textInLabel = name.Substring(0, MaxLength);
                textInLabel = textInLabel + "..";
                Utils.ToolTip(name, label1);
            }
            label1.Text = textInLabel;
            label1.Size = new System.Drawing.Size(60, 13);
            label1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(3+label1.Width+5, 10);
            textBox1.Name = name;
            textBox1.Size = new System.Drawing.Size(400, 20);
            textBox1.TabIndex = 1;
            textBox1.Text = path;
            // 
            // remove_button
            // 
            remove_button.Location = new System.Drawing.Point(3+label1.Width+5+textBox1.Width+10, 10);
            remove_button.Name = "remove_button";
            remove_button.Size = new System.Drawing.Size(20, 20);
            remove_button.TabIndex = 0;
            remove_button.Text = "-";
            remove_button.UseVisualStyleBackColor = true;
            remove_button.Click += new EventHandler((sender, e) => remove_button_Click(sender, e, name));
            // 
            // panel
            // 
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(remove_button);
            panel1.Location = new System.Drawing.Point(3, 3);
            panel1.Name = "panel_" + name;
            panel1.Size = new System.Drawing.Size(label1.Width + textBox1.Width + remove_button.Width + 50, 50);
            panel1.TabIndex = 0;
            this.flowLayoutPanel1.Controls.Add(panel1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Panel[] panelsArray = this.flowLayoutPanel1.Controls.OfType<Panel>().ToArray();
            List<TextBox> textBoxes = new List<TextBox>();
            foreach (Panel p in panelsArray)
            {
                textBoxes.Add(p.Controls.OfType<TextBox>().ToArray()[0]);
            }
            ErrorProvider error = new ErrorProvider();
            bool isErrorExist = false;
            foreach (TextBox box in textBoxes)
            {
                string value = box.Text;
                apps[box.Name] = value;
                if (string.IsNullOrEmpty(value))
                {
                    error.SetError(box,"Empty Box !!");
                    isErrorExist = true;
                }
            }
            if (!isErrorExist)
            {
                this.Hide();
                main.updateAppsCheckBoxes();
                main.Show();
            }
            
        }

        private void remove_button_Click(object sender, EventArgs e, string name)
        {
            apps.Remove(name);
            this.flowLayoutPanel1.Controls.RemoveByKey("panel_"+name);
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            NewApp newApp = new NewApp(this);
            newApp.ShowDialog();
            if (newApp.AppName != null)
            {
                apps.Add(newApp.AppName,newApp.AppPath);
                NewAppPannel(newApp.AppName, newApp.AppPath);
            }
        }
        private void mainFrame_onClose(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            //main.updateAppsCheckBoxes();
            main.Show();
        }
    }
}
