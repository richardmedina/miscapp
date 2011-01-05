using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stprm.DataEx;

namespace Stprm.Web
{
    public partial class BuscarFichaControl : System.Web.UI.UserControl
    {
        private event TrabajadorEventHander _busqueda;

        protected override void OnInit(EventArgs e)
        {
            _busqueda = onbusqueda;
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            _txt_id.Attributes.Add("onkeypress", "return clickButton(event,'" + _btn_search.ClientID + "')");
            _btn_search.Click += new EventHandler(_btn_search_Click);
            _lbl_msg.Text = string.Empty;
        }

        protected virtual void OnBusqueda (Trabajador employee, bool exists)
        {
            _busqueda (this, new TrabajadorEventArgs(employee, exists));
        }

        void _btn_search_Click(object sender, EventArgs e)
        {
            int id;

            if (int.TryParse(_txt_id.Text, out id))
            {
                using (BaseDatos bd = BaseDatos.CreateStprmConnection ())
                {
                    Trabajador trabajador = new Trabajador(bd);
                    trabajador.Ficha = _txt_id.Text.Trim();

                    bool exists = trabajador.Actualizar();
                    if (!exists)
                        _lbl_msg.Text = "Ficha no existe";

                    OnBusqueda (trabajador, exists);
                }
            }
            else
            {
                _lbl_msg.Text = "Ficha Inválida";
                OnBusqueda(new Trabajador(null), false);
            }
        }

        private void onbusqueda(object sender, TrabajadorEventArgs args)
        {
        }

        public TextBox TxtId
        {
            get { return _txt_id; }
        }

        public Label LabelMsg
        {
            get { return _lbl_msg; }
        }

        public Button ButtonOk
        {
            get { return _btn_search; }
        }

        public event TrabajadorEventHander Busqueda
        {
            add { _busqueda += value; }
            remove { _busqueda -= value; }
        }
    }
}