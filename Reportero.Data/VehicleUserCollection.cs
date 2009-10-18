
using System;
using System.Collections.Generic;
using System.Data;

namespace Reportero.Data
{
	
	
	public class VehicleUserCollection : RecordCollection <VehicleUser>
	{
		
		public VehicleUserCollection (Database database) : base (database)
		{
		}
	}
}
