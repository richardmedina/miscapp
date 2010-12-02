
using System;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class GeneradorDescuentosWidget : CustomVBox
	{
		private CategoriaCombo _cmb_clave;
		private Entry _entry_periodo;
		private Entry _entry_anio;
		private DateTimeButton _dtb_fecha;
		
		private DateTimeButton _dtb_inicio;
		private DateTimeButton _dtb_fin;
		
		private CajaFinancieraComboBox _cmb_caja;
		
		private Button _button_agregar;
		private Button _button_eliminar;
		
		private GeneradorDescuentoView _view_gen_descs;
		
		public GeneradorDescuentosWidget ()
		{
			_cmb_clave = new CategoriaCombo ();
			_cmb_clave.Populate ();
			_cmb_clave.Active = 1;
			
			_entry_periodo = new Entry ();
			_entry_periodo.WidthChars = 2;
			_entry_periodo.MaxLength = 2;
			_entry_anio = new Entry ();
			_entry_anio.WidthChars = 4;
			_entry_anio.MaxLength = 4;
			
			_dtb_fecha = new DateTimeButton ();
			_dtb_fecha.Date = DateTime.Today;
			
			_cmb_caja = new CajaFinancieraComboBox ();
			_cmb_caja .Select (CajaFinancieraTipo.Prestamos);
			
			_button_agregar = Factory.Button (Stock.Add, string.Empty);
			_button_eliminar = Factory.Button (Stock.Remove, string.Empty);
			
			_view_gen_descs = new GeneradorDescuentoView ();
			
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
			hbox.PackEnd (_button_agregar, false, false, 0);
			hbox.PackEnd (_button_eliminar, false, false, 0);
			
			
			PackStart (hbox, false, false, 0);
			
			ScrolledWindow scroll = new ScrolledWindow ();
			scroll.Add (_view_gen_descs);
			
			PackStart (new HSeparator (), false, false, 0);
			PackStart (scroll);
			
		}
	}
}
