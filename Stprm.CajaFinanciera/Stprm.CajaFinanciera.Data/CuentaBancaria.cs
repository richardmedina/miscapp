
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class CuentaBancaria : Record
	{
		public int Id;
		public string Cuenta;
		public string Banco;
		public decimal Saldo;
		
		public CuentaBancaria (Database db) : base (db, RecordType.CuentaBancaria)
		{
		}
		
		public static CuentaBancariaCollection GetCollection (Database db)
		{
			CuentaBancariaCollection cuentas = new CuentaBancariaCollection();
			
			IDataReader reader = db.Query ("SELECT * from cuentas");
			
			while (reader.Read ()) {
				CuentaBancaria cuenta = new CuentaBancaria (db);
				cuenta.FillFromReader (reader);
				
				cuentas.Add (cuenta);
			}
			reader.Close ();
			
			return cuentas;
		}
		
		public override void FillFromReader (IDataReader reader)
		{
			Id = reader.GetInt32 (reader.GetOrdinal ("cue_id"));
			Cuenta = reader ["cue_numero"].ToString ();
			Banco = reader ["cue_banco"].ToString ();
			Saldo = reader.GetDecimal (reader.GetOrdinal ("cue_saldo"));
		}

	}
}
