
using System;
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
		
		
		public static string FormatWindowTitle (string title)
		{
			return string.Format ("{0} - {1}",
			                      title, AppName);
		}
	}
}
