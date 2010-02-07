
using System;
using Artemisa.GStreamer;

namespace Artemisa.GStreamer
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
			protected set { _uri = value; }
		}
	}
}
