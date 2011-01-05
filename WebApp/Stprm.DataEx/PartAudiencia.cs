using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class PartAudiencia : Registro
    {
        public int Id;
        public int AudienciaId;
        public string Ficha;
        public string Nombre;
        public string RegContr;
        public string Depto;
        public string Asunto;
        public string Observacion;

        public PartAudiencia(BaseDatos datos) : base (datos, TipoRegistro.PartAudiencia)
        {
        }

        public override bool Actualizar()
        {
            bool result = false;

            IDataReader reader = Bd.Query("SELECT * from {0} where Id = {1}",
                TablaParticipacionAudiencias, Id);

            if (reader.Read())
            {
                result = true;
            }

            return result;
        }
    }
}
