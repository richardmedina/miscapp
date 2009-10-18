
using System;
using System.Data;
using Gtk;

using Reportero.UI;
using Reportero.Data;

namespace Reportero
{
	
	
	public class MainClass
	{
		/*
		static void Main ()
		{
			Database db = new Database ();
			db.Open ();
		
			foreach (Leadership lead in LeadershipCollection.FromDatabase (db)) {
				Console.WriteLine ("Usuarios dentro de Coordinacion '{0}'.", lead.Name);
				foreach (VehicleUser user in VehicleUserCollection.FromLeadership (lead)) {
					Console.WriteLine ("\t{0} -> {1}", user.Name, user.VehicleId);
				}
			}
			db.Close ();
		}
		*/
		
		public static void Main ()
		{
		
			Application.Init ();
			AppSettings.Instance.Deserialize (AppSettings.Filename);
			MainWindow window = new MainWindow ();
			window.ShowAll ();
			Application.Run ();
			AppSettings.Instance.Serialize (AppSettings.Filename);
		}		
	}
}
