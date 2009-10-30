
using System;
using Gtk;

namespace Reportero.UI.Dialogs
{
	
	
	public class ReporteroAboutDialog : Gtk.AboutDialog
	{
		
		public ReporteroAboutDialog()
		{
			Authors = new string [] {"Ricardo Medina López <rmedinalo@pep.pemex.com>"};
			Artists = Authors;
			Version = "1.0prev";
			Comments = "Version preliminar.\nTodos los derechos reservados\nRicardo Medina López\nrmedinalo@pep.pemex.com";
			
			WindowPosition = Gtk.WindowPosition.Center;
			
			Icon = Gdk.Pixbuf.LoadFromResource ("reportero_icon_main.png");
			Logo = Gdk.Pixbuf.LoadFromResource ("reportero_icon_main.png");
		}
	}
}
