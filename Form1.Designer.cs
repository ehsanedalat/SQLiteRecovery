namespace UserInterFace
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.XMLAdressBox = new System.Windows.Forms.TextBox();
            this.browse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.headerBox = new System.Windows.Forms.TextBox();
            this.browse2 = new System.Windows.Forms.Button();
            this.GP = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter XML file format Adress:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "|*.xml";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // XMLAdressBox
            // 
            this.XMLAdressBox.Location = new System.Drawing.Point(312, 15);
            this.XMLAdressBox.Name = "XMLAdressBox";
            this.XMLAdressBox.Size = new System.Drawing.Size(230, 20);
            this.XMLAdressBox.TabIndex = 1;
            this.XMLAdressBox.TextChanged += new System.EventHandler(this.XMLAdressBox_TextChanged);
            // 
            // browse
            // 
            this.browse.Location = new System.Drawing.Point(559, 13);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(75, 23);
            this.browse.TabIndex = 2;
            this.browse.Text = "Browse";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please enter Location adress for saving Read header file:";
            // 
            // headerBox
            // 
            this.headerBox.Location = new System.Drawing.Point(312, 65);
            this.headerBox.Name = "headerBox";
            this.headerBox.Size = new System.Drawing.Size(230, 20);
            this.headerBox.TabIndex = 4;
            // 
            // browse2
            // 
            this.browse2.Location = new System.Drawing.Point(559, 63);
            this.browse2.Name = "browse2";
            this.browse2.Size = new System.Drawing.Size(75, 23);
            this.browse2.TabIndex = 5;
            this.browse2.Text = "Browse";
            this.browse2.UseVisualStyleBackColor = true;
            this.browse2.Click += new System.EventHandler(this.browse2_Click);
            // 
            // GP
            // 
            this.GP.Location = new System.Drawing.Point(276, 124);
            this.GP.Name = "GP";
            this.GP.Size = new System.Drawing.Size(110, 23);
            this.GP.TabIndex = 6;
            this.GP.Text = "Generate Parser";
            this.GP.UseVisualStyleBackColor = true;
            this.GP.Click += new System.EventHandler(this.GP_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            // 
            // toolTip2
            // 
            this.toolTip2.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 159);
            this.Controls.Add(this.GP);
            this.Controls.Add(this.browse2);
            this.Controls.Add(this.headerBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.XMLAdressBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Barname Kheyli Khafan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox XMLAdressBox;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox headerBox;
        private System.Windows.Forms.Button browse2;
        private System.Windows.Forms.Button GP;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolTip toolTip2;
    }
}

