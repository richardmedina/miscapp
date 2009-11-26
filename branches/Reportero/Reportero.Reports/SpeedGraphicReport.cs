
using System;
using System.Threading;
using Gtk;
using Reportero.Data;
using Reportero.Reports.Drawing;

namespace Reportero.Reports
{
	
	
	public class SpeedGraphicReport : Report
	{
		
		private ShapeCollection _shapes;
		
		private VehicleUser _vehicle;
		private LoadingWindow _loader;
		
		private bool _canceled = false;
		
		public SpeedGraphicReport (VehicleUser vehicle) : 
			this (vehicle, DateTime.Now, DateTime.Now)
		{
		}
		
		public SpeedGraphicReport (VehicleUser vehicle, DateTime starting_date, DateTime ending_date) :
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
				//ctx.Target.WriteToPng ("/home/richard/Desktop/png.png");
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
		
		private bool creating_progress (int current, int total) {
			double unit = (double) 100 / (double) total;
			bool retval = false;
			
			RunOnMainThread (delegate {
				double percent = unit * (double) current;
				string text = string.Format ("{0}% ({1}/{2})", (int) percent, current, total, percent);
				_loader.Update (text, percent);
				if (_canceled) {
					Shapes.Clear ();
					_loader.Hide ();
					retval = true;
				}
			});
			return retval;
		}
		
		private void create_graphic_structure ()
		{	
		
			SpeedExceedCollection exceeds = Vehicle.GetSpeedOvertakenFromRange (StartingDate, EndingDate, creating_progress);
			
			Console.WriteLine ("Exceeds for {0} days and Count {1}", 
				(EndingDate - StartingDate).Days + 1,exceeds.Count);
			foreach (SpeedExceedItem exceed in exceeds)
				Console.WriteLine ("{0}: {1} Times.", exceed.Date, exceed.Times);
		
			// vertical line
			Line line = new Line (100, 20, 100, 510);
			// horizontal line
			Line line2 = new Line (100, 510, 700, 510);
			
			ActivityReportBar bar = null;
			int index = 0;
			foreach (SpeedExceedItem exceed in exceeds) {
				bar = new SpeedReportBar (index ++, exceed.Date, exceed.Times, exceed.Times.ToString ());
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
