using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class Categoria : Record
	{
		public string Id;
		public string Name;
		public string Concept;
		
		
		public Categoria (Database db) : base (db, RecordType.Categoria)
		{
		}
		
		public CategoriaCollection GetCollection (Database db)
		{
			CategoriaCollection categorias = new CategoriaCollection ();
			
			IDataReader reader = db.Query ("select * from categorias");
			
			while (reader.Read ()) {
				Categoria categoria = new Categoria (db);
				categoria.FillFromReader (reader);
				
				categorias.Add (categoria);
			}
			reader.Close ();
			
			return categorias;
		}
		
		public override void FillFromReader (IDataReader reader)
		{
			Id = reader.GetString (reader.GetOrdinal ("cat_id"));
			Name = reader.GetString (reader.GetOrdinal ("cat_nombre"));
			Concept = reader.GetString (reader.GetOrdinal ("cat_concepto"));
		}
	}
}
