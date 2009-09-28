
using System;
using Gtk;

namespace Reportero.UI.Widgets
{
	
	
	public class ReportToolbar : Gtk.Toolbar
	{
		private ToolButton _btn_report;
		private ToolButton _btn_assign;
		private ToolButton _btn_home;
		
		
		public ReportToolbar ()
		{
			_btn_report = new ToolButton (Stock.Print);
			_btn_report.Label = "Reporte";
			
			_btn_assign = new ToolButton (Stock.Add);
			_btn_assign.Label = "Asignar";
			
			_btn_home = new ToolButton (Stock.Home);
			
			Insert (_btn_report, -1);
			Insert (_btn_assign, -1);
			Insert (new SeparatorToolItem (), -1);
			Insert (_btn_home, -1);
		}
		
		public ToolButton ReportButton {
			get { return _btn_report; }
		}
		
		public ToolButton AssignButton {
			get { return _btn_assign; }
		}
		
		public ToolButton HomeButton {
			get { return _btn_home; }
		}
	}
}
