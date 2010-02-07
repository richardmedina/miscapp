
using System;
using Gtk;

namespace Artemis.UI.Dialogs
{
	
	
	public class CustomFileChooserDialog : Gtk.FileChooserDialog
	{
		
		public CustomFileChooserDialog ()
		{
			Title = "Please select files to be added..";
			SelectMultiple = true;
			Action = FileChooserAction.Open;
			
			AddButton (Stock.Help, ResponseType.Help);
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		public new ResponseType Run ()
		{
			return (ResponseType) base.Run ();
		}
	}
}
