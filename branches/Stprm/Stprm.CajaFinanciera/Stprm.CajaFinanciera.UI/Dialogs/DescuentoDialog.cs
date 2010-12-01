
using System;
using Gtk;

using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Widgets;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class DescuentoDialog : CustomDialog
	{
		private DescuentosListView _view_descuentos;

		public DescuentoDialog ()
		{
			Title = Globals.FormatWindowTitle ("Descuentos");
			_view_descuentos = new DescuentosListView ();
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_view_descuentos);
			
			VBox.PackStart (scroll);
			VBox.ShowAll ();
			
			AddButton (Stock.Close, ResponseType.Ok);
		}
		
		public void LoadDescuentos (Cobro cobro)
		{
			_view_descuentos.Load (cobro);
		}
	}
}
