
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
			SetSizeRequest (640, 480);
			ModifyBg (StateType.Normal, new Gdk.Color (255, 255, 255));
		}
		
		protected virtual void OnPaint (Cairo.Context context)
		{
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			bool ret = base.OnExposeEvent (evnt);
			
			using (Cairo.Context ctx = Gdk.CairoHelper.Create (evnt.Window)) {
				OnPaint (ctx);
			}
			
			return ret;
		}

	}
}
