using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class Contrato : Registro
    {
        public string Ficha;
        public string Folio;
        public string Categoria;
        public DateTime Inicio;
        public DateTime Termino;
        public DateTime Terminacion;
        public string Jornada;
        public string Nivel;
        public string Plaza;
        public string SituacionContractual;
        public string Clasificacion;
        public string ReferenciaOrigen;
        public string ReferenciaMotivo;
        public string OrigenMovimiento;
        public string Motivo;
        public string Depto;
        public string CentroTrabajo;

        public Contrato(BaseDatos bd)
            : base(bd, TipoRegistro.Contrato)
        {
        }

        public override bool Actualizar()
        {
            bool result = false;

            IDataReader reader = Bd.Query("select Ficha, Folio, Categoria, Inicio, Termino, Terminacion, Jornada, Nivel, Plaza, AreaPersonal, Clasificacion, ReferenciaOrigen, ReferenciaMotivo, OrigenMovimiento, Motivo1, Depto, Centro from {0} where Ficha ='{1}' and Folio='{2}'",
                TablaContratos, Ficha, Folio);

            if (reader.Read ()) {
                SetearDesdeDataReader(reader);
                result = true;
            }
            reader.Close();

            return result;
        }

        public bool ActualizarDesdeEscalafon()
        {
            bool result = false;

            IDataReader reader = Bd.Query("SELECT  from {0} where ficha='{1}'",
                TablaEscalafones, Ficha);

            if (reader.Read()) {
                result = true;
            }
            reader.Close ();

            return result;
        }

        /*
        public bool SetearUltimoContrato()
        {
            bool result = false;

            IDataReader reader = Bd.Query("select top 1 Folio, Categoria, Inicio, Termino, Terminacion, Jornada, Nivel, Plaza, AreaPersonal, Clasificacion, ReferenciaOrigen, ReferenciaMotivo, OrigenMovimiento, Motivo1, Depto, Centro from {0} where Ficha ='{1}' and Termino='99991231' and Terminacion is NULL order by {0}.Inicio desc",
                TablaContratos, Ficha);

            if (reader.Read())
            {
                SetearDesdeDataReader(reader);
                result = true;
            }
            reader.Close();

            return result;
        }

        public bool SetearBaseDesdeficha()
        {
            return false;
        }
        */
        public override void SetearDesdeDataReader(IDataReader reader)
        {
            //Ficha = reader.GetString(reader.GetOrdinal("Ficha"));
            //Folio = reader.GetString(reader.GetOrdinal("Folio"));
            Categoria = reader.GetString(reader.GetOrdinal("Categoria"));
            Inicio = reader.GetDateTime(reader.GetOrdinal("Inicio"));
            Termino = reader.GetDateTime(reader.GetOrdinal("Termino"));
            //if (!reader.IsDBNull (reader.GetOrdinal ("Terminacion")));
                Terminacion = reader.IsDBNull (reader.GetOrdinal("Terminacion")) ? DateTime.MaxValue : reader.GetDateTime(reader.GetOrdinal("Terminacion"));
            
            Jornada = reader.GetString(reader.GetOrdinal("Jornada"));
            Nivel = reader.GetString(reader.GetOrdinal("Nivel"));
            Plaza = reader.GetString(reader.GetOrdinal("Plaza"));
            SituacionContractual = reader.GetString(reader.GetOrdinal("AreaPersonal"));
            Clasificacion = reader.GetString(reader.GetOrdinal("Clasificacion"));
            ReferenciaOrigen = reader.IsDBNull (reader.GetOrdinal("ReferenciaOrigen")) ? string.Empty : reader.GetString(reader.GetOrdinal("ReferenciaOrigen"));
            ReferenciaMotivo = reader.IsDBNull(reader.GetOrdinal("ReferenciaMotivo")) ? string.Empty : reader.GetString(reader.GetOrdinal("ReferenciaMotivo"));
            OrigenMovimiento = reader.IsDBNull(reader.GetOrdinal("OrigenMovimiento")) ? string.Empty : reader.GetString(reader.GetOrdinal("OrigenMovimiento"));
            Motivo = reader.IsDBNull(reader.GetOrdinal("Motivo1")) ? string.Empty : reader.GetString(reader.GetOrdinal("Motivo1"));
            Depto = reader.IsDBNull(reader.GetOrdinal("Depto")) ? string.Empty : reader.GetString(reader.GetOrdinal("Depto"));
            CentroTrabajo = reader.IsDBNull(reader.GetOrdinal("Centro")) ? string.Empty : reader.GetString(reader.GetOrdinal("Centro"));
        }
    }
}
