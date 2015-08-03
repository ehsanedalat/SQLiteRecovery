using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PluginGenerator
{
    static class MainFormPG
    {

    
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFormPluginGenerator(null));
        }
    }
}
