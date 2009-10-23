
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
	}
}
