
using System;
using Gtk;
using Artemisa.UI.Widgets;

namespace Artemisa.UI
{
	
	
	public class MainWindow : Gtk.Window
	{
		private WorkbenchWidget _workbench;
		
		public MainWindow () : base (WindowType.Toplevel)
		{
			Title = "Artemisa Video Editor";
			WindowPosition = WindowPosition.Center;
			
			_workbench = new WorkbenchWidget ();
			
			Add (_workbench);
			
			Resize (800, 600);
		}
		
		protected override bool OnDeleteEvent (Gdk.Event evnt)
		{
			Application.Quit ();
			return false;
		}

	}
}
