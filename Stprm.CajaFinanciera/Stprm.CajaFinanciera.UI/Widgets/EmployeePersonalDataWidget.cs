
using System;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.UI.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class EmployeePersonalDataWidget : Gtk.VBox
	{
		private EditableLabelButton _entry_id;
		private EditableLabelButton _entry_firstname;
		private EditableLabelButton _entry_middlename;
		private EditableLabelButton _entry_lastname;
		private DateTimeButton _button_borndate;
		
		private Gtk.ComboBox _cmb_category;
		private string [] _emp_categories = {"Activo", "Corporativo", "Jubilado", "Transitorio"};
		
		private Gtk.ComboBox _cmb_status;
		private string [] _emp_status = {"Alta", "Baja"};
		
		public EmployeePersonalDataWidget () : base (false, 5)
		{
			Gtk.HBox hbox = new Gtk.HBox (false, 5);
			
			_entry_id = new EditableLabelButton ();
			_entry_firstname = new EditableLabelButton ();
			_entry_middlename = new EditableLabelButton ();
			_entry_lastname = new EditableLabelButton ();
			_button_borndate = new DateTimeButton (new DateTime (0001, 1, 1));
			
			_cmb_category = new ComboBox (_emp_categories);
			_cmb_status = new ComboBox (_emp_status);
			
			
			hbox.PackStart (Factory.Label ("Nombre(s) :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_firstname);
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Ap.Paterno :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_middlename);
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Ap.Materno :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_lastname);
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Fec.Nac :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_button_borndate, false, false, 0);
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Categoría :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_cmb_category, false, false, 0);
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Estado :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_cmb_status, false, false, 0);
			PackStart (hbox, false, false, 0);
			
			ShowAll ();
		}
		
		public void UpdateFromEmployee (Employee employee)
		{
			Console.WriteLine (employee);
			_entry_id.EditableLabel.Text = employee.Id;
			_entry_firstname.EditableLabel.Text = employee.FirstName;
			_entry_middlename.EditableLabel.Text = employee.MiddleName;
			_entry_lastname.EditableLabel.Text = employee.LastName;
		}
	}
}
