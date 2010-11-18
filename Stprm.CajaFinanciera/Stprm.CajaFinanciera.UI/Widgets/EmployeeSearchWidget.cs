
using System;
using Gtk;
using RickiLib.Widgets;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class EmployeeSearchWidget : CustomVBox
	{
		
		private Gtk.Entry _entry_name;
		
		private EmployeeSearchView _view_search;
		
		public EmployeeSearchWidget ()
		{
			_entry_name = new Gtk.Entry ();
			_view_search = new EmployeeSearchView ();
			
			Gtk.HBox hbox = new Gtk.HBox (false, 5);
			hbox.PackStart (Factory.Label ("Nombre :"), false, false, 0);
			hbox.PackEnd (_entry_name);
			
			PackStart (hbox, false, false, 0);
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_view_search);
			PackStart (scroll);
		}
		
		public Entry SearchEntry {
			get { return _entry_name; }
		}
		
		public EmployeeSearchView SearchView {
			get{ return _view_search; }
		}
	}
}
