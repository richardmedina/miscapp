
using System;
using Gtk;
using Artemis.UI;

namespace Artemis.Addins.Samples
{
	
	
	public class NotificationIcon
	{
		private Gtk.StatusIcon _icon;
		private Gdk.Pixbuf _pixbuf;
		
		private MainWindow _window;
		
		public NotificationIcon (MainWindow window)
		{
			_window = window;
			_pixbuf = IconFactory.LookupDefault (Stock.MediaPlay).RenderIcon (window.Style,
				TextDirection.Ltr, StateType.Normal, IconSize.Menu, window, string.Empty);
				
			_icon = new StatusIcon (_pixbuf);
			_icon.Activate += iconActivated;
			
			_icon.PopupMenu += delegate (object o, PopupMenuArgs args) {
				_window.Menubar.File.Popup ();
			};
			
			window.Shown += windowShown;
			
		}
		
		private void iconActivated (object sender, EventArgs args)
		{
				if (Window.Visible)
					Window.Hide ();
				else
					Window.Show ();
		}
		
		private void windowShown (object sender, EventArgs args)
		{
		}
		
		public StatusIcon StatusIcon {
			get { return _icon; }
		}
		
		public MainWindow Window {
			get { return  _window; }
		}
	}	
	
	public class myicon : Gtk.StatusIcon {
	
		public myicon ()
		{
			
		}
	}
}
