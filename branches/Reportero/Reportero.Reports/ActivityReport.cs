
using System;
using Reportero.Reports.Drawing;

namespace Reportero.Reports
{
	
	
	public class ActivityReport : Canvas
	{
		private DateTime _date_starting;
		private DateTime _date_ending;
		
		private ShapeCollection _shapes;
		
		public ActivityReport () : this (DateTime.Now, DateTime.Now)
		{
		}
		
		public ActivityReport (DateTime starting_date, DateTime ending_date)
		{
			_date_starting = starting_date;
			_date_ending = ending_date;
			_shapes = new ShapeCollection ();
			
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
			/*
			using (Cairo.Context context = Gdk.CairoHelper.Create (expose_args.Window)) {
				//context.Rectangle (100, 10, Allocation.Width - 120, Allocation.Height - 20);
				context.MoveTo (100, 10);
				context.LineTo (100, 510);
				context.LineTo (700, 510);
				context.Color = new Cairo.Color (0.5, 0.5, 0.5);
				context.LineWidth = 0.5;
				context.Stroke ();
				
				//Cairo.Color color = new Cairo.Color (0.9, 0.2, 0.2);
			
				context.Rectangle (110, 450, 40, 60);
				context.MoveTo (110, 450);
				context.Save ();
			
				Cairo.Gradient pattern = new Cairo.LinearGradient (110, 450, 150, 450);
				pattern.AddColorStop (0, new Cairo.Color (255, 0, 0));
				pattern.AddColorStop (1, new Cairo.Color (0, 0, 0));
				context.Pattern = pattern;
			
				context.FillPreserve ();
				context.Restore ();
				context.Color = new Cairo.Color (0, 0, 0);
				context.Stroke ();
				
				//context.MoveTo (90, 160);
				
				
				// Remember. Angle must be given in Radians.
				// Take care 360° = 2Math.PI radians
				// so we have 30°= 
				
				context.Arc (90, 160, 4, 0, (2*Math.PI));
				context.LineWidth = 2;
				context.Color = new Cairo.Color (0.5, 0.5, 0.5, 0.5);
				context.FillPreserve ();
				context.Stroke ();
				
				context.MoveTo (82, 160);
				context.LineTo (98, 160);
				context.Color = new Cairo.Color (0.5, 0.5, 0.5, 0.5);
				context.Stroke ();
				
			}
			*/
		}
		
		private void create_graphic_structure ()
		{
			// vertical line
			Line line = new Line (100, 20, 100, 510);
			// horizontal line
			Line line2 = new Line (100, 510, 700, 510);
			
			//((SolidColorPattern)line.Pattern).Color = new Cairo.Color (1, 0, 0, 0.5);
			
			_shapes.Add (line);
			_shapes.Add (line2);
			
			
			/*
			_shapes.Add (new Bar (
				new Cairo.Color (0.5, 0.5, 1), 
				new Cairo.Color (0, 0, 0), 
				110, 100, 30, 100));
				
			_shapes.Add (new Bar (110, 200, 30, 310));
			
			_shapes.Add (new Bar (
				new Cairo.Color (0.8, 0.8, 0.8), 
				new Cairo.Color (0, 0, 0), 
				150, 100, 30, 200));
			*/
			//_shapes.Add (new Bar (150, 300, 30, 210));
			Shapes.Add (new ActivityReportBar (DateTime.Now, TimeSpan.FromMinutes (300)));
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
	}
}
