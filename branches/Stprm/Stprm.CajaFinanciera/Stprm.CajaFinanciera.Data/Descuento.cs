
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class Descuento : Record
	{

		public Descuento (Database db) : base (db, RecordType.Descuento)
		{
		}
		
		public static IDataAdapter GetCollectionInAdapter (Database db, int cobro_id)
		{
			return db.QueryToAdapter ("select pre_folio as Folio, tra_ficha as Ficha, tra_nombrecompleto as Trabajador, TRUNCATE(prem_abono,2) as Importe, TRUNCATE(prem_abono/14,2) as 'Desc. Diario' from {0}, {1}, {2} where {2}.tra_id = {1}.tra_id AND {1}.pre_id = {0}.pre_id AND prem_abono > 0 and {0}.cob_id = {3}",
			                          TablePrestamoMovimientos, TablePrestamos, TableEmployees, cobro_id);
		}
	}
}
