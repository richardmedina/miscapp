
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{

	public class DescuentoMovimiento : Record
	{
		
		public string Ficha;
		public string Pagare;
		public string Folio;
		public string Nombre;
		public decimal Saldo;
		public decimal DescuentoCatorcenal;
		public decimal DescuentoDiario;
		
		public string Clave;
		public string Periodo;
		public string Anio;
		
		
		public DescuentoMovimiento (Database db) : base (db, RecordType.DescuentoMovimiento)
		{
		}
		
		public static IDataAdapter GetCollectionInAdapter (Descuento descuento)
		{
			return descuento.Db.QueryToAdapter ("select dem_folio_act as Folio, tra_ficha as Ficha, tra_nombrecompleto, dem_importe as Importe, dem_desc_diario as 'Desc. Diario' from descuentos_mov,trabajadores where trabajadores.tra_id = descuentos_mov.tra_id and desc_id={1}",
			                                    //"select * from {0} where desc_id = {1}",
			                                         TableDescuentoMovimientos, descuento.Id);
		}
		
		public override string ToString ()
		{
			return string.Format("[DescuentoMovimiento] Ficha = {0}, Pagare = {1}, Folio = {2}, Nombre = {3}, Saldo = {4}, DescuentoCatorcenal = {5}, DescuentDiario = {6}",
			                     Ficha, Folio, Nombre, Saldo, DescuentoCatorcenal, DescuentoDiario);
		}

	}
	
}
