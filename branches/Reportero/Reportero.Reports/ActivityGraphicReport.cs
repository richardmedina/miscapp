
using System;
using System.Threading;
using Reportero.Reports.Drawing;
using Reportero.Data;

namespace Reportero.Reports
{
	
	
	public class ActivityGraphicReport : Report
	{		
		private ShapeCollection _shapes;
		
		private VehicleUser _vehicle;
		private LoadingWindow _loader;
		
		private bool _canceled = false;
		
		public ActivityGraphicReport (VehicleUser vehicle) : 
			this (vehicle, DateTime.Now, DateTime.Now)
		{
		}
		
		public ActivityGraphicReport (VehicleUser vehicle, DateTime starting_date, DateTime ending_date) :
			base (starting_date, ending_date)
		{
			Vehicle = vehicle;
			_shapes = new ShapeCollection ();
			_loader = new LoadingWindow ();
			_loader.CancelButton.Clicked += delegate { _canceled = true; };
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
		
		protected override void OnShown ()
		{
			base.OnShown ();
			
			Thread thread = new Thread ((ThreadStart) delegate {
				_loader.AsyncUpdate (0);
				create_graphic_structure ();
			});
			thread.Start ();
		}
		
		private void create_graphic_structure ()
		{			
		
			// vertical line
			Line line = new Line (100, 20, 100, 510);
			// horizontal line
			Line line2 = new Line (100, 510, 700, 510);
			
			int days = (EndingDate - StartingDate).Days;
			
			ActivityReportBar bar = null;
			for (int i = 0; i <= days; i ++) {
				if (_canceled) {
					Shapes.Clear ();
					RunOnMainThread (delegate {
						_loader.Hide ();
					});
					break;
				}
				double percent = (((double) 100 / ((double) days+1)) * (double) i);
				_loader.AsyncUpdate ((int)percent);
				
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
			
			for (int i = 0; i < 7; i ++) {
				Shapes.Add (new Line (95, 510 - (66 * (i+1)), 105, 510- (66 * (i+1))));
			}
			
			RunOnMainThread (delegate {
				_loader.Hide ();
				_loader.Destroy ();
			});
		}
				
		public ShapeCollection Shapes {
			get { return _shapes; }
		}

		public VehicleUser Vehicle {
			get { return _vehicle; }
			set { _vehicle = value; }
		}

	}
}
