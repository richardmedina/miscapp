using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{
	public class Employee : Record
	{
		public string Ficha;
		public string FirstName;
		public string MiddleName;
		public string LastName;
		
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
			                               TableEmployees, Ficha);
			
			if (reader.Read ()) {
				fill_from_reader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
		
		protected void fill_from_reader (IDataReader reader)
		{
				FirstName = reader ["tra_nombre"].ToString ();
				MiddleName = reader ["tra_apepaterno"].ToString ();
				LastName = reader ["tra_apematerno"].ToString ();
		}
	}
}
