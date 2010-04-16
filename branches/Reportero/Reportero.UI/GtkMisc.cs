
using System;
using Gtk;


namespace Reportero.UI
{
	
	
	public static class GtkMisc
	{
		public static void RunOnMainThread (ReadyEvent callback)
		{
			ThreadNotify notify = new ThreadNotify (callback);
			notify.WakeupMain ();
		}
		
		public static string FileSelect (string path, string title, FileChooserAction action)
		{
			string filename = string.Empty;
			
			Gtk.FileChooserDialog dialog = new Gtk.FileChooserDialog (
				title, null, action);
			
			dialog.AddButton (Stock.Cancel, ResponseType.Cancel);
			
			switch (action) {
				case FileChooserAction.Open:
					dialog.AddButton (Stock.Open, ResponseType.Ok);
				break;
				
				case FileChooserAction.Save:				
					dialog.AddButton (Stock.Save, ResponseType.Ok);
				break;
			}
			
			if (dialog.Run () == (int) ResponseType.Ok)
				filename = dialog.Filename;
				
			dialog.Destroy ();
			
			return filename;
		}
	}
}
