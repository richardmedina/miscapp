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

        public string GetNombreCompleto()
        {
            return string.Format("{0} {1} {2}", Nombre, ApellidoPaterno, ApellidoMaterno);
        }

        public override bool Actualizar()
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
        /*
        public IDataAdapter GetPosicionEscalafonInAdapter()
        {
            return Bd.QueryToAdapter("select Descripcion, Posicion, ClaveEscalafon, DescripcionEscalafon,ClaveDepto as Depto, Clasificacion, Plaza, FichaRec, NombreRec, Parentesco from {0} where Ficha = {1}",
                TablaEscalafones, Ficha);
        }
        */
        public IDataAdapter GetContratosInAdapter()
        {
            return Bd.QueryToAdapter("select Folio, Categoria, CONVERT(VARCHAR, Inicio, 103) as Inicio, CONVERT(VARCHAR,Termino, 103) as Termino, CONVERT(VARCHAR,Terminacion,103) as Terminacion, Jornada, Nivel, Plaza, AreaPersonal, Clasificacion, ReferenciaOrigen, ReferenciaMotivo, OrigenMovimiento, Motivo1, Depto, Centro, DATEDIFF (day, Inicio, IsNull (Terminacion, Termino)) as Dias from {0} where ficha = {1} order by {0}.Inicio desc",
                TablaContratos, Ficha);
        }

        public IDataAdapter GetEscalafonInAdapter()
        {
            return Bd.QueryToAdapter("select Ficha, Nombre, Descripcion as Categoria, Posicion, DescripcionEscalafon as Escalafon, ClaveDepto as Depto, Clasificacion, Plaza  from {0} where DescripcionEscalafon in (Select DescripcionEscalafon from {0} where Ficha = '{1}') order by Posicion asc",
                TablaEscalafones, Ficha);
        }

        public IDataAdapter GetMilitanciaInAdapter()
        {
            return Bd.QueryToAdapter("select {0}.NOMBRE as Evento, {0}.Fecha as Fecha, {0}.Lugar as Lugar, {1}.TIPO_APOYO from {0}, {1} where FICHA = '{2}'  and {0}.ID = {1}.NUM_EVENTO order by {0}.Fecha desc",
                TablaEventos, TablaParticipacionEventos, Ficha);
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
