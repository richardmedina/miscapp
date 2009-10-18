
using System;
using System.Data;

namespace Reportero.Data
{
	
	
	public class Leadership : IRecord
	{
		private string _name;
		private Database _database;
		public Leadership (Database database)
		{
			_database = database;
		}
		
		// in order its anatomy, IRecord interface
		// can't be implemented. The internal system
		// does not possible.
		public void Save ()
		{
			
		}
		
		public bool Update ()
		{
			return false;
		}
		
		public bool Exists ()
		{
			return true;
		}
		
		public static LeadershipCollection FromDatabase (Database database)
		{
			LeadershipCollection leadcol = new LeadershipCollection (database);
			IDataReader reader = database.Query ("select distinct substring(Alias, 0, charindex('-',Alias)) as leadership from VehicleState where Alias <> '' order by Leadership;");
			
			while (reader.Read ()) {
				Leadership leader = new Leadership (database);
				leader.Name = (string) reader ["leadership"];
				leadcol.Add (leader);
			}
			
			reader.Close ();
			
			return leadcol;
		}
		
		public VehicleUserCollection GetVehicles ()
		{
			VehicleUserCollection vehicles = new VehicleUserCollection (Db);
			
			IDataReader reader = Db.Query ("select * from Usuarios where Vehiculo like '{0}%' order by Vehiculo;",
				Name);
			
			while (reader.Read ()) {
				VehicleUser vehicle = new VehicleUser (Db);
				vehicle.Id = (string) reader ["Ficha"];
				vehicle.Category = (string) reader ["Categoria"];
				vehicle.Name = (string) reader ["Nombre"];
				vehicle.VehicleId = (string) reader ["Vehiculo"];
				vehicles.Add (vehicle);
			}
			
			reader.Close ();
			
			return vehicles;
		}
		
		public RecordType Type {
			get { return RecordType.Leadership; }
		}
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		public Database Db {
			get { return _database; }
		}
	}
}
