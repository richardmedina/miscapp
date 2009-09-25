
using System;
using Gtk;

namespace Reportero.UI.Widgets
{
	
	
	public class ReportToolbar : Gtk.Toolbar
	{
		private ToolButton _btn_report;
		private ToolButton _btn_assign;
		
		public ReportToolbar ()
		{
			_btn_report = new ToolButton (Stock.Print);
			_btn_report.Label = "Reporte";
			
			_btn_assign = new ToolButton (Stock.Add);
			_btn_assign.Label = "Asignar";
			
			Insert (_btn_report, -1);
			Insert (_btn_assign, -1);
		}
		
		public ToolButton ReportButton {
			get { return _btn_report; }
		}
		
		public ToolButton AssignButton {
			get { return _btn_assign; }
		}
	}
}
