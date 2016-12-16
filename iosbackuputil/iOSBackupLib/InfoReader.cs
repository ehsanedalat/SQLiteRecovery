using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;


namespace iOSBackupLib
{
    internal class InfoReader
    {
        internal Dictionary<string, object> deviceData { get; }
        internal InfoReader(string infoPath)
        {
            deviceData = new Dictionary<string, object>();
            buildDataDictionary(deviceData, infoPath);
        }

        private void buildDataDictionary(Dictionary<string, object> deviceData, string infoPath)
        {
            XmlDocument doc = new XmlDocument();
            string[] dir = Path.GetDirectoryName(infoPath).Split('\\');
            Debug.WriteLine(dir[dir.Length - 1]);
            if (!Directory.Exists(@".\" + dir[dir.Length - 1]))
                Directory.CreateDirectory(@".\" + dir[dir.Length - 1]);
            File.Copy(infoPath, @".\" + dir[dir.Length - 1]+ @"\Info.plist",true);
            MbdbRecord.humanReadablePlist(@".\" + dir[dir.Length - 1] + @"\Info.plist");
            
            doc.Load(@".\" + dir[dir.Length - 1] + @"\Info.plist");
            XmlNodeList list = doc.GetElementsByTagName("key");
            for (int i = 0; i < list.Count; i++)
            {
                switch (list[i].InnerText)
                {
                    case "Build Version":
                        Debug.WriteLine("Build Version: " + list[i].NextSibling.InnerText);
                        deviceData.Add("Build Version",list[i].NextSibling.InnerText);       
                        break;
                    case "Device Name":
                        Debug.WriteLine("Device Name: " + list[i].NextSibling.InnerText);
                        deviceData.Add("Device Name", list[i].NextSibling.InnerText);
                        break;
                    case "GUID":
                        Debug.WriteLine("GUID: " + list[i].NextSibling.InnerText);
                        deviceData.Add("GUID", list[i].NextSibling.InnerText);
                        break;
                    case "ICCID":
                        Debug.WriteLine("ICCID: " + list[i].NextSibling.InnerText);
                        deviceData.Add("ICCID", list[i].NextSibling.InnerText);
                        break;
                    case "IMEI":
                        Debug.WriteLine("IMEI: " + list[i].NextSibling.InnerText);
                        deviceData.Add("IMEI", list[i].NextSibling.InnerText);
                        break;
                    case "Installed Applications":
                        if (list[i].NextSibling.HasChildNodes)
                        {
                            XmlNode node = list[i].NextSibling.FirstChild;
                            List<string> apps = new List<string>();
                            do
                            {
                                Debug.WriteLine("Apps:" + node.InnerText);
                                apps.Add(node.InnerText);
                                node = node.NextSibling;
                            } while (!node.Equals(list[i].NextSibling.LastChild));
                            Debug.WriteLine("Apps:" + node.InnerText);
                            apps.Add(node.InnerText);
                            deviceData.Add("Apps", apps);
                        }
                        break;
                    case "Product Name":
                        Debug.WriteLine("Product Name: " + list[i].NextSibling.InnerText);
                        deviceData.Add("Product Name", list[i].NextSibling.InnerText);
                        break;
                    case "Product Type":
                        Debug.WriteLine("Product Type: " + list[i].NextSibling.InnerText);
                        deviceData.Add("Product Type", list[i].NextSibling.InnerText);
                        break;
                    case "Product Version":
                        Debug.WriteLine("Product Version: " + list[i].NextSibling.InnerText);
                        deviceData.Add("Product Version", list[i].NextSibling.InnerText);
                        break;
                    case "Serial Number":
                        Debug.WriteLine("Serial Number: " + list[i].NextSibling.InnerText);
                        deviceData.Add("Serial Number", list[i].NextSibling.InnerText);
                        break;
                }
            }


        }


    }
}
