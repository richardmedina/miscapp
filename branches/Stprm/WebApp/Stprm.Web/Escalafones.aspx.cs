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

        public void AgregarColumnasDias(DataSet ds, BaseDatos datos)
        {
            DataTable table = ds.Tables[0];

            DataColumn column_lab = new DataColumn("DiasLab", typeof(string));
            DataColumn column_mili = new DataColumn("DiasMili", typeof(string));

            table.Columns.Add(column_lab);
            table.Columns.Add(column_mili);

            table.AcceptChanges();


            for (int i = 0; i < table.Rows.Count; i++)
            {
                string ficha_rec = table.Rows[i][5].ToString();

                Trabajador recomendado = new Trabajador(datos);
                recomendado.Ficha = ficha_rec;
                if (recomendado.Actualizar())
                {
                    int dias_lab = recomendado.GetDiasLab();
                    int dias_mili = recomendado.GetDiasMilitancia();
                    table.Rows[i][8] = dias_lab;
                    table.Rows[i][9] = dias_mili;
                }
            }
        }

        private void _cmb_escalafon_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            using (BaseDatos bd = BaseDatos.CreateStprmConnection())
            {
                Escalafon.GetDesdeNombre(bd, _cmb_escalafon.SelectedItem.Text).Fill(ds);
                AgregarColumnasDias(ds, bd);
            }
            _gv_escalafones.DataSource = ds;
            _gv_escalafones.DataBind();
        }
    }
}