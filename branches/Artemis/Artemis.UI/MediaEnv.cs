
using System;
using System.Threading;
using Artemis.Core;
using Artemis.Core.GStreamer;

namespace Artemis.UI
{
	
	
	public class MediaEnv
	{
		private MainWindow _main_window;
		private Player _player;
		private Playlist _playlist;
		
		public void Init ()
		{	
			_player = new GstPlayer ();
			_playlist = new Playlist ("Main");
			
			_main_window = new MainWindow (_player, _playlist);
		}
		
		public MainWindow MainWindow {
			get { return _main_window; }
		}
		
		public Player Player {
			get { return _player; }
		}
		
		public Playlist Playlist {
			get { return _playlist; }
		}
	}
}
