
using System;

namespace Reportero.Data
{
	
	
	public class Leadership
	{
		private string _name;
		private Database _database;
		public Leadership (Database db)
		{
			_database = db;
		}
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		public Database Database {
			get { return _database; }
		}
	}
}
