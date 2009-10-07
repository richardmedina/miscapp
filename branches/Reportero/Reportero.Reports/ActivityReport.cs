
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
		
		protected override void OnPaint (Gdk.Pixmap pixmap)
		{
			base.OnPaint (pixmap);
			
			CanvasPaintEventArgs paint_args = new CanvasPaintEventArgs (pixmap);
			
			foreach (Shape shape in Shapes)
				shape.Paint (paint_args);
			
			using (Cairo.Context ctx = Gdk.CairoHelper.Create (pixmap)) {
				Gdk.Pixbuf buf = Gdk.Pixbuf.LoadFromResource ("reportero_icon_pep.png");
				
				Gdk.CairoHelper.SetSourcePixbuf (ctx, 
					buf,
					(Allocation.Width /2) - (buf.Width /2), 0);
				
				ctx.PaintWithAlpha (0.5);
				
			// Let's draw the reference values..
				for (int i = 0; i < 8; i ++) {
					DrawingMisc.ShowLayout (ctx,
						new Cairo.Color (0.5, 0.5, 0.5, 0.5),
						Pango.FontDescription.FromString ("8"),
						60, 510 - (66 * (i)) - 9, 0,
						"{0} hrs", i);
						
					
				}
				ctx.Target.WriteToPng ("/home/richard/Desktop/png.png");
			}
		}
		
		private void create_graphic_structure ()
		{
			// vertical line
			Line line = new Line (100, 20, 100, 510);
			// horizontal line
			Line line2 = new Line (100, 510, 700, 510);
			
			for (int i = 0; i < 7; i ++) {
				Shapes.Add (new Line (95, 510 - (66 * (i+1)), 105, 510- (66 * (i+1))));
			}
			
			int days = (EndingDate - StartingDate).Days;
			
			ActivityReportBar bar = null;
			for (int i = 0; i <= days; i ++) {
				DateTime date = StartingDate.AddDays (i);
				int minutes = Vehicle.GetMinutesRunning (date);
				
				bar = new ActivityReportBar (i, date, TimeSpan.FromMinutes (minutes));
				Shapes.Add (bar);
			}
			
			if (bar != null) {
				SetSizeRequest ((int) (bar.X + bar.Width + 50), Allocation.Height);
				line2.X2 = (int) (bar.X + bar.Width);
			}
			
			line2.X2 += 50;
			
			Shapes.Add (line);
			Shapes.Add (line2);
		/*	
			Shapes.Add (new Rectangle (10, 10, 100, 100));
			Text text = new Text ("Hello", 10, 10);
			text.X = 100;
			text.Y = 50;
			text.RotationAngle = 270;
			Shapes.Add (text);*/
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
