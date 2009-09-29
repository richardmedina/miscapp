
using System;
using Gtk;
using Reportero.Reports;

namespace Reportero.UI.Dialogs
{
	
	
	public class ActivityReportDialog : CustomDialog
	{
		
		private ActivityReport _canvas;
		
		public ActivityReportDialog()
		{
			_canvas = new ActivityReport (DateTime.Now, DateTime.Now);
			Add (_canvas);
			
			AddButton (Stock.Close, ResponseType.Close);
		}
	}
}
