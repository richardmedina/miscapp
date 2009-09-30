
using System;

namespace Reportero.Reports.Drawing
{
	
	
	public delegate void CanvasPaintEventHandler (object sender,
		CanvasPaintEventArgs args);
	
	
	public class CanvasPaintEventArgs : System.EventArgs
	{
		private Canvas _canvas;
		
		public CanvasPaintEventArgs (Canvas canvas)
		{
			_canvas = canvas;
		}
		
		public Canvas Canvas {
			get {return _canvas; }
		}
	}
}
