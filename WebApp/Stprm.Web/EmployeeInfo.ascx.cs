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

using Stprm.DataEx;

namespace Stprm.Web
{

    public partial class EmployeeInfo : System.Web.UI.UserControl
    {

        protected override void OnLoad(EventArgs e)
        {
            //_txt_id.Attributes.Add ("onkeypress", "return clickButton(event,'" + pnledit_btn_add.ClientID + "')");
            base.OnLoad(e);
        }

        public void ClearData()
        {
            Id = string.Empty;
            Name = string.Empty;
            ContractualArrangement = string.Empty;
            PositionNum = string.Empty;
            Level = string.Empty;
            Classif = string.Empty;
            Section = string.Empty;
            ValidityStart = string.Empty;
            ValidityEnd = string.Empty;
            Category = string.Empty;
            Journey = string.Empty;
            Department = string.Empty;
        }

        public void SetEditable(bool state)
        {
            foreach (Control control in Controls)
            {
                if (control is TextBox)
                    ((TextBox)control).ReadOnly = true;
            }
        }

        public void UpdateFromEmployee(Trabajador trabajador)
        {
            UpdateFromEmployee(trabajador, true);
        }

        public void UpdateFromEmployee(Trabajador trabajador, bool editable)
        {
            SetEditable(editable);
            Id = trabajador.Ficha;
            Name = trabajador.GetNombreCompleto();
            //ContractualArrangement = DataMisc.ContractualArrangementToString(employee.ContractualArrangement);
            ContractualArrangement = trabajador.RegimenContractual;


            Contrato contrato; 
            
            
            if (trabajador.GetUltimoContrato(out contrato))
            {
                Classif = contrato.Clasificacion;
                Category = contrato.Categoria;
                Section = "26";
                Level = contrato.Nivel;

                ValidityStart = contrato.Inicio.ToString ("dd/MM/yyyy");
                ValidityEnd = contrato.Termino.ToString("dd/MM/yyyy");
                Journey = contrato.Jornada;
                Department = contrato.Depto;
            }
        }

        public string Id
        {
            get { return _txt_id.Text; }
            set { _txt_id.Text = value; }
        }

        public string Name
        {
            get { return _txt_name.Text; }
            set { _txt_name.Text = value; }
        }

        public string ContractualArrangement
        {
            get { return _txt_contr.Text; }
            set { _txt_contr.Text = value; }
        }

        public string PositionNum
        {
            get { return _txt_posnum.Text; }
            set { _txt_posnum.Text = value; }
        }

        public string Level
        {
            get { return _txt_level.Text; }
            set { _txt_level.Text = value; }
        }

        public string Classif
        {
            get { return _txt_clasif.Text; }
            set { _txt_clasif.Text = value; }
        }

        public string Section
        {
            get { return _txt_section.Text; }
            set { _txt_section.Text = value; }
        }

        public string Category
        {
            get { return _txt_category.Text; }
            set { _txt_category.Text = value; }
        }

        public string Journey
        {
            get { return _txt_jor.Text; }
            set { _txt_jor.Text = value; }
        }

        public string Department
        {
            get { return _txt_depto.Text; }
            set { _txt_depto.Text = value; }
        }

        public string ValidityStart
        {
            get { return _txt_validity_start.Text; }
            set { _txt_validity_start.Text = value; }
        }

        public string ValidityEnd
        {
            get { return _txt_validity_end.Text; }
            set { _txt_validity_end.Text = value; }
        }
    }
}
