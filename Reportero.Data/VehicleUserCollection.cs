
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
		
		public static VehicleUserCollection FromLeadership (Leadership leadership)
		{
			VehicleUserCollection users = new VehicleUserCollection (leadership.Db);
			
			IDataReader reader = users.Db.Query ("select * from Usuarios where Vehiculo like '{0}%' order by Vehiculo;",
				leadership.Name);
			
			while (reader.Read ()) {
				VehicleUser user = new VehicleUser (users.Db);
				user.Id = (string) reader ["Ficha"];
				user.Category = (string) reader ["Categoria"];
				user.Name = (string) reader ["Nombre"];
				user.VehicleId = (string) reader ["Vehiculo"];
				users.Add (user);
			}
			
			reader.Close ();
			
			return users;
		}
	}
}
