
using System;
using System.Data;

namespace Stprm.DataEx
{


	public class Recomendacion : Registro
	{
		public string FichaPlanta;
        public string NombrePlanta;
        public string DatosPlanta;
		public string FichaTransitorio;
        public string NombreTransitorio;
        public string DatosTransitorio;
		public string Parentesco;
		
		public Recomendacion (BaseDatos datos) : base (datos, TipoRegistro.Recomendacion)
		{
		}
		
		public override bool Guardar ()
		{
			bool result = false;
			
			if (Existe ()) {
				Bd.NonQuery ("UPDATE {0} SET FIC_TRANSI='{1}', Parentesco='{2}' where FIC_PLANTA='{3}'", TablaRecomendaciones, FichaTransitorio, Parentesco, FichaPlanta);	
				result = true;
			}
			else if (ExisteTransitorio ()) {
				Bd.NonQuery ("UPDATE {0} SET FIC_PLANTA='{1}', Parentesco='{2}' where FIC_TRANSI='{3}'", TablaRecomendaciones, FichaPlanta, Parentesco, FichaTransitorio);	
				result = true;
			} else {
				Bd.NonQuery ("INSERT INTO {0} (FIC_PLANTA, FIC_TRANSI, PARENTESCO) VALUES ('{1}', '{2}', '{3}')",
				             TablaRecomendaciones, FichaPlanta, FichaTransitorio, Parentesco);
				
				result = true;
			}
			
			return result;
		}
		
		public override bool Actualizar ()
		{
			bool result = false;

            IDataReader reader = Bd.Query("SELECT FIC_PLANTA, FIC_TRANSI, Parentesco, (SELECT MAX(NombreCompleto) from NuevosContratos where FIC_PLANTA = Ficha) as NombrePlanta,  ISNULL((SELECT top 1 ISNULL(AreaPersonal, '') + '::' +ISNULL(Categoria, '') + '::' + ISNULL(Clasificacion, '') + '::' + ISNULL((SELECT DES_DEPTO from Departamentos where CVE_CTRO=NuevosContratos.Centro and CVE_DEPTO=NuevosContratos.Depto),'') from NuevosContratos where FIC_PLANTA=Ficha order by Inicio desc), '::::::') as DatosPlanta, (SELECT MAX(NombreCompleto) from NuevosContratos where FIC_TRANSI = Ficha) as NombreTransitorio, ISNULL((SELECT top 1 ISNULL(AreaPersonal, '') + '::' +ISNULL(Categoria, '') + '::' + ISNULL(Clasificacion, '') + '::' + ISNULL((SELECT DES_DEPTO from Departamentos where CVE_CTRO=NuevosContratos.Centro and CVE_DEPTO=NuevosContratos.Depto),'') from NuevosContratos where FIC_TRANSI=Ficha order by Inicio desc), '::::::') as DatosTransitorio from RECOMENDACIONES where FIC_PLANTA='{0}' order by FIC_PLANTA asc",
			                               FichaPlanta);
			
			if (reader.Read ()) {
				SetearDesdeDataReader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
				
		public bool ActualizarDesdeTransitorio ()
		{
			bool result = false;

            IDataReader reader = Bd.Query("SELECT FIC_PLANTA, FIC_TRANSI, Parentesco, (SELECT MAX(NombreCompleto) from NuevosContratos where FIC_PLANTA = Ficha) as NombrePlanta,  ISNULL((SELECT top 1 ISNULL(AreaPersonal, '') + '::' +ISNULL(Categoria, '') + '::' + ISNULL(Clasificacion, '') + '::' + ISNULL((SELECT DES_DEPTO from Departamentos where CVE_CTRO=NuevosContratos.Centro and CVE_DEPTO=NuevosContratos.Depto),'') from NuevosContratos where FIC_PLANTA=Ficha order by Inicio desc), '::::::') as DatosPlanta, (SELECT MAX(NombreCompleto) from NuevosContratos where FIC_TRANSI = Ficha) as NombreTransitorio, ISNULL((SELECT top 1 ISNULL(AreaPersonal, '') + '::' +ISNULL(Categoria, '') + '::' + ISNULL(Clasificacion, '') + '::' + ISNULL((SELECT DES_DEPTO from Departamentos where CVE_CTRO=NuevosContratos.Centro and CVE_DEPTO=NuevosContratos.Depto),'') from NuevosContratos where FIC_TRANSI=Ficha order by Inicio desc), '::::::') as DatosTransitorio from RECOMENDACIONES where FIC_TRANSI='{0}' order by FIC_PLANTA asc",
			                               FichaTransitorio);
			
			if (reader.Read ()) {
				SetearDesdeDataReader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
		
		public override bool Existe ()
		{
			Recomendacion recomendacion = new Recomendacion (Bd);
			recomendacion.FichaPlanta = FichaPlanta;
			
			return recomendacion.Actualizar ();
		}

		public bool ExisteTransitorio ()
		{
			Recomendacion recomendacion = new Recomendacion (Bd);
			recomendacion.FichaTransitorio = FichaTransitorio;
			
			return recomendacion.ActualizarDesdeTransitorio ();
		}
		
		public override void SetearDesdeDataReader (IDataReader reader)
		{
			FichaPlanta = GetString (reader, "FIC_PLANTA");
			FichaTransitorio = GetString (reader, "FIC_TRANSI");
			Parentesco = GetString (reader, "Parentesco");
		}

        public static IDataAdapter ObtenerEnAdaptter (BaseDatos datos, string filtro)
        {
            return datos.QueryToAdapter("SELECT ROW_NUMBER() OVER(Order by FIC_PLANTA)  as Num, FIC_PLANTA as FichaPlanta,FIC_TRANSI as FichaTransitorio, Parentesco from {0}", TablaRecomendaciones);
        }
        
        public static RecomendacionCollection Obtener (BaseDatos datos, string filtros)
        {
            //IDataReader reader = datos.Query("SELECT ROW_NUMBER() OVER(Order by FIC_PLANTA)  as Num, FIC_PLANTA,FIC_TRANSI, Parentesco from {0}", TablaRecomendaciones);
            RecomendacionCollection recoms = new RecomendacionCollection();

            IDataReader reader = datos.Query("SELECT FIC_PLANTA, FIC_TRANSI, Parentesco, (SeLECT MAX(NombreCompleto) from NuevosContratos where Fic_PLANTA = Ficha) as NombrePlanta,  (SeLECT MAX(NombreCompleto) from NuevosContratos where FIC_TRANSI = Ficha) as NombreTransitorio from {0} order by FIC_PLANTA asc", TablaRecomendaciones);

            while (reader.Read())
            {
                Recomendacion recom = new Recomendacion(datos);
                recom.SetearDesdeDataReader(reader);
                recom.NombrePlanta = GetString(reader, "NombrePlanta");
                recom.DatosPlanta = GetString(reader, "DatosPlanta");

                recom.NombreTransitorio = GetString(reader, "NombreTransitorio");
                recom.DatosTransitorio = GetString(reader, "DatosTransitorio");
                recoms.Add(recom);
            }
            reader.Close();

            return recoms;
        }
	}
}
