
using System;
using Cairo;

namespace Reportero.Reports.Drawing
{
	
	
	public class Shape
	{
		private double _line_width = 1;
		private double _x; 
		private double _y;
		
		private double _width;
		private double _height;
		
		private IPattern _foreground;
		private IPattern _background;
		
		private bool _filled = false;
		private bool _stroked = true;
		
		public Shape ()
		{
		}
		
		public virtual void Paint (CanvasPaintEventArgs args)
		{
		}
		
		public double LineWidth {
			get { return _line_width; }
			set { _line_width = value; }
		}
		
		public double X {
			get { return _x; }
			set { _x = value; }
		}
		
		public double Y {
			get { return _y; }
			set { _y = value; }
		}
		
		public double Width {
			get { return _width; }
			set { _width = value; }
		}
		
		public double Height {
			get { return _height; }
			set { _height = value; }
		}
		
		public IPattern Foreground {
			get { return _foreground; }
			set { _foreground = value; }
		}
		
		public IPattern Background {
			get { return _background; }
			set { _background = value; }
		}
		
		public bool Stroked {	
			get { return _stroked; }
			set { _stroked = value; }
		}
		
		public bool Filled {
			get { return _filled; }
			set { _filled = true; }
		}
	}
}
