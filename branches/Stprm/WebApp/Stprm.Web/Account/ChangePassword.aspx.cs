using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using Stprm.DataEx;
namespace Stprm.Web.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _btn_ok.Click += new EventHandler(_btn_ok_Click);
        }

        void _btn_ok_Click(object sender, EventArgs e)
        {
            _lbl_mensaje.ForeColor = System.Drawing.Color.Red;
           // _lbl_mensaje.Font.Bold = true;

            using (BaseDatos datos = BaseDatos.CreateStprmConnection())
            {
                PerfilUsuario perfil = new PerfilUsuario(datos);

                string nombreusuario = Session["username"] == null ? string.Empty : Session["username"].ToString();
                perfil.Usuario = nombreusuario;


                if (perfil.Actualizar())
                {
                    if (perfil.Autenticar(nombreusuario, _txt_actual.Text))
                    {
                        perfil.Password = _txt_nuevo.Text;
                        if (perfil.Guardar())
                        {
                            _lbl_mensaje.ForeColor = System.Drawing.Color.Green;
                            _txt_nuevo.Text = string.Empty;
                            _txt_repetir.Text = string.Empty;
                            _lbl_mensaje.Text = "Contraseña cambiada satisfactoriamente";
                            Response.Redirect("ChangePasswordSuccess.aspx");
                        }
                    }
                    else
                    {
                        _txt_actual.Text = string.Empty;
                        _txt_nuevo.Text = string.Empty;
                        _txt_repetir.Text = string.Empty;
                        _lbl_mensaje.Text = "Por favor verifique su contraseña actual";
                    }
                }
                else
                {
                    _lbl_mensaje.Text = "Error desconocido obteniendo perfil";
                }
            }
        }
    }
}
