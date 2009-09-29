
using System;
using Gtk;
using Gdk;
using Cairo;

namespace Reportero.Reports.Drawing
{
	
	
	public class Canvas : Gtk.DrawingArea
	{
		
		public Canvas ()
		{
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			bool ret = base.OnExposeEvent (evnt);
			
			using (Cairo.Context ctx = Gdk.CairoHelper.Create (evnt.Window)) {
			
				ctx.Rectangle (10, 10, 100, 100);
				ctx.Stroke ();
			}
			
			return ret;
		}

	}
}
