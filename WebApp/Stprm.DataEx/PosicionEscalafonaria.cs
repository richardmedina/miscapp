using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class PosicionEscalafonaria : Registro
    {
        public string Ficha;
        public string Categoria;
        public int Posicion;
        public int Nivel;
        public string ClaveEscalafon;
        public string DescripcionEscalafon;
        public string Depto;
        public string Clasificacion;
        public string Plaza;
        public string FichaRecomendado;
        public string NombreRecomendado;
        public string ParentescoRecomendado;
        

        public PosicionEscalafonaria(BaseDatos bd)
            : base(bd, TipoRegistro.PosicionEscalafonaria)
        {
        }

        public override bool Actualizar()
        {
            bool result = false;

            IDataReader reader = Bd.Query("SELECT Descripcion as Categoria,Posicion,Nivel,ClaveEscalafon,DescripcionEscalafon,ClaveDepto as Depto,Clasificacion,Plaza,FichaRec,NombreRec,Parentesco from {0} where ficha='{1}'",
                TablaEscalafones, Ficha);

            if (reader.Read())
            {
                SetearDesdeDataReader(reader);
                result = true;
            }
            reader.Close();

            return result;
        }

        public override void SetearDesdeDataReader(IDataReader reader)
        {
            Categoria = reader.GetString(reader.GetOrdinal("Categoria"));
            Posicion = reader.IsDBNull (reader.GetOrdinal ("Posicion")) ? 0 : reader.GetInt32(reader.GetOrdinal("Posicion"));
            Nivel = reader.GetInt32(reader.GetOrdinal("Nivel"));
            ClaveEscalafon = reader.GetString(reader.GetOrdinal("ClaveEscalafon"));
            DescripcionEscalafon = reader.GetString(reader.GetOrdinal("DescripcionEscalafon"));
            Depto = reader.GetString(reader.GetOrdinal("Depto"));
            Clasificacion = reader.GetString(reader.GetOrdinal("Clasificacion"));
            Plaza = reader.GetString(reader.GetOrdinal("Plaza"));
            FichaRecomendado = reader.IsDBNull (reader.GetOrdinal("FichaRec"))? string.Empty : reader.GetString(reader.GetOrdinal("FichaRec"));
            NombreRecomendado = reader.IsDBNull (reader.GetOrdinal ("NombreRec")) ? string.Empty : reader.GetString(reader.GetOrdinal("NombreRec"));
            ParentescoRecomendado = reader.IsDBNull (reader.GetOrdinal("Parentesco")) ? string.Empty : reader.GetString(reader.GetOrdinal("Parentesco")); 

        }
    }
}
