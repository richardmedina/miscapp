
using System;
using Gtk;
using Reportero.UI.Dialogs;

namespace Reportero.UI.Widgets
{
	
	
	public class HelpReportMenu : CustomMenu
	{
		private Gtk.ImageMenuItem _itm_themes;
		private Gtk.ImageMenuItem _itm_home;
		private Gtk.ImageMenuItem _itm_about;
		
		public HelpReportMenu () : base ("A_yuda")
		{
			_itm_themes = new Gtk.ImageMenuItem ("_Temas de ayuda...");
			_itm_themes.Image = new Image (Stock.Help, IconSize.Menu);
			_itm_home = new Gtk.ImageMenuItem ("_Website del proyecto...");
			_itm_home.Image = new Image (Stock.Home, IconSize.Menu);
			
			_itm_about = new Gtk.ImageMenuItem ("Créditos...", null);
			_itm_about.Image = new Image (Stock.About, IconSize.Menu);
			_itm_about.Activated += itm_aboutActivated;
			
			Append (_itm_themes);
			Append (_itm_home);
			Append (new SeparatorMenuItem ());
			Append (_itm_about);
		}
		
		private void itm_aboutActivated (object sender, EventArgs args)
		{
			ReporteroAboutDialog dialog = new ReporteroAboutDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}
		
		public Gtk.ImageMenuItem ThemesItem {
			get { return _itm_themes; }
		}
		
		public Gtk.ImageMenuItem HomeItem {
			get { return _itm_home; }
		}
		
		public Gtk.ImageMenuItem AboutItem {
			get { return _itm_about; }
		}
	}
}
