
using System;
using Gtk;
using Reportero.UI.Dialogs;

namespace Reportero.UI.Widgets
{
	
	
	public class VehicleMenuPopup : Gtk.Menu
	{
		private Gtk.ImageMenuItem _itm_assign;
		private Gtk.ImageMenuItem _itm_statistics;
		private Gtk.ImageMenuItem _itm_about;
		
		public VehicleMenuPopup ()
		{
			_itm_assign = new ImageMenuItem ("Asignar...");
			_itm_assign.Image = new Image (Gdk.Pixbuf.LoadFromResource ("reportero_icon_vehicle_assign.png").ScaleSimple (
					18, 18, Gdk.InterpType.Bilinear));
					
			_itm_statistics = new ImageMenuItem ("Estadísticas...");
			_itm_statistics.Image = new Image (Gdk.Pixbuf.LoadFromResource ("reportero_icon_statistics.png").ScaleSimple (
					18, 18, Gdk.InterpType.Bilinear));
				
			_itm_about = new ImageMenuItem (Stock.About, null);
			_itm_about.Activated += itm_AboutActivated;
			
			Append (_itm_assign);
			Append (new SeparatorMenuItem ());
			Append (_itm_statistics);
			Append (new SeparatorMenuItem ());
			Append (_itm_about);
			
			ShowAll ();
		}
		
		private void itm_AboutActivated (object sender, EventArgs args)
		{
			ReporteroAboutDialog dialog = new ReporteroAboutDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}
		
		public ImageMenuItem AssignItem {
			get { return _itm_assign; }
		}
		
		public ImageMenuItem StatisticsItem {
			get { return _itm_statistics; }
		}
		
		public ImageMenuItem AboutItem {
			get { return _itm_about; }
		}
	}
}
