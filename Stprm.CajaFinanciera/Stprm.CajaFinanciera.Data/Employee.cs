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
		
		private decimal _saldo;
		private DateTime _last_pay_date;
		private string _category_id;

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
			
			IDataReader reader = db.Query ("select * from {0} where concat(tra_nombre,  ' ', tra_apepaterno, ' ', tra_apematerno) like '{1}%'",
			                               TableEmployees, filter);
			
			while (reader.Read ()) {
				Employee employee = new Employee (db);
				employee.FillFromReader (reader);
				employees.Add (employee);
			}
			reader.Close ();
			
			return employees;
		}
		
		public static IDataAdapter GetCollectionInAdapter (Database db)
		{
			return db.QueryToAdapter ("select tra_id as Id, tra_ficha as Ficha, TRIM(CONCAT(tra_nombre, ' ', tra_apepaterno, ' ', tra_apematerno)) as Nombre, CONCAT('$', FORMAT(tra_saldo, 2)) as Saldo, CAST(if (tra_fechaultimopago='00000000','', DATE_FORMAT(tra_fechaultimopago, '%d/%m/%Y')) as CHAR) as FechaUltPago, cat_id as Categoria from {0} order by ficha asc", TableEmployees);
		}
		
		public static IDataAdapter GetCollectionForSearchingInAdapter (Database db)
		{
			return 	db.QueryToAdapter ("select tra_ficha as Ficha, TRIM(CONCAT(tra_nombre, ' ', tra_apepaterno, ' ', tra_apematerno)) as Nombre from {0} order by ficha asc", TableEmployees);
		}
				
		public PrestamoCollection GetPrestamos ()
		{
			PrestamoCollection prestamos = new PrestamoCollection ();
			
			IDataReader reader = Db.Query ("SELECT * FROM {0} where tra_id={1}",
			                               TablePrestamos, InternalId);
			
			while (reader.Read ()) {
				Prestamo prestamo = new Prestamo (Db);
				prestamo.FillFromReader (reader);
				
				prestamos.Add (prestamo);
			}
			reader.Close ();
			
			return prestamos;
		}
		
		public bool GetPrestamoFromPagare (Employee employee, string pagare, out Prestamo prestamo)
		{
			return Prestamo.GetFromPagare (Db, this, pagare, out prestamo);
		}
		
		public IDataAdapter GetPrestamosInAdapter ()
		{
			return Prestamo.GetCollectionForEmployee (Db, InternalId);	
		}
		
		public override bool Save ()
		{
			bool result = false;
			
			if (!Exists () ) {
				Db.NonQuery ("insert into {0} (tra_ficha) values ('{1}')", TableEmployees, Id);
				InternalId = GetLastInsertId ();
			}
			
			Saldo = GetAbono ();
			
			if (InternalId > 0) {
				Db.NonQuery ("UPDATE {0} SET tra_ficha='{1}', tra_nombre='{2}', tra_apepaterno='{3}', tra_apematerno='{4}', tra_saldo={5}, tra_fechaultimopago='{6}', cat_id='{7}' where tra_id={8}",
			             TableEmployees, Id, FirstName, MiddleName, LastName, Saldo, DateTimeToDbFormat (LastPayDate), CategoryId, InternalId);
				result = true;
			}
			
			return result;
		}
		
		public decimal GetAbono ()
		{
			decimal saldo = 0m;
			IDataReader reader = Db.Query ("select sum(pre_saldo) as saldo from {0} where tra_id = {1}",
			                               TablePrestamos, InternalId);
			
			if (reader.Read ()) {
				saldo = GetDecimal (reader, "saldo");
			}
			
			reader.Close ();
			return saldo;

		}
		
		public override bool Exists ()
		{
			Employee employee = new Employee (Db);
			employee.InternalId = InternalId;
				
			return employee.UpdateFromInternalId ();
		}

		
		public override bool Update ()
		{
			bool result = false;
			
			IDataReader reader = Db.Query ("SELECT * FROM {0} WHERE tra_ficha='{1}'",
			                               TableEmployees, Id);
			
			if (reader.Read ()) {
				FillFromReader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
		
		public bool UpdateFromInternalId ()
		{
			bool result = false;
			
			IDataReader reader = Db.Query ("SELECT * from {0} where tra_id={1}",
			                               TableEmployees, InternalId);
			
			if (reader.Read ()) {
				FillFromReader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
		
		public override void FillFromReader (IDataReader reader)
		{
			    InternalId = GetInt32 (reader, "tra_id");
				Id = GetString (reader, "tra_ficha");
				FirstName = GetString (reader, "tra_nombre");
				MiddleName = GetString (reader, "tra_apepaterno");
				LastName = GetString (reader, "tra_apematerno");
				Saldo = GetDecimal (reader,  "tra_saldo");
				LastPayDate = GetDateTime (reader, "tra_fechaultimopago");
				CategoryId = GetString (reader, "cat_id");
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
		
		public decimal Saldo {
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
		
		public string CategoryId {
			get { return _category_id; }
			set { 
				_category_id = value; 
				OnModified (); 
			}
		}		
	}
}
