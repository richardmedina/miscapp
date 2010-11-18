using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class Escalafon : List<PosicionEscalafonaria>
    {

        public static string [] GetNombres (BaseDatos bd)
        {
            IDataReader reader = bd.Query("SELECT DISTINCT DescripcionEscalafon from {0} order by DescripcionEscalafon",
                Registro.TablaEscalafones);

            List<string> nombres = new List<string>();

            while (reader.Read())
            {
                nombres.Add (reader.IsDBNull (reader.GetOrdinal ("DescripcionEscalafon")) ? string.Empty : reader.GetString (reader.GetOrdinal ("DescripcionEscalafon")));
            }
            reader.Close();

            return nombres.ToArray();
        }

        public static IDataAdapter GetDesdeNombre (BaseDatos bd, string nombre)
        {
            return bd.QueryToAdapter("SELECT Posicion, Ficha, Nombre, Descripcion as Categoria, Nivel, FichaRec as [Ficha Recomendado], NombreRec as [Nombre Recomendado], Parentesco FROM {0} WHERE DescripcionEscalafon = '{1}' order by Posicion asc",
                Registro.TablaEscalafones, nombre);
        }
    }
}
