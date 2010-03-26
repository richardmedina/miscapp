
using System;
using System.IO;
using System.Xml.Serialization;

namespace Reportero.UI
{
	
	
	public class AppSettings
	{
		public string DbHostname = "142.125.145.25";
		public string DbUsername = "monitoreovehiculos";
		public string DbPasword = "Qwerty";
		public string DbSource = "MonitoreoVehiculos";
		
		public int RpBarText = 8;
		
		// Reports.
		public bool PdfRunOnGenerated = true;
		public string PdfAppLoader = "/usr/bin/evince";
		
		public string ReportHeaderCompany = "PEMEX EXPLORACION Y PRODUCCIÓN";
		public string ReportHeaderRegion = "Región Sur";
		public string ReportHeaderPlace = "Activo Integral Samaria-Luna";
		
		
		private string title_format = "Reportero";
		
		private static AppSettings _instance;
		private static object _obj = new object ();
		
		public bool EnableConfiguration = false;
		
		public string GetFormatedTitle (string window_title)
		{
			return string.Format ("{0} - {1}", window_title, title_format);
		}
		
		public void Serialize (string filename)
		{
			XmlSerializer serializer = new XmlSerializer (GetType ());
			using (StreamWriter writer = new StreamWriter (filename))
      			serializer.Serialize(writer, this);
		}
		
		public void Deserialize (string filename)
		{
			XmlSerializer serializer = new XmlSerializer (GetType ());
			AppSettings settings = new AppSettings ();
			try {
				using (StreamReader reader = new StreamReader (filename))
					settings = (AppSettings) serializer.Deserialize (reader);
			} catch (Exception exception) {
				Console.WriteLine ("Exception. {0}",
					exception.Message);
			}
			CopyFrom (settings);
		}
		
		public void CopyFrom (AppSettings settings)
		{
			DbHostname = settings.DbHostname;
			DbUsername = settings.DbUsername;
			DbPasword = settings.DbPasword;
			DbSource = settings.DbSource;
			
			ReportHeaderCompany = settings.ReportHeaderCompany;
			ReportHeaderRegion = settings.ReportHeaderRegion;
			ReportHeaderPlace = settings.ReportHeaderPlace;
			
			PdfRunOnGenerated = settings.PdfRunOnGenerated;
			PdfAppLoader = settings.PdfAppLoader;
		}
		/* Lets implement singleton pattern */
		public static AppSettings Instance {
			get {
					lock (_obj) {
						if (_instance == null)
							_instance = new AppSettings ();
						
						return _instance;
					 }
			}
		}
		
		public static string Filename {
			get { 
				return System.IO.Path.Combine (
					System.Environment.GetFolderPath (System.Environment.SpecialFolder.ApplicationData),
					"reportero_settings.xml"); 
			}
		}
	}
}
