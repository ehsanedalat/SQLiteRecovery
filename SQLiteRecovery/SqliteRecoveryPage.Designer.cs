namespace SQLiteRecovery
{
    partial class SqliteRecoveryPage
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
            this.Back_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabsControl = new System.Windows.Forms.TabControl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Back_button
            // 
            this.Back_button.Location = new System.Drawing.Point(12, 472);
            this.Back_button.Name = "Back_button";
            this.Back_button.Size = new System.Drawing.Size(75, 23);
            this.Back_button.TabIndex = 0;
            this.Back_button.Text = "Back";
            this.Back_button.UseVisualStyleBackColor = true;
            this.Back_button.Click += new System.EventHandler(this.back_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabsControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(588, 454);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recoverd Apps data:";
            // 
            // tabsControl
            // 
            this.tabsControl.Location = new System.Drawing.Point(6, 19);
            this.tabsControl.Name = "tabsControl";
            this.tabsControl.SelectedIndex = 0;
            this.tabsControl.Size = new System.Drawing.Size(576, 429);
            this.tabsControl.TabIndex = 0;
            // 
            // SqliteRecoveryPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 507);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Back_button);
            this.Name = "SqliteRecoveryPage";
            this.Text = "SqliteRecoveryPage";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Back_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl tabsControl;
    }
}