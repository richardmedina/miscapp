
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
		
		public override bool Update ()
		{
			bool result = false;
			IDataReader reader = Db.Query ("SELECT cue_id, cue_numero, cue_banco, cue_saldo from {0} where cue_id = {1}",
			                               TableCuentasBancarias, Id);
			
			if (reader.Read ()) {
				FillFromReader (reader);
				result = true;
			}
			reader.Close ();
			return result;
		}
		
		public static CuentaBancariaCollection GetCollection (Database db)
		{
			CuentaBancariaCollection cuentas = new CuentaBancariaCollection();
			
			IDataReader reader = db.Query ("SELECT cue_id, cue_numero, cue_banco, cue_saldo from {0}", TableCuentasBancarias);
			
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
