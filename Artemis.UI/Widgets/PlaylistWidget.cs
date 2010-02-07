
using System;
using Gtk;
using Artemis.Core;

namespace Artemis.UI.Widgets
{
	
	
	public class PlaylistWidget : Gtk.TreeView
	{
		private Gtk.ListStore _store;
		
		private string [] col_names = {
			"Name"
	//		"Artist",
	//		"Album",
	//		"Lenght"
		};
		
		private Gtk.TreeViewColumn [] columns;
		private Gtk.CellRendererText [] renders;
		
		private event MediaStreamEventHandler _activated;
		private event MediaStreamEventHandler _selected;
		//private event MediaStreamEventHandler _selection
		//private MediaStream EventHandler _over;
		
		private Playlist _playlist;
		
		private Gdk.Pixbuf [] _pixbufs;
		
		public PlaylistWidget (Playlist playlist)
		{
			_playlist = playlist;
			_activated = onActivated;
			_selected = onSelected;
			
			_store = new Gtk.ListStore (
				typeof (MediaStream), // MediaStream
				typeof (string), // Songname
				typeof (string), // Artist
				typeof (string), // Album
				typeof (string), //Length;
				typeof (Gdk.Pixbuf)); // icon for mediastream state
			
			Model = _store;
			
			columns = new Gtk.TreeViewColumn [col_names.Length];
			renders = new Gtk.CellRendererText [col_names.Length];
			
			Gtk.CellRendererPixbuf render_pixbuf = new Gtk.CellRendererPixbuf ();
			renders [0] = new CellRendererText ();
			
			columns [0] = new TreeViewColumn ();
			
			columns [0].Title = col_names [0];
			columns [0].PackStart (render_pixbuf, false);
			columns [0].AddAttribute (render_pixbuf, "pixbuf", 5);
			
			columns [0].PackStart (renders [0], true);
			columns [0].AddAttribute (renders [0], "text", 1);
			//columns [0].AddAttribute (renders [0], "font-weight", 6);
			
			AppendColumn (columns [0]);
			
			for (int i = 1; i < col_names.Length; i ++) {
				renders [i] = new CellRendererText ();
				columns [i] = new TreeViewColumn (col_names [i], renders [i], "text", i + 1);
				//AppendColumn (columns [i]);
			}
			
			_pixbufs = new Gdk.Pixbuf [3];
			_pixbufs [(int) PlayerState.Playing] = IconFactory.LookupDefault (Stock.MediaPlay).RenderIcon (Style,
				TextDirection.Ltr,
				StateType.Normal,
				IconSize.Menu,
				this,
				string.Empty);
			
			_pixbufs [(int) PlayerState.Paused] = IconFactory.LookupDefault (Stock.MediaPause).RenderIcon (Style,
				TextDirection.Ltr,
				StateType.Normal,
				IconSize.Menu,
				this,
				string.Empty);
			
			
			_pixbufs [(int) PlayerState.Stopped] = Gdk.Pixbuf.LoadFromResource ("pixbuf_null.png");
			
			RulesHint = true;
			HeadersVisible = false;
			
			Selection.Changed += onSelectionChanged;
			
			_playlist.Added += playlistAdded;
		}
		
		public void ApplyFilter (string filter)
		{
			_store.Clear ();
			foreach (MediaStream stream in _playlist) {
				//foreach (string part in filter.Trim ().ToLower ().Split (" ".ToCharArray ())) {
				if (stream.Name.ToLower ().IndexOf (filter.ToLower ().Trim ()) >= 0) {
					Append (stream);
				//		break;
				//	}
				}
			}
		}
		
		public void Append (MediaStream stream)
		{
			_store.AppendValues (
				stream,
				stream.Name,
				stream.Artist,
				stream.Album,
				stream.Length.ToString (),
				_pixbufs [(int) PlayerState.Stopped]);
		}
		
		public void Remove (MediaStream stream)
		{
			Gtk.TreeIter iter;
			
			if (GetIterFromStream (stream, out iter))
				_store.Remove (ref iter);
		}
		
