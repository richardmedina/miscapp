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

using Stprm.Data;
using Stprm.Reports;

namespace Stprm.Web
{
    public partial class ViewReport : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            string report_type = get_query_value("report_type");
            string employee_idstr = get_query_value("employee_id");

            string filename = @"C:\ricki\r.pdf";

            switch (report_type)
            {
                case "employee_resume":
                    int employeeid;

                    if (int.TryParse(employee_idstr, out employeeid))
                    {
                        using (Database db = Database.CreateStprmConnection())
                        {
                            Employee employee = new Employee(db);
                            employee.Id = employeeid;
                            if (employee.Update())
                            {
                                EmployeeSimpleReport report = new EmployeeSimpleReport(employee.Id);
                                report.Render();
                                using (System.IO.FileStream writer = new System.IO.FileStream(filename, System.IO.FileMode.Create))
                                    writer.Write(report.Stream.GetBuffer(), 0, report.Stream.GetBuffer().Length);
                            }
                        }
                    }
                    break;

                case "militancy":
                    {
                        /*
                            int eventid;
                            string type = Request.QueryString ["type"] == null ? string.Empty : Request.QueryString ["type"].ToString ();

                            ContractualArrangement arra = ContractualArrangement.Plant | ContractualArrangement.Transitory;

                            if (type == "trans")
                                arra &= ContractualArrangement.Transitory;

                            if (type == "plant")
                                arra &= ContractualArrangement.Plant;

                            if (int.TryParse(_txt_id.Value, out eventid))
                            {
                                MilitancyReport report = new MilitancyReport(eventid, arra);
                                report.Save(@"C:\ricki\report.pdf");


                                //f.RegisterStartupScript(panel, Me.GetType(), "redirectMe", "location.href='Aju.aspx?SplashFrom=0';", True)
                            }
                        */
                    }
                    break;

                    default:
                    filename = @"C:\ricki\r.pdf";
                    break;

            }

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-disposition", "attachment; filename=" + filename);

            Response.WriteFile(filename);
            Response.Flush();
            Response.Close();
        }

        private string get_query_value(string key)
        {
            return Request.QueryString[key] == null ? string.Empty : Request.QueryString[key].ToString();
        }
    }
}
