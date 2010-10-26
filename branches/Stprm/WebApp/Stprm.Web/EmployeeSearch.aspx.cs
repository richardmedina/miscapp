using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
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

    public partial class EmployeeSearch : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _pnl_reports.Visible = _txt_data_id.Text.Trim().Length > 0;
        }

        private void _btn_report_simpleClick(object sender, EventArgs args)
        {
            //Report report = new Report ();
            //System.IO.Directory.CreateDirectory("Reports");
            //report.Filename =  @"Reports\report.pdf";
            //report.Create ();

            //Response.Write (string.Format ("new_window ('{0}')", "Reports/report.pdf"));
            /*
            EmployeeSimpleReport report = new EmployeeSimpleReport ();
            report.Render ();
		
            using (System.IO.BinaryWriter bw = new System.IO.BinaryWriter (new System.IO.FileStream (@"C:\ricki\r.pdf", System.IO.FileMode.Create))) {
                bw.Write (report.Stream.GetBuffer ());
            }*/
        }

        protected void _btn_okClick(object sender, EventArgs e)
        {
            _pnl_reports.Visible = false;
            //(@"C:\users\ricardo\Documents\Stprm\seccion26\database.mdb");
            //Database db = new Database(Server.MapPath (@"~\App_Data\database.mdb"));
            clear_forms_data(updatepanel2);

            using (Database db = Database.CreateStprmConnection())
            {
                Employee employee = new Employee(db);

                int intval;

                if (int.TryParse(_txt_id.Text, out intval))
                {
                    employee.Id = intval;
                    if (employee.Update())
                    {
                        _txt_data_id.Text = employee.Id.ToString();
                        _txt_data_name.Text = string.Format("{0} {1} {2}",
                            employee.FirstName, employee.MiddleName, employee.LastName);
                        _txt_data_borndate.Text = employee.BornDate.ToString("dd/MM/yyyy");

                        _txt_data_street.Text = employee.Street;
                        _txt_data_colony.Text = employee.Colony;
                        _txt_data_locality.Text = employee.Locality;
                        _txt_data_state.Text = employee.State;
                        _txt_data_postal.Text = employee.PostalCode.ToString();
                        _txt_data_civil.Text = employee.CivilState;
                        _txt_data_phone_personal.Text = employee.Phone;
                        _txt_data_phone_emergency.Text = "No Implementado";
                        _txt_data_rfc.Text = employee.Rfc;
                        _txt_data_curp.Text = employee.Curp;

                        _txt_data_reg.Text = employee.ContractualArrangement.ToString();

                        /*****************************
                         * Current category filling (if any)
                         * **************************/

                        ContractCollection contracts = employee.GetContracts();

                        if (contracts.Count > 0)
                        {
                            Contract contract = contracts[0];
                            Position position = contract.Position;
                            Category category = position.GetCategory();
                            Department department = position.GetDepartment();

                            _txt_currentposition_posnum.Text = position.Id;
                            _txt_currentposition_section.Text = "26";
                            if (department != null)
                                _txt_currentposition_depto.Text = department.Name;

                            _txt_currentposition_validity_start.Text = contract.StartingDate.ToString("dd/MM/yyyy");
                            _txt_currentposition_validity_end.Text = contract.EndingDate.ToString("dd/MM/yyy");

                            if (category != null)
                            {
                                _txt_currentposition_level.Text = category.Level;
                                _txt_currentposition_category.Text = category.Name;
                                _txt_currentposition_clasif.Text = category.Classification;
                            }
                            _txt_currentposition_jor.Text = position.Jor.ToString();

                            if (department != null)
                                _txt_currentposition_depto.Text = string.Format("-{0}- {1}",
                                    department.Id, department.Name);

                        }

                        /***************************
                         * Let's load parents information
                         * from medical services
                         * ************************/

                        DataSet dataset = new DataSet();
                        IDataAdapter adapter = employee.GetParentsInAdapter();

                        adapter.Fill(dataset);
                        _grid_parents_list.DataSource = dataset;
                        _grid_parents_list.DataBind();

                        Recomendation recomendation = new Recomendation(db);
                        recomendation.Id = employee.Id;
                        recomendation.Update();

                        Employee recommended = new Employee(db);
                        recommended.Id = recomendation.RecommendedId;
                        if (recommended.Update())
                        {
                            _txt_recom_id.Text = recommended.Id.ToString();
                            _txt_recom_name.Text = string.Format("{0} {1} {2}",
                                recommended.FirstName, recommended.MiddleName, recommended.LastName);
                            _txt_recom_borndate.Text = recommended.BornDate.ToString("dd.MM.yyyy");

                            _txt_recom_street.Text = recommended.Street;
                            _txt_recom_colony.Text = recommended.Colony;
                            _txt_recom_locality.Text = recommended.Locality;
                            _txt_recom_state.Text = recommended.State;
                            _txt_recom_postal.Text = recommended.PostalCode.ToString();
                            _txt_recom_civil.Text = recommended.CivilState;
                            _txt_recom_phone_personal.Text = recommended.Phone;
                            _txt_recom_phone_emergency.Text = "No Implementado";
                            _txt_recom_rfc.Text = recommended.Rfc;
                            _txt_recom_curp.Text = recommended.Curp;

                            _txt_recom_reg.Text = recommended.ContractualArrangement.ToString();

                        }

                        dataset.Clear();
                        dataset.Tables.Clear();
                        recommended.GetContractsInAdapter().Fill(dataset);
                        _grid_contracts_recom.DataSource = dataset;
                        _grid_contracts_recom.DataBind();

                        adapter = recommended.GetMilitancyInAdapter();

                        dataset.Clear();
                        dataset.Tables.Clear();

                        adapter.Fill(dataset);

                        _grid_socialwork_recom.CellPadding = 5;
                        _grid_socialwork_recom.CellSpacing = 5;

                        _grid_socialwork_recom.DataSource = dataset;
                        _grid_socialwork_recom.DataBind();

                        adapter = employee.GetDebitsInAdapter();

                        dataset.Clear();
                        dataset.Tables.Clear();
                        adapter.Fill(dataset);

                        _grid_debits.DataSource = dataset;
                        _grid_debits.DataBind();

                        adapter = employee.GetMilitancyInAdapter();

                        dataset.Clear();
                        dataset.Tables.Clear();
                        adapter.Fill(dataset);

                        _grid_militancy.DataSource = dataset;
                        _grid_militancy.DataBind();

                        adapter = employee.GetThirdPartiesInAdapter();
                        dataset.Clear();
                        dataset.Tables.Clear();
                        adapter.Fill(dataset);

                        _grid_thirdparties.DataSource = dataset;
                        _grid_thirdparties.DataBind();

                        dataset.Clear();
                        dataset.Tables.Clear();
                        employee.GetContractsInAdapter().Fill(dataset);
                        _grid_contracts.DataSource = dataset;
                        _grid_contracts.DataBind();

                        dataset.Clear();
                        dataset.Tables.Clear();

                        adapter = employee.GetBenefitsInAdapter();
                        dataset.Clear();
                        dataset.Tables.Clear();
                        adapter.Fill(dataset);

                        _grid_benefits.DataSource = dataset;
                        _grid_benefits.DataBind();

                        _pnl_reports.Visible = true;
                    }
                    db.Close();
                }
            }
        }

        protected void _btn_showreportClick(object sender, EventArgs args)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-disposition", "attachment; filename=" + @"C:\ricki\r.pdf");
            Response.WriteFile(@"C:\ricki\r.pdf");
            Response.Flush();
            Response.Close();
        }

        protected void OnRowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime d;
                foreach (TableCell cell in e.Row.Cells)
                    if (DateTime.TryParse(cell.Text, out d))
                        cell.Text = d.ToString("dd/MM/yyyy");
            }
        }

        private void clear_forms_data(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is GridView)
                {
                    ((GridView)control).DataSource = null;
                    ((GridView)control).DataBind();
                }
                if (control is TextBox)
                    ((TextBox)control).Text = string.Empty;
                if (control.HasControls())
                    clear_forms_data(control);
            }
        }

    }
}