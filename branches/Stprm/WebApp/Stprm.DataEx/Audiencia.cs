using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class Audiencia : Registro
    {
        public int Id;
        public DateTime Fecha;
        public bool PermitirPlanta;
        public bool PermitirTransitorio;
        public bool PermitirExterno;


        public Audiencia(BaseDatos datos)
            : base(datos, TipoRegistro.Audiencia)
        {
        }


        public override bool Actualizar()
        {
            bool result = false;

            IDataReader reader = Bd.Query("SELECT * FROM {0} where Id = {1}",
                TablaAudiencias, Id);

            if (reader.Read())
            {
                SetearDesdeDataReader(reader);
                result = true;
            }

            reader.Close();
            return result;
        }

        public bool ActualizarDesdeFecha()
        {
            bool result = false;

            IDataReader reader = Bd.Query("SELECT * FROM {0} WHERE Fecha = '{1}'",
                TablaAudiencias, Fecha.ToString("yyyyMMdd"));

            if (reader.Read())
            {
                SetearDesdeDataReader(reader);
                result = true;
            }
            reader.Close();

            return result;
        }

        public void Agregar(Trabajador trabajador, string asunto)
        {
            //Contrato contrato = trabajador.RegimenContractual == 'PS' ? trabajador.GetPuestoEscalafon ()
            string depto = string.Empty;
            if (trabajador.RegimenContractual == "PS") {
                PosicionEscalafonaria posicion;
                if (trabajador.GetPuestoEscalafon (out posicion)) {
                    depto = posicion.Depto;
                }
            } else {
                Contrato contrato;
                if (trabajador.GetUltimoContrato (out contrato)) {
                    depto = contrato.Depto;
                }
            }

            ParticipacionAudiencia participacion = new ParticipacionAudiencia(Bd);
            participacion.Ficha = trabajador.Ficha;
            participacion.AudienciaId = Id;
            participacion.Nombre = trabajador.GetNombreCompleto();
            participacion.RegimenContractual = trabajador.RegimenContractual;
            participacion.Depto = depto;
            participacion.Asunto = asunto;


            participacion.Guardar();

            //Bd.NonQuery("INSERT INTO {0} (FICHA, ID_AUD, NOMBRE, REC_CONTR, DEPTO, ASUNTO, OBSERVACION) VALUES ('{1}', {2}, '{3}', '{4}', '{5}', '{6}')",
              //  TablaParticipacionAudiencias, trabajador.Ficha, Id, trabajador.GetNombreCompleto (), trabajador.RegimenContractual, depto, asunto);
        }

        public override void SetearDesdeDataReader(IDataReader reader)
        {
            Id = GetInt32 (reader, "id");
            Fecha = GetDateTime(reader, "fecha");
            PermitirPlanta = GetBool (reader, "Planta");
            PermitirTransitorio = GetBool (reader, "Transitorio");
            PermitirExterno = GetBool (reader, "Externo");
        }

        public IDataAdapter GetCollectionInAdapter()
        {
            return Bd.QueryToAdapter("SELECT Ficha, Nombre,Depto, REG_CONTR as Regimen, Depto, Asunto, Observacion from {0} where ID_AUD = {1} order by Fecha desc",
                TablaParticipacionAudiencias, Id);
        }
    }
}
