
using System;
using Gtk;

using RickiLib.Widgets;


namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class ImportesWidget : CustomVBox
	{

		private Gtk.Entry _entry_importe;
		private Gtk.Entry _entry_descuento;
		private Gtk.Entry _entry_abono;
		private Gtk.Entry _entry_saldo;
		
		public ImportesWidget ()
		{
			_entry_importe = new Entry ();
			_entry_abono = new Entry ();
			_entry_descuento = new Entry ();
			_entry_saldo = new Entry ();
			
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
	}
}
