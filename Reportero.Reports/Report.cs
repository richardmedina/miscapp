
using System;
using Reportero.Reports.Drawing;
using Reportero.Data;

namespace Reportero.Reports
{
	
	
	public class Report : Canvas
	{
		private DateTime _date_starting;
		private DateTime _date_ending;
		
		public Report (DateTime start, DateTime end)
		{
			StartingDate = start;
			EndingDate = end;
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
