
using System;

namespace Reportero.Data
{
	
	
	public class SpeedExceedItem
	{
		private VehicleUser _vehicle;
		private DateTime _date;
		private int _times;
		
		public SpeedExceedItem (VehicleUser vehicle) : this (vehicle, DateTime.Now, 0)
		{
		}
		
		public SpeedExceedItem (VehicleUser vehicle,  DateTime date, int times)
		{
			_vehicle = vehicle;
			_date = date;
			_times = times;
		}
		
		public DateTime Date {
			get { return _date; }
			set { _date = value; }
		}
		
		public int Times {
			get { return _times; }
			set { _times = value; }
		}
		
		public VehicleUser Vehicle {
			get { return _vehicle; }
		}
	}
}
