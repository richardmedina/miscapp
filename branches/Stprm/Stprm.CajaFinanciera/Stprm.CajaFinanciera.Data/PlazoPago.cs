
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class PlazoPago : Record
	{

		public int Id;
		public string Nombre;
		public int NumPagos;
		public int PrePorcentajeInteres;
		
		public PlazoPago (Database db) : base (db, RecordType.PlazoPago)
		{
		}
		
		public override void FillFromReader (System.Data.IDataReader reader)
		{
			Id = reader.GetInt32 (reader.GetOrdinal ("pla_id"));
			Nombre = reader.GetString (reader.GetOrdinal ("pla_nombre"));
			NumPagos = reader.GetInt32 (reader.GetOrdinal ("pla_num_pagos"));
		}
		
		public static PlazoPagoCollection GetCollection (Database db)
		{
			PlazoPagoCollection plazos = new PlazoPagoCollection ();
			
			IDataReader reader = db.Query ("SELECT * FROM plazos");
			
			while (reader.Read ()) {
				PlazoPago plazopago = new PlazoPago (db);
				plazopago.FillFromReader (reader);
				plazos.Add (plazopago);
			}
			reader.Close ();
			
			return plazos;
		}
		
		public static IDataAdapter GetCollectionInAdapter (Database db)
		{
			return db.QueryToAdapter ("select pla_nombre as Plazo, pla_num_pagos as Pagos from plazos");
		}
	}
}
