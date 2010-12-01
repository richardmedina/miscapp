using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class Categoria : Record
	{
		public string Id;
		public string Nombre;
		public string Concepto;
		
		public Categoria (Database db) : base (db, RecordType.Categoria)
		{
		}
		
		public override bool Update ()
		{
			bool result = false;
			
			IDataReader reader = Db.Query ("select cat_id, cat_nombre, cat_concepto from {0} where cat_id='{1}'",
			                               TableCategorias, Id);
			
			if (reader.Read ()) {
				FillFromReader (reader);
				result = true;	
			}
			reader.Close ();
			
			return result;
		}
		
		public static CategoriaCollection GetCollection (Database db)
		{
			CategoriaCollection categorias = new CategoriaCollection ();
			
			IDataReader reader = db.Query ("select cat_id, cat_nombre, cat_concepto from {0}", TableCategorias);
			
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
			Nombre = reader.GetString (reader.GetOrdinal ("cat_nombre"));
			Concepto = reader.GetString (reader.GetOrdinal ("cat_concepto"));
		}
	}
}
