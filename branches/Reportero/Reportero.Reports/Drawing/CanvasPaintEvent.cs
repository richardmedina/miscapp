
using System;

namespace Reportero.Reports.Drawing
{
	
	
	public delegate void CanvasPaintEventHandler (object sender,
		CanvasPaintEventArgs args);
	
	
	public class CanvasPaintEventArgs : System.EventArgs
	{
		private Gdk.EventExpose _expose_args;
		
		public CanvasPaintEventArgs (Gdk.EventExpose event_expose)
		{
			_expose_args = event_expose;
		}
		
		public Gdk.EventExpose ExposeArgs {
			get { return _expose_args; }
		}
	}
}
