using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using MedLib.Controls;
using Stprm.Data;
using Stprm.DataEx;
using Stprm.Reports;

namespace Stprm.Web
{
    public partial class EventList : System.Web.UI.UserControl
    {

        public BaseDatos Datos;

        protected override void OnLoad(EventArgs e)
        {
            _isc_search.TxtId.Attributes.Add("onkeypress", "return clickButton(event,'" + _isc_search.ButtonOk.ClientID + "')");
            _grid_members.PageIndexChanging += new GridViewPageEventHandler(_grid_members_PageIndexChanging);
            _grid_members.RowDataBound += _grid_members_RowDataBound;
            _grid_members.RowCreated += new GridViewRowEventHandler(_grid_members_RowCreated);
            _grid_members.AllowPaging = true;
            _grid_members.PageSize = 50;

            _cmb_places.SelectedIndexChanged += _cmb_places_SelectedIndexChanged;
            _cbb_showtype.SelectedIndexChanged += _cbb_showtype_SelectedIndexChanged;

            _btn_report.Click += _btn_report_Click;

            _isc_search.Busqueda += _isc_search_SearchResultEvent;

            Datos = BaseDatos.CreateStprmConnection ();

            if (!IsPostBack)
            {
                
                //Database db = new Database("Mercurio", "ricki", "09b9085a+", "seccion26");

                //using (Database db = Database.CreateStprmConnection())
                //if (db.Open ())
                using (Database db = BaseDatos.CreateOldConnection ())
                {
                    ListItem item = new ListItem("Todos", "Todos");
                    _cmb_places.Items.Add(item);
                    foreach (string place in Event.GetPlaces(db, string.Empty))
                    {
                        item = new ListItem(place, place);
                        _cmb_places.Items.Add(item);
                    }
                }

                _btn_report.Attributes.Add("onclick", "location.href='ViewReport.aspx'");

                loadEvents();

                if (_lst_events.Items.Count == 0)
                {
                    btn_new_Click(this, EventArgs.Empty);
                }
            }
        }

        void _grid_members_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //_grid_members.PageIndex = e.NewPageIndex;
            //e.Cancel = false;
            //_grid_members.DataBind();
            //_grid_members.
        }

        void _isc_search_SearchResultEvent(object sender, TrabajadorEventArgs  args)
        {
            if (args.Exists)
                pnledit_btn_add_Click(args.Trabajador);
            else
                _isc_search.LabelMsg.Text = "La ficha no existe";
        }

        void _cmb_places_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadEvents();
            SetTextBoxesBold(false);
            _txt_name.Text = _txt_address.Text = _txt_date.Text = string.Empty;

