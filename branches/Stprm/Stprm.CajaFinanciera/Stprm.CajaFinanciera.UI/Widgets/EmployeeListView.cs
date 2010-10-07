
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
			_store = new ListStore (typeof (Employee));
			
			Model = _store;
			
			columns = new TreeViewColumn [2];
			
			columns [0] = new TreeViewColumn ();
			columns [1] = new TreeViewColumn ();
		}
	}
}