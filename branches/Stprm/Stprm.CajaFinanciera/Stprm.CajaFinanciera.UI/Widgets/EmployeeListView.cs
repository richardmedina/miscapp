
using System;
using Gtk;


namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class EmployeeListView : TreeView
	{
		
		private Gtk.ListStore _store;
		
		public EmployeeListView ()
		{
			_store = new ListStore (typeof (string)); // Name
		}
	}
}