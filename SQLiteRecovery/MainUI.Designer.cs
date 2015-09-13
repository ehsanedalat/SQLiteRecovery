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
            this.directTabPage = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.recoverButton = new System.Windows.Forms.Button();
            this.journalPathButton = new System.Windows.Forms.Button();
            this.journalFileTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DBPathButton = new System.Windows.Forms.Button();
            this.DBFileTextBox = new System.Windows.Forms.TextBox();
            this.DBLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.generatePluginButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.editButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rootLabel = new System.Windows.Forms.Label();
            this.rootButton = new System.Windows.Forms.Button();
            this.DBOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.journalOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OSTabsControl.SuspendLayout();
            this.directTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // OSTabsControl
            // 
            this.OSTabsControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.OSTabsControl.Controls.Add(this.directTabPage);
            this.OSTabsControl.Location = new System.Drawing.Point(6, 19);
            this.OSTabsControl.Name = "OSTabsControl";
            this.OSTabsControl.SelectedIndex = 0;
            this.OSTabsControl.Size = new System.Drawing.Size(591, 439);
            this.OSTabsControl.TabIndex = 0;
            // 
            // directTabPage
            // 
            this.directTabPage.Controls.Add(this.label3);
            this.directTabPage.Controls.Add(this.recoverButton);
            this.directTabPage.Controls.Add(this.journalPathButton);
            this.directTabPage.Controls.Add(this.journalFileTextBox);
            this.directTabPage.Controls.Add(this.label2);
            this.directTabPage.Controls.Add(this.DBPathButton);
            this.directTabPage.Controls.Add(this.DBFileTextBox);
            this.directTabPage.Controls.Add(this.DBLabel);
            this.directTabPage.Location = new System.Drawing.Point(4, 22);
            this.directTabPage.Name = "directTabPage";
            this.directTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.directTabPage.Size = new System.Drawing.Size(583, 413);
            this.directTabPage.TabIndex = 0;
            this.directTabPage.Text = "Add DB Directly";
            this.directTabPage.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "(Optional)";
            // 
            // recoverButton
            // 
            this.recoverButton.Location = new System.Drawing.Point(189, 267);
            this.recoverButton.Name = "recoverButton";
            this.recoverButton.Size = new System.Drawing.Size(194, 23);
            this.recoverButton.TabIndex = 6;
            this.recoverButton.Text = "Recover Information";
            this.recoverButton.UseVisualStyleBackColor = true;
            this.recoverButton.Click += new System.EventHandler(this.recoverButton_Click);
            // 
            // journalPathButton
            // 
            this.journalPathButton.Location = new System.Drawing.Point(466, 191);
            this.journalPathButton.Name = "journalPathButton";
            this.journalPathButton.Size = new System.Drawing.Size(75, 23);
            this.journalPathButton.TabIndex = 5;
            this.journalPathButton.Text = "Browse";
            this.journalPathButton.UseVisualStyleBackColor = true;
            this.journalPathButton.Click += new System.EventHandler(this.journalPathButton_Click);
            // 
            // journalFileTextBox
            // 
            this.journalFileTextBox.Location = new System.Drawing.Point(136, 193);
            this.journalFileTextBox.Name = "journalFileTextBox";
            this.journalFileTextBox.Size = new System.Drawing.Size(313, 20);
            this.journalFileTextBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 196);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "DB Journal File Path: ";
            // 
            // DBPathButton
            // 
            this.DBPathButton.Location = new System.Drawing.Point(466, 144);
            this.DBPathButton.Name = "DBPathButton";
            this.DBPathButton.Size = new System.Drawing.Size(75, 23);
            this.DBPathButton.TabIndex = 2;
            this.DBPathButton.Text = "Browse";
            this.DBPathButton.UseVisualStyleBackColor = true;
            this.DBPathButton.Click += new System.EventHandler(this.DBPathButton_Click);
            // 
            // DBFileTextBox
            // 
            this.DBFileTextBox.Location = new System.Drawing.Point(136, 146);
            this.DBFileTextBox.Name = "DBFileTextBox";
            this.DBFileTextBox.Size = new System.Drawing.Size(313, 20);
            this.DBFileTextBox.TabIndex = 1;
            this.DBFileTextBox.TextChanged += new System.EventHandler(this.DBFileTextBox_TextChanged);
            // 
            // DBLabel
            // 
            this.DBLabel.AutoSize = true;
            this.DBLabel.Location = new System.Drawing.Point(27, 149);
            this.DBLabel.Name = "DBLabel";
            this.DBLabel.Size = new System.Drawing.Size(72, 13);
            this.DBLabel.TabIndex = 0;
            this.DBLabel.Text = "DB File Path: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.OSTabsControl);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(603, 464);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Plugins";
            // 
            // generatePluginButton
            // 
            this.generatePluginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.generatePluginButton.Location = new System.Drawing.Point(18, 485);
            this.generatePluginButton.Name = "generatePluginButton";
            this.generatePluginButton.Size = new System.Drawing.Size(178, 27);
            this.generatePluginButton.TabIndex = 3;
            this.generatePluginButton.Text = "New Plugin";
            this.generatePluginButton.UseVisualStyleBackColor = true;
            this.generatePluginButton.Click += new System.EventHandler(this.generatePluginButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(402, 487);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(207, 23);
            this.deleteButton.TabIndex = 4;
            this.deleteButton.Text = "Delete Current Plugin";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(202, 487);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(194, 23);
            this.editButton.TabIndex = 5;
            this.editButton.Text = "Edit Plugin";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // rootLabel
            // 
            this.rootLabel.Location = new System.Drawing.Point(0, 0);
            this.rootLabel.Name = "rootLabel";
            this.rootLabel.Size = new System.Drawing.Size(100, 23);
            this.rootLabel.TabIndex = 0;
            // 
            // rootButton
            // 
            this.rootButton.Location = new System.Drawing.Point(0, 0);
            this.rootButton.Name = "rootButton";
            this.rootButton.Size = new System.Drawing.Size(75, 23);
            this.rootButton.TabIndex = 0;
            // 
            // journalOpenFileDialog
            // 
            this.journalOpenFileDialog.FileName = "openFileDialog2";
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 524);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.generatePluginButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQLite Recovery";
            this.OSTabsControl.ResumeLayout(false);
            this.directTabPage.ResumeLayout(false);
            this.directTabPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl OSTabsControl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button generatePluginButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label rootLabel;
        private System.Windows.Forms.Button rootButton;
        private System.Windows.Forms.TabPage directTabPage;
        private System.Windows.Forms.Label DBLabel;
        private System.Windows.Forms.Button journalPathButton;
        private System.Windows.Forms.TextBox journalFileTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DBPathButton;
        private System.Windows.Forms.TextBox DBFileTextBox;
        private System.Windows.Forms.Button recoverButton;
        private System.Windows.Forms.OpenFileDialog DBOpenFileDialog;
        private System.Windows.Forms.OpenFileDialog journalOpenFileDialog;
        private System.Windows.Forms.Label label3;

    }
}

