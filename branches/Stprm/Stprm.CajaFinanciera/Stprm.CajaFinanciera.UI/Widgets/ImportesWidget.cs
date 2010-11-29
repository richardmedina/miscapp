
using System;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class ImportesWidget : CustomVBox
	{

		private CurrencyEntry _entry_importe;
		private CurrencyEntry _entry_descuento;
		private CurrencyEntry _entry_abono;
		private CurrencyEntry _entry_saldo;
		
		public ImportesWidget ()
		{
			_entry_importe = new CurrencyEntry ();
			_entry_abono = new CurrencyEntry ();
			_entry_descuento = new CurrencyEntry ();
			_entry_saldo = new CurrencyEntry ();
			
			Gtk.HBox hbox = new  Gtk.HBox (false, 5);
			
			hbox.PackStart (Factory.Label ("Importe", 130, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_importe, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new Gtk.HBox (false, 5);
			hbox.PackStart (Factory.Label ("Desc. Catorcenal", 130, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_descuento, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Abono", 130, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_abono, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("<b>Saldo</b>", 130, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_saldo, false, false, 0);
			PackStart (hbox, false, false, 0);
		}
		
		public void UpdateFromPrestamo (Prestamo prestamo)
		{
			EntryImporte.Value  = prestamo.Capital + prestamo.Interes;
			EntryDescuento.Value = (prestamo.Capital + prestamo.Interes) / (prestamo.NumPagos > 0 ? prestamo.NumPagos : 1);
			EntryAbono.Value = prestamo.Abono;
			EntrySaldo.Value = prestamo.Saldo;
		}
		
		public CurrencyEntry EntryImporte {
			get { return _entry_importe; }
		}
		
		public CurrencyEntry EntryDescuento {
			get { return _entry_descuento; }
		}
		
		public CurrencyEntry EntryAbono {
			get { return _entry_abono; }
		}
		
		public CurrencyEntry EntrySaldo {
			get { return _entry_saldo; }
		}
	}
}
