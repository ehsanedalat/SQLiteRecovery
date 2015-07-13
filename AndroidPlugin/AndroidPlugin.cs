using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevicePluginInterface;
using System.Windows.Forms;

namespace AndroidPlugin
{
    public class AndroidPlugin : DeviceRecoveryPluginInterface
    {
        #region overide interface methodes

        public void root()
        {
            throw new NotImplementedException();
        }

        public void copyFilesFromDevice(string storingPathInComputer, Dictionary<string, string> appsPathInDevice)
        {
            throw new NotImplementedException();
        }

        public void setOsTabControls()
        {
            Button recoverButton = new Button();
            recoverButton.Location = new System.Drawing.Point(201, 396);
            recoverButton.Name = "button1";
            recoverButton.Size = new System.Drawing.Size(181, 29);
            recoverButton.TabIndex = 0;
            recoverButton.Text = "Recover selected Apps data";
            recoverButton.UseVisualStyleBackColor = true;

            TabPage Android = new TabPage();
            Android.Controls.Add(recoverButton);
            Android.Location = new System.Drawing.Point(4, 22);
            Android.Name = "Android";
            Android.Padding = new System.Windows.Forms.Padding(3);
            Android.Size = new System.Drawing.Size(583, 431);
            Android.TabIndex = 0;
            Android.Text = "Android";
            Android.UseVisualStyleBackColor = true;
        }
        #endregion

        

        public string recoverButtonName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public TabPage osTabPage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string pluginName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
