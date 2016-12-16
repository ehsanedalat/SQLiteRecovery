using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace iOSBackupLib
{
	/// <summary>
	/// 
	/// </summary>
	public class MbdbRecord
	{
		private string _filenameAsHash = null;

		public string Domain = null;
		public string path = null;
		public string LinkTarget = null;
		public string DataHash = null;
		public string Unknown_I = null; // EncryptionKey
		public ushort Mode = 0; 
		public ulong iNodeLookup = 0; // iNode Lookup Number
		public uint UserId = 0;
		public uint GroupId = 0;
		public uint LastModifiedTime = 0;
		public uint LastAccessTime = 0;
		public uint CreationTime = 0;
		public ulong FileLength = 0;
		public byte ProtectionClass = 0;
		public byte PropertyCount = 0;
		public Dictionary<string, string> Properties = new Dictionary<string, string>();

		/// <summary>
		/// Initializes a new instance of the <see cref="MbdbRecord"/> class.
		/// </summary>
		public MbdbRecord() { }

		/// <summary>
		/// Gets the filename as hash.
		/// </summary>
		/// <value>The filename as hash.</value>
		public string FilenameAsHash
		{
			get
			{
				if (this.RecordMode != MbdbRecordFileMode.FILE)
					return "";
				if (_filenameAsHash != null)
					return _filenameAsHash;

				string fullFile = Domain + "-" + path;
				byte[] bfullFile = Encoding.UTF8.GetBytes(fullFile);
				byte[] bHash = new System.Security.Cryptography.SHA1CryptoServiceProvider().ComputeHash(bfullFile);

				_filenameAsHash = BitConverter.ToString(bHash).Replace("-", "").ToLower();

				return _filenameAsHash;
			}
		}

		/// <summary>
		/// Gets the record mode.
		/// </summary>
		/// <value>The record mode.</value>
		public MbdbRecordFileMode RecordMode
		{
			get
			{
				switch ((this.Mode & 0xF000) >> 12)
				{
					case 0xA:
						return MbdbRecordFileMode.LINK;
					case 0x4:
						return MbdbRecordFileMode.DIR;
					case 0x8:
						return MbdbRecordFileMode.FILE;
					default:
						return MbdbRecordFileMode.UNKNOWN;
				}
			}
		}

		/// <summary>
		/// Filenames as hash exists.
		/// </summary>
		/// <returns></returns>
		public bool FilenameAsHashExistsInBackupDirectory(string selectedBackupPath)
		{
			if (this.RecordMode != MbdbRecordFileMode.FILE)
				return false;
            string[]filesNames = System.IO.Directory.GetFiles(selectedBackupPath);
            
            foreach(string fileName in filesNames)
                if (fileName.Contains(this.FilenameAsHash))
                    return true;
            return false;
		}

		/// <summary>
		/// Prints this instance.
		/// </summary>
		public void PrintInConsole(String selectedBackupPath)
		{
			Console.WriteLine();
			Console.WriteLine("MBDB");
			Console.WriteLine("  Domain          : " + this.Domain);
			Console.WriteLine("  Path            : " + this.path);
			Console.WriteLine("  Link Target     : " + this.LinkTarget == "");
			Console.WriteLine("  Data Hash       : " + this.DataHash);
			Console.WriteLine("  Unknown I       : " + this.Unknown_I);
			Console.WriteLine("  File Mode       : " + this.RecordMode);
			Console.WriteLine("  Unknown II      : " + this.Unknown_I);
			Console.WriteLine("  User ID         : " + this.UserId.ToString());
			Console.WriteLine("  Group ID        : " + this.GroupId.ToString());
			Console.WriteLine("  Time I          : " + InternalUtilities.EpochTimeToString((int)this.LastModifiedTime));
			Console.WriteLine("  Time II         : " + InternalUtilities.EpochTimeToString((int)this.LastAccessTime));
			Console.WriteLine("  Time III        : " + InternalUtilities.EpochTimeToString((int)this.CreationTime));
			Console.WriteLine("  File Length     : " + this.FileLength.ToString());
			Console.WriteLine("  Flag            : " + this.ProtectionClass.ToString());
			Console.WriteLine("  Property Ct     : " + this.PropertyCount.ToString());
			Console.WriteLine("  Filename (Hash) : " + this.FilenameAsHash + " (" + this.FilenameAsHashExistsInBackupDirectory(selectedBackupPath) + ")");

			foreach (string propKey in this.Properties.Keys)
				Console.WriteLine("    " + propKey + " ==> " + this.Properties[propKey]);

			Console.WriteLine();
		}
        public void writeInFile(String selectedBackupPath, StreamWriter file)
        {
            file.WriteLine();
            file.WriteLine("MBDB");
            file.WriteLine("  Domain          : " + this.Domain);
            file.WriteLine("  Path            : " + this.path);
            file.WriteLine("  Link Target     : " + this.LinkTarget == "");
            file.WriteLine("  Data Hash       : " + this.DataHash);
            file.WriteLine("  Unknown I       : " + this.Unknown_I);
            file.WriteLine("  File Mode       : " + this.RecordMode);
            file.WriteLine("  Unknown II      : " + this.Unknown_I);
            file.WriteLine("  User ID         : " + this.UserId.ToString());
            file.WriteLine("  Group ID        : " + this.GroupId.ToString());
            file.WriteLine("  Time I          : " + InternalUtilities.EpochTimeToString((int)this.LastModifiedTime));
            file.WriteLine("  Time II         : " + InternalUtilities.EpochTimeToString((int)this.LastAccessTime));
            file.WriteLine("  Time III        : " + InternalUtilities.EpochTimeToString((int)this.CreationTime));
            file.WriteLine("  File Length     : " + this.FileLength.ToString());
            file.WriteLine("  Flag            : " + this.ProtectionClass.ToString());
            file.WriteLine("  Property Ct     : " + this.PropertyCount.ToString());
            file.WriteLine("  Filename (Hash) : " + this.FilenameAsHash + " (" + this.FilenameAsHashExistsInBackupDirectory(selectedBackupPath) + ")");

            foreach (string propKey in this.Properties.Keys)
                file.WriteLine("    " + propKey + " ==> " + this.Properties[propKey]);

            file.WriteLine("*********END of THIS RECORD*******************");
        }
        public bool copyFile(string selectedBackupPath, string targetPathDirectory)
        {
            if (this.FilenameAsHashExistsInBackupDirectory(selectedBackupPath))
            {
                //Build Directories
                string[] directories = (@"ExtractedFiles/" + this.Domain.Replace("-", "/") + "/" + this.path).Split(new char[] { '/' });
                for (int i = 0; i < directories.Length - 1; i++)
                {
                    if (!System.IO.Directory.Exists(targetPathDirectory + directories[i] + @"\"))
                    {
                        System.IO.Directory.CreateDirectory(targetPathDirectory + directories[i] + @"\");
                    }
                    targetPathDirectory = targetPathDirectory + directories[i] + @"\";
                }
                try
                {
                    string destinationFile = targetPathDirectory + directories[directories.Length - 1];
                    System.IO.File.Copy(selectedBackupPath + "\\" + this._filenameAsHash, destinationFile, true);

                    char[] sqlite = new char[] { 'S', 'Q', 'L', 'i', 't', 'e', ' ', 'f', 'o', 'r', 'm', 'a', 't', ' ', '3', '\0' };
                    byte[] buffer = new byte[16];
                    try
                    {
                        using (FileStream fs = new FileStream(destinationFile, FileMode.Open, FileAccess.Read))
                        {
                            fs.Read(buffer, 0, buffer.Length);
                            fs.Close();
                        }
                    }
                    catch (System.UnauthorizedAccessException ex)
                    {
                        Debug.Print(ex.Message);
                    }
                    bool isSqlite = true;
                    for (int i = 0; i < sqlite.Length; i++)
                    {
                        if (sqlite[i] != buffer[i])
                        {
                            isSqlite = false;
                        }
                    }
                    if (isSqlite)
                    {
                        File.Move(destinationFile, Path.ChangeExtension(destinationFile, ".sqlite"));
                        return true;
                    }
                    var proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = @"PlistConvertor\plutil.exe",
                            Arguments = "-lint " + destinationFile,
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                        }
                    };
                    proc.Start();
                    string result = "";
                    while (!proc.StandardOutput.EndOfStream)
                    {
                        result += proc.StandardOutput.ReadLine();
                    }
                    if (result.ToLower().Contains("ok"))
                    {
                        proc = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = @"PlistConvertor\plutil.exe",
                                Arguments = "-convert xml1 " + destinationFile,
                                UseShellExecute = false,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true
                            }
                        };
                        proc.Start();
                        File.Move(destinationFile, Path.ChangeExtension(destinationFile, ".plist"));
                        return true;
                    }
                }
                catch (FileNotFoundException)
                {
                    return false;
                }
            }
            return false;
        }
    }
}
