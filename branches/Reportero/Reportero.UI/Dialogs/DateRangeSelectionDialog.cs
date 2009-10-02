
using System;
using Gtk;
using Reportero.UI.Widgets;

namespace Reportero.UI.Dialogs
{
	
	
	public class DateRangeSelectionDialog : CustomDialog
	{
		private DateEntry _de_start;
		private DateEntry _de_end;
		
		public DateRangeSelectionDialog()
		{
			_de_start = new DateEntry ("Fecha Inicio");
			_de_start.Label.WidthRequest = 100;
			
			_de_end = new DateEntry ("Fecha Fin");
			_de_end.Label.WidthRequest = 100;
			
			VBox.PackStart (_de_start, false, false, 0);  
			VBox.PackStart (_de_end, false, false, 0);  
			VBox.ShowAll ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
	}
}
