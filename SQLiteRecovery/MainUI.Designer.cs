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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.generatePluginButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OSTabsControl
            // 
            this.OSTabsControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.OSTabsControl.Location = new System.Drawing.Point(6, 19);
            this.OSTabsControl.Name = "OSTabsControl";
            this.OSTabsControl.SelectedIndex = 0;
            this.OSTabsControl.Size = new System.Drawing.Size(591, 439);
            this.OSTabsControl.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OSTabsControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 464);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Suppoted Oprating Systems:";
            // 
            // generatePluginButton
            // 
            this.generatePluginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.generatePluginButton.Location = new System.Drawing.Point(69, 485);
            this.generatePluginButton.Name = "generatePluginButton";
            this.generatePluginButton.Size = new System.Drawing.Size(166, 27);
            this.generatePluginButton.TabIndex = 3;
            this.generatePluginButton.Text = "Generate New Plugin";
            this.generatePluginButton.UseVisualStyleBackColor = true;
            this.generatePluginButton.Click += new System.EventHandler(this.generatePluginButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(353, 487);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(197, 23);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = "Delete Current Plugin";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 524);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.generatePluginButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainUI";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl OSTabsControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button generatePluginButton;
        private System.Windows.Forms.Button deleteButton;

    }
}

