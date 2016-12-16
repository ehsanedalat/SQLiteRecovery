using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace iOSBackupLib
{
    public class iOSBackupUtil
    {
        private string backupfolderPath;
        private InfoReader info;
        private MbdbFile mbdbFile;
        private Dictionary<string, Dictionary<string, object>> allBackupdata;
        private string[] backupDirectoryPath = { @"\Apple Computer\MobileSync\Backup\",
        @"\Apple Computer\MobileSync\Backup\"};
        
        public iOSBackupUtil()
        {
            allBackupdata = new Dictionary<string, Dictionary<string, object>>();
            findBackupFolderAndFetchInfo();
        }

        private bool findBackupFolderAndFetchInfo()
        {
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            foreach(string s in backupDirectoryPath)
            {
                if (Directory.Exists(appdataPath + s))
                {
                    this.backupfolderPath = appdataPath + s;
                    break;
                }
            }
            string[] dirs = getBackupDirectories();
            if (dirs.Length == 0)
                return false;
            foreach (string dir in dirs)
            {
                info = new InfoReader(dir + @"\Info.plist");
                allBackupdata.Add(dir, info.deviceData);
            }
            return true;
        }

        public string[] getBackupDirectories()
        {
            return Directory.GetDirectories(this.backupfolderPath);
        }

        public Dictionary<string,object> getBackupInfo(string backupFolderName)
        {
            return allBackupdata[backupFolderName];
        }

        public string getBackupInfoInText(string backupFolderName)
        {
            var keys = allBackupdata[backupFolderName].Keys;
            string result = "\t";
            foreach(string s in keys)
            {
                result = result + s + " : " + allBackupdata[backupFolderName][s]+"\n\t";
            }
            return result;
        }

        public void extractDataFromBackup(string targetPath, string backupFolderName)
        {
            mbdbFile = new MbdbFile(backupFolderName + @"\Manifest.mbdb");
            mbdbFile.ReadFile();
            foreach (var Record in mbdbFile.MbdbRecords)
                Record.copyFile(backupFolderName, targetPath);
        }
    }
}
