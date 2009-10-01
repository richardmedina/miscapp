
using System;
using Cairo;

namespace Reportero.Reports.Drawing
{
	
	
	public class LinearGradientPattern : IPattern
	{
		private Cairo.Gradient _gradient;
		
		public LinearGradientPattern (Cairo.Gradient gradient)
		{
			_gradient = gradient;
		}
		
		public Cairo.Gradient Gradient {
			get { return _gradient; }
			set { _gradient = value; }
		}
		
		public PatternType Type {
			get {return PatternType.SolidGradient; }
		}
	}
}
