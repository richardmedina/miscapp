using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stprm.Web.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ChangeUserPassword.ChangingPassword += new LoginCancelEventHandler(ChangeUserPassword_ChangingPassword);
        }

        private void ChangeUserPassword_ChangingPassword(object sender, LoginCancelEventArgs e)
        {
            
        }
    }
}
