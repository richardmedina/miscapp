﻿using System;
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

        public PerfilUsuario(BaseDatos datos)
            : base(datos, TipoRegistro.PerfilUsuario)
        {
        }

        public override bool Guardar()
        {
            if (!Existe())
            {
                Bd.NonQuery("INSERT INTO {0} (Username,Password) values ('{1}', '{2}')",
                    TablaPerfilUsuarios, Usuario, Password);
            }
    

            return true;
        }

        public override bool Actualizar ()
        {
            bool result = false;
            IDataReader reader = Bd.Query ("SELECT Username,Password,Name,Email,Active FROM {0} where Username='{1}'",
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

            return perfil.Existe();
        }

        public override void SetearDesdeDataReader(IDataReader reader)
        {
            Usuario = GetString(reader, "Username");
            Password = GetString(reader, "Password");
            Nombre = GetString(reader, "Name");
            Activo = GetBool(reader, "Active");
        }

        public string Password
        {
            get { return _password; }
            set { _password = Encrypt(value); }
        }
    }
}