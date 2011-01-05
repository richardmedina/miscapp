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
            _isc_search.Busqueda += _isc_search_Busqueda;
            _ei_employee.ClearData();
            
        }

        private void _isc_search_Busqueda (object sender, TrabajadorEventArgs args)
        {
            _ei_employee.ClearData();

            if (args.Exists)
                _ei_employee.UpdateFromEmployee(args.Trabajador);
        }
    }
}