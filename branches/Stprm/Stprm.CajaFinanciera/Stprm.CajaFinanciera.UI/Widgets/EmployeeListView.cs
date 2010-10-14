
using System;
using Gtk;

using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Dialogs;

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
			                        typeof (string), // Nombre
			                        typeof (string), //saldo
			                        typeof (string), //ult.fecha.pago.
			                        typeof (string) //cat
			                        );
			
			Model = _store;
			RulesHint = true;
			
			columns = new TreeViewColumn [5];
			columns [0] = AppendColumn ("Ficha", new CellRendererText (), "text", 1);
			columns [1] = AppendColumn ("Nombre", new CellRendererText (), "text", 2);
			columns [2] = AppendColumn ("Saldo", new CellRendererText (), "text", 3);
			columns [3] = AppendColumn ("Ult.Fecha.Pago", new CellRendererText (), "text", 4);
			columns [4] = AppendColumn ("Cat", new CellRendererText (), "text", 5);
		}
		
		public void Add (Employee employee)
		{
			if (!Exists (employee)) {
				_store.AppendValues (employee, 
				                     employee.Id, 
				                     employee.GetFullName (),
				                     employee.Saldo.ToString (),
				                     employee.LastPayDate.ToShortDateString (),
				                     employee.Category);	
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
		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			bool result = base.OnButtonPressEvent (evnt);
			
			Gtk.TreeIter iter;
			
			if (evnt.Type == Gdk.EventType.TwoButtonPress && evnt.Button == 1)
				if (Selection.GetSelected (out iter)) {
					Employee employee =  (Employee) _store.GetValue (iter, 0);
					EmployeeDialog dialog = new EmployeeDialog ();
					dialog.Run ();
					dialog.Destroy ();
				}
				
			return result;
		}

	}
}