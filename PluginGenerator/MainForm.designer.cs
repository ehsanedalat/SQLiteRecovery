namespace PluginGenerator
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.edit_button = new System.Windows.Forms.Button();
            this.checkBoxPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.osComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pluginNameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.browse_button = new System.Windows.Forms.Button();
            this.solutionDialog = new System.Windows.Forms.OpenFileDialog();
            this.build_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.edit_button);
            this.groupBox1.Controls.Add(this.checkBoxPanel);
            this.groupBox1.Location = new System.Drawing.Point(12, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(551, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Specify your desigerd Apps";
            // 
            // edit_button
            // 
            this.edit_button.Location = new System.Drawing.Point(449, 77);
            this.edit_button.Name = "edit_button";
            this.edit_button.Size = new System.Drawing.Size(83, 23);
            this.edit_button.TabIndex = 8;
            this.edit_button.Text = "Edit Apps";
            this.edit_button.UseVisualStyleBackColor = true;
            this.edit_button.Click += new System.EventHandler(this.edit_button_Click);
            // 
            // checkBoxPanel
            // 
            this.checkBoxPanel.AutoScroll = true;
            this.checkBoxPanel.Location = new System.Drawing.Point(6, 19);
            this.checkBoxPanel.Name = "checkBoxPanel";
            this.checkBoxPanel.Size = new System.Drawing.Size(422, 142);
            this.checkBoxPanel.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.osComboBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.pluginNameTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(551, 87);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General information";
            // 
            // osComboBox
            // 
            this.osComboBox.FormattingEnabled = true;
            this.osComboBox.Items.AddRange(new object[] {
            "Android",
            "iOS",
            "Windows"});
            this.osComboBox.Location = new System.Drawing.Point(90, 55);
            this.osComboBox.Name = "osComboBox";
            this.osComboBox.Size = new System.Drawing.Size(178, 21);
            this.osComboBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Supported OS:";
            // 
            // pluginNameTextBox
            // 
            this.pluginNameTextBox.Location = new System.Drawing.Point(89, 23);
            this.pluginNameTextBox.Name = "pluginNameTextBox";
            this.pluginNameTextBox.Size = new System.Drawing.Size(179, 20);
            this.pluginNameTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Plugin name:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.addressTextBox);
            this.groupBox3.Controls.Add(this.browse_button);
            this.groupBox3.Location = new System.Drawing.Point(12, 278);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(551, 60);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Browes to target dll file whitch is impelemented plugin interface";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(10, 22);
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.Size = new System.Drawing.Size(245, 20);
            this.addressTextBox.TabIndex = 1;
            // 
            // browse_button
            // 
            this.browse_button.Location = new System.Drawing.Point(261, 19);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(75, 23);
            this.browse_button.TabIndex = 0;
            this.browse_button.Text = "Browse";
            this.browse_button.UseVisualStyleBackColor = true;
            this.browse_button.Click += new System.EventHandler(this.browse_button_Click);
            // 
            // solutionDialog
            // 
            this.solutionDialog.DefaultExt = "dll";
            this.solutionDialog.Filter = "Assembly files|*.dll";
            this.solutionDialog.InitialDirectory = "F:\\C# Projects\\AndroidPlugin\\AndroidPlugin\\bin\\Debug\\";
            this.solutionDialog.Title = "Dll file path ...";
            // 
            // build_button
            // 
            this.build_button.Location = new System.Drawing.Point(205, 361);
            this.build_button.Name = "build_button";
            this.build_button.Size = new System.Drawing.Size(159, 23);
            this.build_button.TabIndex = 4;
            this.build_button.Text = "Build plugin";
            this.build_button.UseVisualStyleBackColor = true;
            this.build_button.Click += new System.EventHandler(this.build_button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 395);
            this.Controls.Add(this.build_button);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel checkBoxPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox pluginNameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox osComboBox;
        private System.Windows.Forms.Button edit_button;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Button browse_button;
        private System.Windows.Forms.OpenFileDialog solutionDialog;
        private System.Windows.Forms.Button build_button;
    }
}