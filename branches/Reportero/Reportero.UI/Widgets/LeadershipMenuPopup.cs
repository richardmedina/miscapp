
using System;
using Gtk;
using Reportero.UI.Dialogs;

namespace Reportero.UI.Widgets
{
	
	
	public class LeadershipMenuPopup : Gtk.Menu
	{
		private Gtk.ImageMenuItem _itm_explore;
		
		private Gtk.ImageMenuItem _itm_stats;
		private Gtk.ImageMenuItem _itm_statistics;
		private Gtk.ImageMenuItem _itm_statistics_inac;
		private Gtk.ImageMenuItem _itm_statistics_speed;
		private Gtk.ImageMenuItem _itm_statistics_nospeed;
		private Gtk.ImageMenuItem _itm_about;
		
		public LeadershipMenuPopup()
		{
			_itm_explore = new ImageMenuItem ("Ver Vehículos");
			_itm_explore.Image = new Image (Stock.Find, IconSize.Menu);
			
			_itm_stats = new ImageMenuItem ("Estadísticas Generales...");
			_itm_stats.Image = new Image (
				Gdk.Pixbuf.LoadFromResource ("reportero_icon_statistics.png").ScaleSimple (
					18, 18, Gdk.InterpType.Bilinear));
			//_itm_stats.Sensitive = false;
			
			_itm_statistics = new ImageMenuItem ("Actividad de vehiculos...");
			_itm_statistics.Image = new Image (
				Gdk.Pixbuf.LoadFromResource ("reportero_icon_statistics.png").ScaleSimple (
					18, 18, Gdk.InterpType.Bilinear));
			
			_itm_statistics_inac = new ImageMenuItem ("Inactividad de vehiculos...");
			_itm_statistics_inac.Image = new Image (
				Gdk.Pixbuf.LoadFromResource ("reportero_icon_statistics.png").ScaleSimple (
					18, 18, Gdk.InterpType.Bilinear));
				
			_itm_statistics_speed = new ImageMenuItem ("Listado de excesos de velocidad...");
			_itm_statistics_speed.Image = new Image (
				Gdk.Pixbuf.LoadFromResource ("reportero_icon_80km.png").ScaleSimple (
					18, 18, Gdk.InterpType.Bilinear));
			
			_itm_statistics_nospeed = new ImageMenuItem ("Listado historial de excesos de velocidad...");
			_itm_statistics_nospeed.Image = new Image (
				Gdk.Pixbuf.LoadFromResource ("reportero_icon_80k.png").ScaleSimple (
					18, 18, Gdk.InterpType.Bilinear));
			
			_itm_about = new ImageMenuItem ("Créditos...");
			_itm_about.Image = new Image (Stock.About, IconSize.Menu);
			
			Append (_itm_explore);
			//Append (new SeparatorMenuItem ());
			Append (_itm_stats);
			Append (new SeparatorMenuItem ());
			Append (_itm_statistics);
			Append (_itm_statistics_inac);
			Append (new SeparatorMenuItem ());
			Append (_itm_statistics_speed);
			Append (_itm_statistics_nospeed);
			Append (new SeparatorMenuItem ());
			Append (_itm_about);
			
			ShowAll ();
		}
		
		public Gtk.ImageMenuItem ExploreItem {
			get{ return _itm_explore; }
		}
		
		public Gtk.ImageMenuItem StatsItem {
			get { return _itm_stats; }
		}
		
		public Gtk.ImageMenuItem StatisticsItem {
			get { return _itm_statistics; }
		}
		
		public Gtk.ImageMenuItem StatisticsInacItem {
			get { return _itm_statistics_inac; }
		}
		
		public Gtk.ImageMenuItem StatisticsSpeedItem {
			get { return _itm_statistics_speed; }
		}
		
		public Gtk.ImageMenuItem StatisticsNoSpeedItem {
			get { return _itm_statistics_nospeed; }
		}
		
		public Gtk.ImageMenuItem  AboutItem {
			get { return _itm_about; }
		}
	}
}
