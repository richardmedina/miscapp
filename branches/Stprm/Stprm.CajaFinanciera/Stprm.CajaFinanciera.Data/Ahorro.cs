
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class Ahorro : Record
	{
		public int Id;
		public int PlazoId;
		public int TrabajadorId;
		public string Folio;
		public string FolioOriginal;
		public DateTime Fecha;
		public DateTime FechaIniCobro;
		public DateTime FechaSusp;
		public decimal Importe;
		public decimal Abono;
		public decimal Saldo;
		public int NumPagos;
		public int Tipo;
		public string Observaciones;
		public DateTime FechaUltimoAbono;
		public int CueId;
		public decimal Cargo;
		public int CieId;
			
		public Ahorro (Database  db) : base (db, RecordType.Ahorro)
		{
		}
		
		public override bool Update ()
		{
			return true;
		}

		
		public static IDataAdapter GetAllInAdtapter (Database db)
		{
			return db.QueryToAdapter ("SELECT aho_id, CAST(DATE_FORMAT(aho_fecha,'%d/%m/%Y') as CHAR) as Fecha, aho_folio as Folio, tra_ficha as Ficha, TRIM(CONCAT(tra_nombre, ' ', tra_apepaterno, ' ', tra_apematerno)) as Nombre, aho_importe as Importe, aho_abono as Abono, aho_saldo as Saldo, CASE aho_status when 1 then 'RT' when 2 then 'DC' when 3 then 'DT' when 4 then 'SP' when 5 then 'PG' when 6 then 'CA' end as Estado from ahorros,trabajadores where ahorros.tra_id = trabajadores.tra_id");
		}
	}
}
