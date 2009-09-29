
using System;
using Gtk;

namespace Reportero.UI.Widgets
{
	
	
	public class VehicleMenuPopup : Gtk.Menu
	{
		private Gtk.ImageMenuItem _itm_assign;
		private Gtk.ImageMenuItem _itm_statistics;
		//private Gtk.ImageMenuItem _itm_
		
		public VehicleMenuPopup ()
		{
			_itm_assign = new ImageMenuItem ("Asignar...");
			_itm_assign.Image = new Image (Gdk.Pixbuf.LoadFromResource ("reportero_icon_vehicle_assign.png").ScaleSimple (
					18, 18, Gdk.InterpType.Bilinear));
					
			_itm_statistics = new ImageMenuItem ("Estadísticas...");
			_itm_statistics.Image = new Image (Gdk.Pixbuf.LoadFromResource ("reportero_icon_statistics.png").ScaleSimple (
					18, 18, Gdk.InterpType.Bilinear));
			
			Append (_itm_assign);
			Append (new SeparatorMenuItem ());
			Append (_itm_statistics);
			
			ShowAll ();
		}
		
		public ImageMenuItem AssignItem {
			get { return _itm_assign; }
		}
		
		public ImageMenuItem StatisticsItem {
			get { return _itm_statistics; }
		}
	}
}
