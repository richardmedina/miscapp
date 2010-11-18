using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{

    public delegate void TrabajadorEventHander (object sender, TrabajadorEventArgs args);

    public class TrabajadorEventArgs : System.EventArgs
    {
        private Trabajador _trabajador;
        private bool _exists;

        public TrabajadorEventArgs (Trabajador trabajador, bool exists)
        {
            _trabajador = trabajador;
            _exists = exists;
        }

        public bool Exists {
            get {return _exists; }
        }

        public Trabajador Trabajador {
            get { return _trabajador; }
        }
    }
}
