
using System;
using Gtk;

using Stprm.CajaFinanciera.UI.Widgets;


namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class PlazosDialog : CustomDialog
	{
		private PlazoListView _view_plazos;
		
		public PlazosDialog ()
		{
			Title = Globals.FormatWindowTitle ("Seleccionar plazo");
			Resize (320, 240);
			_view_plazos = new PlazoListView ();
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_view_plazos);
			
			VBox.PackStart (scroll);
			VBox.ShowAll ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		public PlazoListView ViewPlazos {
			get { return _view_plazos; }
		}
	}
}
