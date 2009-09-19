
using System;
using Artemisa.GStreamer.Widgets;
namespace Artemisa.GStreamer
{
	
	
	public class MainClass
	{
		
		public static void Main ()
		{
			Gtk.Application.Init ();
			MiniPlayerWindow win = new MiniPlayerWindow ();
			win.ShowAll ();
			Gtk.Application.Run ();
		}
	}
}
