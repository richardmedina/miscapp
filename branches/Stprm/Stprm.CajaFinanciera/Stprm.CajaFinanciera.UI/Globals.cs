
using System;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using Stprm.CajaFinanciera.Data;

using Gtk;

namespace Stprm.CajaFinanciera.UI
{

	
	public static class Globals
	{
		public static Database Db;
		public static MainWindow MainWindow;
		public static CuentaBancaria CuentaActual;
		
		public static string AppName = "MiStprm Caja Financiera";
		public static string AppVersion = "0.1b";
		
		public static bool ViewResponsiveLoading = false;
		
		public static int DiasCatorcenal = 14;
		
		public static string DbHostname = "localhost";
		public static string DbUserId = "ricki";
		public static string DbPassword = "09b9085a+";
		public static string DbName = "caja1";
		
		public static string CurrentHostname = string.Empty;
		
		public static string CurrentCultureName = "es-MX";
		
		public static void Init ()
		{
			CurrentHostname = Dns.GetHostName ();
			Console.WriteLine (CurrentHostname);
			
			Db = new Database (DbHostname, DbUserId, DbPassword, DbName);
			Console.Write ("*Connecting to database");
			Db.Open ();
			Console.WriteLine ("Done");
		}
		
		public static string FormatWindowTitle (string title)
		{
			return string.Format ("{0} - {1}",
			                      title, AppName);
		}
		
		public static void SaveSettings ()
		{
			
		}
		
		public static void LoadSettings ()
		{
		}
		
		public static string SelectSingleFileDialog (FileChooserAction action, string default_filename)
		{
			string title = FormatWindowTitle ("Seleccionar archivo");
			string filename = string.Empty;
			
			FileChooserDialog dialog = new FileChooserDialog (title,
			                                                  Globals.MainWindow,
			                                                  action);
			dialog.CurrentName = default_filename;
			
			string [] filter_name = {"Archivos de Pemex Corporativo (*.prn)", "Archivos de Pemex PEP (*.txt)", "Todos los archivos (*)"};
			string [] filter_pattern = {"*.prn", "*.txt", "*"};
			
			for (int i = 0; i < filter_name.Length; i ++) {
				FileFilter filter = new FileFilter ();
				filter.Name = filter_name [i];
				filter.AddPattern (filter_pattern [i]);
				dialog.AddFilter (filter);
			}
			
			dialog.AddButton (Stock.Cancel, ResponseType.Cancel);
			if (action == FileChooserAction.Save) {
				dialog.DoOverwriteConfirmation = true;
				dialog.AddButton (Stock.Save, ResponseType.Ok);
			} else if (action == FileChooserAction.Open) {
				dialog.AddButton (Stock.Open, ResponseType.Ok);	
			}
			
			if (dialog.Run () == (int) ResponseType.Ok) {
				filename = dialog.Filename;
			}
			
			dialog.Destroy ();
			
			return filename;
		}
	}
}
