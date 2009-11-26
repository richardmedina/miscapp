
using System;

namespace Reportero.Data
{
	
	
	public class DataCollection<T> : System.Collections.Generic.List <T>
	{
		private VehicleUser _vehicle;
		
		public DataCollection(VehicleUser vehicle)
		{
			_vehicle = vehicle;
		}
		
		public VehicleUser Vehicle {
			get { return _vehicle; }
			set { _vehicle = value; }
		}
	}
}
