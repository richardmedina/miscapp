using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Stprm.Data;

namespace Stprm.DataEx
{
    public class Plaza : Registro
    {

        public string NumeroPlaza;
        public string Centro;
        public string Depto;
        public string Categoria;
        public string Clasificacion;
        public int Nivel;

        public Plaza (BaseDatos datos)
            : base(datos, TipoRegistro.Plaza)
        {

        }

        public override bool Actualizar()
        {
            bool result = false;

            IDataReader reader = Bd.Query("SELECT top 1 Plaza,Centro,Depto,Categoria,Clasificacion,Nivel from {0} where Plaza={1}",
                "View_Plazas", NumeroPlaza);

            if (reader.Read())
            {
                SetearDesdeDataReader(reader);
                result = true;
            }
            reader.Close();

            return result;
        }

        public bool ObtenerDepartamento(out Departamento depto)
        {
            depto = new Departamento(Bd);

            depto.ClaveCentro = Centro;
            depto.Clave = Depto;
            
            if (depto.Actualizar())
            {
                return true;
            }
            return false;
        }

        public override void SetearDesdeDataReader(IDataReader reader)
        {
            NumeroPlaza = GetString(reader, "Plaza");
            Centro = GetString(reader, "Centro");
            Depto = GetString(reader, "Depto");
            Categoria = GetString(reader, "Categoria");
            Clasificacion = GetString(reader, "Clasificacion");
            Nivel = GetInt32(reader, "Nivel");
        }
    }
}
