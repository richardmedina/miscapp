
using System;
using Cairo;

namespace Reportero.Reports.Drawing
{
	
	
	public class Rectangle : Shape
	{
		
		public Rectangle (double x, double y, double width, double height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			
			Background = new SolidColorPattern (new Color (0.5, 0.5, 0.5, 0.3));
			Foreground = new SolidColorPattern (new Color (0.2, 0.5, 0.2));
			
			Stroked = true;
			Filled = true;
		}
		
		public override void Paint (CanvasPaintEventArgs args)
		{
			base.Paint (args);
			
			using (Cairo.Context context = Gdk.CairoHelper.Create (args.Pixmap)) {
				context.Rectangle (X, Y, Width, Height);
				if (Filled) {
					if (Background.Type == PatternType.SolidColor)
						context.Color = (Background as SolidColorPattern).Color;
					if (Background.Type == PatternType.SolidGradient)
						context.Pattern = (Background as LinearGradientPattern).Gradient;
					
					if (Stroked)
						context.FillPreserve ();
					else
						context.Fill ();
				}
				if (Stroked) {
					if (Foreground.Type == PatternType.SolidColor)
						context.Color = (Foreground as SolidColorPattern).Color;
					if (Foreground.Type == PatternType.SolidGradient)
						context.Pattern = (Foreground as LinearGradientPattern).Gradient;
					
					context.Stroke ();
				}
			}
		}
	}
}
