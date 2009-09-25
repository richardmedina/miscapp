
using System;
using System.Data;
using Gtk;

using Reportero.UI;
using Reportero.Data;

namespace Reportero
{
	
	
	public class MainClass
	{
		
		public static void Main ()
		{
			Application.Init ();
			MainWindow window = new MainWindow ();
			window.ShowAll ();
			
			Database db = new Database ();
			db.Open ();
			IDataReader reader = db.Query ("select top 10 alias from VehicleState;");
			Console.WriteLine ("End Query");
			Console.WriteLine ("Reading..");
			while (reader.Read ()) {
				Console.WriteLine ("Value: {0}", reader.GetString (0));
			}
			Console.WriteLine ("End Reading");
			
			Application.Run ();
		}
	}
}
