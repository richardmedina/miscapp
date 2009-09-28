
using System;
using System.Collections;

namespace Reportero.Data
{
	
	
	public class RecordCollection<T> : System.Collections.Generic.List<T>
	{
		private Database _database;
		private string _name;
		
		public RecordCollection (Database database)
		{
			_database = database;
		}
		
		public Database Db {
			get { return _database; }
		}
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
	}
}
