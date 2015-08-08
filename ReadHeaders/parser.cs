using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HeaderReader
{
    public class Size
    {
        public int bytes
        {
            get;
            set;
        }
    }
    public class Offset
    {
        public int bytes
        {
            get;
            set;
        }
    }

    public class Type
    {
        public bool bigEndian
        {
            get;
            set;
        }
        public String type
        {
            get;
            set;
        }
    }

    public class Content
    {
        public Content()
        {
            type = new Type();
        }
        public Type type
        {
            get;
            set;
        }

        public String data
        {
            get;
            set;
        }
    }

    public class Structure
    {
        public Structure()
        {
            size = new Size();
            offset = new Offset();
            content = new Content();
        }

        public string name
        {
            get;
            set;
        }
        public Size size
        {
            get;
            set;
        }
        public Offset offset
        {
            get;
            set;
        }
        public Structure[] arrayOfStructures
        {
            get;
            set;
        }
        public Content content
        {
            get;
            set;
        }

        public Content Content
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Size Size
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Type Type
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Offset Offset
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

    public class FileHeader
    {
        public Structure root
        {
            get;
            set;
        }

        public Structure Structure
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        String fileName;

        public FileHeader(String fileName)
        {
            this.fileName = fileName;
            root = new Structure();
            root.size.bytes = 146;
            root.offset.bytes = 0;
            root.arrayOfStructures = new Structure[54];
            for (int i = 0; i < root.arrayOfStructures.Length; i++)
            {
                root.arrayOfStructures[i] = new Structure();
            }
            root.arrayOfStructures[0].size.bytes = 1;
            root.arrayOfStructures[0].name = 1 + "";
            root.arrayOfStructures[0].offset.bytes = 0;
            root.arrayOfStructures[0].content.type.bigEndian = false;
            root.arrayOfStructures[1].size.bytes = 1;
            root.arrayOfStructures[1].name = 2 + "";
            root.arrayOfStructures[1].offset.bytes = 1;
            root.arrayOfStructures[1].content.type.bigEndian = false;
            root.arrayOfStructures[2].size.bytes = 1;
            root.arrayOfStructures[2].name = 3 + "";
            root.arrayOfStructures[2].offset.bytes = 2;
            root.arrayOfStructures[2].content.type.bigEndian = false;
            root.arrayOfStructures[3].size.bytes = 1;
            root.arrayOfStructures[3].name = 4 + "";
            root.arrayOfStructures[3].offset.bytes = 3;
            root.arrayOfStructures[3].content.type.bigEndian = false;
            root.arrayOfStructures[4].size.bytes = 1;
            root.arrayOfStructures[4].name = 5 + "";
            root.arrayOfStructures[4].offset.bytes = 4;
            root.arrayOfStructures[4].content.type.bigEndian = false;
            root.arrayOfStructures[5].size.bytes = 1;
            root.arrayOfStructures[5].name = 6 + "";
            root.arrayOfStructures[5].offset.bytes = 5;
            root.arrayOfStructures[5].content.type.bigEndian = false;
            root.arrayOfStructures[6].size.bytes = 2;
            root.arrayOfStructures[6].name = 7 + "";
            root.arrayOfStructures[6].offset.bytes = 6;
            root.arrayOfStructures[6].content.type.bigEndian = false;
            root.arrayOfStructures[7].size.bytes = 2;
            root.arrayOfStructures[7].name = 8 + "";
            root.arrayOfStructures[7].offset.bytes = 8;
            root.arrayOfStructures[7].content.type.bigEndian = false;
            root.arrayOfStructures[8].size.bytes = 4;
            root.arrayOfStructures[8].name = 9 + "";
            root.arrayOfStructures[8].offset.bytes = 10;
            root.arrayOfStructures[8].content.type.bigEndian = false;
            root.arrayOfStructures[9].size.bytes = 2;
            root.arrayOfStructures[9].name = 10 + "";
            root.arrayOfStructures[9].offset.bytes = 14;
            root.arrayOfStructures[9].content.type.bigEndian = false;
            root.arrayOfStructures[10].size.bytes = 2;
            root.arrayOfStructures[10].name = 11 + "";
            root.arrayOfStructures[10].offset.bytes = 16;
            root.arrayOfStructures[10].content.type.bigEndian = false;
            root.arrayOfStructures[11].size.bytes = 4;
            root.arrayOfStructures[11].name = 12 + "";
            root.arrayOfStructures[11].offset.bytes = 18;
            root.arrayOfStructures[11].content.type.bigEndian = false;
            root.arrayOfStructures[12].size.bytes = 4;
            root.arrayOfStructures[12].name = 13 + "";
            root.arrayOfStructures[12].offset.bytes = 22;
            root.arrayOfStructures[12].content.type.bigEndian = false;
            root.arrayOfStructures[13].size.bytes = 1;
            root.arrayOfStructures[13].name = 14 + "";
            root.arrayOfStructures[13].offset.bytes = 27;
            root.arrayOfStructures[13].content.type.bigEndian = false;
            root.arrayOfStructures[14].size.bytes = 2;
            root.arrayOfStructures[14].name = 15 + "";
            root.arrayOfStructures[14].offset.bytes = 28;
            root.arrayOfStructures[14].content.type.bigEndian = false;
            root.arrayOfStructures[15].size.bytes = 2;
            root.arrayOfStructures[15].name = 16 + "";
            root.arrayOfStructures[15].offset.bytes = 30;
            root.arrayOfStructures[15].content.type.bigEndian = false;
            root.arrayOfStructures[16].size.bytes = 1;
            root.arrayOfStructures[16].name = 17 + "";
            root.arrayOfStructures[16].offset.bytes = 32;
            root.arrayOfStructures[16].content.type.bigEndian = false;
            root.arrayOfStructures[17].size.bytes = 1;
            root.arrayOfStructures[17].name = 18 + "";
            root.arrayOfStructures[17].offset.bytes = 33;
            root.arrayOfStructures[17].content.type.bigEndian = false;
            root.arrayOfStructures[18].size.bytes = 4;
            root.arrayOfStructures[18].name = 19 + "";
            root.arrayOfStructures[18].offset.bytes = 34;
            root.arrayOfStructures[18].content.type.bigEndian = false;
            root.arrayOfStructures[19].size.bytes = 1;
            root.arrayOfStructures[19].name = 20 + "";
            root.arrayOfStructures[19].offset.bytes = 38;
            root.arrayOfStructures[19].content.type.bigEndian = false;
            root.arrayOfStructures[20].size.bytes = 1;
            root.arrayOfStructures[20].name = 21 + "";
            root.arrayOfStructures[20].offset.bytes = 39;
            root.arrayOfStructures[20].content.type.bigEndian = false;
            root.arrayOfStructures[21].size.bytes = 1;
            root.arrayOfStructures[21].name = 22 + "";
            root.arrayOfStructures[21].offset.bytes = 40;
            root.arrayOfStructures[21].content.type.bigEndian = false;
            root.arrayOfStructures[22].size.bytes = 6;
            root.arrayOfStructures[22].name = 23 + "";
            root.arrayOfStructures[22].offset.bytes = 41;
            root.arrayOfStructures[22].content.type.bigEndian = false;
            root.arrayOfStructures[23].size.bytes = 4;
            root.arrayOfStructures[23].name = 24 + "";
            root.arrayOfStructures[23].offset.bytes = 47;
            root.arrayOfStructures[23].content.type.bigEndian = false;
            root.arrayOfStructures[24].size.bytes = 4;
            root.arrayOfStructures[24].name = 25 + "";
            root.arrayOfStructures[24].offset.bytes = 51;
            root.arrayOfStructures[24].content.type.bigEndian = false;
            root.arrayOfStructures[25].size.bytes = 4;
            root.arrayOfStructures[25].name = 26 + "";
            root.arrayOfStructures[25].offset.bytes = 55;
            root.arrayOfStructures[25].content.type.bigEndian = false;
            root.arrayOfStructures[26].size.bytes = 2;
            root.arrayOfStructures[26].name = 27 + "";
            root.arrayOfStructures[26].offset.bytes = 59;
            root.arrayOfStructures[26].content.type.bigEndian = false;
            root.arrayOfStructures[27].size.bytes = 1;
            root.arrayOfStructures[27].name = 28 + "";
            root.arrayOfStructures[27].offset.bytes = 61;
            root.arrayOfStructures[27].content.type.bigEndian = false;
            root.arrayOfStructures[28].size.bytes = 2;
            root.arrayOfStructures[28].name = 29 + "";
            root.arrayOfStructures[28].offset.bytes = 62;
            root.arrayOfStructures[28].content.type.bigEndian = false;
            root.arrayOfStructures[29].size.bytes = 1;
            root.arrayOfStructures[29].name = 30 + "";
            root.arrayOfStructures[29].offset.bytes = 4096;
            root.arrayOfStructures[29].content.type.bigEndian = false;
            root.arrayOfStructures[30].size.bytes = 6;
            root.arrayOfStructures[30].name = 31 + "";
            root.arrayOfStructures[30].offset.bytes = 4122;
            root.arrayOfStructures[30].content.type.bigEndian = false;
            root.arrayOfStructures[31].size.bytes = 1;
            root.arrayOfStructures[31].name = 32 + "";
            root.arrayOfStructures[31].offset.bytes = 8238;
            root.arrayOfStructures[31].content.type.bigEndian = false;
            root.arrayOfStructures[32].size.bytes = 40;
            root.arrayOfStructures[32].name = 33 + "";
            root.arrayOfStructures[32].offset.bytes = 8239;
            root.arrayOfStructures[32].content.type.bigEndian = false;
            root.arrayOfStructures[33].size.bytes = 2;
            root.arrayOfStructures[33].name = 34 + "";
            root.arrayOfStructures[33].offset.bytes = 8448;
            root.arrayOfStructures[33].content.type.bigEndian = false;
            root.arrayOfStructures[34].size.bytes = 2;
            root.arrayOfStructures[34].name = 35 + "";
            root.arrayOfStructures[34].offset.bytes = 8450;
            root.arrayOfStructures[34].content.type.bigEndian = false;
            root.arrayOfStructures[35].size.bytes = 2;
            root.arrayOfStructures[35].name = 36 + "";
            root.arrayOfStructures[35].offset.bytes = 8452;
            root.arrayOfStructures[35].content.type.bigEndian = false;
            root.arrayOfStructures[36].size.bytes = 2;
            root.arrayOfStructures[36].name = 37 + "";
            root.arrayOfStructures[36].offset.bytes = 8454;
            root.arrayOfStructures[36].content.type.bigEndian = false;
            root.arrayOfStructures[37].size.bytes = 2;
            root.arrayOfStructures[37].name = 38 + "";
            root.arrayOfStructures[37].offset.bytes = 8460;
            root.arrayOfStructures[37].content.type.bigEndian = false;
            root.arrayOfStructures[38].size.bytes = 2;
            root.arrayOfStructures[38].name = 39 + "";
            root.arrayOfStructures[38].offset.bytes = 8462;
            root.arrayOfStructures[38].content.type.bigEndian = false;
            root.arrayOfStructures[39].size.bytes = 2;
            root.arrayOfStructures[39].name = 40 + "";
            root.arrayOfStructures[39].offset.bytes = 8464;
            root.arrayOfStructures[39].content.type.bigEndian = false;
            root.arrayOfStructures[40].size.bytes = 2;
            root.arrayOfStructures[40].name = 41 + "";
            root.arrayOfStructures[40].offset.bytes = 8466;
            root.arrayOfStructures[40].content.type.bigEndian = false;
            root.arrayOfStructures[41].size.bytes = 2;
            root.arrayOfStructures[41].name = 42 + "";
            root.arrayOfStructures[41].offset.bytes = 8474;
            root.arrayOfStructures[41].content.type.bigEndian = false;
            root.arrayOfStructures[42].size.bytes = 2;
            root.arrayOfStructures[42].name = 43 + "";
            root.arrayOfStructures[42].offset.bytes = 8476;
            root.arrayOfStructures[42].content.type.bigEndian = false;
            root.arrayOfStructures[43].size.bytes = 1;
            root.arrayOfStructures[43].name = 44 + "";
            root.arrayOfStructures[43].offset.bytes = 8530;
            root.arrayOfStructures[43].content.type.bigEndian = false;
            root.arrayOfStructures[44].size.bytes = 3;
            root.arrayOfStructures[44].name = 45 + "";
            root.arrayOfStructures[44].offset.bytes = 8531;
            root.arrayOfStructures[44].content.type.bigEndian = false;
            root.arrayOfStructures[45].size.bytes = 1;
            root.arrayOfStructures[45].name = 46 + "";
            root.arrayOfStructures[45].offset.bytes = 8539;
            root.arrayOfStructures[45].content.type.bigEndian = false;
            root.arrayOfStructures[46].size.bytes = 1;
            root.arrayOfStructures[46].name = 47 + "";
            root.arrayOfStructures[46].offset.bytes = 8540;
            root.arrayOfStructures[46].content.type.bigEndian = false;
            root.arrayOfStructures[47].size.bytes = 1;
            root.arrayOfStructures[47].name = 48 + "";
            root.arrayOfStructures[47].offset.bytes = 8541;
            root.arrayOfStructures[47].content.type.bigEndian = false;
            root.arrayOfStructures[48].size.bytes = 1;
            root.arrayOfStructures[48].name = 49 + "";
            root.arrayOfStructures[48].offset.bytes = 8542;
            root.arrayOfStructures[48].content.type.bigEndian = false;
            root.arrayOfStructures[49].size.bytes = 4;
            root.arrayOfStructures[49].name = 50 + "";
            root.arrayOfStructures[49].offset.bytes = 8543;
            root.arrayOfStructures[49].content.type.bigEndian = false;
            root.arrayOfStructures[50].size.bytes = 1;
            root.arrayOfStructures[50].name = 51 + "";
            root.arrayOfStructures[50].offset.bytes = 8547;
            root.arrayOfStructures[50].content.type.bigEndian = false;
            root.arrayOfStructures[51].size.bytes = 1;
            root.arrayOfStructures[51].name = 52 + "";
            root.arrayOfStructures[51].offset.bytes = 8548;
            root.arrayOfStructures[51].content.type.bigEndian = false;
            root.arrayOfStructures[52].size.bytes = 1;
            root.arrayOfStructures[52].name = 53 + "";
            root.arrayOfStructures[52].offset.bytes = 8552;
            root.arrayOfStructures[52].content.type.bigEndian = false;
            root.arrayOfStructures[53].size.bytes = 1;
            root.arrayOfStructures[53].name = 54 + "";
            root.arrayOfStructures[53].offset.bytes = 8553;
            root.arrayOfStructures[53].content.type.bigEndian = false;

            setHeaderContentData();
        }

        public byte[] readHeader(int offset,int required)
        {
            byte[] header=new byte[required];

            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {

                    reader.BaseStream.Seek(offset, SeekOrigin.Begin);
                    header = reader.ReadBytes(required);
                }
            }
            return header;

        }

        public void setHeaderContentData()
        {
            for (int structNum = 0; structNum < root.arrayOfStructures.Length; structNum++)
            {
                int byteSize = root.arrayOfStructures[structNum].size.bytes;
                int byteOffset = root.arrayOfStructures[structNum].offset.bytes;
                bool bigEndian = root.arrayOfStructures[structNum].content.type.bigEndian;
                byte[] data=new byte[byteSize];
                data = readHeader(byteOffset, byteSize);

                long contentData = 0;
                if (bigEndian)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        contentData += (data[i] << ((data.Length - 1 - i) * 8));
                    }
                }
                else
                {
                    for (int i = data.Length - 1; i > -1; i--)
                    {
                        contentData += (data[i] << ((i) * 8));
                    }
                }

                root.arrayOfStructures[structNum].content.data = contentData.ToString();
            }
        }

}    }
