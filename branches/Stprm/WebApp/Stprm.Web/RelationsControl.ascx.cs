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

using Stprm.DataEx;

namespace Stprm.Web
{

    public partial class RelationsControl : System.Web.UI.UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
           // _txt_id.Attributes.Add("onkeypress", "return clickButton(event,'" + _btn_search.ClientID + "')");
            //_txt_id_recom.Attributes.Add("onkeypress", "return clickButton(event,'" + _btn_search_recom.ClientID + "')");

            //_btn_search.Click += btn_search_Click;

            _is_buscarplanta.Busqueda += new DataEx.TrabajadorEventHander(_is_buscarplanta_Busqueda);
            _is_buscartransitorio.Busqueda += new DataEx.TrabajadorEventHander(_is_buscartransitorio_Busqueda);
            _btn_assign.Click += btn_assign_Click;
            //_btn_search_recom.Click += btn_search_recom_Click;
            //_btn_assign_cancel.Click += btn_assign_cancel_Click;
        }

        void _is_buscartransitorio_Busqueda(object sender, DataEx.TrabajadorEventArgs args)
        {
            _ei_trans.Visible = false;
            _txt_parentesco.Text = string.Empty;
            _pnl_parentesco.Visible = false;

            if (args.Exists)
            {
                if (args.Trabajador.RegimenContractual == "TS")
                {
                    _ei_trans.UpdateFromEmployee(args.Trabajador);
                    _ei_trans.Visible = true;
                    _txt_parentesco.Text = string.Empty;
                    _txt_parentesco.Visible = true;
                    _btn_assign.Visible = true;
                    _pnl_parentesco.Visible = true;
                    _txt_parentesco.ReadOnly = false;
                    _txt_parentesco.Text = string.Empty;
                }
                else
                {
                    _is_buscartransitorio.LabelMsg.Text = string.Format("{0} NO es transitorio", args.Trabajador.GetNombreCompleto());
                    _pnl_parentesco.Visible = false;
                    //_ei_trans.Visible = false;
                }
            }
        }

        private void _is_buscarplanta_Busqueda(object sender, DataEx.TrabajadorEventArgs args)
        {
            _is_buscartransitorio.Visible = false;
            _ei_trans.Visible = false;
            _btn_assign.Visible = false;
            _pnl_parentesco.Visible = false;
            if (args.Exists)
            {
                if (args.Trabajador.RegimenContractual == "PS")
                {
                    _ei_plant.UpdateFromEmployee(args.Trabajador);
                    _ei_plant.Visible = true;
                    _is_buscartransitorio.Visible = true;
                    Trabajador recomendado;
                    string parentesco;

                    if (args.Trabajador.GetRecomendado(out recomendado, out parentesco))
                    {
                        _ei_trans.UpdateFromEmployee(recomendado);
                        _is_buscartransitorio.Visible = true;
                        _ei_trans.Visible = true;
                        _pnl_parentesco.Visible = true;
                        _txt_parentesco.Text = parentesco;
                        _txt_parentesco.ReadOnly = true;
                    }
                }
                else
                    _is_buscarplanta.LabelMsg.Text = string.Format ("{0} NO es de planta", args.Trabajador.GetNombreCompleto ()) ;
            }
            else
                _ei_plant.Visible = false;
        }

        private void btn_assign_Click(object sender, EventArgs args)
        {
            if (_txt_parentesco.Text.Trim().Length > 0)
            {
                using (BaseDatos datos = BaseDatos.CreateStprmConnection())
                {
                    Recomendacion recomendacion = new Recomendacion(datos);
                    recomendacion.FichaPlanta = _ei_plant.Id;
                    recomendacion.FichaTransitorio = _ei_trans.Id;
                    recomendacion.Parentesco = _txt_parentesco.Text;
                    recomendacion.Nombre = _ei_trans.Name;

                    if (!recomendacion.Guardar())
                    {
                        _is_buscartransitorio.LabelMsg.Text = "Error estableciendo recomendado. Por favor intentelo mas tarde";
                    }
                    else _btn_assign.Visible = false;
                }
            }
            else
            {
                _is_buscartransitorio.LabelMsg.Text = "por favor establezca el parentesco";
                _txt_parentesco.Focus();
            }
       }
    }
}