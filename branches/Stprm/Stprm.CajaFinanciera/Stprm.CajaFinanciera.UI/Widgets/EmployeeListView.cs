
using System;
using Gtk;

using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class EmployeeListView : Gtk.TreeView
	{
		
		private Gtk.ListStore _store;
		
		private Gtk.TreeViewColumn [] columns;
		
		public EmployeeListView ()
		{
			_store = new ListStore (typeof (Employee),
			                        typeof (string), // ficha
			                        typeof (string) // Nombre
			                        );
			
			Model = _store;
			RulesHint = true;
			
			columns = new TreeViewColumn [2];
			columns [0] = AppendColumn ("Ficha", new CellRendererText (), "text", 1);
			columns [1] = AppendColumn ("Nombre", new CellRendererText (), "text", 2);
		}
		
		public void Add (Employee employee)
		{
			if (!Exists (employee)) {
				_store.AppendValues (employee, employee.Id, employee.GetFullName ());	
			}
		}
		
		public bool Exists (Employee employee)
		{
			Gtk.TreeIter iter;
			
			bool result = false;
			
			if (_store.GetIterFirst (out iter))
				do {
					Employee emp = (Employee) _store.GetValue (iter, 0);
					if (emp.Id == employee.Id) {
						result = true;
						break;
					}
				} while (_store.IterNext (ref iter));
			
			return result;
		}
	}
}