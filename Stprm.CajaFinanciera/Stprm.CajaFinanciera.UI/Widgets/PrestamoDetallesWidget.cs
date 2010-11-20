
using System;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Dialogs;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class PrestamoDetallesWidget : CustomVBox
	{

		private Gtk.Entry _entry_ficha;
		private Gtk.Button _button_ficha;
		
		private DateTimeButton _button_fecha;
		
		
		private Gtk.Entry _entry_cheque;
		private Gtk.Entry _entry_pagare;
		
		private Gtk.Label _label_nombre;
		private Gtk.SpinButton _spin_plazo;
		private Gtk.Button _button_plazo;
		
		private DateTimeButton _dtb_inicobro;
		private DateTimeButton _dtb_ultpago;
		
		private CuentaBancariaCombo _cmb_cuenta;
		private OperacionFinancieraEstadoCombo _cmb_estado;
		
		private ImportesWidget _iw_importes;
		
		public PrestamoDetallesWidget ()
		{
			_entry_ficha = new Entry ();
			_entry_ficha.FocusOutEvent += Handle_entry_fichaFocusOutEvent;
			_button_ficha = new Button ();
			_button_ficha.Relief =  ReliefStyle.None;
			_button_ficha.Image = Image.NewFromIconName (Stock.Find, IconSize.Button);
			_button_ficha.Clicked += Handle_button_fichaClicked;
			
			_button_fecha = new DateTimeButton ();
			_button_fecha.Date = DateTime.Now;
			
			_entry_cheque = new Entry ();
			_entry_pagare = new Entry ();
			
			_label_nombre = Factory.Label ("", 300, Justification.Left);
			
			_iw_importes = new ImportesWidget ();
			
			_spin_plazo = new SpinButton (0, 1000, 1);
			_button_plazo = new Button ();
			_button_plazo.Relief = ReliefStyle.None;
			_button_plazo.Image = Image.NewFromIconName (Stock.Find, IconSize.Button);
			_button_plazo.Clicked += Handle_button_plazoClicked;
			
			_dtb_inicobro = new DateTimeButton ();
			_dtb_ultpago = new DateTimeButton ();
			
			_cmb_cuenta = new CuentaBancariaCombo ();
			_cmb_cuenta.Populate (CuentaBancaria.GetCollection (Globals.Db));
			_cmb_cuenta.SelectCuenta (Globals.CuentaActual);
			_cmb_cuenta.Sensitive = false;
			
			_cmb_estado = new OperacionFinancieraEstadoCombo ();
			
			PackStart (Factory.Label ("<b>Detalles del préstamo</b>", 130, Justification.Left), false, false, 0);
			PackStart (new HSeparator (), false, false, 0);
			
			Gtk.HBox hbox = new Gtk.HBox (false, 5);
			
			hbox.PackStart (Factory.Label ("Ficha", 100, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_ficha, false, false, 0);
			hbox.PackStart (_button_ficha, false, false, 0);
			
			hbox.PackEnd (_dtb_inicobro, false, false, 0);
			hbox.PackEnd (Factory.Label ("Inicio de cobro", 100, Justification.Left), false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			
			hbox.PackStart (_label_nombre, false, false, 0);
			hbox.PackEnd (_dtb_ultpago, false, false, 0);
			hbox.PackEnd (Factory.Label ("Último Pago", 100, Justification.Left), false, false, 0);
			
			PackStart (hbox, false, false, 0);
		
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Cheque", 100, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_cheque, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			
			hbox.PackStart (Factory.Label ("Pagaré", 100, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_pagare, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Fecha", 100, Justification.Left), false, false, 0);
			hbox.PackStart (_button_fecha, false, false, 0);
			
			
			hbox.PackEnd (_cmb_cuenta, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Plazo ", 100, Justification.Left), false, false, 0);
			hbox.PackStart (_spin_plazo, false, false, 0);
			hbox.PackStart (_button_plazo, false, false, 0);
			
			hbox.PackEnd (_cmb_estado, false, false, 0);
			hbox.PackEnd (Factory.Label ("Estado", 100, Justification.Left), false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			PackStart (Factory.Label ("<b>Importe desglosado</b>", 100, Justification.Left), false, false, 0);
			PackStart (new HSeparator (), false, false, 0);
			PackStart (_iw_importes);
			
		}
		
		public void UpdateFromPrestamo ()
		{
			
		}

		private void Handle_button_plazoClicked (object sender, EventArgs e)
		{
			string [] fields;
			PlazosDialog dialog = new PlazosDialog ();
			dialog.ViewPlazos.Activated += delegate {
				if (dialog.ViewPlazos.GetSelected (out fields))
					dialog.Respond (ResponseType.Ok);
			};
			dialog.ViewPlazos.Load (Globals.Db);
			dialog.ViewPlazos.Populate ();
			
			
			dialog.SetResponseSensitive (ResponseType.Ok, dialog.ViewPlazos.GetSelected (out fields));
			ResponseType response = dialog.Run ();
			if (response == ResponseType.Ok) {
				int val;
				if (dialog.ViewPlazos.GetSelected (out fields))
						if (int.TryParse (fields [1], out val))
					    	_spin_plazo.Value = val;
			}
			
			dialog.Destroy ();
		}
		
		private void Handle_entry_fichaFocusOutEvent (object o, FocusOutEventArgs args)
		{
			ActualizarNombre (_entry_ficha.Text);
		}
		
		public void ActualizarNombre (string ficha)
		{
			Employee employee = new Employee (Globals.Db);
			employee.Id = ficha;

			_label_nombre.Text = string.Empty;
			
			if (employee.Update ()) {
				_label_nombre.Text = "\t" + employee.GetFullName ();
			}
		}

		private void Handle_button_fichaClicked (object sender, EventArgs e)
		{
			EmployeeSearchDialog dialog = new EmployeeSearchDialog ();
			dialog.SearchWidget.SearchView.Load (Globals.Db);
			dialog.Populate ();
			
			ResponseType response = dialog.Run ();
			if (response == ResponseType.Ok) {
				string [] fields;
				if (dialog.SearchWidget.SearchView.GetSelected (out fields)) {
					_entry_ficha.Text = fields [0];
					ActualizarNombre (fields [0]);
					//_label_nombre.Text = "\t" + fields [1];
				}
			}
			
			dialog.Destroy ();
		}
	}
}
