using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Stprm.DataEx;

namespace Stprm.Web
{
    public partial class Escalafones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _cmb_escalafon.SelectedIndexChanged += _cmb_escalafon_SelectedIndexChanged;
            
            if (!IsPostBack)
            {
                using (BaseDatos bd = BaseDatos.CreateStprmConnection())
                {
                    _cmb_escalafon.Items.Clear();
                    foreach (string str in Escalafon.GetNombres(bd))
                        _cmb_escalafon.Items.Add(str);
                }
            }
            _cmb_escalafon_SelectedIndexChanged(_cmb_escalafon, EventArgs.Empty);
        }

        private void _cmb_escalafon_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            using (BaseDatos bd = BaseDatos.CreateStprmConnection())
            {
                Escalafon.GetDesdeNombre(bd, _cmb_escalafon.SelectedItem.Text).Fill(ds);
            }
            _gv_escalafones.DataSource = ds;
            _gv_escalafones.DataBind();
        }
    }
}