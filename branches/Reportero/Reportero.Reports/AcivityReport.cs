
using System;

namespace Reportero.Reports
{
	
	
	public class ActivityReport : Drawing.Canvas
	{
		private DateTime _date_starting;
		private DateTime _date_ending;
		
		public ActivityReport (DateTime starting_date, DateTime ending_date)
		{
		}
		
		public DateTime StartingDate {
			get { return _date_starting; }
		}
		
		public DateTime EndingDate {
			get { return _date_ending; }
		}
	}
}
