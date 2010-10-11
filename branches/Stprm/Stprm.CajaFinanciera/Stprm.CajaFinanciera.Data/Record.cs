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
		
		private event EventHandler _modified;
		
		public Record (Database database, RecordType recordtype)
		{
			Db = database;
			Type = recordtype;
			_modified = onModified;
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
		
		protected virtual void OnModified ()
		{
			_modified (this, EventArgs.Empty);	
		}
		
		private void onModified (object sender, EventArgs args)
		{
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
		
		public event EventHandler Modified {
			add { _modified += value; }
			remove { _modified -= value; }
		}

		#endregion
		
	}
}
