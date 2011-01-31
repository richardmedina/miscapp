using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class BeneficioSindical : Registro
    {

        public BeneficioSindical (BaseDatos datos) : base (datos, TipoRegistro.BeneficioSindical)
        {
        }
		
		public static IDataAdapter GetColeccion (BaseDatos datos, string ficha)
		{
			return datos.QueryToAdapter ("SELECT Id, TipoBeneficio as Beneficio, Fecha , Observacion FROM {0} where Ficha = '{1}' order by Fecha desc", 
			                      TablaBeneficiosOtorgados, ficha);
		}

        public override bool Guardar()
        {
            return base.Guardar();
        }
    }
}
