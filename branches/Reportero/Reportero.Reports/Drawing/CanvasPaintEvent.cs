
using System;

namespace Reportero.Reports.Drawing
{
	
	
	public delegate void CanvasPaintEventHandler (object sender,
		CanvasPaintEventArgs args);
	
	
	public class CanvasPaintEventArgs : System.EventArgs
	{
		
		private Gdk.Pixmap _pixmap;
		
		public CanvasPaintEventArgs (Gdk.Pixmap pixmap)
		{
			_pixmap = pixmap;
		}
		
		public Gdk.Pixmap Pixmap {
			get { return _pixmap; }
		}
	}
}
