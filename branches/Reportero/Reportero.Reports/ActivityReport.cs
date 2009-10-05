
using System;
using Reportero.Reports.Drawing;
using Reportero.Data;

namespace Reportero.Reports
{
	
	
	public class ActivityReport : Canvas
	{
		private DateTime _date_starting;
		private DateTime _date_ending;
		
		private ShapeCollection _shapes;
		
		private VehicleUser _vehicle;
		
		public ActivityReport (VehicleUser vehicle) : this (vehicle, DateTime.Now, DateTime.Now)
		{
		}
		
		public ActivityReport (VehicleUser vehicle, DateTime starting_date, DateTime ending_date)
		{
			_date_starting = starting_date;
			_date_ending = ending_date;
			_shapes = new ShapeCollection ();
			
			_vehicle = vehicle;
			
			create_graphic_structure ();
		}
		
		protected override void OnPaint (Gdk.EventExpose expose_args)
		{
			base.OnPaint (expose_args);
			
			CanvasPaintEventArgs paint_args = new CanvasPaintEventArgs (expose_args);
			
			foreach (Shape shape in Shapes)
				shape.Paint (paint_args);
			
			using (Cairo.Context ctx = Gdk.CairoHelper.Create (expose_args.Window)) {
				Gdk.Pixbuf buf = Gdk.Pixbuf.LoadFromResource ("reportero_icon_pep.png");
				
				Gdk.CairoHelper.SetSourcePixbuf (ctx, 
					buf,
					(Allocation.Width /2) - (buf.Width /2), 0);
				
				ctx.PaintWithAlpha (0.5);
				
				ctx.Target.WriteToPng ("/home/richard/Desktop/png.png");
			}
		}
		
		protected override void OnShown ()
		{
			base.OnShown ();
		}

		
		private void create_graphic_structure ()
		{
			// vertical line
			Line line = new Line (100, 20, 100, 510);
			// horizontal line
			Line line2 = new Line (100, 510, 700, 510);
			
			//((SolidColorPattern)line.Pattern).Color = new Cairo.Color (1, 0, 0, 0.5);
			
			int days = (EndingDate - StartingDate).Days;
			
			ActivityReportBar bar = null;
			for (int i = 0; i < days; i ++) {
				DateTime date = StartingDate.AddDays (i);
				//date.AddDays (i);
				int minutes = Vehicle.GetMinutesRunning (date);
			//	Console.WriteLine ("Minutes at {0} -> {1}", date.ToString ("dd-MM-yyyy"), minutes);
				
				bar = new ActivityReportBar (i, DateTime.Now, TimeSpan.FromMinutes (minutes));
				Shapes.Add (bar);
			}
			
			if (bar != null) {
				SetSizeRequest ((int) (bar.X + bar.Width), Allocation.Height);
				line2.X2 = (int) (bar.X + bar.Width);
			}
			
			_shapes.Add (line);
			_shapes.Add (line2);
		}

		public DateTime StartingDate {
			get { return _date_starting; }
		}
		
		public DateTime EndingDate {
			get { return _date_ending; }
		}
		
		public ShapeCollection Shapes {
			get { return _shapes; }
		}
		
		public VehicleUser Vehicle {
			get { return _vehicle; }
		}		
	}
}
