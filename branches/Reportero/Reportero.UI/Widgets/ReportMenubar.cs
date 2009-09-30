
using System;
using Gtk;

namespace Reportero.UI.Widgets
{
	
	
	public class ReportMenubar : Gtk.MenuBar
	{
		private FileReportMenu _mnu_file;
		private HelpReportMenu _mnu_help;
		
		public ReportMenubar ()
		{
			_mnu_file = new FileReportMenu ();
			_mnu_help = new HelpReportMenu ();
			
			Append (_mnu_file.Item);
			Append (_mnu_help.Item);
		}
	}
}
