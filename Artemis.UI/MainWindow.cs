
using System;
using System.Threading;
using Gtk;
using Artemis.Core;
using Artemis.Core.GStreamer;
using Artemis.UI.Widgets;
using Artemis.UI.Dialogs;

namespace Artemis.UI
{
		
	public class MainWindow : PopupWindow
	{
		
		private Player _player;
		private Gtk.VBox _vbox;
		
		private MainMenubar _menubar;
		
		private PlayerWidget _pyr_main;
		
		private SearchEntry _entry_search;
		private PlaylistWidget _plw_main;
		private Playlist _pl_main;
		
		private MainStatusbar _statusbar;
		
		private bool infilter = false;
		
		public MainWindow (Player player, Playlist playlist)
		{
			Decorated = false;
			Title = "Artemis Media Player";
			Resize (480, 640);
			AccelGroup accel = new AccelGroup ();
			
			
			_player =  player;
			_player.StateChanged += playerStateChanged;
			
			_vbox = new VBox (false, 5);
			
			_pyr_main = new PlayerWidget (_player);
			
			_entry_search = new SearchEntry ();
			_entry_search.Entry.Changed += entry_searchChanged;
			_entry_search.Entry.Activated += entry_searchActivated;
			
			_menubar = new MainMenubar (accel);
			
			_menubar.File.NewPlaylist.Activated += newPlaylistActivated;
			_menubar.File.ClearPlaylist.Activated += clearPlaylistActivated;
			_menubar.File.ImportLocalFiles.Activated += importLocalFilesActivated;
			_menubar.File.ImportLocalDirectory.Activated += importLocalDirectoryActivated;
			
			_pl_main = playlist;
			
			_plw_main = new PlaylistWidget (_pl_main);
			_plw_main.KeyPressEvent += plw_mainKeyPressEvent;
			_plw_main.Activated += plw_mainActivated;
			_plw_main.Selected += plw_mainSelected;
			
			_statusbar = new MainStatusbar ();
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_plw_main);
						
			_vbox.PackStart (_menubar, false, false, 0);
			_vbox.PackStart (_pyr_main, false, false, 0);
			_vbox.PackStart (_entry_search, false, false, 0);
			_vbox.PackStart (scroll);
			//_vbox.PackEnd (_statusbar, false, false, 0);
			
			Add (_vbox);
			_vbox.ShowAll ();
			_menubar.Hide ();

			Icon = IconFactory.LookupDefault (Stock.MediaPlay).RenderIcon (
				Style,
				TextDirection.Ltr,
				StateType.Normal,
				IconSize.Dialog,
				this,
				string.Empty);
			
			//KeyPressEvent += onKeyPressEvent;
			_entry_search.Entry.GrabFocus ();
			
		}
		
		private int x;
		private int y;
		
		protected override void OnHidden ()
		{
			GetPosition (out x, out y);
			Console.WriteLine ("Saving {0},{1}", x, y);
			base.OnHidden ();
		}

		
		protected override void OnShown ()
		{
			Console.WriteLine ("Restoring {0},{1}", x, y);
			Move (x, y);
			base.OnShown ();
		}
		
		protected override bool OnDeleteEvent (Gdk.Event evnt)
		{
			Hide ();
			return true;
		}
		
		private void newPlaylistActivated (object sender, EventArgs args)
		{
			_pl_main.Clear ();
			_plw_main.ApplyFilter (string.Empty);
		}
		
		private void clearPlaylistActivated (object sender, EventArgs args)
		{
			_pl_main.Clear ();
			_plw_main.ApplyFilter (string.Empty);
		}
		
		private void importLocalFilesActivated (object sender, EventArgs args)
		{
			CustomFileChooserDialog dialog = new CustomFileChooserDialog ();
			ResponseType response = dialog.Run ();
			string [] filenames = (string []) dialog.Filenames.Clone ();
			string [] uris = (string []) dialog.Uris.Clone ();
			dialog.Destroy ();
			if (response == ResponseType.Ok) {
				for (int i = 0; i < filenames.Length; i ++)
					_pl_main.Add (new MediaStream (uris [i], filenames [i]));
			}
			_statusbar.Pop (0);
			_statusbar.Push (0, string.Format ("{0} Files in playlist", _pl_main.Count));
		}
		
