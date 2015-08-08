using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace APA.XmlParser
{
    public class ParserGenerator
    {
        public String outputFileName
        { get; private set; }
        private BinaryHeap<Structure> bs;
        public ParserGenerator(String outputFileName)
        {
            this.outputFileName = outputFileName;
            //create a min heal in order to sort headers wrt. their offsets
            bs = new BinaryHeap<Structure>();
        }
        public String generateParser(String xmlFile)
        {
            //read XML file defining file format
            Structure str = XML.deserialize(xmlFile);

            sortHeaders(str);
            //generate approp. parser
            Library lib = new Library();
            String code = lib.init;

            code += " root.size.bytes =" + str.size.bytes + ";";
            code += "root.offset.bytes = " + str.offset.bytes + ";";
            code += "root.arrayOfStructures=new Structure[" + bs.Count + "];";
            
            code += "for (int i = 0; i < root.arrayOfStructures.Length; i++ )" +
            "{" +
                "root.arrayOfStructures[i] = new Structure();" +
            "}";
            
            int i = -1;
            while (bs.Count > 0)
            {
                i++;
                Structure peek = bs.RemoveRoot();
                code += "root.arrayOfStructures[" + i + "].size.bytes = " + peek.size.bytes + ";" +
                        "root.arrayOfStructures[" + i + "].name = " + peek.Name + "+\"\";"+
                        "root.arrayOfStructures[" + i + "].offset.bytes = " + peek.offset.bytes + ";" +
                        "root.arrayOfStructures[" + i + "].content.type.bigEndian = " + peek.content.type.bigEndian.ToString().ToLower() + ";";
                Log("root.arrayOfStructures[" + i + "].size.bytes = " + peek.size.bytes + ";" + "root.arrayOfStructures[" + i + "].offset.bytes = " + peek.offset.bytes + ";" + "root.arrayOfStructures[" + i + "].content.type.bigEndian = " + peek.content.type.bigEndian.ToString().ToLower() + ";");

            }

            code += lib.end;
            //write parser in output file
            System.IO.File.WriteAllText(outputFileName, code);
            //return generated parser code
            return code;
        }
        private void sortHeaders(Structure str)
        {
            //str is a leaf node
            if (str.structureArray == null || str.structureArray.structureArray == null)
            {
                bs.Insert(str);
                return;
            }
            //sort headers wrt. offsets
            foreach (Structure s in str.structureArray.structureArray)
            {
                //if the structure s is a leaf structure insert it into heap
                if (s.structureArray == null || s.structureArray.structureArray == null)
                    bs.Insert(s);
                else
                {
                    //sort subStructures instead of parent
                    foreach (Structure subStruct in s.structureArray.structureArray)
                        sortHeaders(subStruct);
                }
            }
        }
        [Conditional("DEBUG")]
        public static void Log(string str)
        {
            Debugger.Log(0,"INFO",str);
        }
    }



}
