
using System;

namespace Reportero.UI
{
	
	
	public class AppSettings
	{
		public static string DbHostname = "142.125.145.25";
		public static string DbUserid = "monitoreovehiculos";
		public static string DbPasword = "querty";
		public static string DbSource = "MonitoreoVehiculos";
		
		private static string title_format = "Reportero";
		
		public static string GetFormatedTitle (string window_title)
		{
			return string.Format ("{0} - {1}", window_title, title_format);
		}
	}
}
