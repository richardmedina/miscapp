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
    public partial class EmployeeNew : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _txt_id.Focus();
        }

        protected void btn_saveClick(object sender, EventArgs args)
        {
            using (BaseDatos datos = BaseDatos.CreateStprmConnection ()) {
                Trabajador trabajador = new Trabajador (datos);
                trabajador.Ficha = _txt_id.Text;
                trabajador.Nombre = _txt_firstname.Text + " " + _txt_middlename.Text + " " + _txt_lastname.Text;
                trabajador.RegimenContractual = _cmb_arrangement.SelectedValue;
                if (trabajador.GuardarComoInexistente (_txt_depto.Text)) {
                    _lbl_msg.Text = "Trabajador Guardado";
                } else _lbl_msg.Text = "Error Registrando Trabajador";
            }
        }
        /*
        protected void btn_saveClick(object sender, EventArgs args)
        {
            int id;
            _lbl_msg.Text = "";
            if (!int.TryParse(_txt_id.Text, out id))
                id = 0;

            using (Database db = Database.CreateStprmConnection())
            {
                EmployeeEx employee = new EmployeeEx(db);
                employee.Id = id;

                if (!employee.Exists())
                {
                    employee.FirstName = _txt_firstname.Text;
                    employee.MiddleName = _txt_middlename.Text;
                    employee.LastName = _txt_lastname.Text;

                    employee.ContractualArrangement = _cmb_arrangement.SelectedValue == "plant" ? ContractualArrangement.Plant : ContractualArrangement.Transitory;

                    employee.Save();
                    _lbl_msg.Text = "Agregado";
                }
                else
                    _lbl_msg.Text = "El trabajador ya existe";
            }
         
        }*/
    }
}