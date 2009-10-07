
using System;
using Cairo;
using Pango;

namespace Reportero.Reports.Drawing
{
	
	
	public static class DrawingMisc
	{
		
		public static Pango.Layout ShowLayout (
			Cairo.Context context,
			Cairo.Color color,
			FontDescription fontdesc,
			double x, 
			double y, 
			double rotation_angle, 
			string format, 
			params object [] objs)
		{
			Pango.Layout layout = Pango.CairoHelper.CreateLayout (context);
			context.Save ();
			
			context.MoveTo (x, y);
			layout.FontDescription = fontdesc;
			layout.FontDescription.Weight = Weight.Bold;
			layout.SetText (string.Format (format, objs));
			
			//	string.Format ());
			
			context.Rotate (((2 * Math.PI) / 360) * rotation_angle);
			context.Color = color;
			//context.Color = new Cairo.Color (0.2, 0.5, 0.2);
			Pango.CairoHelper.ShowLayout (context, layout);
			
			context.Restore ();
			return layout;
		}
			
	}
}
