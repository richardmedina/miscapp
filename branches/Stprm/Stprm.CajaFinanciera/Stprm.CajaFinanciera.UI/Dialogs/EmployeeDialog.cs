
using System;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.UI.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class EmployeeDialog : CustomDialog
	{
		private EditableLabelButton _entry_id;
		private EditableLabelButton _entry_firstname;
		private EditableLabelButton _entry_middlename;
		private EditableLabelButton _entry_lastname;
		private DateTimeButton _button_borndate;
		
		
		public EmployeeDialog ()
		{
			Gtk.HBox hbox = new Gtk.HBox (false, 5);
			
			_entry_id = new EditableLabelButton ();
			_entry_firstname = new EditableLabelButton ();
			_entry_middlename = new EditableLabelButton ();
			_entry_lastname = new EditableLabelButton ();
			_button_borndate = new DateTimeButton (new DateTime (1970, 1, 1));
			
			hbox.PackStart (Factory.Label ("Nombre(s) :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_firstname);
			VBox.PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Ap.Paterno :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_middlename);
			VBox.PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Ap.Materno :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_entry_lastname);
			VBox.PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (Factory.Label ("Fec.Nac :", 100, Justification.Right), false, false, 0);
			hbox.PackStart (_button_borndate);
			VBox.PackStart (hbox, false, false, 0);
			
			
			VBox.ShowAll ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		public void UpdateFromEmployee (Employee employee)
		{
		}
	}
}
