
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
		
		
		
		public static IDataAdapter GetMovimientosInAdapter (Prestamo prestamo)
		{
			
			return prestamo.Db.QueryToAdapter ("select  prem_id, CAST(DATE_FORMAT(prem_fecha,'%d/%m/%Y') as CHAR) as Fecha, prem_concepto as Concepto, CONCAT('$', FORMAT(prem_cargo, 2)) as Cargo, CONCAT('$', FORMAT(prem_abono, 2)) as Abono, CONCAT('$', FORMAT(prem_saldo, 2)) as Saldo from {0} where pre_id = {1} and tra_id = {2}",
			                                   TablePrestamoMovimientos, prestamo.Id, prestamo.TrabajadorInternalId);
		}
	}
}
