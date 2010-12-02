
using System;
using Gtk;

using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Widgets;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class DescuentoMovimientoDialog : CustomDialog
	{
		//private DescuentosListView _view_descuentos;
		
		private DescuentoMovimientosListView _view_descuentos;
		
		public DescuentoMovimientoDialog ()
		{
			Title = Globals.FormatWindowTitle ("Descuentos.");
			_view_descuentos = new DescuentoMovimientosListView ();
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_view_descuentos);
			
			VBox.PackStart (scroll);
			VBox.ShowAll ();
			
			AddButton (Stock.Close, ResponseType.Ok);
		}
		
		public void Load (Descuento descuento)
		{
			_view_descuentos.Load (descuento);
		}
	}
}
