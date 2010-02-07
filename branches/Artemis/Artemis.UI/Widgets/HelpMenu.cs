
using System;
using Gtk;
using Artemis.UI.Dialogs;

namespace Artemis.UI.Widgets
{
	
	
	public class HelpMenu : CommonMenu
	{
		
		private Gtk.ImageMenuItem _itm_themes;
		private Gtk.ImageMenuItem _itm_home;
		private Gtk.ImageMenuItem _itm_updates;
		private Gtk.ImageMenuItem _itm_about;
		
		public HelpMenu (AccelGroup accel) : base ("_Help", accel)
		{
			_itm_themes = new Gtk.ImageMenuItem ("Help themes...");
			_itm_themes.Image = Image.NewFromIconName (Stock.Help, IconSize.Menu);
			_itm_home = new Gtk.ImageMenuItem ("Artemis home site...");
			_itm_home.Image = Image.NewFromIconName (Stock.Home, IconSize.Menu);
			_itm_updates = new Gtk.ImageMenuItem ("Find for updates...");
			_itm_updates.Image = Image.NewFromIconName (Stock.Network, IconSize.Menu);
			_itm_about = new Gtk.ImageMenuItem (Stock.About, accel);
			_itm_about.Activated += delegate { ArtemisAboutDialog dialog = new ArtemisAboutDialog (); dialog.Run (); dialog.Destroy (); };
			
			Append (_itm_themes);
			Append (new SeparatorMenuItem ());
			Append (_itm_home);
			Append (_itm_updates);
			Append (new SeparatorMenuItem ());
			Append (_itm_about);
		}
	}
}
