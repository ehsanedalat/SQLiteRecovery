using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using APA.XmlParser;

namespace UserInterFace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void browse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                XMLAdressBox.Text = openFileDialog1.FileName;
              //  XMLAdressBox.ReadOnly = true;
            }
        }

        private void XMLAdressBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void browse2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            headerBox.Text = fbd.SelectedPath;
            //headerBox.ReadOnly = true;
        }

        private void GP_Click(object sender, EventArgs e)
        {
            ParserGenerator generator = new ParserGenerator(headerBox.Text+"\\parser.cs");
            try
            {
                generator.generateParser(XMLAdressBox.Text);
                this.Hide();
                String caption = "Result";
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Information;
                DialogResult result = MessageBox.Show("Opreation was successfull !!! Parser.cs file was created at " + headerBox.Text, caption, button, icon);
                if (DialogResult.OK == result)
                {
                    this.Close();
                }
            }
            catch (System.IO.DirectoryNotFoundException e1)
            {
                toolTip2.Show("Opreation faild !!! there is a mistake in location address for read header file.", this);
                this.Hide();
                String caption = "Result";
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                DialogResult result = MessageBox.Show("Opreation faild !!! there is a mistake in location address for read header file.", caption, button, icon);
                if (DialogResult.OK == result)
                {
                    this.Show();
                }
            }
            catch (System.IO.FileNotFoundException e2)
            {
                this.Hide();
                String caption = "Result";
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                DialogResult result = MessageBox.Show("Opreation faild !!! there is a mistake in XML file format address.", caption, button, icon);
                if (DialogResult.OK == result)
                {
                     this.Show();
                }
            }
        }
    }
}
