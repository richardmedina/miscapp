using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

//using Stprm.Data;
using Stprm.DataEx;

namespace Stprm.Web.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoginUser.Authenticate += new AuthenticateEventHandler(LoginUser_Authenticate);
        }

        void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
        {
            FormsAuthenticationTicket ticket;
            string cookiestr;
            HttpCookie cookie;
            
            e.Authenticated = false;

           
            using (BaseDatos db = BaseDatos.CreateStprmConnection ())
            {
                PerfilUsuario perfil = new PerfilUsuario(db);
                //UserProfile profile = new UserProfile(db);
                //profile.Username = LoginUser.UserName;
                perfil.Usuario = LoginUser.UserName;

                if (perfil.Actualizar () && perfil.Autenticar (LoginUser.UserName, LoginUser.Password))
                {
                    Session["username"] = perfil.Usuario;
                    Session["username_name"] = perfil.Nombre;

                    ticket = new FormsAuthenticationTicket(1,
                        LoginUser.UserName, DateTime.Now, DateTime.Now.AddMinutes(15),
                        LoginUser.RememberMeSet, "Datos x");

                    cookiestr = FormsAuthentication.Encrypt(ticket);

                    cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);

                    if (LoginUser.RememberMeSet)
                        cookie.Expires = ticket.Expiration;

                    cookie.Path = FormsAuthentication.FormsCookiePath;
                    Response.Cookies.Add(cookie);

                    e.Authenticated = true;

                    string strredirectto = Request["ReturnUrl"] == null ? "~/Default.aspx" : Request["ReturnUrl"].ToString();
                    Response.Redirect (strredirectto);
                }
            }
        }
    }
}
