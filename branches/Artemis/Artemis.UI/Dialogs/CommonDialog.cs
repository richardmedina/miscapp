
using System;
using Gtk;

namespace Artemis.UI.Dialogs
{
	
	
	public class CommonDialog : Gtk.Dialog
	{
		
		public CommonDialog()
		{
			AddButton (Stock.Help, ResponseType.Help);
		}
		
		public new ResponseType Run ()
		{
			return (ResponseType) base.Run ();
		}
	}
}