		public bool GetIterFromStream (MediaStream stream, out Gtk.TreeIter iter)
		{
			Gtk.TreeIter current_iter;
			iter = TreeIter.Zero;
			
			if (_store.GetIterFirst (out current_iter))
				do {
					MediaStream current_stream = (MediaStream) _store.GetValue (current_iter, 0);
					if (current_stream == stream) {
						iter = current_iter;
						return true;
					}
				} while (_store.IterNext (ref current_iter));
			
			return false;
		}
		
		public bool GetStreamAtPointer (out MediaStream stream)
		{
			int x;
			int y;
			Gdk.ModifierType modifier;
			
			GdkWindow.GetPointer (out x, out y, out modifier);
			
			return GetStreamAtPos (x, y, out stream);
		}
		
		public bool GetStreamAtPos (int x, int y, out MediaStream stream)
		{
			Gtk.TreeIter iter;
			Gtk.TreePath path;
			
			stream = null;
			
		//	GdkWindow.GetPointer (out x, out y, out modifier);
			
			if (GetPathAtPos (x, y, out path)) {
				if (_store.GetIterFromString (out iter, path.ToString ())) {
					if (GetStreamFromIter (iter, out stream))
						return true;
				}
			}
			
			return false;
		}
		
		public bool GetStreamFromIter (Gtk.TreeIter iter, out MediaStream stream)
		{
			stream = null;
			
			if (_store.IterIsValid (iter)) {
				stream = (MediaStream) _store.GetValue (iter, 0);
				return true;
			}
			
			return false;
		}
		
		public bool GetSelectedStream (out MediaStream stream)
		{
			stream = null;
			Gtk.TreeIter iter;
			if (Selection.GetSelected (out iter)) {
				stream = (MediaStream) _store.GetValue (iter, 0);
				return true;
			}
			return false;
		}
		
		public void SelectFirstStream ()
		{
			Gtk.TreeIter iter;
			
			if (_store.GetIterFirst (out iter)) {
				Selection.SelectIter (iter);
			}
		}
		
		public void UpdateStateForMediaStream (MediaStream stream, PlayerState state)
		{
			Gtk.TreeIter iter;
			Console.WriteLine ("Updating Media state");
			if (GetIterFromStream (stream, out iter)) {
				_store.SetValue (iter, 5, _pixbufs [(int) state]);
			}
		}
		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			MediaStream stream;
			
			if (evnt.Type == Gdk.EventType.TwoButtonPress && evnt.Button == 1) {
				if (GetStreamAtPos ((int) evnt.X, (int) evnt.Y, out stream))
					OnActivated (stream);
			}
			return base.OnButtonPressEvent (evnt);
		}
		
		protected override bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			MediaStream stream;
			if (evnt.Key == Gdk.Key.KP_Enter || evnt.Key == Gdk.Key.Return)
				if (GetSelectedStream (out stream))
					OnActivated (stream);
			return base.OnKeyPressEvent (evnt);
		}
		
		protected virtual void OnSelected (MediaStream stream)
		{
			_selected (this, new MediaStreamEventArgs (stream));
		}
		
		protected virtual void OnActivated (MediaStream stream)
		{
			_activated (this, new MediaStreamEventArgs (stream));
		}
		
		private void onSelected (object sender, MediaStreamEventArgs args)
		{
		}
		
		private void onActivated (object sender, MediaStreamEventArgs args)
		{
		}
		
		private void onSelectionChanged (object sender, EventArgs args)
		{
			MediaStream stream = null;
			GetSelectedStream (out stream);
			OnSelected (stream);
		}
		
		private void playlistAdded (object sender, MediaStreamEventArgs args)
		{
			Append (args.Stream);
		}
		
		public event MediaStreamEventHandler Activated {
			add { _activated += value; }
			remove { _activated -= value; }
		}
		
		public event MediaStreamEventHandler Selected {
			add { _selected += value; }
			remove { _selected -= value; }
		}
		
		public Playlist Playlist {
			get { return _playlist; }
		}
	}
}
