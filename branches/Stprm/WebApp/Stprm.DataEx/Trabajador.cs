using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class Trabajador : Registro
    {

        public string Ficha = string.Empty;
        public string Nombre = string.Empty;
        public string ApellidoPaterno = string.Empty;
        public string ApellidoMaterno = string.Empty;
        public string Curp = string.Empty;
        public string Rfc = string.Empty;

        public string RegimenContractual = string.Empty;

        public Trabajador(BaseDatos basedatos)
            : base(basedatos, TipoRegistro.Trabajador)
        {
        }
        /*
        public void SetRecomendado (Trabajador recomendado, string parentesco)
        {
            bool result = false;

            if (RegimenContractual == "PS" && recomendado.RegimenContractual == "TS")
            {
                Bd.NonQuery ("UPDATE {0} set FichaRec='{1}', NombreRec='{2}' Parentesco='{3}'",
                    TablaEscalafones, 
            }

            return result;
        }
        */

        // ficha, nombre, regimen, depto
        public bool GuardarComoInexistente(string depto)
        {
            bool result = false;
            try
            {
                Bd.NonQuery("INSERT INTO {0} (Ficha, Nombre, Regimen, Depto) values ('{1}', '{2}', '{3}', '{4}')",
                    TablaTrabajadoresInexistentes, Ficha, Nombre, RegimenContractual, depto);
                result = true;
            }
            catch (Exception exception)
            {
            }

            return result;
        }

        public string GetNombreCompleto()
        {
            return string.Format("{0} {1} {2}", Nombre, ApellidoPaterno, ApellidoMaterno);
        }

        public override bool Actualizar ()
        {
            bool result = false;

            IDataReader reader = Bd.Query("select top 1 Nombre, ApellidoPaterno, ApellidoMaterno, Curp, Rfc, AreaPersonal as RegimenContractual from {0} where ficha = '{1}' order by Inicio desc",
                TablaContratos, Ficha);

            if (reader.Read())
            {
                SetearDesdeDataReader(reader);
                result = true;
            }
            reader.Close();
            return result;
        }

        public void SetRecomendado(Trabajador trabajador, string parentezco)
        {
            ;
        }

        public bool GetRecomendado (out Trabajador trabajador, out string parentesco)
        {
            bool result = false;
            trabajador = new Trabajador(Bd);
            parentesco = string.Empty;

            IDataReader reader = Bd.Query("SELECT FichaRec,NombreRec,Parentesco FROM {0} where ficha='{1}'",
                TablaEscalafones, Ficha);

            if (reader.Read())
            {
                trabajador.Ficha = reader.IsDBNull (reader.GetOrdinal ("FichaRec")) ? string.Empty : reader.GetString(reader.GetOrdinal("FichaRec"));
                trabajador.Nombre = reader.IsDBNull(reader.GetOrdinal("NombreRec")) ? string.Empty : reader.GetString(reader.GetOrdinal("NombreRec"));
                parentesco = reader.IsDBNull(reader.GetOrdinal("Parentesco")) ? string.Empty : reader.GetString(reader.GetOrdinal("Parentesco"));
                result = true;
            }
            reader.Close();

            if (result)
                return trabajador.Actualizar();

            return result;
        }

        public bool GetRecomienda (out Trabajador trabajador, out string parentesco)
        {
            bool result = true;
            trabajador = new Trabajador(Bd);
            parentesco = string.Empty;

            IDataReader reader = Bd.Query("SELECT Ficha, Parentesco from {0} where fichArec = '{1}'",
                TablaEscalafones, Ficha);

            if (reader.Read())
            {
                trabajador.Ficha = reader.GetString(reader.GetOrdinal("Ficha"));
                parentesco = reader.GetString(reader.GetOrdinal("Parentesco"));
                result = true;
            }
            reader.Close();

            if (result)
                return trabajador.Actualizar();

            return result;
        }

        public bool GetUltimoDefinitivo (out Contrato contrato)
        {
            bool result = false;
        
            contrato = new Contrato(Bd);

            IDataReader reader = Bd.Query("select top 1 Folio from {0} where Ficha='{1}' and Termino = '99991231' and Terminacion is NULL order by Inicio desc",
                TablaContratos, Ficha);

            if (reader.Read())
            {
                contrato.SetearDesdeDataReader(reader);
                result = true;
            }
            reader.Close();

            return result;
        }

        public bool GetPuestoEscalafon (out PosicionEscalafonaria puesto)
        {
            bool result = false;

            puesto = new PosicionEscalafonaria(Bd);
            puesto.Ficha = Ficha;

            if (puesto.Actualizar())
            {
                result = true;
            }

            return result;
        }

        public bool GetUltimoContrato(out Contrato contrato)
        {
            bool result = false;

            contrato = new Contrato(Bd);

            IDataReader reader = Bd.Query("select top 1 Folio from {0} where ficha='{1}' order by Inicio desc",
                TablaContratos, Ficha);

            if (reader.Read ()) {
                contrato.Ficha = Ficha;
                contrato.Folio = reader.GetString (reader.GetOrdinal ("Folio"));
                result = true;
            }
            reader.Close();

            if (result)
                return contrato.Actualizar();

            return result;
        }

        public int GetDiasLab()
        {
            int dias_lab = 0;

            IDataReader reader = Bd.Query("select SUM (DATEDIFF (day, Inicio, IsNull (Terminacion, Termino))) + 1 as dias_lab from {0} where Ficha='{1}' and Termino < '99991231' and Inicio >= '20100101'",
                TablaContratos, Ficha);

            if (reader.Read())
            {
                dias_lab = reader.IsDBNull (reader.GetOrdinal("dias_lab")) ? 0 : reader.GetInt32(reader.GetOrdinal("dias_lab"));
            }
            reader.Close();

            return dias_lab;
        }
        
        public int GetDiasMilitancia()
        {
            int dias_mili = 0;

            IDataReader reader = Bd.Query("select count (FICHA) as dias_mili from {0} where FICHA = '{1}'",
                TablaParticipacionEventos, Ficha);

            if (reader.Read())
            {
                dias_mili = reader.GetInt32(reader.GetOrdinal("dias_mili"));
            }
            reader.Close();

            return dias_mili;
        }
        
        public IDataAdapter GetContratosInAdapter()
        {
            return Bd.QueryToAdapter("select Folio, Categoria, CONVERT(VARCHAR, Inicio, 103) as Inicio, CONVERT(VARCHAR,Termino, 103) as Termino, CONVERT(VARCHAR,Terminacion,103) as Terminacion, Jornada, Nivel, Plaza, AreaPersonal, Clasificacion, ReferenciaOrigen, ReferenciaMotivo, OrigenMovimiento, Motivo1 + Motivo2 + Motivo3 as Motivo, Depto, Centro, DATEDIFF (day, Inicio, IsNull (Terminacion, Termino)) + 1 as Dias from {0} where ficha = {1} order by {0}.Inicio desc",
                TablaContratos, Ficha);
        }

        public IDataAdapter GetEscalafonInAdapter()
        {
            return Bd.QueryToAdapter("select Ficha, Nombre, Descripcion as Categoria, Posicion, DescripcionEscalafon as Escalafon, ClaveDepto as Depto, Clasificacion, Plaza, FichaRec, NombreRec, Parentesco  from {0} where DescripcionEscalafon in (Select DescripcionEscalafon from {0} where Ficha = '{1}') order by Posicion asc",
                TablaEscalafones, Ficha);
        }

        public IDataAdapter GetMilitanciaInAdapter()
        {
            return Bd.QueryToAdapter("select {0}.NOMBRE as Evento, {0}.Fecha as Fecha, {0}.Lugar as Lugar, {1}.TIPO_APOYO from {0}, {1} where FICHA = '{2}'  and {0}.ID = {1}.NUM_EVENTO order by {0}.Fecha desc",
                TablaEventos, TablaParticipacionEventos, Ficha);
        }
		
		public IDataAdapter GetBeneficiosSindicales ()
		{
			return BeneficioSindical.GetColeccionInAdapter (Bd, Ficha);
		}
		
		public static IDataAdapter ObtenerColeccion (BaseDatos datos, string filtro)
		{
			//return datos.QueryToAdapter ("SELECT Ficha, MAX(NombreCompleto) as Nombre from {0} where NombreCompleto like '%{1}%' group by ficha", TablaContratos, filtro);
			return datos.QueryToAdapter ("SELECT Ficha, MAX(NombreCompleto) as Nombre, MIN(AreaPersonal) as Regimen from {0} where AreaPersonal <> 'PC' and AreaPersonal <> 'TC' group by Ficha order by Ficha", TablaContratos);
		}

        public override void SetearDesdeDataReader(IDataReader reader)
        {
            //Ficha = reader.GetString(reader.GetOrdinal("Nombre"));
            Nombre = reader.GetString(reader.GetOrdinal("Nombre"));
            ApellidoPaterno = reader.GetString(reader.GetOrdinal("ApellidoPaterno"));
            ApellidoMaterno = reader.GetString(reader.GetOrdinal("ApellidoMaterno"));
            Curp = reader.GetString(reader.GetOrdinal("Curp"));
            Rfc = reader.GetString(reader.GetOrdinal("Rfc"));
            RegimenContractual = reader.GetString(reader.GetOrdinal("RegimenContractual"));
        }
    }
}
