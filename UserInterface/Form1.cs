using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using HeaderReader;

namespace UserInterface
{
    public partial class Form1 : Form
    {
        private Structure[] headers;
        private GroupBox gb;
        private Dictionary<long, Structure> HeaderHash;
        private Dictionary<int, string> dic;
        private long currentOffset;
        private long pageSize;
        private long fileSize;
        private const long FILESIZE = 1000;
        private const int BINARYCOLSNUM = 5;
        private const int HEXCOLSNUM = 10;
        private const int DECIMALCOLSNUM = 10;
        private int CurrentLineColNum = 0;
        private long CurrentLineNum = 0;
        private bool NewLine;
        private const int CHARNUMSINLINE_BINARY = BINARYCOLSNUM * 9 + 10;
        private const int CHARNUMSINLINE_HEX = HEXCOLSNUM * 3 + 10;
        private const int CHARNUMSINLINE_DECIMAL = DECIMALCOLSNUM * 4 + 10;
        private const int NUMOFLINENUMBERSCHARS = 8;
        private long TextBoxIndex = 0;
        private long PageProduced = 1;

        public Form1()
        {
            InitializeComponent();
            richTextBox1.first = true;
            show_headers_button.Enabled = false;
            dic = new Dictionary<int, string>();
            currentOffset = 0;
            pageSize = FILESIZE;
            gb = new GroupBox();
            //Hex.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            Hex.Location = new System.Drawing.Point(5, 15);
            Hex.Checked = true;
            gb.Controls.Add(Hex);
            //Decimal.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            Decimal.Location = new System.Drawing.Point(155, 15);
            gb.Controls.Add(Decimal);
            Binary.Location = new System.Drawing.Point(80, 15);
            //Binary.CheckedChanged += new EventHandler(radioButton_CheckedChanged);
            gb.Controls.Add(Binary);
            gb.Location = new System.Drawing.Point(200, 30);
            gb.Size = new System.Drawing.Size(220, 50);
            this.Controls.Add(gb);
            richTextBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(richTextBox1_MouseDown);
            richTextBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(richTextBox1_MouseMove);
            richTextBox1.ThresholdReached += richTextBox1_ThresholdReached;
            richTextBox1.MinimalReached += richTextBox1_MinimalReached;
            richTextBox1.Font = new Font("Arial", 10);
            //richTextBox1.MouseHover += new EventHandler(richTextBox1_MouseHover);
            toolTip1.SetToolTip(richTextBox1, " ");
            LableInitializing();
        }

        public MyRichTextBox MyRichTextBox
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        //event handler when scroll bar reached a special position (top of scroll bar)

        private void richTextBox1_MinimalReached(object sender, EventArgs e)
        {
            
            //checks if current number of page greater than 1
            
            if (currentOffset >= 2 * pageSize)
            {
                //normalize current offset if it is not divisable to size of page
                
                if (currentOffset % pageSize != 0)
                {
                    currentOffset = ((currentOffset / pageSize) + 1) * pageSize;
                }

                //this IF checks, if next page doesn't produce, it will produce it. 

                if (currentOffset / pageSize != PageProduced)
                {
                    dic.Clear();
                    TextBoxIndex = 0;
                    Info.Text = "";
                    PageProduced = 1;
                    CurrentLineNum = ((currentOffset / pageSize) - 2) * pageSize;
                    richTextBox1.Text = "";
                    
                    // test for radio buttons and produce reachTextbox content

                    if (Binary.Checked)
                    {
                        RichTextBoxContent(8, ((currentOffset / pageSize) - 2) * pageSize);
                    }
                    else if (Hex.Checked)
                    {
                        RichTextBoxContent(2, ((currentOffset / pageSize) - 2) * pageSize);
                    }
                    else
                    {
                        RichTextBoxContent(3, ((currentOffset / pageSize) - 2) * pageSize);
                    }

                    // change current position of scroll bar to half of page position

                    richTextBox1.Select(richTextBox1.Text.Length / 2, 0);
                    richTextBox1.ScrollToCaret();
                }
            }
        }

        //event handler when scroll bar reached a special position(bottom of scroll bar) 

        private void richTextBox1_ThresholdReached(object sender, EventArgs e)
        {
            
            // checks if it is not end of file 

            if (currentOffset < fileSize)
            {
                PageProduced++;
                Info.Text = "";

                //normalize current offset if it is not divisable to size of page

                if (fileSize - currentOffset < pageSize)
                {
                    pageSize = fileSize - currentOffset;
                }

                // test for radio buttons and produce reachTextbox content

                if (Binary.Checked)
                {
                    RichTextBoxContent(8, currentOffset);
                }
                else if (Hex.Checked)
                {
                    RichTextBoxContent(2, currentOffset);
                }
                else
                {
                    RichTextBoxContent(3, currentOffset);
                }
                pageSize = FILESIZE;
            }
        }

