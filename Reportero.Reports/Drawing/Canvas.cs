
using System;
using Gtk;
using Gdk;
using Cairo;

namespace Reportero.Reports.Drawing
{
	
	
	public class Canvas : Gtk.DrawingArea
	{
		
		private event CanvasPaintEventHandler _paint;
		
		public Canvas ()
		{
			_paint = onPaint;
			SetSizeRequest (640, 480);
			ModifyBg (StateType.Normal, new Gdk.Color (255, 255, 255));
		}
		
		protected virtual void OnPaint (Gdk.EventExpose expose_args)
		{
			_paint (this, new CanvasPaintEventArgs (expose_args));
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			bool ret = base.OnExposeEvent (evnt);

			OnPaint (evnt);
			
			return ret;
		}
		
		private void onPaint (object sender, CanvasPaintEventArgs args)
		{
		}
		
		public event CanvasPaintEventHandler Paint {
			add { _paint += value; }
			remove { _paint -= value; }
		}
	}
}
