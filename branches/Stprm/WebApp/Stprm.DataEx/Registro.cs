using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class Registro : IRegistro
    {
        private TipoRegistro _tipo;
        private BaseDatos _basedatos;

        public static readonly string TablaContratos = "NuevosContratos";
        public static readonly string TablaEscalafones = "Escalafones";
        public static readonly string TablaEventos = "Eventos";
        public static readonly string TablaParticipacionEventos = "part_eventos";

        public Registro(BaseDatos basedatos, TipoRegistro tipo)
        {
            _basedatos = basedatos;
            _tipo = tipo;
        }

        public virtual bool Guardar ()
        {
            throw new NotImplementedException();
        }

        public virtual bool Actualizar()
        {
            throw new NotImplementedException();
        }

        public virtual bool Eliminar ()
        {
            throw new NotImplementedException ();
        }

        public virtual bool Existe()
        {
            throw new NotImplementedException();
        }

        public virtual void SetearDesdeDataReader(IDataReader reader)
        {
        }

        public TipoRegistro Tipo
        {
            get { return _tipo; }
        }

        public BaseDatos Bd
        {
            get { return _basedatos; }
            protected set { _basedatos = value; }
        }
    }
}
