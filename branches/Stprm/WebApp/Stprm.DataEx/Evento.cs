using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class Evento : Registro
    {
        public int Id = 0;
        public string Nombre = string.Empty;
        public string Lugar = string.Empty;
        public DateTime Fecha = DateTime.MinValue;

        public Evento(BaseDatos datos)
            : base(datos, TipoRegistro.Evento)
        {
        }

        public override bool Existe()
        {
            Evento evento = new Evento(Bd);
            evento.Id = Id;
            return evento.Actualizar();
        }

        public override bool Actualizar()
        {
            bool result = false;

            IDataReader reader = Bd.Query("SELECT * FROM {0} WHERE Id = {1}",
                TablaEventos, Id);

            if (reader.Read())
            {
                SetearDesdeDataReader(reader);
                result = true;
            }
            reader.Close();

            return result;
        }

        public override bool Guardar()
        {
            bool result = false;

            if (!Existe ())
            {
                Id = -1;
                Bd.NonQuery("INSERT INTO {0} (Nombre) values ('')",
                    TablaEventos, Nombre);

                IDataReader reader = Bd.Query("SELECT @@IDENTITY AS Id");

                if (reader.Read())
                {
                    Id = Convert.ToInt32 (GetDecimal(reader, "Id"));
                }
                reader.Close();
            }

            if (Id > 0)
            {
                Bd.NonQuery("UPDATE {0} set Nombre = '{1}', Lugar='{2}', Fecha='{3}' WHERE Id = {4}",
                    TablaEventos, Nombre, Lugar, DateTimeToDbString(Fecha), Id);
            }

            return result;
        }


        public override void SetearDesdeDataReader(IDataReader reader)
        {
            Id = GetInt32(reader, "Id");
            Nombre = GetString(reader, "Nombre");
            Lugar = GetString (reader, "Lugar");
            Fecha = GetDateTime(reader, "Fecha");
        }

        public bool EsParticipante(Trabajador trabajador)
        {
            bool result = false;

            IDataReader reader = Bd.Query("SELECT ficha from {0} where ficha = '{1}' and NUM_EVENTO = {2}",
                TablaParticipacionEventos, trabajador.Ficha, Id);

            if (reader.Read())
            {
                result = true;
            }
            reader.Close();

            return result;
        }

        public void Agregar(Trabajador trabajador, string tipo_apoyo)
        {
            Agregar(trabajador.Ficha, tipo_apoyo);
        }

        public void Agregar(string ficha, string tipo_apoyo)
        {
            Bd.NonQuery("INSERT INTO {0} (Ficha,Num_Evento,Tipo_Apoyo) values ('{1}', {2}, '{3}')",
                TablaParticipacionEventos, ficha, Id, tipo_apoyo);
        }

        public void Eliminar(Trabajador trabajador)
        {
            Bd.NonQuery("DELETE FROM {0} WHERE NUM_EVENTO = {1} and Ficha = '{2}'",
                TablaParticipacionEventos, Id, trabajador.Ficha);
        }

        public IDataAdapter ParticipantesEnAdapter()
        {
            return Bd.QueryToAdapter("SELECT ROW_NUMBER() OVER (ORDER BY FICHA ASC) AS Num, Ficha, MAX(NombreCompleto) as Nombres, MIN(AreaPersonal) as SitContr, 'Apoyo' as Apoyo FROM     NuevosContratos WHERE    AreaPersonal <> 'PC' and AreaPersonal <> 'TC' and Ficha     IN    (SELECT    Ficha    FROM     PART_EVENTOS    WHERE    NUM_EVENTO =  {0} ) group by Ficha", Id);
        }
		
		public static IDataAdapter ObtenerEnAdapter (BaseDatos datos)
		{
			return 	datos.QueryToAdapter ("Select * from {0} order by Fecha desc", TablaEventos);
		}
    }
}
