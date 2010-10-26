using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Stprm.Data;
using Stprm.DataEx;


public partial class EmployeeNew : System.Web.UI.Page
{

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        _txt_id.Focus();
    }

    protected void btn_saveClick(object sender, EventArgs args)
    {
        int id;

        if (!int.TryParse (_txt_id.Text, out id)){
            
        }

        using (Database db = Database.CreateStprmConnection())
        {
            EmployeeEx employee = new EmployeeEx(db);
            
            
         //   employee.Id = 
        }
    }
}