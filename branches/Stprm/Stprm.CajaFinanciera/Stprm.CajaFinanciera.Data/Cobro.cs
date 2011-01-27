
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
		
		public Cobro (Database db) : base (db, RecordType.Cobro)
		{
		}
		
		public static IDataAdapter GetCollectionInAdapter (Database db)
		{
			return db.QueryToAdapter ("select cob_id as Id, cob_clave as Clave, CONCAT (CAST(cob_periodo as CHAR), '-',CAST(cob_anio as CHAR)) as Periodo, CAST(DATE_FORMAT(cob_fecha,'%d/%m/%Y') as CHAR) as Fecha from {0} where cob_clave <> '' order by cob_fecha desc",
			                          TableCobros);
		}
		
		public IDataAdapter GetDescuentosInAdapter ()
		{
			return Descuento.GetCollectionInAdapter (Db);
		}
	}
}
