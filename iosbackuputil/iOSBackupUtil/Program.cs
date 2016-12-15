using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iOSBackupLib;
using System.IO;

namespace iOSBackupUtil
{
	class Program
	{
		public static bool IsInDebugMode = System.Diagnostics.Debugger.IsAttached;

		static void Main(string[] args)
		{
			// A test "Manifest.mbdb" file is currently copied
			// into the working-directory prior to build.
			bool go = false;
			string[] dirs = null;
			int selection = -1;

			do
			{
				Console.WriteLine("******************************");
				Console.WriteLine("IOSBACKUPUTIL " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
				Console.WriteLine("******************************");
				Console.WriteLine("Please select a backup to analyze:");

				dirs = System.IO.Directory.GetDirectories(@"C:\Users\Ehsan\AppData\Roaming\Apple Computer\MobileSync\Backup");
				for (int i = 0; i < dirs.Length; i++)
				{
					Console.Write(i.ToString(new string('0', dirs.Length.ToString().Length)));
					Console.Write(") ");
					Console.Write(new DirectoryInfo(dirs[i]).LastWriteTime.ToString("yyyy-MM-dd"));
					Console.WriteLine(dirs[i].Substring(dirs[i].LastIndexOf(Path.DirectorySeparatorChar)));
				}

				Console.Write("Backup to analyze: ");
				string readVal = Console.ReadLine();

				Console.WriteLine();
				Console.WriteLine();

				selection = -1;
				go = (int.TryParse(readVal, out selection) && selection >= 0 && selection < dirs.Length);
			} while (go == false);

      string dbgFile = @"C:\Users\Ehsan\AppData\Roaming\Apple Computer\MobileSync\Backup\42b248ee727bb973106eeeaae9fae5785b25c184\Manifest.mbdb";
      MbdbFile mbdbFile = new MbdbFile(dirs[selection] + @"\Manifest.mbdb");
			mbdbFile.ReadFile();
            StreamWriter file = new StreamWriter(@"C:\Users\Ehsan\Desktop\Records.txt");

            foreach (var Record in mbdbFile.MbdbRecords)
                if (Record.FilenameAsHashExistsInBackupDirectory(dirs[selection]))
                    Record.writeInFile(dirs[selection], file);
            foreach (var Record in mbdbFile.MbdbRecords)
                Record.copyFile(dirs[selection], @"C:\Users\Ehsan\Desktop\");

                Console.WriteLine();
			Console.Write("Done, press ENTER to quit...");
			Console.ReadLine();
		}

		internal static string ByteArrayAsStringOfHexDigits(byte[] bytes)
		{
			return BitConverter.ToString(bytes);
		}
	}
}
