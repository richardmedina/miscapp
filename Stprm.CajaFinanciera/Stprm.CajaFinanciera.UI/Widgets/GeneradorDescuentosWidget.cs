
using System;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;

using Stprm.CajaFinanciera.UI.Dialogs;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class GeneradorDescuentosWidget : CustomVBox
	{
		private CategoriaCombo _cmb_clave;
		private Entry _entry_periodo;
		private Entry _entry_anio;
		private DateTimeButton _dtb_fecha;
		
		private Gtk.HBox _hbox_dates;
		
		private DateTimeButton _dtb_inicio;
		private DateTimeButton _dtb_fin;
		
		private CajaFinancieraComboBox _cmb_caja;
		
		private Button _button_agregar;
		private Button _button_eliminar;
		
		private GeneradorDescuentosView _view_gen_descs;
		
		public GeneradorDescuentosWidget ()
		{
			_cmb_clave = new CategoriaCombo ();
			_cmb_clave.Populate ();
			_cmb_clave.Changed += Handle_cmb_claveChanged;
			
			_entry_periodo = new Entry ();
			_entry_periodo.WidthChars = 2;
			_entry_periodo.MaxLength = 2;
			_entry_anio = new Entry ();
			_entry_anio.WidthChars = 4;
			_entry_anio.MaxLength = 4;
			
			_dtb_fecha = new DateTimeButton ();
			_dtb_fecha.Date = DateTime.Today;
			
			_dtb_inicio = new DateTimeButton ();
			_dtb_inicio.Date = DateTime.Today;
			_dtb_fin = new DateTimeButton ();
			_dtb_fin.Date = DateTime.Today;
			
			_cmb_caja = new CajaFinancieraComboBox ();
			_cmb_caja.Select (CajaFinancieraTipo.Prestamos);
			_cmb_caja.Sensitive = false;
			
			_button_agregar = Factory.Button (Stock.Add, string.Empty);
			_button_agregar.Clicked += Handle_button_agregarClicked;
			_button_eliminar = Factory.Button (Stock.Remove, string.Empty);
			_button_eliminar.Clicked += Handle_button_eliminarClicked;
			
			_view_gen_descs = new GeneradorDescuentosView ();
			
			HBox hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Clave", 50, Justification.Left), false, false, 0);
			hbox.PackStart (_cmb_clave, false, false, 0);
			
			hbox.PackStart (Factory.Label ("Periodo"), false, false, 0);
			hbox.PackStart (_entry_periodo, false, false, 0);
			hbox.PackStart (Factory.Label ("Año"), false, false, 0);
			hbox.PackStart (_entry_anio, false, false, 0);
			
			hbox.PackStart (Factory.Label ("Fecha"), false, false, 0);
			hbox.PackStart (_dtb_fecha, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Caja", 50, Justification.Left), false, false, 0);
			hbox.PackStart (_cmb_caja, false, false, 0);
			
			_hbox_dates = new HBox (false, 5);
			
			_hbox_dates.PackStart (Factory.Label ("Inicio"), false, false, 0);
			_hbox_dates.PackStart (_dtb_inicio, false, false, 0);
			
			_hbox_dates.PackStart (Factory.Label ("Fin"), false, false, 0);
			_hbox_dates.PackStart (_dtb_fin, false, false, 0);
			
			hbox.PackStart (_hbox_dates);
			
			hbox.PackEnd (_button_eliminar, false, false, 0);
			hbox.PackEnd (_button_agregar, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			ScrolledWindow scroll = new ScrolledWindow ();
			scroll.Add (_view_gen_descs);
			
			PackStart (new HSeparator (), false, false, 0);
			PackStart (scroll);
			
			_entry_anio.Text = _dtb_fecha.Date.Year.ToString ("0000");
			
			_cmb_clave.Active = 0;
		}
		
		protected override void OnShown ()
		{
			update_dates_visibility ();
			base.OnShown ();
		}


		private void Handle_cmb_claveChanged (object sender, EventArgs e)
		{
			update_dates_visibility ();
		}
		
		private void update_dates_visibility ()
		{
			Categoria categoria;
			
			_hbox_dates.Hide ();
			if (_cmb_clave.GetSelected (out categoria)) {
				if (categoria.Nombre == "Corporativos") {
					_hbox_dates.Show ();
				}
			}	
		}

		private void Handle_button_agregarClicked (object sender, EventArgs e)
		{
			BuscarPrestamoDialog dialog = new BuscarPrestamoDialog ();
			if (dialog.Run () == ResponseType.Ok) {
				Prestamo prestamo;
				if (dialog.GetSelectedPrestamo (out prestamo))
					_view_gen_descs.AppendPrestamo (prestamo);
			}
			dialog.Destroy ();
		}

		private void Handle_button_eliminarClicked (object sender, EventArgs e)
		{
			_view_gen_descs.RemoveSelected ();
		}
		
		public Descuento GetDescuento ()
		{
			Descuento descuento = new Descuento (Globals.Db);
			descuento.Fecha = _dtb_fecha.Date;
			descuento.FechaIni = _dtb_inicio.Date;
			descuento.FechaFin = _dtb_fin.Date;
			descuento.Anio = _dtb_fecha.Date.Year;
			Categoria categoria;
			if (_cmb_clave.GetSelected (out categoria))
				descuento.CategoriaId = categoria.Id;
			
			descuento.Periodo = int.Parse (_entry_periodo.Text);
			
			return descuento;
		}
		
		public DescuentoMovimientoCollection GetMovimientos ()
		{
			Categoria categoria;
			
			_cmb_clave.GetSelected (out categoria);
			return _view_gen_descs.GetDescuentoMovimientos (categoria);
		}
		
		public void ChangePrestamosStatus ()
		{
			_view_gen_descs.ChangePrestamosStatus ();	
		}
		
		public bool OnValidate (out string message)
		{
			bool result = true;
			int valor;
			message = string.Empty;
			
			if (_entry_periodo.Text.Trim () == string.Empty || !int.TryParse (_entry_periodo.Text, out valor)) {
				message = "Por favor verifique el periodo";
				result = false;
			}
			
			else if (_entry_anio.Text.Trim () == string.Empty || !int.TryParse (_entry_anio.Text, out valor)) {
				message = "Por favor verifique el Año";
				result = false;
			}
			
			else {
				Categoria categoria;
				_cmb_clave.GetSelected (out categoria);
				_view_gen_descs.GetDescuentoMovimientos (categoria);
				DescuentoMovimientoCollection movs = _view_gen_descs.GetDescuentoMovimientos (categoria);
				
				if (movs.Count == 0) {
					message = "No hay movimientos";
					result = false;
				}
				
				for (int i = 0; i < movs.Count; i ++) {
					if (movs [i].TrabajadorInternalId == 0) {
						message = string.Format ("La ficha en la fila {0} no puede ser nula", (i + 1));
						result = false;
						break;
					}
					
					if (movs [i].Folio == string.Empty) {
						message = string.Format ("Por favor establezca el numero de pagaré en la fila {0}", (i + 1));	
						result = false;
						break;
					}
				}
		    }
			
			return result;
		}
	}
}
