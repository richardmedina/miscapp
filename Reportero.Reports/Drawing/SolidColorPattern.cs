
using System;
using Cairo;

namespace Reportero.Reports.Drawing
{
	
	
	public class SolidColorPattern : IPattern
	{
		private Cairo.Color _color;
		
		public SolidColorPattern (Cairo.Color color)
		{
			_color = color;
		}
		
		public Cairo.Color Color {
			get { return _color; }
			set { _color= value; }
		}
		
		public PatternType Type {
			get { return PatternType.SolidColor; }
		}
	}
}
