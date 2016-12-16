using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iOSBackupLib;
using System.IO;

namespace iOSBackupUtilTest
{
	class Program
	{
		public static bool IsInDebugMode = System.Diagnostics.Debugger.IsAttached;

		static void Main(string[] args)
		{
			bool go = false;
			string[] dirs = null;
			int selection = -1;
            iOSBackupUtil util = new iOSBackupUtil();
			do
			{
				Console.WriteLine("******************************");
				Console.WriteLine("IOSBACKUPUTIL " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
				Console.WriteLine("******************************");
				Console.WriteLine("Please select a backup to analyze:");

                dirs = util.getBackupDirectories();
				for (int i = 0; i < dirs.Length; i++)
				{
					Console.Write(i.ToString(new string('0', dirs.Length.ToString().Length)));
					Console.Write(") ");
                    Console.WriteLine(dirs[i]);
                    Console.Write(util.getBackupInfoInText(dirs[i]));
					
				}

				Console.Write("Backup to analyze: ");
				string readVal = Console.ReadLine();

				Console.WriteLine();
				Console.WriteLine();

				selection = -1;
				go = (int.TryParse(readVal, out selection) && selection >= 0 && selection < dirs.Length);
			} while (go == false);

            util.extractDataFromBackup(@"C:\Users\Ehsan\Desktop", dirs[selection]);
      
		
            Console.WriteLine();
			Console.Write("Done, press ENTER to quit...");
			Console.ReadLine();
		}
	}
}
