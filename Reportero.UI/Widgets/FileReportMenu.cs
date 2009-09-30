
using System;
using Gtk;

namespace Reportero.UI.Widgets
{
	
	
	public class FileReportMenu : CustomMenu
	{
		private Gtk.ImageMenuItem _itm_prefs;
		private Gtk.ImageMenuItem _itm_Quit;
		
		public FileReportMenu () : base ("_Archivo")
		{
			_itm_prefs = new ImageMenuItem ("Preferencias...");
			_itm_prefs.Image = new Image (Stock.Preferences, IconSize.Menu);
			
			_itm_Quit = new ImageMenuItem (Stock.Quit, null);
			_itm_Quit.Activated += _itm_QuitActivated;
			
			Append (_itm_prefs);
			Append (new SeparatorMenuItem ());
			Append (_itm_Quit);
		}
		
		private void _itm_QuitActivated (object sender, EventArgs args)
		{
			Application.Quit ();
		}
	}
}
