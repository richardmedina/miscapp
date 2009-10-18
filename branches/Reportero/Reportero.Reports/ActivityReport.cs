
using System;
using Reportero.Reports.Drawing;

namespace Reportero.Reports
{
	
	
	public class ActivityReport : Canvas
	{
		private DateTime _date_starting;
		private DateTime _date_ending;
						
		public ActivityReport()
		{
		}
		
		public DateTime StartingDate {
			get { return _date_starting; }
			protected set { _date_starting = value; }
		}
		
		public DateTime EndingDate {
			get { return _date_ending; }
			protected set { _date_ending = value; }
		}

	}
}
