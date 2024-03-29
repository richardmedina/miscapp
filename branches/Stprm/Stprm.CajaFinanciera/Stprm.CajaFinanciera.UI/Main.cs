using System;
using System.Threading;
using Gtk;
using Stprm.CajaFinanciera.Data;
using System.Globalization;

namespace Stprm.CajaFinanciera.UI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			
			ParseArgs (args);
			
			CultureInfo culture = new CultureInfo (Globals.CurrentCultureName, false);
			Thread.CurrentThread.CurrentCulture = culture;
			
			Application.Init ();
			
			//Globals.ViewResponsiveLoading = true;
			
			//Globals.Db = new Database (Globals.DbHostname, Globals.DbUserId, Globals.DbPassword, Globals.DbName);
			Console.WriteLine ("Connecting to database");
			//Globals.Db.Open ();
			Console.WriteLine ("Creating MainWindow");
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
						Globals.DbHostname = args [i + 1];
					break;
					
					case "--dbuserid":
						Globals.DbUserId = args [i + 1];
					break;
					
					case "--dbpassword":
						Globals.DbPassword = args [i + 1];
					break;
					
					case "--dbname":
						Globals.DbName = args [i + 1];
					break;
				}
			}
	
		}
	}
}
