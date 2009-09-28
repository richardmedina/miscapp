
using System;

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
