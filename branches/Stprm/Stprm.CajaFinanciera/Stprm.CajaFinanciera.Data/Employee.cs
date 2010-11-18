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
		private int _internalid;
		
		private string _firstname;
		private string _middlename;
		private string _lastname;
		
		private double _saldo;
		private DateTime _last_pay_date;
		private string _category;

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
		
		public static IDataAdapter GetCollectionInAdapter (Database db)
		{
			return 	db.QueryToAdapter ("select tra_ficha as Ficha, TRIM(CONCAT(tra_nombre, ' ', tra_apepaterno, ' ', tra_apematerno)) as Nombre from trabajadores order by ficha asc");
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
				if (!int.TryParse (reader ["tra_id"].ToString (), out _internalid))
			    	InternalId = 0;
				Id = reader ["tra_ficha"].ToString ();
				FirstName = reader ["tra_nombre"].ToString ();
				MiddleName = reader ["tra_apepaterno"].ToString ();
				LastName = reader ["tra_apematerno"].ToString ();
				if (!double.TryParse (reader ["tra_saldo"].ToString (), out _saldo))
					Saldo = 0;
				
				if (!DateTime.TryParse (reader ["tra_fechaultimopago"].ToString (), out _last_pay_date))
					LastPayDate = DateTime.MinValue;
			
				Category = (string) reader ["cat_id"];
		}
		
		public override string ToString ()
		{
			return string.Format("[EmployeeDialog].InternalId = {0}, FirstName = {1}, MiddleName = {2}, LastName = {3}",
			                     InternalId, FirstName, MiddleName, LastName);
		}
		
		public int InternalId {
			get { return _internalid; }
			set { 
				_internalid = value; 
				OnModified ();
			}
		}
		
		public string Id {
			get { return _id; }
			set { 
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
		
		public double Saldo {
			get { return _saldo; }
			set { 
				_saldo = value; 
				OnModified ();
			}
		}
		
		public DateTime LastPayDate {
			get { return _last_pay_date; }
			set {
				_last_pay_date = value;
				OnModified ();
			}
		}
		
		public string Category {
			get { return _category; }
			set { 
				_category = value; 
				OnModified (); 
			}
		}		
	}
}
