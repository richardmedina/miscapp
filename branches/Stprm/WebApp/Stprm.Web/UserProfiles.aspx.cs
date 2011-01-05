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
    public partial class UserProfiles : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _btn_new.Click += _btn_new_Click;
            _btn_cancel.Click += _btn_cancel_Click;
            _btn_save.Click += btn_save_Click;

            if (!IsPostBack)
                load_users();
        }

        private void _btn_cancel_Click(object sender, EventArgs e)
        {
            clear_textboxes();
            _pnl_userdata.Visible = false;
            _btn_save.Enabled = false;
            _btn_new.Enabled = true;
        }

        public void load_users()
        {
            using (Database db = BaseDatos.CreateOldConnection ())
            {
                IDataAdapter adapter = UserProfile.GetAllInAdapter(db);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                _grid_profiles.DataSource = ds;
                _grid_profiles.DataBind();
            }
        }

        protected void btn_edit_Click(object sender, EventArgs args)
        {
            Button button = sender as Button;


            using (Database db = BaseDatos.CreateOldConnection())
            {

                UserProfile profile = new UserProfile(db);

                _txt_username.Text = button.CommandName;
                _txt_username.Enabled = false;

                profile.Username = _txt_username.Text;

                if (profile.Update())
                {
                    _txt_password.Text = string.Empty;
                    _txt_email.Text = profile.Email;
                    _txt_name.Text = profile.Name;
                }

                _pnl_userdata.Visible = true;
                _btn_new.Enabled = false;
                _btn_save.Enabled = true;
            }
        }

        protected void btn_save_Click(object cender, EventArgs args)
        {
            using (Database db = BaseDatos.CreateOldConnection ())
            {
                UserProfile profile = new UserProfile(db);
                profile.Username = _txt_username.Text.Trim();
                profile.Password = _txt_password.Text.Trim();
                profile.Name = _txt_name.Text.Trim();
                profile.Email = _txt_email.Text.Trim();

                if (profile.Save())
                {
                    load_users();
                }
            }

        }

        private void _btn_new_Click(object sender, EventArgs e)
        {
            _pnl_userdata.Visible = true;
            _btn_save.Enabled = true;
            _btn_new.Enabled = false;
        }

        private void clear_textboxes()
        {
            _txt_name.Text = string.Empty;
            _txt_name.Enabled = true;

            _txt_email.Text = string.Empty;
            _txt_email.Enabled = true;

            _txt_password.Text = string.Empty;
            _txt_password.Enabled = true;

            _txt_username.Text = string.Empty;
            _txt_username.Enabled = true;

        }

        private void set_panel_enabled(bool state)
        {
        }
    }
}