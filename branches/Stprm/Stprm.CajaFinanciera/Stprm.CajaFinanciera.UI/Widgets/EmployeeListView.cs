
using System;
using System.Data;
using Gtk;

using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Dialogs;

namespace Stprm.CajaFinanciera.UI.Widgets
{

	public class EmployeeListView : DataSetView
	{
		
		public EmployeeListView ()
		{
			RulesHint = true;	
		}
		
		public override void New ()
		{
			EmployeeDialog dialog = new EmployeeDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}

		public override void Load ()
		{
			DataSet ds = new DataSet ();
			Employee.GetCollectionInAdapter (Globals.Db).Fill (ds);
			LoadDataSet (ds);
			Populate ();
			Columns [0].Visible = false;
		}
				
		public override void EditSelected ()
		{
			string [] fields;
			
			if (GetSelected (out fields)) {
				int internal_id;
				if (int.TryParse (fields [0], out internal_id)) {
					Employee employee = new Employee (Globals.Db);
					employee.InternalId = internal_id;
				
					if (employee.UpdateFromInternalId ()) {
						EmployeeDialog dialog = new EmployeeDialog ();
						dialog.UpdateFromEmployee (employee);
						if (dialog.Run () == ResponseType.Ok) {
							Gtk.TreeIter iter;
							if (Selection.GetSelected (out iter)) {
									
							}
						}
						dialog.Destroy ();
					}
				}
			}
		}
		
		protected override void OnActivated ()
		{
			EditSelected ();
			
			base.OnActivated ();
		}

	}
	
	/*
	public class EmployeeListView : Gtk.TreeView
	{
		
		private Gtk.ListStore _store;
		
		private Gtk.TreeViewColumn [] columns;
		private Gtk.CellRendererText [] renders;
		
		private string [] column_text = {
			"Ficha",
			"Nombre",
			"Saldo",
			"Ult.Fec.Pago",
			"Cat"
		};
		
		public EmployeeListView ()
		{
			_store = new ListStore (typeof (Employee),
			                        typeof (string), // ficha
			                        typeof (string), // Nombre
			                        typeof (string), //saldo
			                        typeof (string), //ult.fecha.pago.
			                        typeof (string) //cat
			                        );
			
			
			RulesHint = true;
			
			columns = new TreeViewColumn [5];
			renders = new CellRendererText [5];
			
			for (int i = 0; i < renders.Length; i ++) {
				//_store.SetSortFunc (i, sort_func);
				renders [i] = new CellRendererText ();
				columns [i] = 	AppendColumn (column_text [i], renders [i], "text", i + 1);
				columns [i].Resizable = true;
				columns [i].Clickable = true;
				columns [i].SortColumnId = i+1;
				
				columns [i].Reorderable = true;
				
				if (i == 2 || i == 3) // Balance
					renders [i].Xalign = 1f;
			}
			Model = _store;
			_store.DefaultSortFunc = sort_func;
		}
		
		private int sort_func (TreeModel model, TreeIter a, TreeIter b)
		{
			int sort_id;
			SortType sort_type;
			
			Console.WriteLine ("Sorting...");
			if (_store.GetSortColumnId (out sort_id, out sort_type)) {
				string astr = (string) _store.GetValue (a, sort_id);
				string bstr = (string) _store.GetValue (b, sort_id);
				
				if (sort_id == 1)
					return byFicha (astr, bstr) * -1;
				if (sort_id == 2)
					return byNombre (astr, bstr) * -1;
				if (sort_id == 3)
					return byCantidad(astr, bstr) * -1;
				if (sort_id == 4)
					return byFecha (astr, bstr) * -1;
				
			}
			
			return 0;
		}
		
		private int byFecha (string a, string b)
		{
			DateTime adate = DateTime.Parse (a);
			DateTime bdate = DateTime.Parse (b);
			
			if (adate > bdate)
				return -1;
			if (bdate > adate)
				return 1;
			
			return 0;
		}
		
		private int byCantidad (string a, string b)
		{
			a = a.Replace ("$", string.Empty).Replace ("(", string.Empty).Replace (")", string.Empty).Replace (",", string.Empty);
			b = b.Replace ("$", string.Empty).Replace ("(", string.Empty).Replace (")", string.Empty).Replace (",", string.Empty);
			
			double adouble = double.Parse (a);
			double bdouble = double.Parse (b);
			if (adouble == bdouble)
				return 0;
			if (adouble > bdouble)
				return -1;
			
			return 1;
		}
		
		private int byNombre (string a, string b)
		{
			return string.Compare (a, b);	
		}
		
		private int byFicha (string a, string b)
		{
			int aint = int.Parse (a);
			int bint = int.Parse (b);
				
			if (aint == bint)
				return 0;
			if (aint > bint)
				return -1;
			
			return 1;
		}
		
		public void Add (Employee employee)
		{
			if (!Exists (employee)) {
				employee.Modified += HandleEmployeeModified;
				_store.AppendValues (employee, 
				                     employee.Id, 
				                     employee.GetFullName (),
				                     employee.Saldo.ToString ("C"),
				                     employee.LastPayDate.ToString ("dd/MM/yyyy"),
				                     employee.CategoryId);	
			}
		}

		private void HandleEmployeeModified (object sender, EventArgs e)
		{
			Gtk.TreeIter iter;
			
			Employee modified_employee = (Employee) sender;
			
			if (_store.GetIterFirst (out iter))
				do {
					Employee employee = (Employee) _store.GetValue (iter, 0);
					if (modified_employee.Id == employee.Id) {
						UpdateEmployeeData (iter, employee);
					}
				} while (_store.IterNext (ref iter));
				
		}
		
		public void UpdateEmployeeData (Gtk.TreeIter iter, Employee employee)
		{
			_store.SetValue (iter, 1, employee.Id);
			_store.SetValue (iter, 2, employee.GetFullName ());
			_store.SetValue (iter, 3, employee.Saldo.ToString ("C"));
			_store.SetValue (iter, 4, employee.LastPayDate.ToString ("dd/MM/yyyy"));
			_store.SetValue (iter, 5, employee.CategoryId);
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
					dialog.UpdateFromEmployee (employee);
					dialog.Run ();
					dialog.Destroy ();
				}
				
			return result;
		}

	}
	*/
}