		private void importLocalDirectoryActivated (object sender, EventArgs args)
		{
			FolderChooserDialog dialog = new FolderChooserDialog ();
			ResponseType response = dialog.Run ();
			string folder = dialog.SelectedPath;
			
			dialog.Destroy ();
			
			if (response == ResponseType.Ok) {
				loadFilesFromFolder (folder);
			}
			_statusbar.Pop (0);
			_statusbar.Push (0, string.Format ("{0} Files in playlist", _pl_main.Count));
		}
		
		private void loadFilesFromFolder (string folder)
		{
			try {
				foreach (string filename in System.IO.Directory.GetFiles (folder))
					_pl_main.Add (new MediaStream (string.Format ("file://{0}", filename), filename));
			
				foreach (string current_folder in System.IO.Directory.GetDirectories (folder))
					loadFilesFromFolder (current_folder);
			} catch (Exception exception) {
				MessageDialog dialog = new MessageDialog (this, 
					DialogFlags.Modal, 
					MessageType.Error,
					ButtonsType.Ok,
					exception.Message);
				
				dialog.Run ();
				dialog.Destroy ();
			}
		}
		[GLib.ConnectBefore]
		private void plw_mainKeyPressEvent (object sender, KeyPressEventArgs args)
		{
			if (args.Event.KeyValue >= 65 && args.Event.KeyValue <= 122) {
				_entry_search.Entry.Text += ((char) args.Event.KeyValue).ToString ();
				_entry_search.Entry.GrabFocus ();
				int length = _entry_search.Entry.Text.Length;
				_entry_search.Entry.SelectRegion (length, length);
			}
		}
		
		private void plw_mainActivated (object  sender, MediaStreamEventArgs args)
		{
			_pyr_main.Stream = args.Stream;
			_pyr_main.PlayButton.Click ();
			//Console.WriteLine ("Waiting for play state");
		}
		
		private void plw_mainSelected (object sender, MediaStreamEventArgs args)
		{
			_pyr_main.Stream = args.Stream;
		}
		
		private void playerStateChanged (object sender, EventArgs args)
		{
			_plw_main.UpdateStateForMediaStream (_player.Current, _player.State);
		}

		private void entry_searchChanged (object sender, EventArgs args)
		{
			if (_entry_search.Entry.Text.Length > 2) {
				_plw_main.ApplyFilter (_entry_search.Entry.Text);
				_plw_main.UpdateStateForMediaStream (_pyr_main.Player.Current, _pyr_main.Player.State);
				_plw_main.SelectFirstStream ();
				infilter = true;
			}
			else if (infilter) {
				_plw_main.ApplyFilter (string.Empty);
				_plw_main.UpdateStateForMediaStream (_pyr_main.Player.Current, _pyr_main.Player.State);
				_plw_main.SelectFirstStream ();
				infilter = false;
			}
		}
		
		private void entry_searchActivated (object sender, EventArgs args)
		{
			_pyr_main.PlayButton.Click ();
		}
		
		//[GLib.ConnectBefore]
		//private void onKeyPressEvent (object sender, KeyPressEventArgs args)
		protected override bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			if (evnt.Key == Gdk.Key.Alt_L ||
				evnt.Key == Gdk.Key.Alt_R) {
				if (_menubar.Visible) {
					_menubar.Hide ();
					Decorated = false;
				}
				else {
					_menubar.Show ();
					Decorated = true;
				}
			}
			return base.OnKeyPressEvent (evnt);
		}
		
		public PlaylistWidget PlaylistWidget {
			get { return _plw_main; }
		}
		
		public PlayerWidget PlayerWidget {
			get { return _pyr_main; }
		}
		
		public Player Player {
			get { return _player; }
		}
		
		public Playlist Playlist {
			get { return _pl_main; }
		}
		
		public MainMenubar Menubar {
			get { return _menubar; }
		}
	}
}
