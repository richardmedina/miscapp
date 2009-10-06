
using System;
using Gtk;
using Reportero.Reports;
using Reportero.Data;

namespace Reportero.UI.Dialogs
{
	
	
	public class ActivityReportDialog : CustomDialog
	{
		
		private ActivityReport _canvas;
		
		private VehicleUser _vehicle;
		
		
		public ActivityReportDialog (VehicleUser vehicle, DateTime start, DateTime end)
		{
			_vehicle = vehicle;
			Title = AppSettings.Instance.GetFormatedTitle ("Reporte de Actividad");
			Resize (800, 650);
			
			_canvas = new ActivityReport (Vehicle, start, end);
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.AddWithViewport (_canvas);
			
			VBox.PackStart (scroll); 
			VBox.ShowAll ();
			
			AddButton (Stock.Close, ResponseType.Close);
		}
		
		public VehicleUser Vehicle {
			get{ return _vehicle; }
		}
	}
}
