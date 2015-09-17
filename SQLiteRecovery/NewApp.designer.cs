namespace PluginGenerator
{
    partial class NewApp
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
            this.NewAppDialog = new System.Windows.Forms.GroupBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.biuld_new_app_button = new System.Windows.Forms.Button();
            this.app_path_textBox = new System.Windows.Forms.TextBox();
            this.app_name_textBox = new System.Windows.Forms.TextBox();
            this.app_path_label = new System.Windows.Forms.Label();
            this.name_label = new System.Windows.Forms.Label();
            this.NewAppDialog.SuspendLayout();
            this.SuspendLayout();
            // 
            // NewAppDialog
            // 
            this.NewAppDialog.Controls.Add(this.cancel_button);
            this.NewAppDialog.Controls.Add(this.biuld_new_app_button);
            this.NewAppDialog.Controls.Add(this.app_path_textBox);
            this.NewAppDialog.Controls.Add(this.app_name_textBox);
            this.NewAppDialog.Controls.Add(this.app_path_label);
            this.NewAppDialog.Controls.Add(this.name_label);
            this.NewAppDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewAppDialog.Location = new System.Drawing.Point(0, 0);
            this.NewAppDialog.Name = "NewAppDialog";
            this.NewAppDialog.Size = new System.Drawing.Size(312, 161);
            this.NewAppDialog.TabIndex = 0;
            this.NewAppDialog.TabStop = false;
            this.NewAppDialog.Text = "New App";
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(49, 129);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 5;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // biuld_new_app_button
            // 
            this.biuld_new_app_button.Location = new System.Drawing.Point(162, 129);
            this.biuld_new_app_button.Name = "biuld_new_app_button";
            this.biuld_new_app_button.Size = new System.Drawing.Size(91, 23);
            this.biuld_new_app_button.TabIndex = 4;
            this.biuld_new_app_button.Text = "Biuld new App";
            this.biuld_new_app_button.UseVisualStyleBackColor = true;
            this.biuld_new_app_button.Click += new System.EventHandler(this.biuld_new_app_button_Click);
            // 
            // app_path_textBox
            // 
            this.app_path_textBox.Location = new System.Drawing.Point(78, 83);
            this.app_path_textBox.Name = "app_path_textBox";
            this.app_path_textBox.Size = new System.Drawing.Size(200, 20);
            this.app_path_textBox.TabIndex = 3;
            // 
            // app_name_textBox
            // 
            this.app_name_textBox.Location = new System.Drawing.Point(78, 31);
            this.app_name_textBox.Name = "app_name_textBox";
            this.app_name_textBox.Size = new System.Drawing.Size(200, 20);
            this.app_name_textBox.TabIndex = 2;
            // 
            // app_path_label
            // 
            this.app_path_label.AutoSize = true;
            this.app_path_label.Location = new System.Drawing.Point(12, 86);
            this.app_path_label.Name = "app_path_label";
            this.app_path_label.Size = new System.Drawing.Size(54, 13);
            this.app_path_label.TabIndex = 1;
            this.app_path_label.Text = "App Path:";
            // 
            // name_label
            // 
            this.name_label.AutoSize = true;
            this.name_label.Location = new System.Drawing.Point(12, 34);
            this.name_label.Name = "name_label";
            this.name_label.Size = new System.Drawing.Size(60, 13);
            this.name_label.TabIndex = 0;
            this.name_label.Text = "App Name:";
            // 
            // NewApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 161);
            this.Controls.Add(this.NewAppDialog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "NewApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "NewApp";
            this.NewAppDialog.ResumeLayout(false);
            this.NewAppDialog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox NewAppDialog;
        private System.Windows.Forms.Label name_label;
        private System.Windows.Forms.Label app_path_label;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button biuld_new_app_button;
        private System.Windows.Forms.TextBox app_path_textBox;
        private System.Windows.Forms.TextBox app_name_textBox;
    }
}