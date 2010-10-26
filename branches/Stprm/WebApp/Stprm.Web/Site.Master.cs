using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Stprm.Data;

namespace Stprm.Web
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string current_user_name = Session["username_name"] == null ? string.Empty : Session["username_name"].ToString();
            string username = Session["username"] == null ? string.Empty : Session["username"].ToString();
		
            if (!IsPostBack)
            {
                //using (Database db = Database.CreateStprmConnection())
                Database db = new Database("poseidon", "ricki", "09b9085a+", "seccion26");
                if (db.Open()) 
                {
                    MenuItem root_item = new MenuItem("Secretarías");
                    
                    foreach (Secretariat sec in Secretariat.GetAll(db))
                    {
                        MenuItem item = new MenuItem(sec.GetFullname(), string.Empty, string.Empty, "Secretariat.aspx?id=" + sec.Id);
                        foreach (SecretariatModule module in sec.GetModules())
                        {
                            UserPermission permission = new UserPermission(db);
                            //permission.Id = string.Format ("{0}.{1}", 

                            if (username.Trim() == "arroyo" || username.Trim() == "oscar")
                            {
                                break;
                            }

                            //string page = Request
                            //Request.Url.AbsoluteUri.LastIndexOf ("/") + 1	
                            MenuItem subitem = new MenuItem(module.Name, string.Empty, string.Empty, module.Url);
                            item.ChildItems.Add(subitem);
                        }

                        root_item.ChildItems.Add(item);

                        if (sec.Id == 7)
                        {

                            if (username.Trim() == "arroyo" || username.Trim() == "oscar")
                            {
                                item.ChildItems.Add(new MenuItem("Militancias", string.Empty, string.Empty, "Events.aspx"));
                            }
                            if (username.Trim() == "arroyo")
                            {
                                item.ChildItems.Add(new MenuItem("Contratos", string.Empty, string.Empty, "Contracts.aspx"));
                                item.ChildItems.Add(new MenuItem("Consulta de Trabajadores", string.Empty, string.Empty, "EmployeeSearch.aspx"));
                            }
                        }
                    }
                    _mnu_main.Items.AddAt(1, root_item);
                    _mnu_main.Items.Add(new MenuItem("Salir", string.Empty, string.Empty, "Logout.aspx"));
                }
            }	
        }
    }
}
