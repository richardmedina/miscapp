
using System;
using Gtk;

namespace Artemis.UI.Dialogs
{
	
	
	public class ArtemisAboutDialog : Gtk.AboutDialog
	{
		
		public ArtemisAboutDialog()
		{
			Logo = RenderIcon (Stock.MediaPlay, IconSize.Dialog, string.Empty);
			Authors = new string[] {"Ricardo Medina <ricki9@gmail.com>"};
			Version = "0.1";
			Comments = "Artemis is a lightweight multimedia player.";
			ProgramName = "Artemis Media Player";
			Website = "http://artemis.danaproject.com";
		}
	}
}
