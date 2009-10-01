
using System;
using Cairo;

namespace Reportero.Reports.Drawing
{
	
	
	public class Shape
	{
		private double _line_width;
		private double _x; 
		private double _y;
		
		private IPattern _pattern;
		
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
		
		public IPattern Pattern {
			get { return _pattern; }
			set { _pattern = value; }
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
