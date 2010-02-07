
using System;
using Gtk;

namespace Artemis.Addins.Samples
{
	
	
	public class AddinManagerWindow : Gtk.Window
	{
		
		public AddinManagerWindow () : base (WindowType.Toplevel)
		{
			Title = "Addin Manager";
			
			Resize (640, 480);
		}
	}
}
