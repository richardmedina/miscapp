using System;
using Gtk;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			
			ParseArgs (args);		
			Application.Init ();
			
			//Globals.ViewResponsiveLoading = true;
			Globals.Db = new Database (Globals.DbHostname, Globals.DbUserId, Globals.DbPassword, Globals.DbName);
			Globals.Db.Open ();
			MainWindow win = new MainWindow ();
			Globals.MainWindow = win;
			win.ShowAll ();
			Application.Run ();
			//Globals.Db.Close ();
		}
		
		private static void ParseArgs (string [] args)
		{
			if (args.Length == 0) {
				Console.WriteLine ("Sintaxis:\n\tCajaFinanciera.exe [--dbhostname hostname] [--dbuserid userid] [--dbpassword password] [--dbname database_name]");
			} else for (int i = 0; i < args.Length; i ++) {
				switch (args [i]) {
					case "--dbhostname":
						Globals.DbHostname = args [i+1];
					break;
					
					case "--dbuserid":
						Globals.DbUserId = args [i +1];
					break;
					
					case "--dbpassword":
						Globals.DbPassword = args [i +1];
					break;
					
					case "--dbname":
						Globals.DbName = args [i +1];
					break;
				}
			}
	
		}
	}
}
