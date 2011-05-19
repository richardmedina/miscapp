using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class ParticipacionAudiencia : Registro
    {
        public int Id;
        public int AudienciaId;
        public string Ficha;
        public string Nombre;
        public string RegimenContractual;
        public string Depto;
        public string Asunto;
        public string Observacion;

        public ParticipacionAudiencia(BaseDatos datos)
            : base(datos, TipoRegistro.PartAudiencia)
        {
        }

        public override bool Guardar()
        {
            bool result = false;

            if (!Existe())
            {
                Bd.NonQuery("INSERT INTO {0} (FICHA, ID_AUD, STD_CR, STD_MD) values ('', 0, GETDATE(), GETDATE())",
                    TablaParticipacionAudiencias);

                IDataReader reader = Bd.Query("SELECT @@IDENTITY as Id");

                if (reader.Read())
                {
                    Id = Convert.ToInt32 (GetDecimal (reader, "Id"));
                }
                reader.Close();
            }


            if (Id > 0)
            {
                try
                {
                    Bd.NonQuery("UPDATE {0} SET FICHA= '{1}', ID_AUD={2}, NOMBRE='{3}', REG_CONTR='{4}', DEPTO='{5}', ASUNTO='{6}' WHERE Id={7}",
                        TablaParticipacionAudiencias, Ficha, AudienciaId, Nombre, RegimenContractual, Depto, Asunto, Id);
                    result = true;
                }
                catch (Exception e)
                {
                    result = false;
                }
            }
            return result;
        }

        public override bool Actualizar()
        {
            bool result = false;

            IDataReader reader = Bd.Query("SELECT * FROM {0} where Id = {1}", TablaParticipacionAudiencias, Id);

            if (reader.Read())
            {
                SetearDesdeDataReader(reader);
                result = true;
            }
            reader.Close();

            return result;
        }


       
        /*
        public bool ActualizarDesdeFicha ()
        {
            bool result = false;

            IDataReader reader = Bd.Query("SELECT Ficha FROM {0} where ID_AUD={1} and Ficha='{2}'",
                TablaParticipacionAudiencias, Id, Ficha);

            

            return result;
        }
        */
        public override bool Eliminar()
        {
            bool result = false;
            try
            {
                Bd.NonQuery("DELETE FROM {0} where id={1}",
                    TablaParticipacionAudiencias, Id);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public override bool Existe()
        {
            ParticipacionAudiencia participacion = new ParticipacionAudiencia(Bd);
			participacion.Id = Id;
            return participacion.Actualizar();
        }

        public override void SetearDesdeDataReader(IDataReader reader)
        {
            Id = GetInt32(reader, "Id");
            AudienciaId = GetInt32(reader, "ID_AUD");
            Ficha = GetString(reader, "Ficha");
            Nombre = GetString(reader, "Nombre");
            RegimenContractual = GetString(reader, "REG_CONTR");
            Depto = GetString(reader, "Depto");
            Asunto = GetString(reader, "Asunto");
            Observacion = GetString(reader, "Observacion");
        }
    }
}
