
using System;
using Gtk;
using Artemisa.UI.Dialogs;
using Artemisa.GStreamer.Widgets;

namespace Artemisa.UI.Widgets
{
	
	
	[System.ComponentModel.ToolboxItem(true)]
	public partial class WorkbenchWidget : Gtk.Bin
	{
		private MoviePlayer _movie_player;

		protected virtual void OnQuitActionActivated (object sender, System.EventArgs e)
		{
			Application.Quit ();
		}
		
		public WorkbenchWidget()
		{
			this.Build();
			
			_movie_player = new MoviePlayer ();
			_movieplayer_container.Add (_movie_player);
			_movieplayer_container.ShowAll ();
			
			statusbar.ModifyBg (StateType.Normal, new Gdk.Color (255, 0, 0));
		}

		protected virtual void OnAboutActionActivated (object sender, System.EventArgs e)
		{
			ArtemisaAboutDialog dialog = new ArtemisaAboutDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}
	}
}
