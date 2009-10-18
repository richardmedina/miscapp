
using System;
using Gtk;
using Reportero.Reports.Drawing;
using Reportero.Data;

namespace Reportero.UI.Dialogs
{
	
	
	public class ReportDialog : CustomDialog
	{
		
		private Canvas _canvas;
		
		public ReportDialog (Canvas canvas)
		{
			Resize (800, 650);
			
			_canvas = canvas;
			//new ActivityGraphicReport (Vehicle, start, end);
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.AddWithViewport (_canvas);
			
			VBox.PackStart (scroll); 
			VBox.ShowAll ();
			
			AddButton (Stock.Close, ResponseType.Close);
			
			//_printing = new PrintOperation ();
			//_printing.BeginPrint += printingBeginPrint;
			//_printing.DrawPage += printingDrawPage;
		}
		
		protected override void OnShown ()
		{
			base.OnShown ();
			//_printing.Run (PrintOperationAction.PrintDialog, this);
		}
				
		public Canvas ReportCanvas {
			get { return _canvas; }
		}
	}
}
