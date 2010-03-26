
using System;
using System.Data;
using Gtk;

using Reportero.UI;
using Reportero.Data;

namespace Reportero
{
	
	
	public class MainClass
	{
		
		public static void Main (string [] args)
		{
			foreach (string arg in args)
				if (arg == "-configure")
					AppSettings.Instance.EnableConfiguration = true;
		
			Application.Init ();
			AppSettings.Instance.Deserialize (AppSettings.Filename);
			MainWindow window = new MainWindow ();
			window.ShowAll ();
			Application.Run ();
			AppSettings.Instance.Serialize (AppSettings.Filename);
		}		
	}
}
