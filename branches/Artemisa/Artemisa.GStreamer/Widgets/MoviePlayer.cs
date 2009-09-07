
using System;
using Gtk;

namespace Artemisa.GStreamer.Widgets
{
	
	
	public class MoviePlayer : Gtk.DrawingArea, IPlayer
	{
		
		public MoviePlayer ()
		{
			ModifyBg (StateType.Normal, new Gdk.Color (255, 0, 0));
		}
		
		public void Play ()
		{
		}
		
		public void Stop ()
		{
		}
		
		public void Pause ()
		{
		}
		
		public void Seek (long position)
		{
		}
	}
}
