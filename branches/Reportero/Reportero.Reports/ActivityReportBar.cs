
using System;


using Reportero.Reports.Drawing;

namespace Reportero.Reports
{
	
	
	public class ActivityReportBar : Bar
	{
		private DateTime _date;
		private TimeSpan _moving_time;
		
		private static double _rootx = 50;
		private static double _rooty = 510;
		private static double _topx = 110;
		private static double _topy = 48;
		
		public ActivityReportBar (DateTime date, TimeSpan moving_time) :
			base (_topx, _topy, _rootx, _rooty)
		{
			_date = date;
			_moving_time = moving_time;
			
			double pixels = (moving_time.TotalMinutes*1.1);
			
			Y = Height - pixels;
			Height -= (Height -pixels);
			
			//StrokeColor = new SolidColorPattern (new Cairo.Color (0.0, 0.5, 0.0));
			//Stroked = true;
		}
		
		public DateTime Date {
			get { return _date; }
		}
		
		public TimeSpan MovingTime {
			get { return _moving_time; }
		}
	}
}
