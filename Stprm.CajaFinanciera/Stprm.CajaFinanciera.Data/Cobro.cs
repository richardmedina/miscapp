
using System;
using System.Data;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class Cobro : Record
	{
		public int Id;
		public string Archivo;
		public int Periodo;
		public int Anio;
		public DateTime Fecha;
		public string Clave;
		public decimal Importe;
		public decimal ImporteCapital;
		public decimal ImporteInteres;
		public string Documento;
		public string Pagare;
		public int CuentaId;
		public int Count;
		
		public Cobro ()
		{
		}
		
		public static IDataAdapter GetcollectionInAdapter (Database db)
		{
			return db.QueryToAdapter ("select cob_id as Id, cob_clave as Clave, CONCAT (cob_periodo, '-',cob_anio) as Periodo, cob_fecha as Fecha from {0} where cob_clave <> '' order by Fecha desc",
			                          TableCobros);
		}
		
	}
}
