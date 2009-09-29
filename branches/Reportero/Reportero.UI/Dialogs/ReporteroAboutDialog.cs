
using System;

namespace Reportero.UI.Dialogs
{
	
	
	public class ReporteroAboutDialog : Gtk.AboutDialog
	{
		
		public ReporteroAboutDialog()
		{
			Authors = new string [] {"Ricardo Medina López <rmedinalo@pep.pemex.com>"};
			Artists = Authors;
		}
	}
}
