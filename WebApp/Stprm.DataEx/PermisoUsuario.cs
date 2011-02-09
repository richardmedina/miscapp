
using System;
using System.Data;

namespace Stprm.DataEx
{


	public class PermisoUsuario : Registro
	{
		public int Id;
		public string Nombre;
		public string NombreCorto;
		public bool Ver;
		public bool Agregar;
		public bool Editar;
		public bool Eliminar;
		
		public PermisoUsuario (BaseDatos datos) : base (datos, TipoRegistro.PermisoUsuario)
		{
		}
		
		public override void SetearDesdeDataReader (IDataReader reader)
		{
			
		}

	}
}
