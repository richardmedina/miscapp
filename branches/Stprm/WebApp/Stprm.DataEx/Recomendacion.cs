
using System;
using System.Data;

namespace Stprm.DataEx
{


	public class Recomendacion : Registro
	{
		public string FichaPlanta;
		public string FichaTransitorio;
		public string Parentesco;
		
		public Recomendacion (BaseDatos datos) : base (datos, TipoRegistro.Recomendacion)
		{
		}
		
		public override bool Guardar ()
		{
			bool result = false;
			
			if (Existe ()) {
				Bd.NonQuery ("UPDATE {0} SET FIC_TRANSI='{1}', Parentesco='{2}' where FIC_PLANTA='{3}'", TablaRecomendaciones, FichaTransitorio, Parentesco, FichaPlanta);	
				result = true;
			}
			else if (ExisteTransitorio ()) {
				Bd.NonQuery ("UPDATE {0} SET FIC_PLANTA='{1}', Parentesco='{2}' where FIC_TRANSI='{3}'", TablaRecomendaciones, FichaPlanta, Parentesco, FichaTransitorio);	
				result = true;
			} else {
				Bd.NonQuery ("INSERT INTO {0} (FIC_PLANTA, FIC_TRANSI, PARENTESCO) VALUES ('{1}', '{2}', '{3}')",
				             TablaRecomendaciones, FichaPlanta, FichaTransitorio, Parentesco);
				
				result = true;
			}
			
			return result;
		}
		
		public override bool Actualizar ()
		{
			bool result = false;
			
			IDataReader reader = Bd.Query ("SELECT * FROM {0} where FIC_PLANTA = '{1}'",
			                               TablaRecomendaciones, FichaPlanta);
			
			if (reader.Read ()) {
				SetearDesdeDataReader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
				
		public bool ActualizarDesdeTransitorio ()
		{
			bool result = false;
			
			IDataReader reader = Bd.Query ("SELECT * from {0} where FIC_TRANSI='{1}'",
			                               TablaRecomendaciones, FichaTransitorio);
			
			if (reader.Read ()) {
				SetearDesdeDataReader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
		
		public override bool Existe ()
		{
			Recomendacion recomendacion = new Recomendacion (Bd);
			recomendacion.FichaPlanta = FichaPlanta;
			
			return recomendacion.Actualizar ();
		}

		public bool ExisteTransitorio ()
		{
			Recomendacion recomendacion = new Recomendacion (Bd);
			recomendacion.FichaTransitorio = FichaTransitorio;
			
			return recomendacion.ActualizarDesdeTransitorio ();
		}
		
		public override void SetearDesdeDataReader (IDataReader reader)
		{
			FichaPlanta = GetString (reader, "FIC_PLANTA");
			FichaTransitorio = GetString (reader, "FIC_TRANSI");
			Parentesco = GetString (reader, "Parentesco");
		}
	}
}
