
using System;
using Gtk;
using Reportero.UI.Dialogs;

namespace Reportero.UI.Widgets
{
	
	
	public class ReportToolbar : Gtk.Toolbar
	{
		private ToolButton _btn_report;
		private ToolButton _btn_assign;
		private ToolButton _btn_home;
		private ToolButton _btn_about;
		
		
		public ReportToolbar ()
		{
			_btn_report = new ToolButton (Stock.Print);
			_btn_report.Label = "Reporte";
			
			_btn_assign = new ToolButton (Stock.Add);
			_btn_assign.Label = "Asignar";
			
			_btn_home = new ToolButton (Stock.Home);
			_btn_home.Label = "Inicio";
			
			_btn_about = new ToolButton (Stock.About);
			_btn_about.Label = "Acerca de...";
			_btn_about.Clicked += delegate {
				ReporteroAboutDialog dialog = new ReporteroAboutDialog ();
				dialog.Run ();
				dialog.Destroy ();
			};
			
			
			Insert (_btn_home, -1);
			Insert (new SeparatorToolItem (), -1);
			
			Insert (_btn_report, -1);
			Insert (_btn_assign, -1);
			Insert (new SeparatorToolItem (), -1);
			
			Insert (_btn_about, -1);
			
			ToolbarStyle = ToolbarStyle.Icons;
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
		
		public ToolButton AboutButton {
			get { return _btn_about; }
		}
	}
}
