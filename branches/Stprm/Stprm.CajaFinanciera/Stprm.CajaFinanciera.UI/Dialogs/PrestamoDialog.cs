
using System;
using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Widgets;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class PrestamoDialog : CustomDialog
	{
		private PrestamoDetallesWidget _pdw_prestamo;
		
		public PrestamoDialog ()
		{
			Title = Globals.FormatWindowTitle ("Prestamo");
			_pdw_prestamo = new PrestamoDetallesWidget ();
			
			VBox.PackStart (_pdw_prestamo);
			
			ShowAll ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Save, ResponseType.Ok);
		}
		
		public void UpdateFromPrestamo (Prestamo prestamo)
		{
			_pdw_prestamo.UpdateFromPrestamo (prestamo);
		}
	}
}
