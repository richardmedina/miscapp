using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class Departamento : Registro
    {

        public string ClaveCentro;
        public string Clave;
        public string Descripcion;

        public Departamento(BaseDatos datos)
            : base(datos, TipoRegistro.Departamento)
        {
        }

        public static DepartamentoCollection Obtener (BaseDatos datos, int centro) 
        {
            DepartamentoCollection deptos = new DepartamentoCollection();

            string query = string.Format(@"select '(' + Centro + '-' + Depto + ') ' + ISNULL(MAX(DES_DEPTO), '') as Descripcion, LTRIM(RTRIM(CAST(Centro as CHAR))) as ClaveCentro, LTRIM(RTRIM(CAST(Depto as CHAR))) as Clave from NuevosContratos 
                LEFT OUTER JOIN Departamentos ON NuevosContratos.Centro = Departamentos.CVE_CTRO and NuevosContratos.Depto = Departamentos.CVE_DEPTO where Seccion = 26 where Centro={0} group by Centro,Depto order by Centro,Depto asc", centro);

            if (centro == -1)
            {
                query = string.Format(@"select '(' + Centro + '-' + Depto + ') ' + ISNULL(MAX(DES_DEPTO), '') as Descripcion, LTRIM(RTRIM(CAST(Centro as CHAR))) as ClaveCentro, LTRIM(RTRIM(CAST(Depto as CHAR))) as Clave from NuevosContratos 
                LEFT OUTER JOIN Departamentos ON NuevosContratos.Centro = Departamentos.CVE_CTRO and NuevosContratos.Depto = Departamentos.CVE_DEPTO where Seccion = 26 group by Centro,Depto order by Centro,Depto asc", centro);
            }

            IDataReader reader = datos.Query(query);

            while (reader.Read())
            {
                Departamento depto = new Departamento(datos);
                depto.SetearDesdeDataReader(reader);
                deptos.Add(depto);
            }

            reader.Close();

            return deptos;
        }


        public PlazaCollection ObtenerPlazas ()
        {
            PlazaCollection plazas = new PlazaCollection();

            IDataReader reader = Bd.Query(@"select * from View_Plazas where Centro={0} and Depto={1}", ClaveCentro, Clave);

            while (reader.Read())
            {
                Plaza plaza = new Plaza(Bd);
                plaza.SetearDesdeDataReader(reader);
                plazas.Add(plaza);
            }
            reader.Close();

            return plazas;
        }

        public IDataAdapter ObtenerTrabajadoresInAdapter()
        {
            return Bd.QueryToAdapter("select Ficha,MAX(NombreCompleto) as Nombre,MIN(AreaPersonal) as Regimen from {0} where Centro={1} and Depto={2} group by Ficha",
                TablaContratos, ClaveCentro, Clave);
        }

        public override bool Actualizar()
        {
            bool result = false;
            IDataReader reader = Bd.Query("SELECT LTRIM(RTRIM(CAST(CVE_CTRO as CHAR)))as ClaveCentro, LTRIM(RTRIM(CAST(CVE_DEPTO as CHAR))) as Clave, DES_DEPTO as Descripcion from Departamentos where CVE_CTRO={0} and CVE_DEPTO={1}",
                ClaveCentro, Clave);

            if (reader.Read())
            {
                SetearDesdeDataReader(reader);
                result = true;
            }

            reader.Close();

            return result;
        }

        public override bool Existe()
        {
            Departamento depto = new Departamento(Bd);
            depto.Clave = Clave;
            depto.ClaveCentro = ClaveCentro;

            return depto.Actualizar();
        } 

        public override void SetearDesdeDataReader(System.Data.IDataReader reader)
        {
            Clave = GetString (reader, "Clave");
            ClaveCentro = GetString(reader, "ClaveCentro");
            Descripcion = GetString(reader, "Descripcion");
        }
    }
}
