
using System;
using Gtk;
using Artemis.Core;
using Artemis.Core.GStreamer;

namespace Artemis.UI.Widgets
{
	
	
	public class PlayerWidget : Gtk.VBox
	{
		private Gtk.Button _btn_play;
		private Gtk.Button _btn_pause;
		private Gtk.Button _btn_stop;
		
		private Player _player;
		private MediaStream _stream;
		
		public PlayerWidget (Player player)
		{
			_player = player;
		
			_btn_play = new Gtk.Button (Stock.MediaPlay);
			_btn_play.Relief = ReliefStyle.None;
			_btn_play.Clicked += btn_playClicked;
			
			_btn_pause = new Button (Stock.MediaPause);
			_btn_pause.Relief = ReliefStyle.None;
			
			_btn_pause.Clicked += btn_pauseClicked;
			
			_btn_stop = new Button (Stock.MediaStop);
			_btn_stop.Relief = ReliefStyle.None;
			_btn_stop.Clicked += btn_stopClicked;
			
			Gtk.HBox hbox = new Gtk.HBox (false, 10);
			hbox.PackStart (_btn_play, false, false, 0);
			hbox.PackStart (_btn_pause, false, false, 0);
			hbox.PackStart (_btn_stop, false, false, 0);
			
			PackStart (hbox);
		}
		
		private void btn_playClicked (object sender, EventArgs args)
		{
			if (_stream != null)
				Player.Play (Stream);
		}
		
		private void btn_pauseClicked (object sender, EventArgs args) 
		{
		/*
			if (Player.State == PlayerState.Playing)
				Player.Pause ();
			else if (_player.State == PlayerState.Paused)
				Player.Play (_player.Current);
		*/
				Player.Pause ();
		}
		
		private void btn_stopClicked (object sender, EventArgs args)
		{
			Player.Stop ();
		}
		
		public Player Player {
			get { return _player; }
		}
		
		public Gtk.Button PlayButton {
			get { return _btn_play; }
		}
		
		public Gtk.Button PauseButton {
			get { return _btn_pause; }
		}
		
		public Gtk.Button StopButton {
			get { return _btn_stop; }
		}
		
		public MediaStream Stream {
			get { return _stream; }
			set { _stream = value; }
		}
	}
}
