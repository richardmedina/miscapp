
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class Descuento : Record
	{
		public int Id;
		public string Status;
		public string CategoriaId;
		public int Periodo;
		public DateTime Fecha;
		public DateTime FechaIni;
		public DateTime FechaFin;
		public int Anio;
		
		public Descuento (Database db) : base (db, RecordType.Descuento)
		{
		}
		
		public override bool Save ()
		{
			bool result = false;
			if (!Exists ()) {
				Db.NonQuery ("insert into {0} (desc_creadopor) values (0)",
				             TableDescuentos);
				Id = GetLastInsertId ();
			}
			
			if (Id > 0) {
				//try {
					Db.NonQuery ("UPDATE {0} SET desc_fecha='{1}', desc_fechaini='{2}', desc_fechafin='{3}', desc_anio={4}, desc_periodo={5}, cat_id='{6}' where desc_id ={7}",
				             TableDescuentos, DateTimeToDbFormat (Fecha), DateTimeToDbFormat (FechaIni), DateTimeToDbFormat (FechaFin), Anio, Periodo, CategoriaId, Id);
					result = true;
			/*	} catch (Exception ex) {
					Console.WriteLine ("Descuento.Save Error : {0}", ex);
					result = false;
				}*/
			}
			
			return result;
		}

		
		public override bool Update ()
		{
			bool result = false;
			
			IDataReader reader = Db.Query ("select desc_id, desc_status, cat_id, desc_periodo, desc_fecha, desc_fechaini, desc_fechafin, desc_anio from {0} where desc_id = {1}",
			                               TableDescuentos, Id);
			
			if (reader.Read ()) {
				FillFromReader (reader);
				result = true;	
			}
			reader.Close ();
			
			return result;
		}
		
		public override bool Exists ()
		{
			Descuento descuento = new Descuento (Db);
			descuento.Id = Id;
			
			return descuento.Update ();
		}

		
		public override void FillFromReader (IDataReader reader)
		{
			Id = GetInt32 (reader, "desc_id");
			Status = GetString (reader, "desc_status");
			CategoriaId = GetString (reader, "cat_id");
			Periodo = GetInt32 (reader, "desc_periodo");
			Fecha = GetDateTime (reader, "desc_fecha");
			FechaIni = GetDateTime (reader, "desc_fechaini");
			FechaFin = GetDateTime (reader, "desc_fechafin");
		}
		
		public bool AgregarMovimiento (DescuentoMovimiento movimiento)
		{
			movimiento.DescuentoId = Id;
			return movimiento.Save ();
		}
		
		public DescuentoMovimientoCollection GetMovimientos ()
		{
			return DescuentoMovimiento.GetCollection (this);	
		}
		
		public IDataAdapter GetMovimientosInAdapter ()
		{
			return DescuentoMovimiento.GetCollectionInAdapter (this);	
		}
		
		public static IDataAdapter GetCollectionInAdapter (Database db)
		{
			return db.QueryToAdapter ("select desc_id as Id, cat_concepto as Clave, CONCAT(CAST(desc_periodo AS CHAR),'-', CAST(desc_anio AS CHAR)) as Periodo, CAST(DATE_FORMAT(desc_fecha,'%d/%m/%Y') as CHAR)as Fecha from {0}, {1} where {0}.cat_id = {1}.cat_id order by desc_fecha desc",
			                          TableDescuentos, TableCategorias);
			//return db.QueryToAdapter ("select pre_folio as Folio, tra_ficha as Ficha, tra_nombrecompleto as Trabajador, TRUNCATE(prem_abono,2) as Importe, TRUNCATE(prem_abono/14,2) as 'Desc. Diario' from {0}, {1}, {2} where {2}.tra_id = {1}.tra_id AND {1}.pre_id = {0}.pre_id AND prem_abono > 0 and {0}.cob_id = {3}",
			  //                        TablePrestamoMovimientos, TablePrestamos, TableEmployees, cobro_id);
		}
	}
}
