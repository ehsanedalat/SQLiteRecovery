namespace PluginGenerator
{
    partial class Apps_and_pathes
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
            this.pathes = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.add_button = new System.Windows.Forms.Button();
            this.pathes.SuspendLayout();
            this.SuspendLayout();
            // 
            // pathes
            // 
            this.pathes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathes.AutoSize = true;
            this.pathes.Controls.Add(this.flowLayoutPanel1);
            this.pathes.Location = new System.Drawing.Point(12, 12);
            this.pathes.Name = "pathes";
            this.pathes.Size = new System.Drawing.Size(557, 329);
            this.pathes.TabIndex = 0;
            this.pathes.TabStop = false;
            this.pathes.Text = "Pathes";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(551, 310);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(396, 347);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save changes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // add_button
            // 
            this.add_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.add_button.Location = new System.Drawing.Point(62, 347);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(95, 23);
            this.add_button.TabIndex = 2;
            this.add_button.Text = "Add new App";
            this.add_button.UseVisualStyleBackColor = true;
            this.add_button.Click += new System.EventHandler(this.add_button_Click);
            // 
            // Apps_and_pathes
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 382);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pathes);
            this.Name = "Apps_and_pathes";
            this.Text = "Apps and pathes";
            this.pathes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox pathes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button add_button;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}