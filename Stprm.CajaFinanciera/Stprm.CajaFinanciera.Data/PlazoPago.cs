
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
		
		
		public override bool Update ()
		{
			bool result = false;
			
			IDataReader reader = Db.Query ("SELECT * FROM {0} where pla_id= {1}",
			                               TablePlazos, Id);
			
			if (reader.Read ()) {
				FillFromReader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
		
		public override void FillFromReader (System.Data.IDataReader reader)
		{
			Id = GetInt32 (reader, "pla_id"); // reader.GetInt32 (reader.GetOrdinal ("pla_id"));
			Nombre = GetString (reader, "pla_nombre"); //reader.GetString (reader.GetOrdinal ("pla_nombre"));
			NumPagos = GetInt32 (reader, "pla_num_pagos"); //reader.GetInt32 (reader.GetOrdinal ("pla_num_pagos"));
		}
		
		public static PlazoPagoCollection GetCollection (Database db)
		{
			PlazoPagoCollection plazos = new PlazoPagoCollection ();
			
			IDataReader reader = db.Query ("SELECT * FROM {0}", TablePlazos);
			
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
			Console.WriteLine (@"select pla_nombre as Plazo, pla_num_pagos as Pagos, CONCAT(CAST(pre_porcentaje_interes AS CHAR), '%') as Interes from {0}",
			                          TablePlazos);
			return db.QueryToAdapter (@"select pla_nombre as Plazo, pla_num_pagos as Pagos, CONCAT(CAST(pre_porcentaje_interes AS CHAR), '%') as Interes from {0}",
			                          TablePlazos);
		}
	}
}
