
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
		private Gtk.PrintOperation printing;
		
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
			
			printing = new PrintOperation ();
			printing.BeginPrint += printingBeginPrint;
			printing.DrawPage += printingDrawPage;
		}
		
		protected override void OnShown ()
		{
			base.OnShown ();
			//printing.Run (PrintOperationAction.PrintDialog, this);
		}
		
		private void printingBeginPrint (object o, BeginPrintArgs args)
		{
			printing.NPages = 1;
		}
		
		private void printingDrawPage (object sender, DrawPageArgs args)
		{
			using (Cairo.Context ctx = Gdk.CairoHelper.Create (_canvas.Pixmap)) {
				//args.Context.SetCairoContext (ctx, args.Context.DpiX, args.Context.DpiY);
				
				
			}
			//args.Context.
		}
		
		public VehicleUser Vehicle {
			get{ return _vehicle; }
		}
	}
}
