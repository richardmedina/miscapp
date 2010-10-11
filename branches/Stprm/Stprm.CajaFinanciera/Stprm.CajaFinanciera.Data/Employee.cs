using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{
	public class Employee : Record
	{
		private string _id;
		private string _firstname;
		private string _middlename;
		private string _lastname;

		public Employee (Database db) : base (db, RecordType.Employee)
		{
		}
		
		public string GetFullName ()
		{
			return string.Format ("{0} {1} {2}", FirstName, MiddleName, LastName);
		}
		
		public static EmployeeCollection GetStartingWith (Database db, string filter)
		{
			EmployeeCollection employees = new EmployeeCollection ();
			
			IDataReader reader = db.Query ("select  * from {0} where concat(tra_nombre,  ' ', tra_apepaterno, ' ', tra_apematerno) like '{1}%'",
			                               TableEmployees, filter);
			
			while (reader.Read ()) {
				Employee employee = new Employee (db);
				employee.fill_from_reader (reader);
				employees.Add (employee);
			}
			reader.Close ();
			
			return employees;
		}
		
		public override bool Update ()
		{
			bool result = false;
			
			IDataReader reader = Db.Query ("SELECT * FROM {0} WHERE tra_ficha='{1}'",
			                               TableEmployees, Id);
			
			if (reader.Read ()) {
				fill_from_reader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
		
		protected void fill_from_reader (IDataReader reader)
		{
				Id = reader ["tra_ficha"].ToString ();
				FirstName = reader ["tra_nombre"].ToString ();
				MiddleName = reader ["tra_apepaterno"].ToString ();
				LastName = reader ["tra_apematerno"].ToString ();
		}
		
		public string Id {
			get { return _id; }
			internal set { 
				_id = value; 
				OnModified ();
			}
		}
		
		public string FirstName {
			get { return _firstname; }
			set { 
				_firstname = value; 
				OnModified ();
			}
		}
		
		public string MiddleName {
			get { return _middlename; }
			set { 
				_middlename = value; 
				OnModified ();
			}
		}
		
		public string LastName {
				get { return _lastname; }
				set { 
					_lastname = value; 
					OnModified ();
			}
		}
	}
}
