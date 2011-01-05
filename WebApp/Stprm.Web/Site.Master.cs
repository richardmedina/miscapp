using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Stprm.Data;
using Stprm.DataEx;

namespace Stprm.Web
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        private BaseDatos _datos;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string current_user_name = Session["username_name"] == null ? string.Empty : Session["username_name"].ToString();
            string username = Session["username"] == null ? string.Empty : Session["username"].ToString();
		    _datos = BaseDatos.CreateStprmConnection ();

            if (!IsPostBack)
            {
                MenuItem root_item = new MenuItem("Secretarías");
                //Database db = new Database("mercurio", "ricki", "09b9085a+", "seccion26");
                //if (db.Open()) 
                using (Database db = BaseDatos.CreateOldConnection ())
                {
                    foreach (Secretariat sec in Secretariat.GetAll(db))
                    {
                        MenuItem item = new MenuItem(sec.GetFullname(), string.Empty, string.Empty, "Secretariat.aspx?id=" + sec.Id);
                        
                        foreach (SecretariatModule module in sec.GetModules())
                        {
                            UserPermission permission = new UserPermission(db);

                            if (username.Trim() == "arroyo" || username.Trim() == "oscar" || username.Trim () == "capturista")
                            {
                                break;
                            }

                            MenuItem subitem = new MenuItem(module.Name, string.Empty, string.Empty, module.Url);
                            item.ChildItems.Add(subitem);
                        }

                        root_item.ChildItems.Add(item);

                        if (sec.Id == 7)
                        {

                            if (username.Trim() == "arroyo" || username.Trim() == "oscar" || username.Trim () == "capturista")
                            {
                                item.ChildItems.Add(new MenuItem("Militancias", string.Empty, string.Empty, "Events.aspx"));
                            }
                            if (username.Trim() == "arroyo")
                            {
                                item.ChildItems.Add(new MenuItem("Contratos", string.Empty, string.Empty, "Contracts.aspx"));
                                //item.ChildItems.Add(new MenuItem("Consulta de Trabajadores", string.Empty, string.Empty, "EmployeeSearch.aspx"));
                            }
                        }
                    }
                }
                _mnu_main.Items.AddAt(1, root_item);
                //_mnu_main.Items.Add(new MenuItem("Salir", string.Empty, string.Empty, "Logout.aspx"));
                _mnu_main.CssClass = "menu";
            }	
        }

        public BaseDatos Datos
        {
            get { return _datos; }
        }
    }
}
