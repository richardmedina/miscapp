
using System;
using System.Data;

namespace Reportero.Data
{
	
	
	public class VehicleUser : IRecord
	{
		private string _id;
		private string _vehicle_id;
		private string _name;
		private string _category;
		private Database _database;
		
		public VehicleUser (Database db)
		{
			_database = db;
		}
		
		public bool Exists ()
		{
			if (_vehicle_id.Trim ().Length == 0)
				return false;
			IDataReader reader = Db.Query ("select ficha from Usuarios where Vehiculo='{0}';", _vehicle_id);
			
			bool exists = reader.Read ();
			reader.Close ();
			return exists;
		}
		
		public bool Update ()
		{
			bool updated = false;
			if (Exists ()) {
				IDataReader reader = Db.Query ("select * from usuarios where Vehiculo='{0}';", _vehicle_id);
				if (reader.Read ()) {
					_id = (string) reader ["Ficha"];
					_vehicle_id = (string) reader ["Vehiculo"];
					_category = (string) reader ["Categoria"];
					_name = (string) reader ["Nombre"];
					updated = true;
				}
				reader.Close ();
			}
			return updated;
		}
		
		public void Save ()
		{
			if (Exists ()) {
				Db.NonQuery ("update Usuarios SET Ficha='{0}', Categoria='{1}', Nombre='{2}' where Vehiculo='{3}';",
					Id, Category, Name, VehicleId);
			} else {
				Db.NonQuery ("insert into Usuarios (Ficha, Vehiculo, Categoria, Nombre) Values ('{0}', '{1}', '{2}', '{3}');", Id, VehicleId, Category, Name);
			}
		}
		
		public int GetMinutesRunning (DateTime date)
		{
			int minutes_running = 0;
			
			IDataReader reader = Db.Query ("select Count (Date_Time) * 3 as minutes from VehicleState where PC_Date='{0}' and alias='{1}' and Speed>0;",
				date.ToString ("yyyy-MM-dd"), VehicleId);
			
			if (reader.Read ())
				minutes_running = (int) reader ["minutes"];
			
			reader.Close ();
			
			return minutes_running;
		}
		
		public int GetMinutesAtSpeedExceeded (DateTime date)
		{
			int minutes = 0;
			
			//IDataReader reader = Db.Query ("select * from VehicleStateAdapatacion"
		}
		
		public string Id {
			get { return _id; }
			set { _id = value; }
		}
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		public string VehicleId {
			get { return _vehicle_id; }
			set { _vehicle_id = value; }
		}
		
		public string Category {
			get { return _category; }
			set { _category = value; }
		}
		
		public Database Db {
			get { return _database; }
		}
		
		public RecordType Type {
			get { return RecordType.VehicleUser; }
		}
	}
}