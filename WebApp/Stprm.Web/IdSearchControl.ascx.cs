using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


using MedLib.Controls;
using Stprm.DataEx;

namespace Stprm.Web
{

    public partial class IdSearchControl : System.Web.UI.UserControl
    {

        private event TrabajadorEventHander _search_result_event;

        public string ErrorMessage = "Ficha no existe";

        protected override void OnInit(EventArgs e)
        {
            _search_result_event = on_search_result_event;
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            _txt_id.Attributes.Add("onkeypress", "return clickButton(event,'" + _btn_search.ClientID + "')");
            _btn_search.Click += new EventHandler(_btn_search_Click);
            _lbl_msg.Text = string.Empty;
        }

        protected virtual void OnSearchResult(Trabajador trabajador, bool existe)
        {
            _search_result_event(this, new TrabajadorEventArgs (trabajador, existe));
        }

        private void _btn_search_Click(object sender, EventArgs e)
        {
            int id;

            if (int.TryParse(_txt_id.Text, out id))
            {
                using (BaseDatos datos = BaseDatos.CreateStprmConnection ())
                {
                    Trabajador trabajador = new Trabajador(datos);
                    trabajador.Ficha = _txt_id.Text;
                    //Employee employee = new Employee(db);
                    //employee.Id = id;

                    bool existe = trabajador.Actualizar ();
                    if (!existe)
                        _lbl_msg.Text = ErrorMessage;

                    OnSearchResult(trabajador, existe);
                }
            }
            else
            {
                _lbl_msg.Text = "Ficha Inválida";
            }
        }

        private void on_search_result_event(object sender, TrabajadorEventArgs args)
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
            add { _search_result_event += value; }
            remove { _search_result_event -= value; }
        }
    }
}
