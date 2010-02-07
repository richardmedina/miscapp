
using System;
using Artemis.GStreamer;

namespace Artemis.GStreamer
{
	
	
	public class MediaStream
	{
		private string _uri;
		
		public MediaStream () : this (string.Empty)
		{
		}
		
		public MediaStream (string uri)
		{
		}
		
		public string Uri {
			get { return _uri; }
		}
	}
}
