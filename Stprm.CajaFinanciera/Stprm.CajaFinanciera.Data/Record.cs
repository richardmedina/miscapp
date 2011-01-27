using System;
using System.Data;
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
		public static readonly string TablePrestamos = "prestamos";
		public static readonly string TablePrestamoMovimientos = "prestamos_mov";
		public static readonly string TableCobros = "cobros";
		public static readonly string TableCuentasBancarias = "cuentas";
		public static readonly string TablePlazos = "plazos";
		public static readonly string TableCategorias = "categorias";
		public static readonly string TableDescuentos = "descuentos";
		public static readonly string TableDescuentoMovimientos = "descuentos_mov";
		

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
		
		public virtual void FillFromReader (IDataReader reader)
		{
		}
		
		public virtual int GetLastInsertId ()
		{
			int id = 0;
			IDataReader reader = Db.Query ("select last_insert_id() as id");
			
			if (reader.Read ()) {
				id = GetInt32 (reader, "id");//reader.GetInt32 (reader.GetOrdinal ("id"));	
			}
			reader.Close ();
			
			return id;
		}
		
		public string DateTimeToDbFormat (DateTime date)
		{
			return date == DateTime.MinValue ? "00000000" : date.ToString ("yyyyMMdd");
		}
		
		// gets an array for special fields of each record
		public string [] GetRecordUpdateFields ()
		{
			string [] fields = new string [0];
			
			switch (Type) {
				case RecordType.Categoria:
					fields = new string [] {"categorias", "cat_creadopor", "cat_creadofec", "cat_modifipor", "cat_modififec"};
				break;
				
				case RecordType.Employee:
					fields =new string [] {"trabajadores", "tra_creadopor", "tra_creadofec", "tra_modifipor", "tra_modififec"};
				break;
			}
			
			return fields;
		}
		
		public virtual void SaveSpecialRecordInfo (int createdby_id, DateTime createdby_date, int modifyby_id, DateTime modifyby_date)
		{
			//string [] fields = GetRecordUpdateFields ();
			
			if (createdby_id != 0) {
				/*Db.NonQuery ("UPDATE {0} SET {1}={2}, {3}={4}",
				             fields [0], fields[1], */
			}
		}
		
		protected virtual void OnModified ()
		{
			_modified (this, EventArgs.Empty);	
		}
		
		protected static string GetString (IDataReader reader, string field_name)
		{
			return reader.IsDBNull (reader.GetOrdinal (field_name)) ? string.Empty : reader.GetString (reader.GetOrdinal (field_name));
		}
		
		protected static DateTime GetDateTime (IDataReader reader, string field_name)
		{
			DateTime date = DateTime.MinValue;
			try {
				string date_str = reader [field_name].ToString ();
				
				if (!date_str.StartsWith ("0/0/0000"))
					date = reader.IsDBNull (reader.GetOrdinal (field_name)) ? DateTime.MinValue : reader.GetDateTime (reader.GetOrdinal (field_name));
			} catch (Exception exception) {	
				Console.Write ("Field ({0}).Excepcion atrapada mientras DateTime conversion : {1}", field_name, exception.Message);
			}
			
			//Console.WriteLine ("OK");
			return date;
		}
		
		public static int GetInt32 (IDataReader reader, string field_name)
		{
			return 	reader.IsDBNull (reader.GetOrdinal (field_name)) ? 0 : reader.GetInt32 (reader.GetOrdinal (field_name));
		}
		
		public static decimal GetDecimal (IDataReader reader, string field_name)
		{
			return reader.IsDBNull (reader.GetOrdinal (field_name)) ? 0 : reader.GetDecimal (reader.GetOrdinal (field_name));
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
