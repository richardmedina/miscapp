
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
			return prestamo.Db.QueryToAdapter ("select * from {0} where pre_id = {1} and tra_id = {2}",
			                                   TablePrestamoMovimientos, prestamo.Id, prestamo.TrabajadorInternalId);
		}
	}
}
