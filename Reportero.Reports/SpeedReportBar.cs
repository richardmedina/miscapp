
using System;
using Reportero.Reports.Drawing;

namespace Reportero.Reports
{
	
	
	public class SpeedReportBar : ActivityReportBar
	{
		
		public SpeedReportBar (int position, DateTime date, int times, string label) : 
			base (position, date, times, label)
		{
		}
	}
}