        //initializing text labal on top of reach text box for specifying the name of each Columns

        private void LableInitializing()
        {
            string offset = "Offset";
            label4.Text = "";
            if (Hex.Checked)
            {
                CurrentLineColNum = HEXCOLSNUM;
                label3.Text = offset;
                int Length;
                string stmp;
                for (int i = 0; i < HEXCOLSNUM; i++)
                {
                    stmp = Convert.ToString(i, 16);
                    Length = stmp.Length;
                    for (int j = 0; j < 2 - Length; j++)
                    {
                        stmp = stmp.Insert(0, "0");
                    }
                    label4.Text += stmp + "            ";
                }
            }
            else if (Binary.Checked)
            {
                CurrentLineColNum = BINARYCOLSNUM;
                label3.Text = offset;
                int Length;
                string stmp;
                for (int i = 0; i < BINARYCOLSNUM; i++)
                {
                    stmp = Convert.ToString(i, 2);
                    Length = stmp.Length;
                    for (int j = 0; j < 8 - Length; j++)
                    {
                        stmp = stmp.Insert(0, "0");
                    }
                    label4.Text += stmp + "                ";
                }
            }
            else if (Decimal.Checked)
            {
                CurrentLineColNum = DECIMALCOLSNUM;
                label3.Text = offset;
                int Length;
                string stmp;
                for (int i = 0; i < DECIMALCOLSNUM; i++)
                {
                    stmp = Convert.ToString(i, 10);
                    Length = stmp.Length;
                    for (int j = 0; j < 3 - Length; j++)
                    {
                        stmp = stmp.Insert(0, "0");
                    }
                    label4.Text += stmp + "          ";
                }
            }
        }

        // browse bottun on click event handler

