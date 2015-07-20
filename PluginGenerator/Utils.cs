using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PluginGenerator
{
    class Utils
    {
        public static void ToolTip(string text, Control control)
        {
            ToolTip toolTip1 = new ToolTip();
            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(control, text);
        }
    }
}
