using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class BeneficioSindical : Registro
    {

        public BeneficioSindical (BaseDatos datos) : base (datos, TipoRegistro.BeneficioSindical)
        {

        }

        public override bool Guardar()
        {
            return base.Guardar();
        }
    }
}
