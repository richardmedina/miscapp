
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
		
		public int GetTimesSpeedOvertaken (DateTime date)
		{
			int times = 0;
			
			IDataReader reader = Db.Query (
			@"
			select count(ID) as times
			from VehicleStateAdaptacion 
			where 
			Alias = '{0}' 
			and Event = 17 
			and FlagAlarmed=0 
			and TiempoDifAnt=0
			and convert(varchar, InsertDate, 105) = '{1}';", VehicleId, date.ToString ("dd-MM-yyyy"));
			
			if (reader.Read ())
				times = (int) reader ["times"];
			reader.Close ();

			return times;
		}
		
		public SpeedExceedCollection GetSpeedOvertakenFromRange (DateTime date1, DateTime date2, ProgressCallback progress_callback)
		{
			SpeedExceedCollection exceeds = new SpeedExceedCollection (this);
			DateTime current_date = date1;
			int progress = 0;
			int total = (date2 - date1).Days + 1;
			
			while (current_date <= date2) {
				progress ++;
				if (progress_callback != null)
					if (!progress_callback (progress, total))
						break;
				
				int times = GetTimesSpeedOvertaken (current_date);
				
				//if (times > 0) {
					exceeds.Add (new SpeedExceedItem (this, current_date, times));
				//}
				current_date = current_date.AddDays (1);
			}
			return exceeds;
		}
		
		public SpeedExceedCollection GetSpeedOvertakenFromRangeWithNulls (DateTime date1, DateTime date2, ProgressCallback progress_callback)
		{
			throw new NotImplementedException ();
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
