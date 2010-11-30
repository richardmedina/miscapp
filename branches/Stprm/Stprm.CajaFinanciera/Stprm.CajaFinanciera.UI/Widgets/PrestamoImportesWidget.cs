
using System;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class PrestamoImportesWidget : CustomVBox
	{

		private CurrencyEntry _entry_capital;
		
		private SpinButton _spin_interes;
		
		private CurrencyEntry _entry_interes;
		private CurrencyEntry _entry_total;
		private CurrencyEntry _entry_abono;
		private CurrencyEntry _entry_saldo;
		
		private int _num_pagos = 1;
		
		public PrestamoImportesWidget ()
		{
			_entry_capital = new CurrencyEntry ();
			_entry_capital.FocusOutEvent += Handle_entry_capitalFocusOutEvent;
			_spin_interes = new SpinButton (0, 100, 1);
			//_spin_interes.Changed += Handle_spin_interesChanged;
			_spin_interes.ValueChanged += Handle_spin_interesValueChanged;
			
			//_spin_interes.WidthRequest = 200;
			_entry_interes = new CurrencyEntry ();
			_entry_total = new CurrencyEntry ();
			_entry_abono = new CurrencyEntry ();
			_entry_saldo = new CurrencyEntry ();
			
			Gtk.HBox hbox = new  Gtk.HBox (false, 5);
			
			hbox.PackStart (Factory.Label ("Capital", 130, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_capital, false, false, 0);
			hbox.PackStart (Factory.Label ("Interes", 70, Justification.Left), false, false, 0);
			hbox.PackStart (SpinInteres, false ,false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new Gtk.HBox (false, 5);
			hbox.PackStart (Factory.Label ("Interes Generado", 130, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_interes, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Total", 130, Justification.Left), false, false, 0);
			hbox.PackStart (_entry_total, false, false, 0);
			
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

		private void Handle_spin_interesValueChanged (object sender, EventArgs e)
		{
			UpdateEntries ();
		}
		
		public void UpdateEntries ()
		{
			EntryInteres.Value = ((EntryCapital.Value > 0 ? EntryCapital.Value : 1) / 100) * Convert.ToDecimal (_spin_interes.Value);
			EntryTotal.Value = EntryTotal.Value + EntryInteres.Value;
			EntryAbono.Value = EntryTotal.Value / NumeroPagos;
			Console.WriteLine (NumeroPagos);
		}

		private void Handle_entry_capitalFocusOutEvent (object o, FocusOutEventArgs args)
		{
			UpdateEntries ();
			//Handle_spin_interesChanged (_spin_interes, EventArgs.Empty);
		}
		
		public void UpdateFromPrestamo (Prestamo prestamo)
		{
			EntryCapital.Value  = prestamo.Capital;
			SpinInteres.Value = prestamo.PorcentajeInteres;
			EntryInteres.Value = prestamo.Interes;
			EntryTotal.Value = prestamo.Capital + prestamo.Interes;
			EntryAbono.Value = prestamo.Abono;
			EntrySaldo.Value = prestamo.Saldo;
		}
		
		public CurrencyEntry EntryCapital {
			get { return _entry_capital; }
		}
		
		public SpinButton SpinInteres {
			get { return _spin_interes; }
		}
		
		public CurrencyEntry EntryInteres {
			get { return _entry_interes; }
		}
		
		public  CurrencyEntry EntryTotal {
			get { return _entry_total; }	
		}
		
		public CurrencyEntry EntryAbono {
			get { return _entry_abono; }
		}
		
		public CurrencyEntry EntrySaldo {
			get { return _entry_saldo; }
		}
		
		public int NumeroPagos {
			get { return _num_pagos; }
			set { _num_pagos = value; }
		}
	}
}
