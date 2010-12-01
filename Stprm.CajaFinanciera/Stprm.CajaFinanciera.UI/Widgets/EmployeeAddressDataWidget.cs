
using System;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class EmployeeAddressDataWidget : CustomVBox
	{

		private Gtk.Entry _entry_street;
		private Gtk.Entry _entry_colony;
		private Gtk.Entry _entry_city;
		private Gtk.ComboBox _cmb_country;
		private Gtk.Entry _entry_postal;
		private Gtk.Entry _entry_phone;
		
		public EmployeeAddressDataWidget ()
		{
			_entry_street = new Gtk.Entry ();
			_entry_colony = new Gtk.Entry ();
			_entry_city = new Gtk.Entry ();
			
			CountryCollection countries = Country.GetFromDatabase (Globals.Db);
			
			
			
			string [] str_countries = new string [countries.Count];
			for (int i = 0; i < str_countries.Length; i ++) {
				str_countries [i] = countries [i].Name;
				//Console.WriteLine (str_countries [i]);
			}
			
			_cmb_country = new Gtk.ComboBox (str_countries);
			_cmb_country.Active = 0;
			
			_entry_postal = new Gtk.Entry ();
			_entry_phone = new Gtk.Entry ();
			
			Gtk.HBox hbox = new Gtk.HBox (false, 5);
			
			hbox.PackStart (Factory.Label ("Calle :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_street);
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Colonia :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_colony);
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Ciudad :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_city);
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Estado :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_cmb_country);
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Cod.Postal :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_postal);
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Teléfono :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_phone);
			PackStart (hbox, false, false, 0);
		}
		
		public void UpdateFromEmployee (Employee employee)
		{
		}
	}
}
