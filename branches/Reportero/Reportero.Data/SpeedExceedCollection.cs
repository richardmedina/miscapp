
using System;
using System.Collections.Generic;

namespace Reportero.Data
{
	
	
	public class SpeedExceedCollection : DataCollection <SpeedExceedItem>
	{
		
		public SpeedExceedCollection (VehicleUser vehicle) : base (vehicle)
		{
		}
				
		public void OrderByDate ()
		{
			Sort (new Comparison <SpeedExceedItem> (DateComparison));
		}
		
		public void OrderByTimes ()
		{
			Sort (new Comparison <SpeedExceedItem> (TimesComparison));
		}
		
		
		private int DateComparison (SpeedExceedItem speed1, SpeedExceedItem speed2)
		{
			return speed1.Date.CompareTo (speed2.Date);
		}
		
		private int TimesComparison (SpeedExceedItem speed1, SpeedExceedItem speed2)
		{
			return speed1.Times.CompareTo (speed2.Times);
		}
	}
}
