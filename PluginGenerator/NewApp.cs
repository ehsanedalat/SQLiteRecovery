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
    public partial class NewApp : Form
    {
        public string AppName { get; set; }
        public string AppPath { get; set; }
        private Form main;

        public NewApp(Form main)
        {
            this.main = main;
            InitializeComponent();
        }

        private void biuld_new_app_button_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(app_path_textBox.Text) && !string.IsNullOrEmpty(app_name_textBox.Text))
            {
                AppName = app_name_textBox.Text;
                AppPath = app_path_textBox.Text;
                this.Hide();
                main.Show();
            }
            else
            {
                ErrorProvider error = new ErrorProvider();
                if (string.IsNullOrEmpty(app_path_textBox.Text))
                    error.SetError(app_path_textBox, "This text box should have a value!!!");
                if (string.IsNullOrEmpty(app_name_textBox.Text))
                    error.SetError(app_name_textBox,"This text box should have a value!!!");
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            AppName = null;
            AppPath = null;
            this.Hide();
            main.Show();
        }
    }
}
