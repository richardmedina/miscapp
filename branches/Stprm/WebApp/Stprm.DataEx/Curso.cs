
using System;
using System.Data;

namespace Stprm.DataEx
{


	public class Curso : Registro
	{
		public int Id;
		public string Nombre;
		public string Descripcion;
		public DateTime FechaInicio;
		public DateTime FechaTermino;
		public int DuracionHoras;
		public string Coordinador;
		public string Ponente;
		
		public Curso (BaseDatos datos) : base (datos, TipoRegistro.Curso)
		{
		}
		
		public override bool Actualizar ()
		{
			bool result = false;
			
			IDataReader reader = Bd.Query ("SELECT * From {0} where Id = {1}",
			                               TablaCursos, Id);
			
			if (reader.Read ()) {
				SetearDesdeDataReader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}

		public override void SetearDesdeDataReader (System.Data.IDataReader reader)
		{	
			Id = GetInt32 (reader, "Id");
			Nombre = GetString (reader, "Nombre");
			Descripcion = GetString (reader, "Descripcion");
			FechaInicio = GetDateTime (reader, "FechaInicio");
			FechaTermino = GetDateTime (reader, "FechaTermino");
			DuracionHoras = GetInt32 (reader, "DuracionHoras");
			Coordinador = GetString (reader, "Coordinador");
			Ponente = GetString (reader, "Ponente");
		}
	}
}
