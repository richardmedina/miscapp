
using System;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class CFAboutDialog : Gtk.AboutDialog
	{

		public CFAboutDialog ()
		{
			Version = "0.1b";
			Copyright = "S.T.P.R.M. 2009-2010";
			
			Authors = new string [] {"Ricardo Medina López <ricardo.medina@pemex.com>"};
		}
	}
}
