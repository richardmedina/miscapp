
using System;
using Gtk;
using Reportero.UI.Dialogs;

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
			_itm_prefs.Activated += itm_prefsActivated;
			
			_itm_Quit = new ImageMenuItem ("Salir");
			_itm_Quit.Activated += _itm_QuitActivated;
			_itm_Quit.Image = new Image (Stock.Quit, IconSize.Menu);
			
			//Append (_itm_prefs);
			//Append (new SeparatorMenuItem ());
			Append (_itm_Quit);
		}
		
		private void _itm_QuitActivated (object sender, EventArgs args)
		{
			Application.Quit ();
		}
		
		private void itm_prefsActivated (object sender, EventArgs args)
		{
			SettingsDialog dialog = new SettingsDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}
	}
}
