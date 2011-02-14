using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Stprm.DataEx;

namespace Stprm.DataEx
{
    public class PerfilUsuario : Registro
    {

        public string Usuario;
        private string _password;
        public string Nombre;
        public string Email;
        public bool Activo;
		public bool Admin;

        public PerfilUsuario(BaseDatos datos)
            : base(datos, TipoRegistro.PerfilUsuario)
        {
        }

        public override bool Guardar()
        {
			bool result = false;
			
            if (Existe()) {
				Bd.NonQuery ("UPDATE {0} SET Password='{2}',Name='{3}', IsActive='{4}',IsAdmin where Username='{1}'",
				             TablaPerfilUsuarios, Usuario, Password, Nombre, Activo, Admin);
				result = true;
			}
			else
            {
                Bd.NonQuery("INSERT INTO {0} (Username,Password,Name,Email,Active,IsAdmin) values ('{1}', '{2}', '{3}', {4},5)",
                    TablaPerfilUsuarios, Usuario, Password, Nombre, Email, Activo, Admin );
				result = true;
            }
    

            return result;
        }
		
		public bool Autenticar (string username, string password)
		{
			/*Console.WriteLine ("{0}({4}) == {1}({5}) && {2}({6}) == {3}({7}) **",
			                   Usuario, username, Password, Cifrar(password),
			                   Usuario.Length, username.Length, Password.Length, password.Length);*/
			
			return (Usuario == username && Password == Cifrar (password));
		}

        public override bool Actualizar ()
        {
            bool result = false;
            IDataReader reader = Bd.Query ("SELECT Username,Password,Name,Email,IsActive,IsAdmin FROM {0} where Username='{1}'",
                TablaPerfilUsuarios, Usuario);

            if (reader.Read ()) {
                SetearDesdeDataReader(reader);
                result = true;
            }
			
            reader.Close();

            return result;
        }

        public override bool Existe ()
        {
            PerfilUsuario perfil = new PerfilUsuario(Bd);
            perfil.Usuario = Usuario;

            return perfil.Actualizar();
        }

        public override void SetearDesdeDataReader(IDataReader reader)
        {
            Usuario = GetString(reader, "Username").Trim ();
            _password = GetString(reader, "Password").Trim ();
			Email = GetString (reader ,"Email");
            Nombre = GetString(reader, "Name");
            Activo = GetBool(reader, "IsActive");
			Admin = GetBool (reader, "IsAdmin");
        }

        public string Password
        {
            get { return _password; }
            set { _password = Cifrar (value); }
        }
    }
}
