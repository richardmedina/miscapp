
using System;
using System.Data;
using Gtk;

using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Widgets;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class BuscarPrestamoDialog : CustomDialog
	{

		private BuscarPrestamoWidget _bpw_prestamos;
		
		public BuscarPrestamoDialog ()
		{
			Title = Globals.FormatWindowTitle ("Buscar préstamo");
			Resize (420, 340);
			
			_bpw_prestamos = new BuscarPrestamoWidget ();
			_bpw_prestamos.BuscarPrestamoListView.Activated += Handle_bpw_prestamosBuscarPrestamoListViewActivated;
			VBox.PackStart (_bpw_prestamos);
			VBox.ShowAll ();
			
			_bpw_prestamos.BuscarPrestamoListView.Load ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}

		private void Handle_bpw_prestamosBuscarPrestamoListViewActivated (object sender, EventArgs e)
		{
			string [] fields;
			if (_bpw_prestamos.BuscarPrestamoListView.GetSelected (out fields)){
				Respond (ResponseType.Ok);	
			}
		}
		
		public bool GetSelectedPrestamo (out Prestamo prestamo)
		{
			return _bpw_prestamos.GetSelectedPrestamo (out prestamo);
		}
	}
}
