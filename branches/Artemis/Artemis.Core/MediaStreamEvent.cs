
using System;

namespace Artemis.Core
{
	
	public delegate void MediaStreamEventHandler (object sender, MediaStreamEventArgs args);
	
	public class MediaStreamEventArgs : System.EventArgs
	{
		private MediaStream _stream;
		
		public MediaStreamEventArgs (MediaStream stream)
		{
			_stream = stream;
		}
		
		public MediaStream Stream {
			get { return _stream; }
		}
	}
}
