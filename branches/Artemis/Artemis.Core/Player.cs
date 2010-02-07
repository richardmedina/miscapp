
using System;

namespace Artemis.Core
{
	
	
	public class Player : IPlayer
	{
		private MediaStream _current;
		private PlayerState _state;
		
		private PlayerEngineType _engine = PlayerEngineType.Undefined;
		
		private event EventHandler _media_change_request;
		private event EventHandler _state_changed;
		
		public Player ()
		{
			_state_changed = onStateChanged;
			_media_change_request = onMediaChangeRequest;
		}
		
		public virtual void Play (MediaStream media)
		{
		}
		
		public virtual void Stop ()
		{
		}
		
		public virtual void Pause ()
		{
		}
		
		public virtual void Seek (double position)
		{
		}
		
		protected virtual void OnStateChanged ()
		{
			_state_changed (this, EventArgs.Empty);
		}
		
		protected virtual void OnMediaChangeRequest ()
		{
			_media_change_request (this, EventArgs.Empty);
		}
		
		private void onStateChanged (object sender, EventArgs args)
		{
		}
		
		private void onMediaChangeRequest (object sender, EventArgs args)
		{
		}
		
		public PlayerEngineType Engine {
			get { return _engine; }
			set { _engine = value; }
		}
		
		public PlayerState State {
			get { return _state; }
			protected set { 
				_state = value; 
				Console.WriteLine ("Changind state to : {0}", value);
				OnStateChanged ();
			}
		}
		
		public MediaStream Current {
			get { return _current; }
			protected set { _current = value; }
		}
		
		public event EventHandler StateChanged {
			add { _state_changed += value; }
			remove { _media_change_request -= value; }
		}
		
		public event EventHandler MediaChangeRequest {
			add { _media_change_request += value; }
			remove { _media_change_request -= value; }
		}
	}
}
