
using System;
using Artemis.Core;

namespace Artemis.Core
{
	
	
	public class Playlist : MediaStreamCollection
	{
		private string _name;
		private PlaylistRepeatType _repeat_type = PlaylistRepeatType.All;
		private PlaylistShuffleType _shuffle_type = PlaylistShuffleType.BySong;
		
		private event EventHandler _name_changed;
		
		private static Random _random = new Random ();
		
		public Playlist (string name)
		{
			_name = name;
			_name_changed = onNameChanged;
		}
		
		public MediaStream GetRandomStream ()
		{
			int index = _random.Next (Count);
			return this [index];
		}
		
		public MediaStream GetNextStream (MediaStream stream)
		{
			int index = IndexOf (stream);
			if (index > -1)
				return this [index];
			
			return null;
		}
		
		protected virtual void OnNameChanged ()
		{
			_name_changed (this, EventArgs.Empty);
		}
		
		private void onNameChanged (object sender, EventArgs args)
		{
		}
		
		public string Name {
			get { return _name; }
			set { 
				_name = value; 
				OnNameChanged ();
			}
		}
		
		public PlaylistRepeatType RepeatType {
			get{ return _repeat_type; }
			set { _repeat_type = value; }
		}
		
		public PlaylistShuffleType ShuffleType {
			get { return _shuffle_type; }
			set { _shuffle_type = value; }
		}
		
		public event EventHandler NameChanged {
			add { _name_changed += value; }
			remove { _name_changed -= value; }
		}
	}
}
