using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using Stprm.Data;

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

            Database db = new Database("poseidon", "ricki", "09b9085a+", "seccion26");
            if (db.Open ())
            {
                UserProfile profile = new UserProfile(db);
                profile.Username = LoginUser.UserName;

                if (profile.Update() && profile.Authenticate(LoginUser.UserName, LoginUser.Password))
                {
                    Session["username"] = profile.Username;
                    Session["username_name"] = profile.Name;

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
                    Response.Redirect(strredirectto);
                }
                else
                {
                    e.Authenticated = false;
                    /*if (_txt_username.Text.Trim().Length == 0)
                        _txt_username.Focus();
                    else
                        _txt_password.Focus();*/
                }
            }
            db.Close();
        }
    }
}
