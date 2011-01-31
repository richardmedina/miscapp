
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
		
		private PrestamoImportesWidget _iw_importes;
		
		private Prestamo _prestamo = null;
		
		private int _plazo_id = 0;
		private int _trabajador_internal_id = 0;
		
		private Gtk.Entry _entry_folio;
		private Gtk.Label _label_fecha_susp;
		
		private DateTime FechaSusp = DateTime.Now;
		
		
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
			_entry_pagare.MaxLength = 4;
			
			_label_nombre = Factory.Label ("", 300, Justification.Left);
			
			_iw_importes = new PrestamoImportesWidget ();
			
			_spin_plazo = new SpinButton (0, 1000, 1);
			_spin_plazo.ValueChanged += Handle_spin_plazoChanged;
			_button_plazo = new Button ();
			_button_plazo.Relief = ReliefStyle.None;
			_button_plazo.Image = Image.NewFromIconName (Stock.Find, IconSize.Button);
			_button_plazo.Clicked += Handle_button_plazoClicked;
			
			_dtb_inicobro = new DateTimeButton ();
			_dtb_ultpago = new DateTimeButton ();
			
			_cmb_cuenta = new CuentaBancariaCombo ();
			_cmb_cuenta.Populate ();
			_cmb_cuenta.SelectCuenta (Globals.CuentaActual);
			_cmb_cuenta.Sensitive = false;
			
			_cmb_estado = new OperacionFinancieraEstadoCombo ();
			
			PackStart (Factory.Label ("<b>Detalles del préstamo</b>", 130, Justification.Left), false, false, 0);
			PackStart (new HSeparator (), false, false, 0);
			
			Gtk.HBox hbox = new Gtk.HBox (false, 5);
			
			hbox.PackStart (Factory.Label ("Ficha", 100, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_ficha, false, false, 0);
			hbox.PackStart (_button_ficha, false, false, 0);
			
			hbox.PackEnd (_button_fecha, false, false, 0);
			hbox.PackEnd (Factory.Label ("Otorgado", 100, Justification.Left), false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			
			hbox.PackStart (_label_nombre, false, false, 0);
			hbox.PackEnd (_dtb_inicobro, false, false, 0);
			hbox.PackEnd (Factory.Label ("Inicio de cobro", 100, Justification.Left), false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Cheque", 100, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_cheque, false, false, 0);
			hbox.PackEnd (_dtb_ultpago, false, false, 0);
			hbox.PackEnd (Factory.Label ("Último Pago", 100, Justification.Left), false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			
			hbox.PackStart (Factory.Label ("Pagaré", 100, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_pagare, false, false, 0);
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
			Console.WriteLine (_dtb_inicobro.Date);
			
		}

		private void Handle_spin_plazoChanged (object sender, EventArgs e)
		{
			_iw_importes.NumeroPagos = Convert.ToInt32 (_spin_plazo.Value);
			_iw_importes.UpdateEntries ();
		}
		
		public Prestamo GetAsPrestamo ()
		{
			if (_prestamo == null) {
				_prestamo = new Prestamo (Globals.Db);
				_prestamo.Id = 0;
			}
			
			_prestamo.PlazoId = _plazo_id;
			_prestamo.TrabajadorInternalId = _trabajador_internal_id;
			_prestamo.NumPagos = Convert.ToInt32 (_spin_plazo.Value);
			
			_prestamo.Fecha = _button_fecha.Date;
			_prestamo.FechaIniCobro = _dtb_inicobro.Date;
			/*
			_prestamo.Capital = _iw_importes.EntryCapital.Value;
			_prestamo.Interes = _iw_importes.EntryInteres.Value;
			*/
			_iw_importes.SaveToPrestamo (_prestamo);
			
			_prestamo.Cheque = _entry_cheque.Text;
			_prestamo.Pagare = _entry_pagare.Text;
			_prestamo.Status = _cmb_estado.Estado;
			_prestamo.CuentaId = Globals.CuentaActual.Id;
			
			return _prestamo;
		}
		
		public void UpdateFromPrestamo (Prestamo prestamo)
		{
			_prestamo = prestamo;
			Employee employee = new Employee (prestamo.Db);
			employee.InternalId = prestamo.TrabajadorInternalId;
			
			_trabajador_internal_id = prestamo.TrabajadorInternalId;
			
			if (employee.UpdateFromInternalId ()) {
				_entry_ficha.Text = employee.Id;
				SetLabelText (employee.GetFullName ());
			}
			
			_plazo_id = _prestamo.PlazoId;
			
			_cmb_estado.Estado = prestamo.Status;
			_entry_cheque.Text = prestamo.Cheque;
			_entry_pagare.Text = prestamo.Pagare;
			_button_fecha.Date = prestamo.Fecha;
			_spin_plazo.Value = prestamo.NumPagos;
			_dtb_inicobro.Date = prestamo.FechaIniCobro;
			
			CuentaBancaria cuenta = new CuentaBancaria (prestamo.Db);
			cuenta.Id = prestamo.CuentaId;
			
			if (cuenta.Update ()) {
				_cmb_cuenta.SelectCuenta (cuenta);
			}
			
			_iw_importes.UpdateFromPrestamo (prestamo);
		}
		
		public void SetLabelText (string text)
		{
			_label_nombre.Text = string.Format ("\t{0}", text);	
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
				if (dialog.ViewPlazos.GetSelected (out fields)) {
						if (int.TryParse (fields [1], out val))
					    	_spin_plazo.Value = val;
						if (int.TryParse (fields [2].Replace ("%", string.Empty), out val)) {
							_iw_importes.SpinInteres.Value = Convert.ToDouble (val);
						}
				}
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
				_trabajador_internal_id = employee.InternalId;
				SetLabelText (employee.GetFullName ());
			}
		}

		private void Handle_button_fichaClicked (object sender, EventArgs e)
		{
			EmployeeSearchDialog dialog = new EmployeeSearchDialog ();
			dialog.SearchWidget.SearchView.Load ();
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
