using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Stprm.DataEx;

namespace Stprm.Web
{
    public partial class InfoTrabControl : System.Web.UI.UserControl
    {

        public void Setear (string val)
        {
            Nombre.Text = val;
            Ficha.Text = val;
            ContratoActual.Text = val;
            MotivoActual.Text = val;
            ContratoBase.Text = val;
            Estadistica.Text = val;
        }

        public void SetSeleccionable(bool state)
        {
            _btn_seleccionar.Enabled = state;
        }

        public void SetSeleccionar (bool state)
        {
            string css_class = state ? "button_selected" : "button_unselected";

            _lbl_ficha.CssClass = css_class;
            _lbl_tra_cat_actual.CssClass = css_class;
            _lbl_tra_cat_motivo.CssClass = css_class;
            _lbl_tra_cat_base.CssClass = css_class;
            _lbl_nombre.CssClass = css_class;
            _lbl_estadis.CssClass = css_class;

            Nombre.CssClass = css_class;
            Ficha.CssClass = css_class;
            ContratoActual.CssClass = css_class;
            MotivoActual.CssClass = css_class;
            ContratoBase.CssClass = css_class;
            Estadistica.CssClass = css_class;
        }

        public void ActualizarDesdeTrabajador(Trabajador trabajador)
        {

            Nombre.Text = string.Format("{0} ({1})",
                trabajador.GetNombreCompleto(), trabajador.GetDiasLab());

            Ficha.Text = trabajador.Ficha;

            Contrato contrato;

            Estadistica.Text = string.Format("Días Trabajados: {0} ** Días Militancia : {1}", trabajador.GetDiasLab(), trabajador.GetDiasMilitancia());

            if (trabajador.GetUltimoContrato(out contrato))
            {
                ContratoActual.Text = contrato.Categoria;
                MotivoActual.Text = contrato.Motivo;
            }
            else
            {
                ContratoActual.Text = "N/A";
            }

            PosicionEscalafonaria puesto;
            if (trabajador.GetPuestoEscalafon(out puesto))
                ContratoBase.Text = puesto.Categoria;
            else
                ContratoBase.Text = "N/A";
        }

        public string Text
        {
            get { return _lbl_cab.Text; }
            set { _lbl_cab.Text = value; }
        }

        public TextBox Ficha
        {
            get { return _txt_ficha; }
        }

        public TextBox Nombre
        {
            get { return _txt_trab_nom; }
        }

        public TextBox ContratoActual
        {
            get { return _txt_tra_cat_actual; }
        }

        public TextBox MotivoActual
        {
            get { return _txt_tra_cat_motivo; }
        }

        public TextBox ContratoBase
        {
            get { return _txt_tra_cat_base; }
        }

        public Button SeleccionarButton
        {
            get { return _btn_seleccionar; }
        }

        public TextBox Estadistica
        {
            get { return _txt_stadis; }
        }  
    }
}