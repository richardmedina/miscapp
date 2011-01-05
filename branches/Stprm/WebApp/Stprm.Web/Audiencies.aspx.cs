using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Stprm.Data;
using Stprm.DataEx;

namespace Stprm.Web
{
    public partial class Audiencies : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            DateTime date = DateTime.Now;

            _radio_today.Text = string.Format("Audiencia para hoy ({0})",
                DateTime.Now.ToString("dd-MM-yyyy"));

            _txt_matter.Attributes.Add("onkeypress", "return clickButton(event,'" + _btn_add.ClientID + "')");
            _txt_id.Attributes.Add("onkeypress", "return focusControl(event,'" + _txt_matter.ClientID + "')");

            _radio_today.CheckedChanged += _radio_CheckedChanged;
            _radio_later.CheckedChanged += _radio_CheckedChanged;

            _btn_aud.Click += _btn_aud_Click;
            _btn_add.Click += new EventHandler(_btn_add_Click);

            _txt_date.TextChanged += _txt_date_TextChanged;

            //_grid_employees.RowCommand += new GridViewCommandEventHandler(_grid_employees_RowCommand);
            //_grid_employees.RowDeleted += new GridViewDeletedEventHandler(_grid_employees_RowDeleted);
            //_grid_employees.RowDeleting += new GridViewDeleteEventHandler(_grid_employees_RowDeleting);
            _cmb_type.SelectedIndexChanged += new EventHandler(_cmb_type_SelectedIndexChanged);
            if (!IsPostBack)
                update_audience(date);
        }

        void _cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cmb_type.SelectedValue == "worker")
            {
                _lbl_type.Text = "Ficha: ";
            }
            else
            {
                _lbl_type.Text = "Nombre";
            }
        }
        /*
            void _grid_employees_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
		
            }

            void _grid_employees_RowDeleted(object sender, GridViewDeletedEventArgs e)
            {
		
            }
        */
        protected void btn_delete_Click(object sender, EventArgs args)
        {
            Button button = (Button)sender;

            //int index = Convert.ToInt32(button.CommandName);

            //int id = int.Parse(_grid_employees.DataKeys[index].Value.ToString());

            int id = int.Parse(button.CommandName);

            using (Database db = Database.CreateStprmConnection())
            {
                Audience aud = new Audience(db);
                aud.Date = SelectedDate;
                if (aud.Update())
                {
                    aud.Remove(id);
                    update_audience(SelectedDate);
                    _txt_id.Focus();
                }
            }
        }
       
        private void _txt_date_TextChanged(object sender, EventArgs e)
        {
            update_audience(SelectedDate);
        }

        private void _btn_add_Click(object sender, EventArgs e)
        {
            int id;

            if (_cmb_type.SelectedValue == "worker")
            {
                if (int.TryParse(_txt_id.Text, out id))
                    using (BaseDatos datos = BaseDatos.CreateStprmConnection ())
                    {

                        Trabajador trabajador = new Trabajador(datos);
                        trabajador.Ficha = id.ToString("000000");

                        if (trabajador.Actualizar ())
                        {

                            //Audience audience = new Audience(datos);
                            Audiencia audiencia = new Audiencia(datos);

                            audiencia.Fecha = SelectedDate;

                            if (audiencia.ActualizarDesdeFecha())
                            {
                                //audience.Add(employee, _txt_matter.Text);
                                audiencia.Agregar(trabajador, _txt_matter.Text);
                                _txt_id.Text = string.Empty;
                                _txt_matter.Text = string.Empty;
                                update_audience(SelectedDate);
                            }
                        }
                    }
            }
            else
            {
                using (Database db = Database.CreateStprmConnection())
                {
                    Audience audience = new Audience(db);
                    audience.Date = SelectedDate;
                    if (audience.Update())
                    {
                        audience.Add(_txt_id.Text, _txt_matter.Text);
                        _txt_id.Text = string.Empty;
                        _txt_matter.Text = string.Empty;
                        update_audience(SelectedDate);
                        _txt_id.Focus();
                    }
                }
            }
        }

        private void update_audience(DateTime date)
        {
            _pnl_employees.Visible = false;
            _btn_aud.Text = "Crear Audiencia";

            using (Database db = Database.CreateStprmConnection())
            {
                Audience audience = new Audience(db);
                audience.Date = date;

                if (audience.Update())
                {
                    _chk_plant.Checked = audience.AllowPlant;
                    _chk_trans.Checked = audience.AllowTransitory;
                    _chk_extern.Checked = audience.AllowExtern;

                    _pnl_employees.Visible = true;
                    _btn_aud.Text = "Actualizar Audiencia";

                    DataSet ds = new DataSet();
                    IDataAdapter adapter = audience.GetParticipantsInAdapter();

                    adapter.Fill(ds);

                    _grid_employees.DataSource = ds;
                    _grid_employees.DataBind();
                }
            }
        }

        private void _btn_aud_Click(object sender, EventArgs e)
        {
            using (Database db = Database.CreateStprmConnection())
            {
                Audience audience = new Audience(db);
                audience.Date = SelectedDate;

                audience.AllowPlant = _chk_plant.Checked;
                audience.AllowTransitory = _chk_trans.Checked;
                audience.AllowExtern = _chk_extern.Checked;

                if (!audience.AllowExtern && !audience.AllowPlant && !audience.AllowTransitory)
                {
                    _btn_aud.Text = "Selecciona un tipo";
                    return;
                }
                if (audience.Save())
                {
                    update_audience(audience.Date);
                }
            }
        }

        private void _radio_CheckedChanged(object sender, EventArgs e)
        {
            _radio_today.Font.Bold = _radio_today.Checked;
            _radio_later.Font.Bold = _radio_later.Checked;
            _txt_date.Enabled = _radio_later.Checked;
            _txt_date.Visible = _radio_later.Checked;

            DateTime date = DateTime.Now;

            if (_radio_later.Checked)
            {
                _txt_date.Text = DateTime.Now.ToString("dd-MM-yyyy");

                if (!DateTime.TryParse(_txt_date.Text, out date))
                    date = DateTime.Now;
            }

            update_audience(date);
        }

        public DateTime SelectedDate
        {
            get
            {
                DateTime date = DateTime.Now;

                if (_radio_later.Checked)
                {
                    if (!DateTime.TryParse(_txt_date.Text, out date))
                        date = DateTime.Now;
                }

                return date;
            }
        }
    }
}