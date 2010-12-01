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
			/*
			PrestamoCollection prestamos = Prestamo.GetCollection (Globals.Db);
			decimal total = 0;
			decimal desc_catorcenal = 0;
			
			foreach (Prestamo prestamo in prestamos) {
				Console.WriteLine ("Prestamo {0}: Capital {1}, Interes {2}, abonos {3},{4}",
				                   prestamo.Id, prestamo.Capital, prestamo.Interes, prestamo.Abono, prestamo.Cargo);
				total += prestamo.Capital;
				if (prestamo.AbonoCapital < prestamo.Capital)
					desc_catorcenal += prestamo.AbonoCapital + prestamo.AbonoInteres;
			}
			Console.WriteLine ("{0} prestamos, Capital: {1:C}, Desc.Catorcenal: {2:C}", prestamos.Count, total, desc_catorcenal);
			*/
			MainWindow win = new MainWindow ();
			Globals.MainWindow = win;
			win.ShowAll ();
			Application.Run ();
		}
	}
}
