
using System;
using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Widgets;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class PrestamoDialog : CustomDialog
	{
		private Gtk.Notebook _notebook;
		private PrestamoDetallesWidget _pdw_prestamo;
		private MovimientosListView _lv_movimientos;
		
		public PrestamoDialog ()
		{
			Title = Globals.FormatWindowTitle ("Prestamo");
			_pdw_prestamo = new PrestamoDetallesWidget ();
			_lv_movimientos = new MovimientosListView ();
			
			_notebook = new Notebook ();
			_notebook.AppendPage (_pdw_prestamo, new Label ("Detalles"));
			
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_lv_movimientos);
			
			_notebook.AppendPage (scroll, new Label ("Movimientos"));
			
			VBox.PackStart (_notebook);
			
			ShowAll ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Save, ResponseType.Ok);
		}
		
		public void UpdateFromPrestamo (Prestamo prestamo)
		{
			_pdw_prestamo.UpdateFromPrestamo (prestamo);
			_lv_movimientos.LoadMovimientos (prestamo);
		}
	}
}
