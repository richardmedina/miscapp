
using System;
using System.Data;

namespace Stprm.DataEx
{


	public class PermisoUsuario : Registro
	{
		public int Id;
		public string Usuario;
		public string Referencia;
		public string Nombre;
		public bool Ver;
		public bool Editar;
		public bool Remover;
		
		public PermisoUsuario (BaseDatos datos) : base (datos, TipoRegistro.PermisoUsuario)
		{
		}
		
		public override bool Guardar ()
		{
			bool result = false;
			
			if (!Existe ()) {
				try {
					Bd.NonQuery ("INSERT INTO {0} (Usuario,Referencia,Nombre,Ver,Editar,Eliminar) values ('{1}', '{2}', '{3}', {4}, {5}, {6})",
				             TablaPermisosUsuarios, Usuario, Referencia, Nombre, Ver, Editar, Remover);
					Id = GetLastInsertId ();
				} catch (Exception exception) {
					Console.WriteLine (exception);
					Id = 0;
				}
			}
			
			if (Id > 0) {
				Bd.NonQuery ("UPDATE {0} SET Usuario = '{1}',Referencia='{2}',Nombre='{3}',Ver={4},Editar={5},Eliminar={6} where Id={7}",
				             TablaPermisosUsuarios, Usuario, Referencia, Nombre, Ver, Editar, Remover, Id);
				result = true;
			}
			
			return result;
		}
		
		public override bool Eliminar ()
		{
			bool result = false;
			
			if (Existe ()) {
				try {
					Bd.NonQuery ("DELETE FROM {0} WHERE Id = {1}",
				             TablaPermisosUsuarios, Id);
					result = true;
				} catch (Exception exception) {
					result = false;
				}
			}
			
			return result;
		}
		
		public override bool Actualizar ()
		{
			bool result = false;
			
			IDataReader reader = Bd.Query ("SELECT * FROM {0} where Id = {1}",
			                               TablaPermisosUsuarios, Id);
			
			if (reader.Read ()) {
				SetearDesdeDataReader (reader);
				result = true;
			}
			
			return result;
		}
		
		public override bool Existe ()
		{
			PermisoUsuario permiso = new PermisoUsuario (Bd);
			permiso.Id = Id;
			
			return permiso.Existe ();
		}
		
		public override void SetearDesdeDataReader (IDataReader reader)
		{
			Id = GetInt32 (reader, "Id");
			Usuario = GetString (reader, "Usuario");
			Referencia = GetString (reader, "Referencia");
			Nombre = GetString (reader, "Nombre");
			Ver = GetBool (reader, "Ver");
			Editar = GetBool (reader, "Editar");
			Remover = GetBool (reader, "Eliminar");
		}
		
		public static PermisoUsuarioCollection ObtenerColeccion (BaseDatos datos, string usuario)
		{
			PermisoUsuarioCollection permisos = new PermisoUsuarioCollection ();
			
			IDataReader reader = datos.Query ("SELECT * FROM {0} where perfil='{1}'",
			                                  TablaPermisosUsuarios, usuario);
			
			while (reader.Read ()) {
				PermisoUsuario permiso = new PermisoUsuario (datos);
				permiso.SetearDesdeDataReader (reader);
				permisos.Add (permiso);
			}
			
			return permisos;
		}

	}
}