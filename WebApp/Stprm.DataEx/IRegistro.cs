using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public interface IRegistro
    {
        bool Guardar ();
        bool Eliminar();
        bool Actualizar ();

        void SetearDesdeDataReader (IDataReader reader);

        bool Existe ();
        bool GuardarAtributos ();

        BaseDatos Bd
        {
            get;
        }

        TipoRegistro Tipo
        {
            get;
        }
    }
}
