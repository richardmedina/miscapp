
using System;
using Gtk;
using Gdk;
using Cairo;

namespace Reportero.Reports.Drawing
{
	
	
	public class Canvas : Gtk.DrawingArea
	{
		
		private event CanvasPaintEventHandler _paint;
		
		private Gdk.Pixmap _pixmap = null;
		
		public Canvas ()
		{
			_paint = onPaint;
			SetSizeRequest (640, 480);
			ModifyBg (StateType.Normal, new Gdk.Color (255, 255, 255));
		}
		
		protected virtual void OnPaint (Gdk.Pixmap pixmap)
		{
			int width, height;
			pixmap.GetSize (out width, out height);
			
			using (Cairo.Context context = Gdk.CairoHelper.Create (pixmap)) {
				context.MoveTo (0, 0);
				context.Rectangle (0, 0, width, height);
				context.Color = new Cairo.Color (1, 1, 1);
				context.Fill ();
			}
			
			_paint (this, new CanvasPaintEventArgs (pixmap));
		}
		
		protected override bool OnConfigureEvent (Gdk.EventConfigure evnt)
		{
			bool ret = base.OnConfigureEvent (evnt);
			
			// precious double buffer
			_pixmap = new Pixmap (GdkWindow, Allocation.Width, Allocation.Height);
			OnPaint (_pixmap);
			
			return ret;
		}
		
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
		//	bool ret = base.OnExposeEvent (evnt);

			// Double buffer rules!
			evnt.Window.DrawDrawable (Style.WhiteGC,
				_pixmap,
				evnt.Area.X, evnt.Area.Y,
				evnt.Area.X, evnt.Area.Y,
				Allocation.Width, Allocation.Height);
			
			return false;
		}
		
		private void onPaint (object sender, CanvasPaintEventArgs args)
		{
		}
		
		public Gdk.Pixmap Pixmap {
			get { return _pixmap; }
		}
		
		public event CanvasPaintEventHandler Paint {
			add { _paint += value; }
			remove { _paint -= value; }
		}
	}
}
