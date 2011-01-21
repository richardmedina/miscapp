
using System;
using System.Xml;
using System.Xml.Serialization;
using Stprm.CajaFinanciera.Data;
namespace Stprm.CajaFinanciera.UI
{

	
	public static class Globals
	{
		public static Database Db;
		public static MainWindow MainWindow;
		public static CuentaBancaria CuentaActual;
		
		public static string AppName = "Administración de Caja Financiera";
		public static string AppVersion = "0.1b";
		
		public static bool ViewResponsiveLoading = false;
		
		public static int DiasCatorcenal = 14;
		
		public static string DbHostname = "localhost";
		public static string DbUserId = "ricki";
		public static string DbPassword = "09b9085a+";
		public static string DbName = "caja2";
		
		
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
	}
}
