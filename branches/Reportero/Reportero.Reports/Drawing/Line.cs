
using System;
using Gdk;
using Cairo;
namespace Reportero.Reports.Drawing
{
	
	
	public class Line : Shape
	{
		
		private double _x2;
		private double _y2;
		
		public Line () : this (0, 0, 0, 0)
		{
		}
		
		public Line (double x1, double y1, double x2, double y2)
		{
			X = x1;
			Y = y1;
			X2 = x2;
			Y2 = y2;
			Foreground = new SolidColorPattern (new Cairo.Color (0.5, 0.5, 0.5, 0.5));
		}
		
		public override void Paint (CanvasPaintEventArgs args)
		{
			using (Cairo.Context context = Gdk.CairoHelper.Create (args.Pixmap))
			{
				context.MoveTo (X, Y);
				context.LineTo (X2, Y2);
				if (Foreground.Type == PatternType.SolidColor)
					context.Color = (Foreground as SolidColorPattern).Color;
				context.Stroke ();
			}
		}

		
		public double X2 {
			get { return _x2; }
			set { _x2 = value; }
		}
		
		public double Y2 {
			get { return _y2; }
			set { _y2 = value; }
		}
	}
}
