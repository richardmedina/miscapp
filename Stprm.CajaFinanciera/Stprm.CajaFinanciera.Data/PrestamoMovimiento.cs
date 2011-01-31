
using System;
using System.Data;


namespace Stprm.CajaFinanciera.Data
{


	public class PrestamoMovimiento : Record
	{
		public int Id;
		public int PrestamoId;
		public int TrabajadorInternalId;
		public int CobroId;
		public DateTime Fecha;
		public string Concepto;
		public decimal Cargo;
		public decimal CargoCapital;
		public decimal CargoInteres;
		public decimal Abono;
		public decimal AbonoCapital;
		public decimal AbonoInteres;
		public decimal Saldo;

		
		public PrestamoMovimiento (Database db) : base (db, RecordType.PrestamoMovimiento)
		{
		}
		
		public override bool Save ()
		{
			bool result = false;
			if (!Exists ()) {
				try {
					Db.NonQuery ("Insert into {0} (pre_id) values ({1})",
					TablePrestamoMovimientos, PrestamoId);
				
					Id = GetLastInsertId ();
					result = true;
				} catch (Exception ex) {
					result = false;
					Console.WriteLine (ex);	
				}
			}
			
			if (Id > 0) {
				try {
					Console.WriteLine ("Updating PrestamoMovimiento");
					Db.NonQuery ("UPDATE {0} SET pre_id={1}, tra_id={2},cob_id={3},prem_fecha='{4}',prem_concepto='{5}',prem_cargo={6},prem_cargo_capital={7},prem_cargo_interes={8},prem_abono={9},prem_abono_capital={10},prem_abono_interes={11},prem_saldo={12} where prem_id ={13}",
					             TablePrestamoMovimientos, PrestamoId, TrabajadorInternalId, CobroId, DateTimeToDbFormat (Fecha), Concepto, Cargo, CargoCapital, CargoInteres, Abono, AbonoCapital, AbonoInteres, Saldo, Id);
				} catch (Exception ex) {
					Console.WriteLine (ex);
					result = false;
				}
			}
			
			return result;
		}
		
		public override bool Update ()
		{
			bool result = false;
			
			IDataReader reader = Db.Query ("select * from {0} where prem_id = {1}", 
			                               TablePrestamoMovimientos, Id);
			
			if (reader.Read ()) {
				FillFromReader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
		
		public override bool Exists ()
		{
			PrestamoMovimiento prestamo_movimiento = new PrestamoMovimiento (Db);
			prestamo_movimiento.Id = Id;
			
			return prestamo_movimiento.Update ();
		}
		
		public override void FillFromReader (IDataReader reader)
		{
			Id = GetInt32 (reader, "prem_id");
			PrestamoId = GetInt32 (reader, "pre_id");
			TrabajadorInternalId = GetInt32 (reader, "tra_id");
			CobroId = GetInt32 (reader, "cob_id");
			Fecha = GetDateTime (reader, "prem_Fecha");
			Concepto = GetString (reader, "prem_concepto");
			Cargo = GetDecimal (reader, "prem_cargo");
			CargoCapital = GetDecimal (reader, "prem_cargo_capital");
			CargoInteres = GetDecimal (reader, "prem_cargo_interes");
			Abono = GetDecimal (reader, "prem_abono");
			AbonoCapital = GetDecimal (reader, "prem_abono_capital");
			AbonoInteres = GetDecimal (reader, "prem_abono_interes");
			Saldo = GetDecimal (reader, "prem_saldo");
		}
		
		
		public static IDataAdapter GetPrestamoMovimientosInAdapter (Prestamo prestamo)
		{
			return prestamo.Db.QueryToAdapter ("select  prem_id, CAST(DATE_FORMAT(prem_fecha,'%d/%m/%Y') as CHAR) as Fecha, prem_concepto as Concepto, CONCAT('$', FORMAT(prem_cargo, 2)) as Cargo, CONCAT('$', FORMAT(prem_abono, 2)) as Abono, CONCAT('$', FORMAT(prem_saldo, 2)) as Saldo from {0} where pre_id = {1}  order by prem_fecha desc",
			                                   TablePrestamoMovimientos, prestamo.Id);
		}
	}
}
