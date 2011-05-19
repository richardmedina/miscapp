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
		public int MaximaAudiencia;


        public Audiencia(BaseDatos datos)
            : base(datos, TipoRegistro.Audiencia)
        {
        }
		
		public override bool Guardar ()
		{
			bool result = false;
			
			if (!Existe ()) {
				Bd.NonQuery ("INSERT INTO {0} (Fecha,Planta,Transitorio,Externo,MaximaAudiencia) values ('{1}', '{2}', '{3}', '{4}', {5})",
				             TablaAudiencias, DateTimeToDbString(Fecha), PermitirPlanta, PermitirTransitorio, PermitirExterno, MaximaAudiencia);
				
				Id = GetLastInsertId ();
			}
			
			if (Id > 0) {
				Bd.NonQuery ("UPDATE {0} SET Fecha='{1}', Planta='{2}', Transitorio='{3}', Externo='{4}', MaximaAudiencia={5} Where Id={6}",
				             TablaAudiencias, DateTimeToDbString(Fecha), PermitirPlanta, PermitirTransitorio, PermitirExterno, MaximaAudiencia, Id);
				result = true;
			}
			
			return result;
		}
		
		public override bool Existe ()
		{
			Audiencia audiencia = new Audiencia (Bd);
			audiencia.Id = Id;
			
			return audiencia.Actualizar ();
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

        //public void Agregar (string ficha, string nombre, string regimen, 

        public void Remover(int id)
        {
            ParticipacionAudiencia participacion = new ParticipacionAudiencia(Bd);
            participacion.AudienciaId = Id;
            participacion.Id = id;
            participacion.Eliminar();
        }

        public void Agregar(Trabajador trabajador, string asunto)
        {
            //Contrato contrato = trabajador.RegimenContractual == 'PS' ? trabajador.GetPuestoEscalafon ()
            string depto = string.Empty;
            /*
            if (trabajador.RegimenContractual == "PS") {
                PosicionEscalafonaria posicion;
                if (trabajador.GetPuestoEscalafon (out posicion)) {
                    depto = posicion.Depto;
                }
            } else {*/
                Contrato contrato;
                if (trabajador.GetUltimoContrato (out contrato)) {
                    Departamento fdepto = new Departamento (Bd);
                    fdepto.ClaveCentro = contrato.CentroTrabajo;
                    fdepto.Clave = contrato.Depto;
                    
                    fdepto.Actualizar ();
                    depto = string.Format ("({0}:{1}) {2}", fdepto.ClaveCentro, fdepto.Clave, fdepto.Descripcion);
                }
            //}

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

        public bool FichaEsParticipante (string ficha)
        {
            bool result = false;

            IDataReader reader = Bd.Query("select top 1 Ficha from {0} where Ficha='{1}' and ID_AUD={2};",
                TablaParticipacionAudiencias, ficha, Id);

            if (reader.Read())
            {
                result = true;
            }

            reader.Close();

            return result;
        }

        public override void SetearDesdeDataReader(IDataReader reader)
        {
            Id = GetInt32 (reader, "id");
            Fecha = GetDateTime(reader, "fecha");
            PermitirPlanta = GetBool (reader, "Planta");
            PermitirTransitorio = GetBool (reader, "Transitorio");
            PermitirExterno = GetBool (reader, "Externo");
			MaximaAudiencia = GetInt32 (reader, "MaximaAudiencia");
        }
		
		public IDataAdapter ObtenerParticipantesEnAdapter ()
		{
			return Bd.QueryToAdapter ("SELECT Id,Ficha,Nombre,Reg_Contr as Regimen, Depto, Asunto from {0} where ID_AUD={1}",
			                   TablaParticipacionAudiencias, Id);
		}

		
		public static IDataAdapter ObtenerColeccionEnAdapter (BaseDatos datos)
		{
			return datos.QueryToAdapter ("select Id,{0} as Fecha, MaximaAudiencia, case Planta when '0' then 'No' when '1' then 'Si' end as PermitirPlanta, case Transitorio when '0' then 'No' when '1' then 'Si' end as PermitirTransitorios,case Externo when '0' then 'No' when '1' then 'Si' end as PermitirExternos from {1} order by Audiencias.Fecha desc", DbDateTimeToString ("Fecha"), TablaAudiencias);
		}
		
		public static AudienciaCollection ObtenerColeccion (BaseDatos datos)
		{
			AudienciaCollection audiencias = new AudienciaCollection ();
			
			IDataReader reader = datos.Query ("select * from {0} order by Fecha desc", TablaAudiencias);
			
			while (reader.Read ()) {
				Audiencia audiencia = new Audiencia (datos);
				audiencia.SetearDesdeDataReader (reader);
				audiencias.Add (audiencia);
			}
			reader.Close ();
			
			return audiencias;
		}
		
		/*
        public IDataAdapter GetCollectionInAdapter()
        {
            return Bd.QueryToAdapter("SELECT Ficha, Nombre,Depto, REG_CONTR as Regimen, Depto, Asunto, Observacion from {0} where ID_AUD = {1} order by Fecha desc",
                TablaParticipacionAudiencias, Id);
        }
        */
    }
}
