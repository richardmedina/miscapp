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


using MedLib.Controls;
using Stprm.Data;

namespace Stprm.Web
{

    public partial class IdSearchControl : System.Web.UI.UserControl
    {

        private event EmployeeEventHandler _search_result_event;

        protected override void OnInit(EventArgs e)
        {
            _search_result_event = on_search_result_event;
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            _txt_id.Attributes.Add("onkeypress", "return clickButton(event,'" + _btn_search.ClientID + "')");
            _btn_search.Click += new EventHandler(_btn_search_Click);
            _lbl_msg.Text = string.Empty;
        }

        protected virtual void OnSearchResult(Employee employee, bool exists)
        {
            _search_result_event(this, new EmployeeEventArgs(employee, exists));
        }

        void _btn_search_Click(object sender, EventArgs e)
        {
            int id;

            if (int.TryParse(_txt_id.Text, out id))
            {
                using (Database db = Database.CreateStprmConnection())
                {
                    Employee employee = new Employee(db);
                    employee.Id = id;

                    bool exists = employee.Update();
                    if (!exists)
                        _lbl_msg.Text = "Ficha no existe";

                    OnSearchResult(employee, exists);
                }
            }
            else
            {
                _lbl_msg.Text = "Ficha Inválida";
            }
        }

        private void on_search_result_event(object sender, EmployeeEventArgs args)
        {
        }


        public event EmployeeEventHandler SearchResultEvent
        {
            add { _search_result_event += value; }
            remove { _search_result_event -= value; }
        }
    }
}
