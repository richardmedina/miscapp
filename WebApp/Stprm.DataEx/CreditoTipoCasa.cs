
using System;
using System.Data;

namespace Stprm.DataEx
{


	public class CreditoTipoCasa : Registro
	{

		public int Id;
		public string TipoCasa;
		public decimal MontoCredito;
		public string Descripcion;
		
		public CreditoTipoCasa (BaseDatos datos) : base (datos, TipoRegistro.TipoCasa)
		{
		}
		
		public override bool Actualizar ()
		{
			bool result = false;
			
			IDataReader reader = Bd.Query ("select * from {0} where Id = {1}",
			                               TablaTipoCasas, Id);
			
			if (reader.Read ()) {
				SetearDesdeDataReader (reader);	
			}
			reader.Close ();
			
			return result;
		}

		public override void SetearDesdeDataReader (IDataReader reader)
		{
			Id = GetInt32 (reader, "Id");
			TipoCasa = GetString (reader, "Tipo");
			MontoCredito = GetInt64 (reader, "MontoCredito");
			Descripcion = GetString (reader ,"Descripcion");
		}
		
		public static CreditoTipoCasaCollection ObtenerColeccion (BaseDatos datos)
		{
			CreditoTipoCasaCollection casas = new CreditoTipoCasaCollection ();	
			
			IDataReader reader = datos.Query ("select * from {0}",
			                                  TablaTipoCasas);
			
			while (reader.Read ()) {
				CreditoTipoCasa casa = new CreditoTipoCasa (datos);
				casa.SetearDesdeDataReader (reader);
				casas.Add (casa);
			}
			reader.Close ();
			
			return casas;
		}
		
		public static IDataAdapter ObtenerColeccionEnAdapter (BaseDatos datos)
		{
			return 	datos.QueryToAdapter ("select * from {0}",
			                              TablaTipoCasas);
		}
	}
}