        private void Browse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                address_text_box.Text = openFileDialog1.FileName;
                show_headers_button.Enabled = true;
            }
        }

        // show_headers_button on click event handler

        private void show_headers_button_Click(object sender, EventArgs e)
        {
            
            //checks if file address is valid

            if (!File.Exists(address_text_box.Text))
            {
                show_headers_button.Enabled = false;
                MessageBox.Show("Invalid file address", "Address Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    new BinaryReader(File.Open(address_text_box.Text, FileMode.Open)).Close();
                }
                catch (UnauthorizedAccessException)
                {
                    show_headers_button.Enabled = false;
                    MessageBox.Show("you have not permission to access this file !!", "Unauthorized Access Exception",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (address_text_box.Text != "")
            {

                //initialize parameters

                dic.Clear();
                PageProduced = 1;
                TextBoxIndex = 0;
                currentOffset = 0; CurrentLineNum = 0;
                richTextBox1.Text = "";

                // use parser.cs to get file headers

                FileHeader FileHeader = new FileHeader(address_text_box.Text);
                headers = FileHeader.root.arrayOfStructures;
                BuildingHash();
                
                //producing rich text box content

                if (Binary.Checked)
                {
                    RichTextBoxContent(8, currentOffset);
                }
                else if (Hex.Checked)
                {
                    RichTextBoxContent(2, currentOffset);
                }
                else
                {
                    RichTextBoxContent(3, currentOffset);
                }
                show_headers_button.Enabled = false;
            }
        }

        /*public string ConvertTabs(string poorlyFormedRtf)
        {
            string unicodeTab = "\t";
            string result = poorlyFormedRtf.Replace(unicodeTab, "\\tab");
            return result;
        }
        */

        /*
         * inserting black bytes to rich text box
         * blacks: list of black bytes
         * bit: type of displaying bytes (Hex:2,binary:8,decimal:3)
         * TextBoxIndex: richtextbox current text position
         */

        private long addBlacks(List<byte> blacks, int bit, long TextBoxIndex)
        {
            string content;
            if (blacks.Count > 0)
            {
                // adding all black bytes in list to content string with normalization

                content = "";
                foreach (byte b in blacks)
                {
                    if (bit == 8)
                    {

                        // convert to binary and normalize it in 8 bits

                        string stmp = Convert.ToString(b, 2);
                        int Length = stmp.Length;
                        for (int j = 0; j < 8 - Length; j++)
                        {
                            stmp = stmp.Insert(0, "0");
                        }
                        //append current byte to content 
                      
                        AppendCurrentByte(ref content, stmp, true);
                    }
                    else if (bit == 2)
                    {
                        // convert to Hex and normalize it in 2 bits

                        string stmp = Convert.ToString(b, 16);
                        int Length = stmp.Length;
                        for (int j = 0; j < 2 - Length; j++)
                        {
                            stmp = stmp.Insert(0, "0");
                        }
                        //append current byte to content 

                        AppendCurrentByte(ref content, stmp, true);

                    }
                    else if (bit == 3)
                    {
                        // convert to Decimal and normalize it in 3 bits

                        string stmp = Convert.ToString(b, 10);
                        int Length = stmp.Length;
                        for (int j = 0; j < 3 - Length; j++)
                        {
                            stmp = stmp.Insert(0, "0");
                        }

                        //append current byte to content 

                        AppendCurrentByte(ref content, stmp, true);

                    }
                }

                /*
                 * for adding line number to each line in AppendCurrentByte method each line sourounded with # 
                 * so this # is replaced with \n for new lines and \t for first bytes in each lines.
                 */

                if (content.StartsWith("#"))
                {
                    content = content.Remove(0, 1);
                }
                content = content.Replace("\n#", "\n");
                content = content.Replace("#", "\t");
                
                // adding first address of black bytes in dictionary of headers null string in order to specifying the non-header bytes

                dic.Add((int)TextBoxIndex, "");

                // updating the pointer position in richTextBox

                TextBoxIndex += content.Length;

                //appending bytes to richtexbox with black color

                richTextBox1.SelectionStart = (int)TextBoxIndex;
                richTextBox1.SelectionLength = content.Length;
                richTextBox1.SelectionColor = Color.Black;
                richTextBox1.AppendText(content);

                // remove all member of blacks list

                blacks.RemoveRange(0, blacks.Count);
            }
            return TextBoxIndex;
        }

        /*
         * this function is used for adding bytes to content string in a special format: it meanes beautify the lines and adding line numbers 
         * content: call by refrence string that holds the content of RichTextBox
         * stmp: current string that should be added to content
         * isBlack: boolean parameter that specify that current stmp is Black(non-header bytes) or colored(header bytes)
         */ 


        private void AppendCurrentByte(ref string content, string stmp, bool isBlacks)
        {
            // check for if the pointer is at the begining of a line 

            if ((CurrentLineColNum == BINARYCOLSNUM && Binary.Checked) || (CurrentLineColNum == HEXCOLSNUM && Hex.Checked) || (CurrentLineColNum == DECIMALCOLSNUM && Decimal.Checked))
            {
                // convert current line number to Hex or Decimal and then normalize it in NUMOFLINENUMBERSCHARS bits

                string num;
                if (Hex.Checked)
                {
                    num = Convert.ToString(CurrentLineNum, 16);
                }
                else if (Binary.Checked)
                {
                    num = Convert.ToString(CurrentLineNum, 16);
                }
                else
                {
                    num = Convert.ToString(CurrentLineNum, 10);
                }
                int Length = num.Length;
                for (int j = 0; j < NUMOFLINENUMBERSCHARS - Length; j++)
                {
                    num = num.Insert(0, "0");
                }

                // use # charachter for specifying the current line number

                content += "#" + num + ":#" + stmp + "\t";

                //for each byte that add to content 

                CurrentLineColNum--;
                if (!isBlacks)
                    NewLine = true;
            }
            else
            {
                // if the pointer is at the last byte add byte and then New line charachter added: '\n'
                //then CurrentLineColNum and CurrentLineNum updated

                if (CurrentLineColNum == 1)
                {
                    content += stmp + "\n";
                    if (Hex.Checked)
                    {
                        CurrentLineColNum = HEXCOLSNUM;
                        CurrentLineNum += HEXCOLSNUM;
                    }
                    else if (Binary.Checked)
                    {
                        CurrentLineColNum = BINARYCOLSNUM;
                        CurrentLineNum += BINARYCOLSNUM;
                    }
                    else if (Decimal.Checked)
                    {
                        CurrentLineColNum = DECIMALCOLSNUM;
                        CurrentLineNum += DECIMALCOLSNUM;
                    }
                }
                else
                {
                    // for bytes at the middel of line, just add the byte and then Tab charachter:'\t'

                    content += stmp + "\t";

                    //for each byte that add to content 

                    CurrentLineColNum--;
                }
            }
        }

        /*
         * this function is used for adding file content to richTextBox with colored header bytes
         * bit: type of nubers(Hex, binary, decimal)
         * offset: current offset of bytes that is added to richTexBox
         */

        private void RichTextBoxContent(int bit, long offset)
        {
            
            // open the file

            using (BinaryReader reader = new BinaryReader(File.Open(address_text_box.Text, FileMode.Open)))
            {
                // initialize the variabales
                
                currentOffset = offset;
                long FileIndex = currentOffset;
                fileSize = reader.BaseStream.Length;
                Color[] color = { Color.Red, Color.DarkGreen, Color.Blue, Color.Brown, Color.DeepPink };
                int ColorIndex = 0;
                List<byte> blacks = new List<byte>();
                string content = "";
                int currentLineCharNums = 0;
                reader.BaseStream.Seek(currentOffset, SeekOrigin.Begin);

                //reading from file if pointer is not at the end of file and position is less than "pageSize + currentOffset"

                while (reader.BaseStream.Position != reader.BaseStream.Length && reader.BaseStream.Position < pageSize + currentOffset)
                {

                    // check for being header for current byte

                    if (HeaderHash.ContainsKey(FileIndex))
                    {
                        // adding current contents of balcks list to richTextBox

                        TextBoxIndex = addBlacks(blacks, bit, TextBoxIndex);
                        
                        // get the structure of current header from dictionary
                        
                        Structure CurrentStructure;
                        HeaderHash.TryGetValue(FileIndex, out CurrentStructure);
                        content = "";
                        
                        // reading from file in size of current header 

                        byte[] tmp = reader.ReadBytes(CurrentStructure.size.bytes);



                        foreach (byte b in tmp)
                        {
                            if (bit == 8)
                            {
                                // convert to Binary and normalize it in 8 bits

                                string stmp = Convert.ToString(b, 2);
                                int Length = stmp.Length;
                                for (int j = 0; j < 8 - Length; j++)
                                {
                                    stmp = stmp.Insert(0, "0");
                                }
                                //append current byte to content 

                                AppendCurrentByte(ref content, stmp, false);

                                //set current number of charachters in line 

                                currentLineCharNums = CHARNUMSINLINE_BINARY;
                            }
                            else if (bit == 2)
                            {
                                // convert to Hex and normalize it in 2 bits

                                string stmp = Convert.ToString(b, 16);
                                int Length = stmp.Length;
                                for (int j = 0; j < 2 - Length; j++)
                                {
                                    stmp = stmp.Insert(0, "0");
                                }
                                //append current byte to content 

                                AppendCurrentByte(ref content, stmp, false);

                                //set current number of charachters in line 

                                currentLineCharNums = CHARNUMSINLINE_HEX;
                            }
                            else if (bit == 3)
                            {
                                // convert to Decimal and normalize it in 3 bits

                                string stmp = Convert.ToString(b, 10);
                                int Length = stmp.Length;
                                for (int j = 0; j < 3 - Length; j++)
                                {
                                    stmp = stmp.Insert(0, "0");
                                }
                                //append current byte to content 

                                AppendCurrentByte(ref content, stmp, false);

                                //set current number of charachters in line 

                                currentLineCharNums = CHARNUMSINLINE_DECIMAL;
                            }
                        }

                        // if it is new line and current number of line not added

                        if (NewLine && TextBoxIndex % currentLineCharNums == 0)
                        {
                            // spiliting the content by #. line numbers souronding with #.

                            string[] text = content.Split(new char[] { '#' });
                            string num = "", postfix = "";

                            // adding each split to a new line.

                            for (int i = 1; i < text.Length; i = i + 2)
                            {
                                num = text[i];

                                // if there is a content after line number, it will set in postfix.

                                if (i + 1 < text.Length)
                                {
                                    postfix = text[i + 1];
                                }
                                else
                                {
                                    postfix = "";
                                }

                                // appending current line number in black color to richTextBox 

                                richTextBox1.SelectionStart = (int)TextBoxIndex;
                                richTextBox1.SelectionLength = num.Length + 1;
                                richTextBox1.SelectionColor = Color.Black;
                                richTextBox1.AppendText(num + "\t");

                                // appending bytes of headers in next color from color array to richTextBox

                                richTextBox1.SelectionStart = (int)TextBoxIndex + NUMOFLINENUMBERSCHARS + 2;
                                richTextBox1.SelectionLength = postfix.Length;
                                richTextBox1.SelectionColor = color[ColorIndex];
                                richTextBox1.AppendText(postfix);
                            }
                            // setting next color pointer

                            ColorIndex++;
                            ColorIndex = ColorIndex % color.Length;

                            //adding length of line number string plus 2 (':', space) to current richtextbox content pointer
                            TextBoxIndex += NUMOFLINENUMBERSCHARS + 2;
                        }

                        // if it has new line and current pointer of richtextbox is not at the begining of new line 

                        else if (NewLine)
                        {
                            NewLine = false;

                            // spiliting the content by #. line numbers souronding with #.

                            string[] text = content.Split(new char[] { '#' });

                            // if there is a content before line number, it will set in prefix.
                            // if there is a content after line number, it will set in postfix.
                            // number is set in num.

                            string prefix = text[0];
                            string num = text[1];
                            string postfix = text[2];

                            // appending bytes of headers in current color from color array to richTextBox

                            richTextBox1.SelectionStart = (int)TextBoxIndex;
                            richTextBox1.SelectionLength = prefix.Length;
                            richTextBox1.SelectionColor = color[ColorIndex];
                            richTextBox1.AppendText(prefix);

                            // appending current line number in black color to richTextBox

                            richTextBox1.SelectionStart = (int)TextBoxIndex + prefix.Length;
                            richTextBox1.SelectionLength = num.Length + 1;
                            richTextBox1.SelectionColor = Color.Black;
                            richTextBox1.AppendText(num + "\t");

                            // appending bytes of headers in current color from color array to richTextBox

                            richTextBox1.SelectionStart = (int)TextBoxIndex + prefix.Length + num.Length + 1;
                            richTextBox1.SelectionLength = postfix.Length;
                            richTextBox1.SelectionColor = color[ColorIndex];
                            richTextBox1.AppendText(postfix);

                            // setting start pointer for adding next lines.

                            int start = (int)TextBoxIndex + prefix.Length + num.Length + 1 + postfix.Length;

                            // if there is more than one new line.

                            if (text.Length > 3)
                            {

                                // for each new line 

                                for (int i = 3; i < text.Length; i = i + 2)
                                {
                                    // if there is a content after line number, it will set in postfix.
                                    // line number set in num.
                                    num = text[i];
                                    if (i + 1 < text.Length)
                                    {
                                        postfix = text[i + 1];
                                    }
                                    else
                                    {
                                        postfix = "";
                                    }

                                    // appending current line number in black color to richTextBox

                                    richTextBox1.SelectionStart = start;
                                    richTextBox1.SelectionLength = num.Length + 1;
                                    richTextBox1.SelectionColor = Color.Black;
                                    richTextBox1.AppendText(num + "\t");

                                    // appending bytes of headers in current color from color array to richTextBox

                                    richTextBox1.SelectionStart = start + num.Length + 1;
                                    richTextBox1.SelectionLength = postfix.Length;
                                    richTextBox1.SelectionColor = color[ColorIndex];
                                    richTextBox1.AppendText(postfix);

                                    // updating start pointer.

                                    start += num.Length + postfix.Length + 1;
                                }
                            }

                            // setting next color pointer.

                            ColorIndex++;
                            ColorIndex = ColorIndex % color.Length;
                        }

                        // if it has not new line

                        else
                        {
                            // appending bytes of headers in current color from color array to richTextBox

                            richTextBox1.SelectionStart = (int)TextBoxIndex;
                            richTextBox1.SelectionLength = content.Length;
                            richTextBox1.SelectionColor = color[ColorIndex];
                            richTextBox1.AppendText(content);

                            // setting next color pointer.

                            ColorIndex++;
                            ColorIndex = ColorIndex % color.Length;
                        }

                        // adding current header information with its address in richtextbox to dictionary.

                        dic.Add((int)TextBoxIndex, "Name: " + CurrentStructure.name + 
                            " Size: " + CurrentStructure.size.bytes + " byte(s) offset: " + CurrentStructure.offset.bytes +
                            " data: " + CurrentStructure.content.data + " is BigEndian: " + CurrentStructure.content.type.bigEndian+" .");
                        
                        // updating richtextbox content offset 

                        if (NewLine)
                        {
                            if (content.StartsWith("#"))
                            {
                                content = content.Remove(0, 1);
                            }
                            content = content.Replace("\n#", "\n");
                            content = content.Replace("#", "\t");
                            TextBoxIndex += content.Length - (NUMOFLINENUMBERSCHARS + 2);
                            NewLine = false;
                        }
                        else
                        {
                            if (content.StartsWith("#"))
                            {
                                content = content.Remove(0, 1);
                            }
                            content = content.Replace("\n#", "\n");
                            content = content.Replace("#", "\t");
                            TextBoxIndex += content.Length;
                        }
                    }

                    // if it is not header, it will be added to blacks list.
                    else
                    {
                        byte b = reader.ReadByte();
                        blacks.Add(b);
                    }

                    //updating current offset of file.

                    FileIndex = reader.BaseStream.Position;
                }
                // adding black bytes to richtextBox.

                TextBoxIndex=addBlacks(blacks, bit, TextBoxIndex);
                currentOffset = reader.BaseStream.Position;
                reader.Close();
            }
        }


        /*  initialize dictionary named 'HeaderHash' by headers that parser.cs defined
            key: header offset value: header structure
        */
        private void BuildingHash()
        {
            HeaderHash = new Dictionary<long, Structure>();
            foreach (Structure str in headers)
            {
                HeaderHash.Add(str.offset.bytes, str);
            }
        }

        /* setting current offset.
         * richTextBox1: richtextbox
         * currentOffset: call by reference of current offset variable.
         */ 

        private void setCurrentOffset(MyRichTextBox richTextBox1, ref long currentOffset)
        {
            // checking for size of a page in richTextBox

            if (richTextBox1.maxPageSize != 0)
            {
                // get current page number by position of scroll bar

                long currentPage = (richTextBox1.currentScrollPos / richTextBox1.maxPageSize) + 1;
                
                // setting current offset by current number of pages.
                if (currentPage > PageProduced)
                {
                    currentPage = PageProduced;
                }
                Debug.WriteLine("getpage: " + currentPage);
                currentOffset = currentPage * pageSize;
            }
        }

        // event handler for changing radio box and turn into Hex

        private void Hex_CheckedChanged(object sender, EventArgs e)
        {
            if (headers != null)
            {
                // setting page size is enabaled by setting first to true.
                richTextBox1.first = true;
                
                // define the culomn lables

                LableInitializing();
                // initialize some variables
                if (PageProduced == 1)
                {
                    richTextBox1.maxPageSize = 0;
                }
                dic.Clear();
                TextBoxIndex = 0;
                richTextBox1.Text = "";
                
                // normalize the current offset
                if (currentOffset % pageSize != 0)
                {
                    currentOffset = ((currentOffset / pageSize) + 1) * pageSize;
                }

                // set current offset
                setCurrentOffset(richTextBox1,ref currentOffset);

                //set current line number
                CurrentLineNum = ((currentOffset / pageSize) - 1) * pageSize;

                // adding a page from current offset to richtextbox
                RichTextBoxContent(2, ((currentOffset / pageSize) - 1) * pageSize);

                //set current scrollbar position
                richTextBox1.Select(2 * richTextBox1.Text.Length / 10, 0);
                richTextBox1.ScrollToCaret();

                PageProduced = 1;
            }
        }

        // event handler for changing radio box and turn into Decimal

        private void Decimal_CheckedChanged(object sender, EventArgs e)
        {
            if (Decimal.Checked)
            {
                // define the culomn lables
                LableInitializing();

                if (headers != null)
                {
                    // setting page size is enabaled by setting first to true.
                    richTextBox1.first = true;

                    // initialize some variables
                    dic.Clear();
                    TextBoxIndex = 0;
                    if (PageProduced == 1)
                    {
                        richTextBox1.maxPageSize = 0;
                    }
                    richTextBox1.Text = "";
                    richTextBox1.maxPageSize = 0;

                    // normalize the current offset
                    if (currentOffset % pageSize != 0)
                    {
                        currentOffset = ((currentOffset / pageSize) + 1) * pageSize;
                    }

                    // set current offset
                    setCurrentOffset(richTextBox1, ref currentOffset);

                    //set current line number
                    CurrentLineNum = ((currentOffset / pageSize) - 1) * pageSize;

                    // adding a page from current offset to richtextbox
                    RichTextBoxContent(3, ((currentOffset / pageSize) - 1) * pageSize);

                    //set current scrollbar position
                    richTextBox1.Select(2 * richTextBox1.Text.Length / 10, 0);
                    richTextBox1.ScrollToCaret();

                    PageProduced = 1;
                }
            }
        }

        // event handler for changing radio box and turn into Binary

        private void Binary_CheckedChanged(object sender, EventArgs e)
        {
            if (Binary.Checked)
            {
                // define the culomn lables
                LableInitializing();

                if (headers != null)
                {
                    // setting page size is enabaled by setting first to true.
                    richTextBox1.first = true;

                    // initialize some variables
                    dic.Clear();
                    if (PageProduced == 1)
                    {
                        richTextBox1.maxPageSize = 0;
                    }
                    TextBoxIndex = 0;
                    richTextBox1.Text = "";
                    richTextBox1.maxPageSize = 0;

                    // normalize the current offset
                    if (currentOffset % pageSize != 0)
                    {
                        currentOffset = ((currentOffset / pageSize) + 1) * pageSize;
                    }

                    // set current offset
                    setCurrentOffset(richTextBox1, ref currentOffset);

                    //set current line number
                    CurrentLineNum = ((currentOffset / pageSize) - 1) * pageSize;

                    // adding a page from current offset to richtextbox
                    RichTextBoxContent(8, ((currentOffset / pageSize) - 1) * pageSize);

                    //set current scrollbar position
                    richTextBox1.Select(2 * richTextBox1.Text.Length / 10, 0);
                    richTextBox1.ScrollToCaret();

                    PageProduced = 1;
                }
            }
        }
        
        /* this function is used to find the header information from dictionary by current mouse position
         * positionToSearch: current mouse position
        */
        private string GetValueFromDictionary(int positionToSearch)
        {
            // define and initialize some variables
            int pos;
            string data;
            int step;
            int LineChars;
            if (Hex.Checked)
            {
                step = 3;
                LineChars = CHARNUMSINLINE_HEX;
            }
            else if (Binary.Checked)
            {
                step = 9;
                LineChars = CHARNUMSINLINE_BINARY;
            }
            else if (Decimal.Checked)
            {
                step = 4;
                LineChars = CHARNUMSINLINE_DECIMAL;
            }
            else
            {
                step = 3;
                LineChars = CHARNUMSINLINE_HEX;
            }
            pos = positionToSearch;
            int LineNumber = pos / LineChars;
            int PositionIncurrentLine = pos % LineChars;
            PositionIncurrentLine -= NUMOFLINENUMBERSCHARS + 2;
            
            // if pointer is in line number scope it returns.
            if (PositionIncurrentLine < 0)
            {
                return null;
            }

            // normalize the current position in current line
            if (PositionIncurrentLine % step != 0)
            {
                PositionIncurrentLine = (PositionIncurrentLine / step) * step;
            }
            
            while (true)
            {
                // check if current position is contained in dictionary 
                if (dic.TryGetValue(LineNumber * LineChars + PositionIncurrentLine + NUMOFLINENUMBERSCHARS + 2, out data))
                {
                    return data;
                }
                else
                {
                    // if current line has more bytes, current position minus step set to current position
                    if (PositionIncurrentLine != 0)
                    {
                        PositionIncurrentLine -= step;
                    }
                    else
                    {
                        // if this is the first line of richtextbox
                        if (LineNumber == 0)
                        {
                            // check if current position is contained in dictionary
                            if (dic.TryGetValue(PositionIncurrentLine + NUMOFLINENUMBERSCHARS + 2, out data))
                            {
                                return data;
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            // setting new current position in new line 
                            PositionIncurrentLine = (LineNumber * LineChars - step) % LineChars - (NUMOFLINENUMBERSCHARS + 2);
                            
                            //updating remain line numbers to search
                            LineNumber--;
                        }
                    }
                }
            }
        }
        
        // handeling mouse down event

        private void richTextBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                // check for clicking of left button of mouse
                if (e.Clicks == 1 && e.Button == MouseButtons.Left)
                {
                    // get current position of mouse in richtextbox
                    int positionToSearch = richTextBox1.GetCharIndexFromPosition(new Point(e.X, e.Y));

                    //get current header information and set it in tooltip 
                    string data = GetValueFromDictionary(positionToSearch);
                    Info.Text = data;
                    toolTip1.SetToolTip(richTextBox1, data);
                }
            }
        }

        private void richTextBox1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //MyRichTextBox.HideCaret(richTextBox1.Handle);
            if (richTextBox1.Text != "")
            {
                // get current position of mouse in richtextbox
                int positionToSearch = richTextBox1.GetCharIndexFromPosition(new Point(e.X, e.Y));

                //get current header information and set it in tooltip
                string data = GetValueFromDictionary(positionToSearch);
                if(data!=toolTip1.GetToolTip(richTextBox1))
                    toolTip1.SetToolTip(richTextBox1, data);
                Debug.WriteLine("MOVE");
               /* if (data != "")
                {
                    MyRichTextBox.ShowCaret(richTextBox1.Handle);
                    richTextBox1.Cursor = Cursors.Hand;
                }
                else
                {
                    MyRichTextBox.HideCaret(richTextBox1.Handle);
                    richTextBox1.Cursor = Cursors.IBeam;
                }*/

            }
            base.OnMouseMove(e);
        }

        private void richTextBox1_MouseHover(object sender, System.EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                // get current position of mouse in richtextbox
                Control control = (Control)sender;
                Point pos = control.PointToClient(System.Windows.Forms.Cursor.Position);
                int positionToSearch = richTextBox1.GetCharIndexFromPosition(pos);

                //get current header information and set it in tooltip
                string data = GetValueFromDictionary(positionToSearch);
                //Info.Text = data;
                //toolTip1.SetToolTip(richTextBox1, data);
                if (data != "")
                {
                    control.Cursor = Cursors.Hand;
                }
                else
                {
                    control.Cursor = Cursors.IBeam;
                }
            }

        }

       
        /*
        private void naxt_Click(object sender, EventArgs e)
        {
            if (currentOffset < fileSize)
            {
                richTextBox1.Text = "";
                if (fileSize - currentOffset < pageSize)
                {
                    pageSize = fileSize - currentOffset;
                }
                if (Binary.Checked)
                {
                    RichTextBoxContent(8, currentOffset);
                }
                else if (Hex.Checked)
                {
                    RichTextBoxContent(2, currentOffset);
                }
                else
                {
                    RichTextBoxContent(3, currentOffset);
                }
                pageSize = FILESIZE;
            }
        }

        private void last_Click(object sender, EventArgs e)
        {
            if (currentOffset >= 2 * pageSize)
            {
                if (currentOffset % pageSize != 0)
                {
                    currentOffset = ((currentOffset / pageSize) + 1) * pageSize;
                }
                CurrentLineNum = ((currentOffset / pageSize) - 2) * pageSize;
                richTextBox1.Text = "";
                if (Binary.Checked)
                {
                    RichTextBoxContent(8, ((currentOffset / pageSize) - 2) * pageSize);
                }
                else if (Hex.Checked)
                {
                    RichTextBoxContent(2, ((currentOffset / pageSize) - 2) * pageSize);
                }
                else
                {
                    RichTextBoxContent(3, ((currentOffset / pageSize) - 2) * pageSize);
                }
            }
        }
        */
        private void richTextBox1_MouseEnter(object sender, EventArgs e)
        {
            //richTextBox1.Cursor = Cursors.IBeam;
            Control c = (Control)sender;
            c.Cursor = Cursors.IBeam;
        }

        private void address_text_box_TextChanged(object sender, EventArgs e)
        {
            if (address_text_box.Text != null)
            {
                show_headers_button.Enabled = true;
            }
        }

    }

    /*
     * customize the built in richtextbox of msdn to add some events and functions 
     */

    public class MyRichTextBox : RichTextBox
    {
        private int? _lastLocation = null;
        public long currentScrollPos { get; private set; }
        public bool first {get;set;}
        public long maxPageSize { get;  set; }

        [DllImport("user32.dll", EntryPoint = "ShowCaret")]
        public static extern long ShowCaret(IntPtr hwnd);
        [DllImport("user32.dll", EntryPoint = "HideCaret")]
        public static extern long HideCaret(IntPtr hwnd);

        // override the WndProc function inorder to add 2 events that one of them raise when scroll bar reaches 10% of first of richtextbox
        //and the other raise when scroll bar reaches 90% of first of richtextbox inorder to load new page

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x115) // catches WM_VSCROLL  
            {
                if (m.WParam.ToInt32() != 0x8)
                {
                    if (_lastLocation.HasValue)
                    {
                        if (m.WParam.ToInt32() > _lastLocation)
                        {
                            //DOWN!
                            if ((int)GetRichTextBoxScrolPos(this) >= 89/* && (int)GetRichTextBoxScrolPos(this) <= 91*/)
                            {
                                Debug.WriteLine("D:" + _lastLocation);
                                OnThresholdReached(EventArgs.Empty);
                            }
                        }
                        else if (m.WParam.ToInt32() < _lastLocation - 1)
                        {
                            //UP!
                            if (/*(int)GetRichTextBoxScrolPos(this) >= 9 &&*/ (int)GetRichTextBoxScrolPos(this) <= 11)
                            {
                                Debug.WriteLine("U:" + _lastLocation);
                                OnMinimalReached(EventArgs.Empty);

                            }
                        }
                        else
                        {
                            //"No change"
                            Debug.WriteLine("n" + _lastLocation);
                        }
                    }

                    _lastLocation = m.WParam.ToInt32();
                } if (m.WParam.ToInt32() == 0x8)
                {
                    _lastLocation = null;
                }

            }
            base.WndProc(ref m);
        }
        
        private double GetRichTextBoxScrolPos(RichTextBox textBox)
        {
            if (textBox.TextLength == 0) return 0;
            var p1 = textBox.GetPositionFromCharIndex(0);
            var p2 = textBox.GetPositionFromCharIndex(textBox.TextLength - 1);

            int scrollPos = -p1.Y;
            int maxScrolPos = p2.Y - p1.Y - textBox.ClientSize.Height;

            if (maxScrolPos <= 0) return 0;
            currentScrollPos = scrollPos;
            if (first)
            {
                maxPageSize = maxScrolPos;
                first = false;
            }
            double d = 100.0 * (double)scrollPos / (double)maxScrolPos;
            Debug.WriteLine("scrollPos: " + scrollPos);
            if (d < 0) d = 0;
            else if (d > 100) d = 100;

            return d;
        }
        protected virtual void OnThresholdReached(EventArgs e)
        {
            EventHandler handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler ThresholdReached;
        protected virtual void OnMinimalReached(EventArgs e)
        {
            EventHandler handler = MinimalReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler MinimalReached;
    } 

}
   
