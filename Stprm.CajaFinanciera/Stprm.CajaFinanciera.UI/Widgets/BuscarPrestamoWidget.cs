
using System;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;
namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class BuscarPrestamoWidget : CustomVBox
	{
		
		private Gtk.Entry _entry_buscar;
		private BuscarPrestamoListView _view_buscarprestamo;
		
		public BuscarPrestamoWidget ()
		{
			_entry_buscar = new Gtk.Entry ();
			_entry_buscar.Changed += Handle_entry_buscarChanged;
			_entry_buscar.Activated += Handle_entry_buscarActivated;
			_view_buscarprestamo = new BuscarPrestamoListView ();
			
			Gtk.HBox hbox = new Gtk.HBox (false, 5);
			hbox.PackStart (Factory.Label ("Buscar"), false, false, 0);
			hbox.PackStart (_entry_buscar);
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_view_buscarprestamo);
			
			PackStart (hbox, false, false, 0);
			PackStart (new HSeparator (), false, false, 0);
			PackStart (scroll);
		}
		
		public bool GetSelectedPrestamo (out Prestamo prestamo)
		{
			return _view_buscarprestamo.GetSelectedPrestamo (out prestamo);	
		}

		private void Handle_entry_buscarChanged (object sender, EventArgs e)
		{
			_view_buscarprestamo.CurrentFilter = _entry_buscar.Text;
		}
		
		private void Handle_entry_buscarActivated (object sender, EventArgs args)
		{
			_view_buscarprestamo.SendActivated ();
		}
		
		public BuscarPrestamoListView BuscarPrestamoListView {
			get { return _view_buscarprestamo; }
		}
	}
}
