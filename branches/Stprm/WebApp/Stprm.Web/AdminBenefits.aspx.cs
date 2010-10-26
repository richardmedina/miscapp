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
using Stprm.DataEx;

namespace Stprm.Web
{
    public partial class AdminBenefits : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            _isc_search.SearchResultEvent += _isc_search_SearchResultEvent;
            _ei_employee.ClearData();
            
            using (Database db = Database.CreateStprmConnection())
            {
                foreach (Concept concept in Concept.GetAllFromDb(db))
                {
                   _cmb_apoyos.Items.Add(new ListItem(concept.Name, concept.Id.ToString ()));
                }
            }
        }

        private void _isc_search_SearchResultEvent(object sender, EmployeeEventArgs args)
        {
            if (args.Exists)
                _ei_employee.UpdateFromEmployee(args.Employee);
            else _ei_employee.ClearData();
        }
    }
}