using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APA.XmlParser
{
    class Library
    {
        private string inputFileAddress;
        public Library()
        {
           // inputFileAddress = input;
            init = "using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.IO;  namespace IPV4HeaderReader {     class Size     {         public int bytes         {             get;             set;         }     }     class Offset     {         public int bytes         {             get;             set;         }     }      class Type     {         public bool bigEndian         {             get;             set;         }         public String type         {             get;             set;         }     }          class Content     {         public Content()         {             type = new Type();         }         public Type type         {             get;             set;         }          public String data         {             get;             set;         }     }          class Structure     {         public Structure()         {             size=new Size();             offset=new Offset();             content = new Content();         }          public string name         {             get;             set;         }         public Size size         {             get;             set;         }         public Offset offset         {             get;             set;         }         public Structure[] arrayOfStructures         {             get;             set;         }         public Content content         {             get;             set;         }     }      class IPV4Header     {         public Structure root         {             get;             set;         }         String fileName;          public IPV4Header(String fileName)         {             this.fileName = fileName;             root = new Structure(); ";
            end = "setHeaderContentData();         }          public byte[] readHeader(int offset,int required)         {             byte[] header=new byte[required];              if (File.Exists(fileName))             {                 using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))                 {                                         reader.BaseStream.Seek(offset, SeekOrigin.Begin);                     header = reader.ReadBytes(required);                 }             }             return header;          }          public void setHeaderContentData()         {             for (int structNum = 0; structNum < root.arrayOfStructures.Length; structNum++)             {                 int byteSize = root.arrayOfStructures[structNum].size.bytes;                 int byteOffset = root.arrayOfStructures[structNum].offset.bytes;                 bool bigEndian = root.arrayOfStructures[structNum].content.type.bigEndian;                 byte[] data=new byte[byteSize];                 data = readHeader(byteOffset, byteSize);                  long contentData = 0;                 if (bigEndian)                 {                     for (int i = 0; i < data.Length; i++)                     {                         contentData += (data[i] << ((data.Length - 1 - i) * 8));                     }                 }                 else                 {                     for (int i = data.Length - 1; i > -1; i--)                     {                         contentData += (data[i] << ((i) * 8));                     }                 }                  root.arrayOfStructures[structNum].content.data = contentData.ToString();             }         }      }        class Program     {         static void Main(string[] args)         {             Console.WriteLine(\"please enter input file address: \");             IPV4Header ip = new IPV4Header(@\"\"+Console.ReadLine());             foreach (Structure s in ip.root.arrayOfStructures)             {                 long data=long.Parse(s.content.data);                 Console.WriteLine(\"name: \"+s.name+\" offset: \"+s.offset.bytes.ToString(\"X\")+\" size: \"+s.size.bytes+\" data: \"+data.ToString(\"X\"));             }              Console.ReadLine();         }     } } ";
        }
        public String init
        {
            get;
            private set;
        }
        public String end
        {
            get;
            private set;
        }
    }
}
