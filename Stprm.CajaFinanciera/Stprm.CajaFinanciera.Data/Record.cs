using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stprm.CajaFinanciera.Data
{
	public class Record : IRecord
	{
		
		/****
		 * Table names
		 * ******/
		
		public static readonly string TableEmployees = "trabajadores";

		private Database _database;

		private RecordType _recordtype;
		
		public Record (Database database, RecordType recordtype)
		{
			Db = database;
			Type = recordtype;
		}
		
		#region IRecord Members


		public virtual bool Update()
		{
			throw new NotImplementedException();
		}

		public virtual bool Save()
		{
			throw new NotImplementedException();
		}

		public virtual bool Exists()
		{
			throw new NotImplementedException();
		}

		public Database Db
		{
			get { return _database; }
			set { _database = value; }
		}

		public virtual RecordType Type
		{
			get { return _recordtype; }
			protected set { _recordtype = value; }
		}

		#endregion
		
	}
}
