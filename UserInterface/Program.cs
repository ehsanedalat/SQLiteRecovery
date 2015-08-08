using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UserInterface
{
    static class Program
    {
        public static Form1 Form1
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

}
