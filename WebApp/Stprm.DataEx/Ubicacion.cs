
using System;

namespace Stprm.DataEx
{


	public class Ubicacion : Registro
	{
		public int Id;
		public string Nombre;
		public string Direccion;
		
		public Ubicacion (BaseDatos datos) : base (datos, TipoRegistro.Ubicacion)
		{
		}
		
		public override bool Actualizar ()
		{
			bool result = false;
			
			//IDataReader reader = Bd.Query ("SELECT * FROM {0}", TablaUbicaciones);
			
			return result;
		}

	}
}
