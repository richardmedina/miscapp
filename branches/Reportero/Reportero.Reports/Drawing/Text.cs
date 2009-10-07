
using System;
using Gdk;
using Cairo;
using Pango;

namespace Reportero.Reports.Drawing
{
	
	
	public class Text : Shape
	{
		
		private string _text = string.Empty;
		
		private Pango.FontDescription _fontdesc;
		
		private double _rotation_angle;
		
		public Text (string text, int x, int y)
		{
			_text = text;
			X = x;
			Y = y;
			Foreground = new SolidColorPattern (new Cairo.Color (0.2, 0.5, 0.2));
		}
		
		public override void Paint (CanvasPaintEventArgs args)
		{
			base.Paint (args);
			using (Cairo.Context context = Gdk.CairoHelper.Create (args.Pixmap)) {
				Pango.Layout layout = Pango.CairoHelper.CreateLayout (context);
				layout.FontDescription = Fontdescription;
				layout.SetText (TextString);
				if (Foreground.Type == PatternType.SolidColor)
					context.Color = (Foreground as SolidColorPattern).Color;
				if (Foreground.Type == PatternType.SolidGradient)
					context.Pattern = (Foreground as LinearGradientPattern).Gradient;
				
				context.MoveTo (X, Y);
				context.Save ();
				context.Rotate (((2 * Math.PI) / 360) * RotationAngle);
				Pango.CairoHelper.ShowLayout (context, layout);
				context.Restore ();
			}
		}

		
		public string TextString {
			get { return _text; }
			set { _text = value; }
		}
		
		public Pango.FontDescription Fontdescription {
			get {return _fontdesc; }
			set { _fontdesc = value; }
		}
		
		public double RotationAngle {
			get { return _rotation_angle; }
			set { _rotation_angle = value; }
		}
	}
}
