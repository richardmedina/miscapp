using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class BeneficioSindical : Registro
    {
		public int Id;
		public string Ficha;
		public DateTime Fecha;
		public string TipoBeneficio;
		public string Observacion;
		
		public string Estado;
		
		
		
        public BeneficioSindical (BaseDatos datos) : base (datos, TipoRegistro.BeneficioSindical)
        {
        }
		
		public static IDataAdapter GetColeccionInAdapter (BaseDatos datos, string ficha)
		{
			return datos.QueryToAdapter ("SELECT Id, {2} as Fecha, TipoBeneficio as Beneficio, Estado, Observacion FROM {0} where Ficha = '{1}' order by Fecha desc", 
			                      TablaBeneficiosOtorgados, ficha, DbDateTimeToString ("Fecha"));
		}

        public override bool Guardar()
        {
			bool result = false;
			
            if (!Existe ()) {
				Bd.NonQuery ("insert into {0} (ficha) values ('{1}')",
				             TablaBeneficiosOtorgados, Ficha);
				
				Id = GetLastInsertId ();
			}
			
			if (Id > 0) {
				Bd.NonQuery ("UPDATE {0} SET TipoBeneficio='{1}',Fecha='{2}',Observacion='{3}', Estado='{4}' where Id={5}",
				             TablaBeneficiosOtorgados, TipoBeneficio, DateTimeToDbString (Fecha), Observacion, Estado, Id);
				result = true;
			}
			
			return result;
        }
		
		public override bool Existe ()
		{
			BeneficioSindical beneficio = new BeneficioSindical (Bd);
			beneficio.Id = Id;
			
			return beneficio.Actualizar ();
		}
		
		public override bool Actualizar ()
		{
			bool result = false;
			
			IDataReader reader = Bd.Query ("Select * from {0} where Id = {1}",
			                               TablaBeneficiosOtorgados, Id);
			
			if (reader.Read ()) {
				SetearDesdeDataReader (reader);
				result = true;
			}
			
			reader.Close ();
			
			return result;
		}
		
		public override bool Eliminar ()
		{
			bool result = false;
			
			if (Existe ()) {
				Bd.NonQuery ("DELETE FROM {0} WHERE  Id='{1}'",
				             TablaBeneficiosOtorgados, Id);
				result = true;
			}
			
			return result;
		}

		
		public override void SetearDesdeDataReader (IDataReader reader)
		{
			Id = GetInt32 (reader, "Id");
			Ficha = GetString (reader, "Ficha");
			Fecha = GetDateTime (reader, "Fecha");
			TipoBeneficio = GetString (reader, "TipoBeneficio");
			Observacion = GetString (reader, "Observacion");
			Estado = GetString (reader, "Estado");
		}
    }
}
