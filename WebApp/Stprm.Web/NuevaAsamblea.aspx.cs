using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stprm.Web
{
    public partial class NuevaAsamblea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _ids_ficha.Busqueda += new DataEx.TrabajadorEventHander(_ids_ficha_Busqueda);
            _ids_ficha.ErrorMessage = "La Ficha no existe. <a href='nuevo.aspx' target='_blank'>Agregar</a> de todas formas";
            _btn_continuar.Click += new EventHandler(_btn_continuar_Click);
        }

        private void _btn_continuar_Click(object sender, EventArgs e)
        {
            _pnl_bancotrabajo.Visible = true;
            _btn_continuar.Visible = false;
        }

        void _ids_ficha_Busqueda(object sender, DataEx.TrabajadorEventArgs args)
        {
        }
    }
}