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
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}
