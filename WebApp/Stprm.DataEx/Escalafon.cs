using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class Escalafon 
    {

        public string Nombre;
        public string Descripcion;

        //public Escalafon (BaseDatos datos) : base (datos, TipoRegistro.PosicionEscalafonaria)

        public static string [] GetNombres (BaseDatos bd)
        {
            IDataReader reader = bd.Query("SELECT DISTINCT ClaveEscalafon + '.' + DescripcionEscalafon as DescripcionEscalafon from {0} order by DescripcionEscalafon",
                Registro.TablaEscalafones);

            //EscalafonCollection escalafones = new EscalafonCollection();
            List <string> nombres = new List<string> ();

            while (reader.Read())
            {
                //Escalafon esc = new Escalafon(bd);

                /*
                esc.Nombre = reader.IsDBNull (reader.GetOrdinal ("DescripcionEscalafon")) ? string.Empty : reader.GetString (reader.GetOrdinal ("DescripcionEscalafon"));
                esc.Descripcion = 
                */
                nombres.Add (reader.IsDBNull (reader.GetOrdinal ("DescripcionEscalafon")) ? string.Empty : reader.GetString (reader.GetOrdinal ("DescripcionEscalafon")));
            }
            reader.Close();

            return nombres.ToArray();
        }

        public static IDataAdapter GetDesdeNombre (BaseDatos bd, string nombre)
        {
            return bd.QueryToAdapter("SELECT Posicion, Ficha, Nombre, Descripcion as Categoria, Nivel, FichaRec as [Ficha Recomendado], NombreRec as [Nombre Recomendado], Parentesco FROM {0} WHERE ClaveEscalafon + '.' + DescripcionEscalafon = '{1}' order by Posicion asc",
                Registro.TablaEscalafones, nombre);
        }
    }
}
