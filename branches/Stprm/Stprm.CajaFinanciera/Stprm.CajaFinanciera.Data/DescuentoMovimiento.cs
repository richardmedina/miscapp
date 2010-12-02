
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class DescuentoMovimiento : Record
	{

		public DescuentoMovimiento (Database db) : base (db, RecordType.DescuentoMovimiento)
		{
		}
		
		public static IDataAdapter GetCollectionInAdapter (Descuento descuento)
		{
			return descuento.Db.QueryToAdapter ("select dem_folio_act as Folio, tra_ficha as Ficha, tra_nombrecompleto, dem_importe as Importe, dem_desc_diario as 'Desc. Diario' from descuentos_mov,trabajadores where trabajadores.tra_id = descuentos_mov.tra_id and desc_id={1}",
			                                    //"select * from {0} where desc_id = {1}",
			                                         TableDescuentoMovimientos, descuento.Id);
		}
	}
}
