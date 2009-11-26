
using System;

namespace Reportero.Data
{
	
	
	public class MinutesRunningItem
	{
		private DateTime _date;
		private int _minutes;
		
		public MinutesRunningItem () : this (DateTime.Now, 0)
		{
		}
		
		public MinutesRunningItem (DateTime date, int minutes)
		{
			_date = date;
			_minutes = minutes;
		}
		
		public DateTime Date {
			get { return _date; }
			set { _date = value; }
		}
		
		public int Minutes {
			get { return _minutes; }
			set { _minutes = value; }
		}
	}
}
