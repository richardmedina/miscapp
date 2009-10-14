
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
		//private ReportPrinter printer;
		private PrintOperation _printing;
		
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
			
			_printing = new PrintOperation ();
			_printing.BeginPrint += printingBeginPrint;
			_printing.DrawPage += printingDrawPage;
		}
		
		protected override void OnShown ()
		{
			base.OnShown ();
			_printing.Run (PrintOperationAction.PrintDialog, this);
		}
		
		private void printingBeginPrint (object sender, BeginPrintArgs args)
		{
			(sender as PrintOperation).NPages = 3;
		}
		
		private void printingDrawPage (object sender, DrawPageArgs args)
		{
			using (Cairo.Context cr = args.Context.CairoContext) {
				using (Cairo.Context ctx = Gdk.CairoHelper.Create (Canvas.Pixmap)) {
					ctx.Target.Show (cr, 0 ,0);
				}
			}
		}
				
		public VehicleUser Vehicle {
			get{ return _vehicle; }
		}
		
		public ActivityReport Canvas {
			get { return _canvas; }
		}
	}
}
