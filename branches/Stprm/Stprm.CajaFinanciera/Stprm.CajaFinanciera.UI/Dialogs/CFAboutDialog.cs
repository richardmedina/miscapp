
using System;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class CFAboutDialog : Gtk.AboutDialog
	{

		public CFAboutDialog ()
		{
			ProgramName = "Caja Financiera";
			Comments = "Todo bajo control";
			Version = "0.1b";
			Copyright = "S.T.P.R.M. 2010-2012";
			Logo = Gdk.Pixbuf.LoadFromResource ("CajaFinanciera.png");
			Icon = Logo;
			
			Authors = new string [] {"Ricardo Medina López <ricardo.medina@mistprm.org>"};
			Artists = Authors;
			Documenters = Artists;
		}
	}
}
