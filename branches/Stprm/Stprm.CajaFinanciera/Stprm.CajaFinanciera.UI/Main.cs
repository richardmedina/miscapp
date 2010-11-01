using System;
using Gtk;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			Globals.Db = Database.CreateStprmConnection ();
			MainWindow win = new MainWindow ();
			Globals.MainWindow = win;
			win.ShowAll ();
			Application.Run ();
		}
	}
}
