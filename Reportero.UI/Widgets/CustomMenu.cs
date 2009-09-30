
using System;
using Gtk;

namespace Reportero.UI.Widgets
{
	
	
	public class CustomMenu : Gtk.Menu
	{
		
		private Gtk.MenuItem _itm;
		
		public CustomMenu (string label)
		{
			_itm = new MenuItem (label);
			_itm.Submenu = this;
		}
		
		public new Gtk.MenuItem Item {
			get { return _itm; }
		}
	}
}
