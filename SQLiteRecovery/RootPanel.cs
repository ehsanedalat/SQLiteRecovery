using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SQLiteRecovery
{
    public partial class RootPanel : Form
    {
        private object plugin;
        private readonly string kingoRootApk = "../Tools/KingoRoot.apk";
        private readonly string kingoRootExe = "../Tools/KingoRoot.exe";

        public RootPanel(object plugin)
        {
            InitializeComponent();

            this.plugin = plugin;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PluginServices.installApp(plugin,Path.GetFullPath(kingoRootApk)))
                ((Button)sender).Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetFullPath(kingoRootExe));
        }
    }
}
