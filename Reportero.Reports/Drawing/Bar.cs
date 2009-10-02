
using System;
using Cairo;

namespace Reportero.Reports.Drawing
{
	
	
	public class Bar : Shape
	{
		
		private double _width;
		private double _height;
		
		private SolidColorPattern _stroke_color;
		
		public Bar (double x, double y, double width, double height) : 
			this (new Cairo.Color (((double)1/(double)255)*(double)0xA0, 0, 0), 
				new Cairo.Color (0, 0, 0),
				x, y, width, height)
		{ 
		}
		
		public Bar (Cairo.Color color1, Cairo.Color color2, double x, double y, double width, double height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			
			Gradient gradient = new Cairo.LinearGradient (X, Y, X+ Width, Y);
			gradient.AddColorStop (0, color1);
			gradient.AddColorStop (1, color2);
			
			//gradient.AddColorStop (2, new Cairo.Color (0, 0, 1));
			
			Pattern = new LinearGradientPattern (gradient);
			StrokeColor = new SolidColorPattern (new Color (0, 0, 0));
			
			Stroked = false;
			Filled = true;
		}
		
		public override void Paint (CanvasPaintEventArgs args)
		{
			base.Paint (args);
			
			using (Cairo.Context context = Gdk.CairoHelper.Create (args.ExposeArgs.Window))
			{
				context.MoveTo (X, Y);
				context.Rectangle (X, Y, Width, Height);
				
				if (Filled) {
					if (Pattern.Type == PatternType.SolidColor)
						context.Color = (Pattern as SolidColorPattern).Color;
					if (Pattern.Type == PatternType.SolidGradient)
						context.Pattern = (Pattern as LinearGradientPattern).Gradient;
					
					if (Stroked)
						context.FillPreserve ();
					else
						context.Fill ();
				}
				if (Stroked) {
					context.Color = StrokeColor.Color;
					context.Stroke ();
				}
				
			}
		}
		
		public double Width {
			get { return _width; }
			set { _width = value; }
		}
		
		public double Height {
			get { return _height; }
			set { _height = value; }
		}
		
		public SolidColorPattern StrokeColor {
			get { return _stroke_color; }
			set { _stroke_color = value; }
		}
	}
}
