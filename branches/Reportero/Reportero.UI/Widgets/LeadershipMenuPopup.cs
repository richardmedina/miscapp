
using System;
using Gtk;
using Reportero.UI.Dialogs;

namespace Reportero.UI.Widgets
{
	
	
	public class LeadershipMenuPopup : Gtk.Menu
	{
		private Gtk.ImageMenuItem _itm_explore;
		private Gtk.ImageMenuItem _itm_statistics;
		private Gtk.ImageMenuItem _itm_about;
		
		public LeadershipMenuPopup()
		{
			_itm_explore = new ImageMenuItem ("Ver Vehículos");
			_itm_explore.Image = new Image (Stock.Find, IconSize.Menu);
			
			_itm_statistics = new ImageMenuItem ("Indicadores de velocidad...");
			_itm_statistics.Image = new Image (
				Gdk.Pixbuf.LoadFromResource ("reportero_icon_statistics.png").ScaleSimple (
					18, 18, Gdk.InterpType.Bilinear));
			
			_itm_about = new ImageMenuItem ("Créditos...");
			_itm_about.Image = new Image (Stock.About, IconSize.Menu);
			
			Append (_itm_explore);
			Append (_itm_statistics);
			Append (new SeparatorMenuItem ());
			Append (_itm_about);
			
			ShowAll ();
		}
		
		public Gtk.ImageMenuItem ExploreItem {
			get{ return _itm_explore; }
		}
		
		public Gtk.ImageMenuItem StatisticsItem {
			get { return _itm_statistics; }
		}
		
		public Gtk.ImageMenuItem  AboutItem {
			get { return _itm_about; }
		}
	}
}
