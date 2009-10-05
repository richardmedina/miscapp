
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
			Title = AppSettings.Instance.GetFormatedTitle ("Reporte de Actividad");
			Resize (800, 600);
			
			_canvas = new ActivityReport (DateTime.Now, DateTime.Now);
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.AddWithViewport (_canvas);
			
			VBox.PackStart (scroll); 
			VBox.ShowAll ();
			
			AddButton (Stock.Close, ResponseType.Close);
		}
	}
}
