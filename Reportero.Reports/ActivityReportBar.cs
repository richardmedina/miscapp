
using System;


using Reportero.Reports.Drawing;

namespace Reportero.Reports
{
	
	
	public class ActivityReportBar : Bar
	{
		private DateTime _date;
		private TimeSpan _moving_time;
		
		private static double _rootx = 110;
		private static double _rooty = 462;
		private static double _topx = 150;
		private static double _topy = 48;
		
		public ActivityReportBar (DateTime date, TimeSpan moving_time) :
			base (_rootx, _topy, _topx, _rooty)
		{
			_date = date;
			_moving_time = moving_time;
			
			double pixels = (moving_time.TotalMinutes*1.1);
			
			Y = pixels;
			Height = Y;
			Y = _rooty - pixels;
			
			Console.WriteLine ("({0},{1})({2},{3})", X, Y, Width, Height);
		}
		
		public DateTime Date {
			get { return _date; }
		}
		
		public TimeSpan MovingTime {
			get { return _moving_time; }
		}
	}
}
