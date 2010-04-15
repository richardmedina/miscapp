
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
		
		public static string FileSelect (string path, string title, string okbuttonstr)
		{
			string filename = string.Empty;
			
			Gtk.FileChooserDialog dialog = new Gtk.FileChooserDialog (
				title, null, FileChooserAction.Open);	
			
			if (dialog.Run () == (int) ResponseType.Ok)
				filename = dialog.Filename;
				
			dialog.Destroy ();
			
			return filename;
		}
	}
}
