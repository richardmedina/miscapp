using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Stprm.DataEx;

namespace Stprm.Web
{
    public partial class RegistrarFichaControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void _btn_ok_Click(object sender, EventArgs e)
        {
            using (BaseDatos datos = BaseDatos.CreateStprmConnection())
            {
                Trabajador trabajador = new Trabajador(datos);
                trabajador.Ficha = _txt_ficha.Text;

                if (!trabajador.Existe())
                {
                    trabajador.Nombre = _txt_nombre.Text;
                    trabajador.RegimenContractual = _txt_regimen.Text;
                    if (!trabajador.GuardarComoInexistente(_txt_depto.Text))
                    {
                        _btn_ok.Text = "Error";
                    }
                }
            }
        }
    }
}