
using System;
using System.Collections.Generic;

namespace Artemis.Core
{
	
	
	public class MediaStreamCollection : 
		System.Collections.Generic.List <MediaStream>
	{
	
		private event MediaStreamEventHandler _added;
		private event MediaStreamEventHandler _removed;
		
		public MediaStreamCollection ()
		{
			_added = onAdded;
			_removed = onRemoved;
		}
		
		public new virtual void Add (MediaStream stream)
		{
			base.Add (stream);
			OnAdded (stream);
		}
		
		public new virtual void Remove (MediaStream stream)
		{
			base.Remove (stream);
			OnRemoved (stream);
		}
		
		protected virtual void OnAdded (MediaStream stream)
		{
			_added (this, new MediaStreamEventArgs (stream));
		}
		
		protected virtual void OnRemoved (MediaStream stream)
		{
			_removed (this, new MediaStreamEventArgs (stream));
		}
		
		private void onAdded (object sender, MediaStreamEventArgs args)
		{
		}
		
		private void onRemoved (object sender, MediaStreamEventArgs args)
		{
		}
		
		public event MediaStreamEventHandler Added {
			add { _added += value; }
			remove { _added -= value; }
		}
		
		public event MediaStreamEventHandler Removed {
			add { _removed += value; }
			remove { _removed -= value; }
		}
	}
}
