
using System;
using System.Data;

namespace Reportero.Data
{
	
	
	public class VehicleUser
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
			IDataReader reader =_database.Query ("select ficha from Usuarios where Vehiculo='{0}';", _vehicle_id);
			
			bool exists = reader.Read ();
			reader.Close ();
			return exists;
		}
		
		public bool Update ()
		{
			bool updated = false;
			if (Exists ()) {
				IDataReader reader =_database.Query ("select * from usuarios where Vehiculo='{0}';", _vehicle_id);
				if (reader.Read ()) {
					_id = (string) reader ["Ficha"];
					_vehicle_id = (string) reader ["Vehiculo"];
					_category = (string) reader ["CategorIA"];
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
				_database.NonQuery ("update Usuarios SET Ficha='{0}', Categoria='{1}', Nombre='{2}' where Vehiculo='{3}';",
					_id, _category, _name, _vehicle_id);
			} else {
				_database.NonQuery ("insert into Usuarios (Ficha, Vehiculo, Categoria, Nombre) Values ('{0}', '{1}', '{2}', '{3}');", _id, _vehicle_id, _category, _name);
			}
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
		}
		
		public Database Database {
			get { return _database; }
		}
	}
}
