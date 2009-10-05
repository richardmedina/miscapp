
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
		
		private string title_format = "Reportero";
		
		private static AppSettings _instance;
		private static object _obj = new object ();
		
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
			} catch (FileNotFoundException exception) {
				Console.WriteLine ("Settings File '{0}' not found. It will be created..",
					exception.FileName);
			}
			Instance.CopyFrom (settings);
		}
		
		public void CopyFrom (AppSettings settings)
		{
			DbHostname = settings.DbHostname;
			DbUsername = settings.DbUsername;
			DbPasword = settings.DbPasword;
			DbSource = settings.DbSource;
		}
		
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
			get { return "settings.xml"; }
		}
	}
}
