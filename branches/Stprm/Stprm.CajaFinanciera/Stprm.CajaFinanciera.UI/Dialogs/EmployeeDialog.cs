
using System;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.UI.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class EmployeeDialog : CustomDialog
	{
		private Gtk.Notebook _notebook;
		private EmployeePersonalDataWidget _emp_personaldata;
		private EmployeeAddressDataWidget _emp_addrdata;
		private EmployeeLoanWidget _emp_loans;
		
		private Employee _employee = null;
		
		
		public EmployeeDialog ()
		{
			Title = Globals.FormatWindowTitle ("Información de trabajador");
			
			_notebook = new Notebook ();
			_emp_personaldata = new EmployeePersonalDataWidget ();
			_emp_addrdata = new EmployeeAddressDataWidget ();
			_emp_loans = new EmployeeLoanWidget ();
			
			_notebook.AppendPage (_emp_personaldata, new Label ("Datos Personales"));
			_notebook.AppendPage (_emp_addrdata, new Label ("Dirección"));
			_notebook.AppendPage (_emp_loans, new Label ("Préstamos"));
			
			VBox.PackStart (_notebook);
			
			VBox.ShowAll ();AddButton (Stock.Close, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		public void UpdateFromEmployee (Employee employee)
		{
			_employee = employee;
			_emp_personaldata.UpdateFromEmployee (employee);	
			_emp_loans.UpdateFromEmployee (employee);
		}
		
		public Employee GetAsEmployee  ()
		{
			if (_employee == null) {
				_employee = new Employee (Globals.Db);
			}
			
			_emp_personaldata.SaveToEmployee (_employee);
			
			return _employee;
		}
	}
}
