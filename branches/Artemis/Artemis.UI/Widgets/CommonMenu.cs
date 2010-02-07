
using System;
using Gtk;

namespace Artemis.UI.Widgets
{
	
	
	public class CommonMenu : Gtk.Menu
	{
		private Gtk.MenuItem _menuitem;
		private Gtk.AccelGroup _accel;
		
		public CommonMenu (string label, Gtk.AccelGroup accel)
		{
			_accel = accel;
		
			_menuitem = new MenuItem (label);
			_menuitem.Submenu = this;
		}
		
		public Gtk.MenuItem MenuItem {
			get { return _menuitem; }
		}
		
		public Gtk.AccelGroup Accel {
			get { return _accel; }
		}
	}
}
