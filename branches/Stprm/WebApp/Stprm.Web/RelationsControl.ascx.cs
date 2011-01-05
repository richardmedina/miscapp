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

namespace Stprm.Web
{

    public partial class RelationsControl : System.Web.UI.UserControl
    {
        protected override void OnLoad(EventArgs e)
        {
            _txt_id.Attributes.Add("onkeypress", "return clickButton(event,'" + _btn_search.ClientID + "')");
            _txt_id_recom.Attributes.Add("onkeypress", "return clickButton(event,'" + _btn_search_recom.ClientID + "')");

            _btn_search.Click += btn_search_Click;
            _btn_assign.Click += btn_assign_Click;
            _btn_search_recom.Click += btn_search_recom_Click;
            _btn_assign_cancel.Click += btn_assign_cancel_Click;
        }

        void btn_assign_cancel_Click(object sender, EventArgs e)
        {
            btn_search_Click(_btn_search, e);
        }

        private void btn_search_Click(object sender, EventArgs args)
        {
            /*
            _lbl_recom_result.Visible = false;
            _txt_id_recom.Text = string.Empty;

            using (Database db = Database.CreateStprmConnection())
            {
                Employee employee = new Employee(db);
                int intval = 0;

                if (int.TryParse(_txt_id.Text, out intval))
                {
                    employee.Id = intval;
                    if (employee.Update())
                    {
                        _lbl_search.Visible = true;
                        if (employee.ContractualArrangement == ContractualArrangement.Plant)
                        {
                            _ei_plant.UpdateFromEmployee(employee);
                            _ei_plant.Visible = true;
                            _lbl_search.Visible = false;
                            Recomendation rec = new Recomendation(db);
                            rec.Id = employee.Id;
                            if (rec.Update())
                            {
                                Employee recom = new Employee(db);
                                recom.Id = rec.RecommendedId;
                                _btn_assign.Enabled = false;

                                if (recom.Update())
                                {
                                    _ei_trans.UpdateFromEmployee(recom);
                                    _ei_trans.Visible = true;
                                }
                                else
                                {
                                    _ei_trans.Visible = false;
                                }
                            }
                            else
                            {
                                _ei_trans.Visible = false;
                            }
                        }
                        else
                        {
                            _lbl_search.Text = "Trabajador existe pero no es de planta..";
                            _ei_plant.ClearData();
                            _ei_trans.Visible = false;
                        }
                    }
                    else
                    {
                        _ei_plant.ClearData();
                        _ei_plant.Visible = false;
                        _lbl_search.Text = "Trabajador no existe";
                        _lbl_search.Visible = true;
                        _ei_trans.Visible = false;
                    }
                }
            }*/
        }

        private void btn_search_recom_Click(object sender, EventArgs args)
        {
            /*
            using (Database db = Database.CreateStprmConnection())
            {
                Employee employee = new Employee(db);
                int intval = 0;

                if (int.TryParse(_txt_id_recom.Text, out intval))
                {
                    employee.Id = intval;
                    if (employee.Update())
                    {
                        if (employee.ContractualArrangement == ContractualArrangement.Plant)
                        {
                            _lbl_recom_result.Text = "No se puede recomendar a un trabajador de planta";
                            _lbl_recom_result.Visible = true;
                            return;
                        }
                        _lbl_recom_result.Visible = false;
                        _ei_trans.UpdateFromEmployee(employee);
                        _ei_trans.Visible = true;
                        _btn_assign.Enabled = true;
                        _btn_assign_cancel.Enabled = true;
                    }
                    else
                    {
                        _lbl_recom_result.Text = "Trabajador no existe";
                        _lbl_recom_result.Visible = true;
                    }
                }
                else
                {
                    _lbl_recom_result.Text = "Ficha inválida";
                    _lbl_recom_result.Visible = true;
                }

            }*/
        }

        private void btn_assign_Click(object sender, EventArgs args)
        {
            int id;
            int recomid;

            if (int.TryParse(_ei_plant.Id, out id) && int.TryParse(_ei_trans.Id, out recomid))
            {
                using (Database db = Database.CreateStprmConnection())
                {
                    Recomendation rec = new Recomendation(db);
                    rec.Id = id;
                    rec.RecommendedId = recomid;
                    if (rec.Save())
                    {
                        _btn_assign.Enabled = false;
                    }
                }
            }

        }
    }
}