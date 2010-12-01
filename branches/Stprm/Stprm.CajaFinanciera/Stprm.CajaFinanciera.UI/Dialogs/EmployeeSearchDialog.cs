
using System;
using Gtk;
using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Widgets;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class EmployeeSearchDialog : CustomDialog
	{
		private EmployeeSearchWidget _view_searchemployee;
		
		public EmployeeSearchDialog ()
		{
			Title = Globals.FormatWindowTitle ("Buscar trabajador");
			Resize (430, 340);
			WindowPosition = WindowPosition.CenterOnParent;
			
			_view_searchemployee = new EmployeeSearchWidget ();
			_view_searchemployee.SearchEntry.Changed += entry_nameChanged;;
			_view_searchemployee.SearchEntry.Activated += entry_nameActivated;
			_view_searchemployee.SearchView.Activated += entry_nameActivated;
			
			VBox.PackStart (_view_searchemployee);
			VBox.ShowAll ();
			
			AddButton (Stock.Close, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		private void entry_nameChanged (object sender, EventArgs args)
		{
			_view_searchemployee.SearchView.CurrentFilter = SearchWidget.SearchEntry.Text;
			Populate ();
		}
		
		private void entry_nameActivated (object sender, EventArgs args)
		{
			Gtk.TreeIter iter;
			if (SearchWidget.SearchView.Store.GetIterFirst (out iter))
				Respond (ResponseType.Ok);
		}
		
		public void Populate ()
		{
			int count = SearchWidget.SearchView.Populate ();
			SetResponseSensitive (ResponseType.Ok, count > 0);
		}
		
		public EmployeeSearchWidget SearchWidget {
			get { return _view_searchemployee; }	
		}
	}
}