            _pnl_edit.Visible = false;
            _btn_save.Visible = false;
        }

        void _grid_members_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //	e.Row.ForeColor = System.Drawing.Color.Red;
        }

        void _btn_report_Click(object sender, EventArgs e)
        {
            int eventid;

            ContractualArrangement arra = ContractualArrangement.Plant | ContractualArrangement.Transitory;

            if (_cbb_showtype.SelectedValue == "trans")
                arra &= ContractualArrangement.Transitory;

            if (_cbb_showtype.SelectedValue == "plant")
                arra &= ContractualArrangement.Plant;

            if (int.TryParse(_txt_id.Value, out eventid))
            {
                MilitancyReport report = new MilitancyReport(eventid, arra);
                report.Save(@"C:\ricki\report.pdf");


                //f.RegisterStartupScript(panel, Me.GetType(), "redirectMe", "location.href='Aju.aspx?SplashFrom=0';", True)
            }
        }

        private void _cbb_showtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_edit_Click(_btn_edit, EventArgs.Empty);
        }

        protected void _grid_members_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string lastid = ViewState["LastAddedId"] == null ? string.Empty : ViewState["LastAddedId"].ToString();

            if (lastid == string.Empty)
                return;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#DDDDFF'");
                //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='transparent'");
                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cell.Text == lastid)
                    {
                        //foreach (TableCell tmpcell in e.Row.Cells) {
                        //cell.Text = "* " + cell.Text;
                        
                        e.Row.ForeColor = System.Drawing.Color.Green;
                        e.Row.Font.Bold = true;
                        //}
                        //break;
                    }
                }
            }
        }

        public void SetTextBoxesBold(bool state)
        {
            _lbl_name.Font.Bold = state;
            _lbl_place.Font.Bold = state;
            _lbl_date.Font.Bold = state;

            _txt_name.Font.Bold = state;
            _txt_address.Font.Bold = state;
            _txt_date.Font.Bold = state;
        }

        protected void _lst_events_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTextBoxesBold(false);
            _txt_name.Text = _txt_address.Text = _txt_date.Text = string.Empty;

            _pnl_edit.Visible = false;
            _btn_save.Visible = false;

            using (BaseDatos datos = BaseDatos.CreateStprmConnection ())
            {
                //Event evnt = new Event(db);
                Evento evento = new Evento (datos);

                int intval = 0;
                if (int.TryParse(_lst_events.SelectedValue.ToString(), out intval))
                {
                    evento.Id = intval;
                    if (evento.Actualizar ())
                    {
                        _txt_id.Value = evento.Id.ToString("0");
                        _txt_name.Text += evento.Nombre;
                        _txt_address.Text = evento.Lugar;
                        _txt_date.Text = evento.Fecha.ToShortDateString();
                    }
                }
            }
        }

        protected string GetContractual(string sit)
        {
            //ContractualArrangement cont_arr = DataMisc.ContractualArrangementFromString (sit);
            //return DataMisc.ContractualArrangementToString (cont_arr);
            return "Hola";
        }

        private void loadEvents()
        {
            _lst_events.Items.Clear();

            using (Database db = BaseDatos.CreateOldConnection ())
            {
                EventCollection events = Event.GetByHint(db, string.Empty, SelectedPlace);

                foreach (Event evnt in events)
                {
                    _lst_events.Items.Add(new ListItem(evnt.Name, evnt.Id.ToString()));
                }
            }

            SetTextBoxesBold(false);
        }

        private void selectEventId(int id)
        {
            foreach (ListItem item in _lst_events.Items)
                if (item.Value == id.ToString("0"))
                {
                    item.Selected = true;
                    return;
                }
        }

        protected void btn_delete_Click(object sender, EventArgs args)
        {
            int eventoid;
            int trabajadorid;

            Button button = (Button)sender;

            SetTextBoxesBold(false);

            if (int.TryParse(_txt_id.Value, out eventoid) && int.TryParse(button.CommandName, out trabajadorid))
            {
                using (BaseDatos bd = BaseDatos.CreateStprmConnection())
                {
                    Evento evento = new Evento(bd);
                    evento.Id = eventoid;

                    Trabajador trabajador = new Trabajador(bd);
                    trabajador.Ficha = trabajadorid.ToString();

                    if (evento.Actualizar() && trabajador.Actualizar())
                    {
                        if (evento.EsParticipante(trabajador))
                        {
                            evento.Eliminar(trabajador);
                            string current_text = _isc_search.TxtId.Text;
                            btn_edit_Click(_btn_edit, args);
                            _isc_search.TxtId.Text = current_text;
                        }
                    }
                }
            }
        }

        /*
        protected void btn_delete_Click(object sender, EventArgs args)
        {
            SetTextBoxesBold(false);
            Button button = (Button)sender;

            //_isc_search.LabelMsg.Text = string.Empty;

            int eventid;
            int employeeid;

            if (int.TryParse(_txt_id.Value, out eventid) && int.TryParse(button.CommandName, out employeeid))
            {
                using (Database db = Database.CreateStprmConnection())
                {
                    Event evnt = new Event(db);
                    evnt.Id = eventid;
                    Employee employee = new Employee(db);
                    employee.Id = employeeid;

                    if (evnt.Update() && employee.Update())
                    {
                        if (evnt.IsParticipant(employee))
                        {
                            evnt.Remove(employee);
                            string current_text = _isc_search.TxtId.Text;
                            btn_edit_Click(_btn_edit, args);
                            _isc_search.TxtId.Text = current_text;
                        }
                    }
                }
            }
        }
        */
        protected void btn_new_Click(object sender, EventArgs args)
        {
            _txt_name.Text = string.Empty;
            _txt_address.Text = _cmb_places.SelectedValue == "Todos" ? string.Empty : _cmb_places.SelectedValue;
            _txt_date.Text = string.Empty;
            _txt_id.Value = "0";

            _pnl_edit.Visible = false;
            _btn_save.Visible = true;
            SetTextBoxesBold(true);
            _txt_name.Focus();
        }

        protected void btn_edit_Click(object sender, EventArgs args)
        {
            int intval;

            if (_lst_events.SelectedIndex == -1) return;

            _lst_events_SelectedIndexChanged(this, EventArgs.Empty);

            _isc_search.LabelMsg.Text = string.Empty;
            _pnl_edit.Visible = true;
            _btn_save.Visible = true;
           
                        DataSet ds = new DataSet ();
                        using (BaseDatos bd = BaseDatos.CreateStprmConnection ()) {
                            Evento evento = new Evento(bd);

                            if (int.TryParse(_txt_id.Value, out intval))

                            evento.Id = intval;
                            ds.Clear();
                            ds.Tables.Clear();

                            evento.ParticipantesEnAdapter().Fill(ds);

                            CargarDatos (ds, bd);

                            _grid_members.DataSource = ds;
                            _grid_members.DataBind();
                        }
                 //   }
                //}

            //}

            _grid_members.SelectedIndex = 0;
            _isc_search.TxtId.Text = string.Empty;

            SetTextBoxesBold(true);
            _isc_search.TxtId.Focus();

        }

        private void CargarDatos(DataSet ds, BaseDatos db)
        {
            
            DataTable table = ds.Tables[0];

            //DataColumn column_sc = new DataColumn("Sit.Contractual", typeof(string));
            //table.Columns.Add(column_sc);

            //table.AcceptChanges ();
            
            /****
             * Activar para alentar
             * **/
            /*
            foreach (DataRow row in table.Rows) {
                string ficha = row [0].ToString ();
                Trabajador trab =  new Trabajador (db);
                trab.Ficha = ficha;
                if (trab.Actualizar ()) {
                    row [2] = trab.RegimenContractual;
                }
            }
             * */
        }

        protected void _btn_save_Click(object sender, EventArgs e)
        {
            int id = 0;
            DateTime date = DateTime.Now;

            if (int.TryParse(_txt_id.Value, out id) && DateTime.TryParse(_txt_date.Text, out date))
            {
                using (Database db = BaseDatos.CreateOldConnection())
                {
                    Event evnt = new Event(db);
                    evnt.Id = id;
                    evnt.Name = _txt_name.Text;
                    evnt.Address = _txt_address.Text;
                    evnt.Date = date;

                    if (evnt.Save())
                    {
                        _btn_save.Visible = false;
                        _pnl_edit.Visible = false;
                        loadEvents();
                        selectEventId(evnt.Id);
                    }
                }
            }

            SetTextBoxesBold(false);
        }

        protected void pnledit_btn_add_Click(Trabajador trabajador)
        {
            Evento evento = new Evento(trabajador.Bd);

            int id;

            if (int.TryParse(_txt_id.Value, out id))
            {
                evento.Id = id;

                if (evento.Actualizar())
                    if (evento.EsParticipante(trabajador))
                        _isc_search.LabelMsg.Text = "El trabajador ya es participante";
                    else
                    {
                        evento.Agregar(trabajador, "Apoyo");
                        ViewState["LastAddedId"] = trabajador.Ficha;
                        btn_edit_Click(_btn_edit, EventArgs.Empty);
                    }
            }
        }

        /*
        protected void pnledit_btn_add_Click(Trabajador trabajador)
        {
            using (Database db = Database.CreateStprmConnection())
            {
                Employee employee = new Employee(db);
                employee.Id = int.Parse(trabajador.Ficha);

                if (employee.Update())
                {
                    int id;
                    if (int.TryParse(_txt_id.Value, out id))
                    {
                        Event evnt = new Event(employee.Db);
                        evnt.Id = id;
                        if (evnt.Update())
                        {
                            if (evnt.IsParticipant(employee))
                            {
                                _isc_search.LabelMsg.Text = "El trabajador ya es participante";
                            }
                            else
                            {
                                evnt.Add(employee);

                                ViewState["LastAddedId"] = employee.Id.ToString();
                                btn_edit_Click(_btn_edit, EventArgs.Empty);
                            }
                        }
                    }
                }
            }
        }
        */

        public string SelectedPlace
        {
            get
            {
                if (_cmb_places.SelectedValue == "Todos") return string.Empty;
                return _cmb_places.SelectedValue;
            }
        }
    }
}
