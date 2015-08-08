using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace APA.XmlParser
{
    [Serializable()]
    public class XML : IComparable
    {
        [XmlElement("structure")]
        public Structure structure
        {
            get;
            set;
        }


        int IComparable.CompareTo(object obj)
        {
            return ((IComparable)this.structure).CompareTo(((XML)obj).structure);
        }

        internal static Structure deserialize(String xmlFile)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(Structure));

            StreamReader reader = new StreamReader(xmlFile);
            Structure structure = (Structure)serializer.Deserialize(reader);
            reader.Close();
            try
            {

                //calculate offsets in byte which are defined as 'afterPrev'
                structure.reOrganize();
            }
            catch (UndefinedSizeException e)
            {
                Console.WriteLine(e.Message);
                System.Environment.Exit(0);
            }
            catch (UncompatibleSizeException e1)
            {
                Console.WriteLine(e1.message);
                System.Environment.Exit(0);
            }
            return structure;
        }
    }
    [Serializable()]
    public class Size
    {
        [XmlAttribute("bytes")]
        public String bytes
        {
            get;
            set;
        }
        [XmlAttribute("bits")]
        public String bits
        {
            get;
            set;
        }
        [XmlAttribute("terminatedBy")]
        public String terminatedBy
        {
            get;
            set;
        }
        [XmlAttribute("wrapper")]
        public bool wrapper
        {
            get;
            set;
        }
        [XmlElement("resource")]
        public String resource
        {
            get;
            set;
        }
    }
    [Serializable()]
    public class offset : IComparable
    {
        [XmlAttribute("bytes")]
        public String bytes
        {
            get;
            set;
        }
        /*        [XmlAttribute("afterPrev")]
        public bool afterPrev
        {
        get;
        set;
        }
        [XmlElement("resource")]
        public String resource
        {
        get;
        set;
        }*/

        int IComparable.CompareTo(object obj)
        {
            Int32 mval = int.Parse(this.bytes);
            Int32 yval = int.Parse(((offset)obj).bytes);
            return mval == yval ? 0 : (mval < yval ? -1 : 1);
        }
    }
    [Serializable()]
    public class ArrayOfStructure
    {
        [XmlArray("arrayOfStructure")]
        [XmlArrayItem("structure", typeof(Structure))]
        public Structure[] structureArray
        {
            get;
            set;
        }
    }
    [Serializable()]
    public class Handler
    {
        [XmlAttribute("src")]
        public String src
        {
            get;
            set;
        }
        [XmlElement("code")]
        public String code
        {
            get;
            set;
        }
    }
    [Serializable()]
    public class Type
    {
        [XmlElement("attribute")]
        public String attribute
        {
            get;
            set;
        }
        [XmlElement("primitive")]
        public String primitive
        {
            get;
            set;
        }
        [XmlAttribute("bigEndian")]
        public bool bigEndian
        {
            get;
            set;
        }
    }
    [Serializable()]
    public class Range
    {
        [XmlAttribute("min")]
        public String Min
        {
            get;
            set;
        }
        [XmlAttribute("max")]
        public String Max
        {
            get;
            set;
        }
        [XmlAttribute("step")]
        public String Step
        {
            get;
            set;
        }
        [XmlArray("list")]
        [XmlArrayItem("item", typeof(double))]
        public double[] list
        {
            get;
            set;
        }
    }
    [Serializable()]
    public class Content
    {
        [XmlElement("type")]
        public Type type
        {
            get;
            set;
        }
        [XmlElement("size")]
        public Size size
        {
            get;
            set;
        }
        [XmlElement("data")]
        public String data
        {
            get;
            set;
        }
        [XmlElement("matches")]
        public String matches
        {
            get;
            set;
        }
        [XmlElement("range")]
        public Range range
        {
            get;
            set;
        }

    }

    [Serializable()]
    [XmlRoot("structure")]
    public class Structure : IComparable
    {
        private int startingOffset
        {
            get;
            set;
        }
        [XmlElement("name")]
        public string Name
        {
            get;
            set;
        }
        [XmlElement("size")]
        public Size size
        {
            get;
            set;
        }
        [XmlElement("offset")]
        public offset offset
        {
            get;
            set;
        }
        [XmlElement("arrayOfStructure")]
        public ArrayOfStructure structureArray
        {
            get;
            set;
        }
        /*        [XmlElement("handler")]
        public Handler handler
        {
        get;
        set;
        }*/
        [XmlElement("content")]
        public Content content
        {
            get;
            set;
        }

        int IComparable.CompareTo(object obj)
        {
            return ((IComparable)this.offset).CompareTo(((Structure)obj).offset);
        }

        public void reOrganize()
        {
            //set undefined sizes recursively 
            size = new Size();
            size.bytes = CalculateSize(this) + "";
            //set startingOffset which the xml is started from there -> usually zero
            setStartingOffset();
            //calculate offsets recursively starting from 'startingOffset'
            calculateOffsets(startingOffset, this);
        }
        private int CalculateSize(Structure str)
        {
            //Induction Basis
            //if this is a leaf structure it is supposed to have an explicitly defined size in bytes, both itself and its content
            if (str.structureArray == null || str.structureArray.structureArray == null)
            {
                if ((str.size == null || str.size.bytes == null) || (str.content != null && str.content.size.bytes == null))
                    throw new UndefinedSizeException("Leaf structures are expected to have explicitly defined size in bytes.");
                else
                {
                    int s = int.Parse(str.size.bytes);
                    //if a leaf structure contains any content it is supposed to have no sizes in bytes or have a size in bytes equal by content's size in bytes
                    //if (str.hasContent())
                    //{
                    //    if (s > 0 && s != int.Parse(str.content.size.bytes))
                    //        throw new UncompatibleSizeException("Leaf structures are expected to have no sizes in bytes itselves or has a size in bytes which equals to the content's size in bytes.");
                    //    else
                    //        s = int.Parse(str.content.size.bytes);
                    //}
                    return s;
                }
            }
            //if there is an explicit size in bytes defined for structure str keep calm and just return specified value! :D
            if (str.size != null && str.size.bytes != null)
                return int.Parse(str.size.bytes);
            //Induction Definition
            //Otherwise total size is equal by summation of subStructures size ('wrapper mode')
            int totalSize = 0;
            foreach (Structure child in str.structureArray.structureArray)
                totalSize += CalculateSize(child);
            Size size = new Size();
            size.bytes = totalSize + "";
            str.size = size;
            return totalSize;

        }
        private bool hasContent()
        {
            return content != null;
        }
        private void setStartingOffset()
        {
            //if there is no explicit offset set offset to zero, otherwise set it to required value
            if (offset == null || offset.bytes == null)
                startingOffset = 0;
            else
                startingOffset = int.Parse(offset.bytes);
        }
        private void calculateOffsets(int prevOffset, Structure structure)
        {
            if (structure.offset == null)
            {
                offset offs = new offset();
                offs.bytes = prevOffset + "";
                structure.offset = offs;
            }
            Structure[] childs = structure.structureArray.structureArray;
            //if the offset of first child is not setted in bytes, this child is expected to appear right at starting of file described by XML, meaning that it appears in 'startingOffset'
            if (childs[0].offset == null || childs[0].offset.bytes == null)
            {
                offset offs = new offset();
                offs.bytes = prevOffset + "";
                childs[0].offset = offs;
            }//if first child has some substructures calculate their offset recursively begining from its offset
            if (childs[0].structureArray != null && childs[0].structureArray.structureArray != null && childs[0].structureArray.structureArray.Length > 0)
                calculateOffsets(int.Parse(childs[0].offset.bytes), childs[0]);
            for (int i = 1; i < childs.Length; i++)
            {
                //if the offset of ith child is not setted in bytes, this child is expected to appear right after previous child means the offset is supposed to be equal by (i-1)th childs offset augmented by its size in bytes.
                //thats why it is neccessary to calculate structures and substructures sizes right before offsets calculation
                if (childs[i].offset == null || childs[i].offset.bytes == null)
                {
                    offset off = new offset();
                    off.bytes = (int.Parse(childs[i - 1].offset.bytes) + int.Parse(childs[i - 1].size.bytes)).ToString();
                    childs[i].offset = off;
                }
                //if ith child has some substructures calculate their offset recursively begining from its offset
                if (childs[i].structureArray != null && childs[i].structureArray.structureArray != null && childs[i].structureArray.structureArray.Length > 0)
                    calculateOffsets(int.Parse(childs[i].offset.bytes), childs[i]);
            }
        }
    }
}


