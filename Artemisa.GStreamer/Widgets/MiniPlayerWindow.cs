
using System;
using Gtk;

namespace Artemisa.GStreamer.Widgets
{
	
	
	public class MiniPlayerWindow : Gtk.Window
	{
		private MediaPlayer _player;
		
		
		public MiniPlayerWindow () : base ("Media Player")
		{
			Gtk.VBox vbox = new Gtk.VBox (false, 5);
			
			_player = new MediaPlayer ();
			
			Gtk.Button button = new Gtk.Button (Stock.MediaPlay);
			button.Clicked += delegate {
				FileChooserDialog dialog = new FileChooserDialog (
					"Open medina file...",
					this,
					FileChooserAction.Open);
				
				dialog.AddButton (Stock.Cancel, ResponseType.Cancel);
				dialog.AddButton (Stock.Ok, ResponseType.Ok);
				
			};
			
			
			vbox.PackStart (button);
			
			Add (vbox);
		}
		
		protected override bool OnDeleteEvent (Gdk.Event evnt)
		{
			Application.Quit ();
			return false;
		}

	}
}
