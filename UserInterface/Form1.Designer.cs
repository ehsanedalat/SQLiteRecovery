namespace UserInterface
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
            this.address_text_box = new System.Windows.Forms.TextBox();
            this.Browse = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.show_headers_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Info = new System.Windows.Forms.Label();
            this.Hex = new System.Windows.Forms.RadioButton();
            this.Binary = new System.Windows.Forms.RadioButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Decimal = new System.Windows.Forms.RadioButton();
            this.naxt = new System.Windows.Forms.Button();
            this.last = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox1 = new UserInterface.MyRichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please enter file address: ";
            // 
            // address_text_box
            // 
            this.address_text_box.Location = new System.Drawing.Point(167, 14);
            this.address_text_box.Name = "address_text_box";
            this.address_text_box.Size = new System.Drawing.Size(363, 20);
            this.address_text_box.TabIndex = 2;
            this.address_text_box.TextChanged += new System.EventHandler(this.address_text_box_TextChanged);
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(536, 11);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(75, 23);
            this.Browse.TabIndex = 3;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // show_headers_button
            // 
            this.show_headers_button.Location = new System.Drawing.Point(421, 43);
            this.show_headers_button.Name = "show_headers_button";
            this.show_headers_button.Size = new System.Drawing.Size(89, 23);
            this.show_headers_button.TabIndex = 4;
            this.show_headers_button.Text = "Show Headers";
            this.show_headers_button.UseVisualStyleBackColor = true;
            this.show_headers_button.Click += new System.EventHandler(this.show_headers_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Header Info: ";
            // 
            // Info
            // 
            this.Info.AutoSize = true;
            this.Info.Location = new System.Drawing.Point(108, 85);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(0, 13);
            this.Info.TabIndex = 6;
            // 
            // Hex
            // 
            this.Hex.AutoSize = true;
            this.Hex.Location = new System.Drawing.Point(244, 43);
            this.Hex.Name = "Hex";
            this.Hex.Size = new System.Drawing.Size(44, 17);
            this.Hex.TabIndex = 8;
            this.Hex.TabStop = true;
            this.Hex.Text = "Hex";
            this.Hex.UseVisualStyleBackColor = true;
            this.Hex.CheckedChanged += new System.EventHandler(this.Hex_CheckedChanged);
            // 
            // Binary
            // 
            this.Binary.AutoSize = true;
            this.Binary.Location = new System.Drawing.Point(318, 43);
            this.Binary.Name = "Binary";
            this.Binary.Size = new System.Drawing.Size(54, 17);
            this.Binary.TabIndex = 9;
            this.Binary.TabStop = true;
            this.Binary.Text = "Binary";
            this.Binary.UseVisualStyleBackColor = true;
            this.Binary.CheckedChanged += new System.EventHandler(this.Binary_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 5000;
            this.toolTip1.ReshowDelay = 1000;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.ToolTipTitle = "Header Information";
            // 
            // Decimal
            // 
            this.Decimal.AutoSize = true;
            this.Decimal.Location = new System.Drawing.Point(389, 43);
            this.Decimal.Name = "Decimal";
            this.Decimal.Size = new System.Drawing.Size(63, 17);
            this.Decimal.TabIndex = 10;
            this.Decimal.TabStop = true;
            this.Decimal.Text = "Decimal";
            this.Decimal.UseVisualStyleBackColor = true;
            this.Decimal.CheckedChanged += new System.EventHandler(this.Decimal_CheckedChanged);
            // 
            // naxt
            // 
            this.naxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.naxt.Location = new System.Drawing.Point(318, 457);
            this.naxt.Name = "naxt";
            this.naxt.Size = new System.Drawing.Size(75, 23);
            this.naxt.TabIndex = 11;
            this.naxt.Text = "Next";
            this.naxt.UseVisualStyleBackColor = true;
            this.naxt.Visible = false;
            //this.naxt.Click += new System.EventHandler(this.naxt_Click);
            // 
            // last
            // 
            this.last.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.last.Location = new System.Drawing.Point(213, 457);
            this.last.Name = "last";
            this.last.Size = new System.Drawing.Size(75, 23);
            this.last.TabIndex = 12;
            this.last.Text = "Pervious";
            this.last.UseVisualStyleBackColor = true;
            this.last.Visible = false;
            //this.last.Click += new System.EventHandler(this.last_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "label4";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.richTextBox1.first = false;
            this.richTextBox1.Location = new System.Drawing.Point(36, 129);
            this.richTextBox1.maxPageSize = ((long)(0));
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(575, 308);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 492);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.last);
            this.Controls.Add(this.naxt);
            this.Controls.Add(this.Decimal);
            this.Controls.Add(this.Binary);
            this.Controls.Add(this.Hex);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.show_headers_button);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.address_text_box);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Form1";
            this.Text = "Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox address_text_box;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button show_headers_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Info;
        //private System.Windows.Forms.RichTextBox richTextBox1;
        private MyRichTextBox richTextBox1;
        private System.Windows.Forms.RadioButton Hex;
        private System.Windows.Forms.RadioButton Binary;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton Decimal;
        private System.Windows.Forms.Button naxt;
        private System.Windows.Forms.Button last;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

