using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace SQLiteRecovery
{
    public partial class CopyFilesProgress : Form
    {
        private Dictionary<string, string> apps;
        private object plugin;
        private string distination;
        private MainUI parent;
        private Dictionary<string, ArrayList> appsInfo;
        private bool last;
        private string currentFile;

        public CopyFilesProgress(MainUI parent, Dictionary<string,string> apps,object plugin,string distination, Dictionary<string, ArrayList> appsInfo)
        {
            InitializeComponent();
            //status.Text = "0 file(s) copied of " + apps.Count;

            this.parent = parent;
            this.apps = apps;
            this.plugin = plugin;
            this.distination = distination;
            this.appsInfo = appsInfo;
            last = false;

            backgroundWorker1.RunWorkerAsync();

        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int index = 0;
            foreach (string key in apps.Keys)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                currentFile = key;
                PluginServices.copyAppDataBaseFromDevice(plugin, key, apps[key], distination);
                index++;
                if (index == apps.Count)
                    last = true;
                backgroundWorker1.ReportProgress((index * 100) / apps.Count);
            }
            
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            status.Text = e.ProgressPercentage.ToString()+"%";
            copy.Text = currentFile+" copied.";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.Hide();
                parent.Show();
                backgroundWorker1.Dispose();
            }
            if (last)
            {
                this.Hide();
                SqliteRecoveryPage recoveryPage = null;
                try
                {
                    recoveryPage = new SqliteRecoveryPage(parent, appsInfo);
                    recoveryPage.Show();
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show("There is no siutable file in given path!!!( " + ex.FileName + " )", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CopyFilesProgress_FormClosed(object sender, FormClosedEventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
        
    }
}
