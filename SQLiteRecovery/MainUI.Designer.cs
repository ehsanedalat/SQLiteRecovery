namespace SQLiteRecovery
{
    partial class MainUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OSTabsControl = new System.Windows.Forms.TabControl();
            this.Android = new System.Windows.Forms.TabPage();
            this.iOS = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectedDevicesTabControl = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.OSTabsControl.SuspendLayout();
            this.Android.SuspendLayout();
            this.ConnectedDevicesTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // OSTabsControl
            // 
            this.OSTabsControl.Controls.Add(this.Android);
            this.OSTabsControl.Controls.Add(this.iOS);
            this.OSTabsControl.Location = new System.Drawing.Point(12, 39);
            this.OSTabsControl.Name = "OSTabsControl";
            this.OSTabsControl.SelectedIndex = 0;
            this.OSTabsControl.Size = new System.Drawing.Size(599, 457);
            this.OSTabsControl.TabIndex = 0;
            // 
            // Android
            // 
            this.Android.Controls.Add(this.label2);
            this.Android.Controls.Add(this.ConnectedDevicesTabControl);
            this.Android.Location = new System.Drawing.Point(4, 22);
            this.Android.Name = "Android";
            this.Android.Padding = new System.Windows.Forms.Padding(3);
            this.Android.Size = new System.Drawing.Size(591, 431);
            this.Android.TabIndex = 0;
            this.Android.Text = "Android";
            this.Android.UseVisualStyleBackColor = true;
            // 
            // iOS
            // 
            this.iOS.Location = new System.Drawing.Point(4, 22);
            this.iOS.Name = "iOS";
            this.iOS.Padding = new System.Windows.Forms.Padding(3);
            this.iOS.Size = new System.Drawing.Size(591, 431);
            this.iOS.TabIndex = 1;
            this.iOS.Text = "iOS";
            this.iOS.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Suppoted Oprating Systems:";
            // 
            // ConnectedDevicesTabControl
            // 
            this.ConnectedDevicesTabControl.Controls.Add(this.tabPage2);
            this.ConnectedDevicesTabControl.Location = new System.Drawing.Point(7, 42);
            this.ConnectedDevicesTabControl.Name = "ConnectedDevicesTabControl";
            this.ConnectedDevicesTabControl.SelectedIndex = 0;
            this.ConnectedDevicesTabControl.Size = new System.Drawing.Size(578, 383);
            this.ConnectedDevicesTabControl.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(570, 357);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Connected Devices:";
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 508);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OSTabsControl);
            this.Name = "MainUI";
            this.Text = "Form1";
            this.OSTabsControl.ResumeLayout(false);
            this.Android.ResumeLayout(false);
            this.Android.PerformLayout();
            this.ConnectedDevicesTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl OSTabsControl;
        private System.Windows.Forms.TabPage Android;
        private System.Windows.Forms.TabPage iOS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl ConnectedDevicesTabControl;
        private System.Windows.Forms.TabPage tabPage2;

    }
}

