
using System;
using Gtk;

namespace Artemisa.UI.Dialogs
{
	
	
	public class ArtemisaAboutDialog : Gtk.AboutDialog
	{
		
		public ArtemisaAboutDialog ()
		{
			WindowPosition = WindowPosition.CenterOnParent;
			
			Authors = new string [] {"Ricardo Medina <ricki9@gmail.com>"};
			ProgramName = "Artemisa Video Editor";
			LogoIconName = Stock.DialogAuthentication;
			Website = "http://artemisa.danaproject.org";
		}
	}
}
