using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Stprm.DataEx
{
    public class Registro : IRegistro
    {
        private TipoRegistro _tipo;
        private BaseDatos _basedatos;

        public static readonly string TablaContratos = "NuevosContratos";
        public static readonly string TablaEscalafones = "Escalafones";
        public static readonly string TablaEventos = "Eventos";
        public static readonly string TablaParticipacionEventos = "part_eventos";
        public static readonly string TablaAudiencias = "Audiencias";
        public static readonly string TablaParticipacionAudiencias = "part_audiencias";
        public static readonly string TablaPerfilUsuarios = "Perfiles";
        public static readonly string TablaTrabajadoresInexistentes = "TrabajadoresInexistentes";
		
		public static readonly string TablaBeneficiosOtorgados = "BeneficiosSindicalesOtorgados";

        private static MD5 _md5 = MD5.Create();

        public Registro(BaseDatos basedatos, TipoRegistro tipo)
        {
            _basedatos = basedatos;
            _tipo = tipo;
        }

        public virtual bool Guardar ()
        {
            throw new NotImplementedException();
        }

        public virtual bool Actualizar()
        {
            throw new NotImplementedException();
        }

        public virtual bool Eliminar ()
        {
            throw new NotImplementedException ();
        }

        public virtual bool Existe()
        {
            throw new NotImplementedException();
        }

        public virtual void SetearDesdeDataReader(IDataReader reader)
        {
        }
		
		protected int GetLastInsertId ()
		{
			int id = 0;
			try {
				IDataReader reader = Bd.Query("SELECT @@IDENTITY AS Id");

    	        if (reader.Read())
        	    {
            		id = Convert.ToInt32 (GetDecimal(reader, "Id"));
	            }
            	reader.Close();	
			}catch (Exception exception) {
				Console.WriteLine (exception);
			}
			
			return id;
		}

        protected static string GetString(IDataReader reader, string field_name)
        {
            return reader.IsDBNull(reader.GetOrdinal(field_name)) ? string.Empty : reader.GetString(reader.GetOrdinal(field_name));
        }

        protected static DateTime GetDateTime(IDataReader reader, string field_name)
        {
            DateTime date = DateTime.MinValue;
            try
            {
                string date_str = reader[field_name].ToString();

                if (!date_str.StartsWith("0/0/0000"))
                    date = reader.IsDBNull(reader.GetOrdinal(field_name)) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal(field_name));
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message + "Excepcion atrapada mientras DateTime conversion");
            }

            return date;
        }

        public static int GetInt32(IDataReader reader, string field_name)
        {
            return reader.IsDBNull(reader.GetOrdinal(field_name)) ? 0 : reader.GetInt32(reader.GetOrdinal(field_name));
        }

        public static bool GetBool (IDataReader reader, string field_name)
        {
            return reader.IsDBNull(reader.GetOrdinal(field_name)) ? false : reader.GetBoolean(reader.GetOrdinal(field_name));
        }

        public static decimal GetDecimal(IDataReader reader, string field_name)
        {
            return reader.IsDBNull(reader.GetOrdinal(field_name)) ? 0 : reader.GetDecimal(reader.GetOrdinal(field_name));
        }

        public static string Cifrar (string text)
        {
			string cifrado = string.Empty;
			
			byte [] bytes = _md5.ComputeHash(Encoding.Default.GetBytes(text));
			
			for (int i = 0; i < bytes.Length; i++)
				cifrado += bytes[i].ToString("x2").ToLower();
			
            return cifrado;
        }

        public static string DateTimeToDbString(DateTime date)
        {
            return date.ToString("yyyyMMdd");
        }


        public TipoRegistro Tipo
        {
            get { return _tipo; }
        }

        public BaseDatos Bd
        {
            get { return _basedatos; }
            protected set { _basedatos = value; }
        }
    }
}
