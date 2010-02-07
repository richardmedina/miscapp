
using System;

namespace Artemis.GStreamer
{
	
	
	public class Mp3MediaStream : MediaStream
	{
		
		public Mp3MediaStream (string filename) : base (filename)
		{
		}
		
		public MediaType Type {
			get { return MediaType.Mp3; }
		}
	}
}
