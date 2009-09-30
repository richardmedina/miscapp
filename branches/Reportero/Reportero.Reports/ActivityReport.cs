
using System;
using Reportero.Reports.Drawing;

namespace Reportero.Reports
{
	
	
	public class ActivityReport : Canvas
	{
		private DateTime _date_starting;
		private DateTime _date_ending;
		
		public ActivityReport (DateTime starting_date, DateTime ending_date)
		{
		}
		
		protected override void OnPaint (Cairo.Context context)
		{
			base.OnPaint (context);
			
			//context.Rectangle (100, 10, Allocation.Width - 120, Allocation.Height - 20);
			context.MoveTo (100, 10);
			context.LineTo (100, 510);
			context.LineTo (700, 510);
			context.Color = new Cairo.Color (0.5, 0.5, 0.5);
			context.LineWidth = 0.5;
			context.Stroke ();
			//Cairo.Color color = new Cairo.Color (0.9, 0.2, 0.2);
			
			context.Rectangle (110, 450, 40, 60);
			context.MoveTo (110, 450);
			context.Save ();
			
			Cairo.Gradient pattern = new Cairo.LinearGradient (110, 450, 150, 450);
			pattern.AddColorStop (0, new Cairo.Color (255, 0, 0));
			pattern.AddColorStop (1, new Cairo.Color (0, 0, 0));
			context.Pattern = pattern;
			
			context.FillPreserve ();
			context.Restore ();
			context.Color = new Cairo.Color (0, 0, 0);
			context.Stroke ();
		}

		public DateTime StartingDate {
			get { return _date_starting; }
		}
		
		public DateTime EndingDate {
			get { return _date_ending; }
		}
	}
}
