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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.OSTabsControl.SuspendLayout();
            this.Android.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OSTabsControl
            // 
            this.OSTabsControl.Controls.Add(this.Android);
            this.OSTabsControl.Location = new System.Drawing.Point(6, 19);
            this.OSTabsControl.Name = "OSTabsControl";
            this.OSTabsControl.SelectedIndex = 0;
            this.OSTabsControl.Size = new System.Drawing.Size(591, 457);
            this.OSTabsControl.TabIndex = 0;
            // 
            // Android
            // 
            this.Android.Controls.Add(this.button1);
            this.Android.Location = new System.Drawing.Point(4, 22);
            this.Android.Name = "Android";
            this.Android.Padding = new System.Windows.Forms.Padding(3);
            this.Android.Size = new System.Drawing.Size(583, 431);
            this.Android.TabIndex = 0;
            this.Android.Text = "Android";
            this.Android.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OSTabsControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 482);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Suppoted Oprating Systems:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(201, 396);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 29);
            this.button1.TabIndex = 0;
            this.button1.Text = "Recover selected Apps data";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 500);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainUI";
            this.Text = "Form1";
            this.OSTabsControl.ResumeLayout(false);
            this.Android.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl OSTabsControl;
        private System.Windows.Forms.TabPage Android;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;

    }
}

