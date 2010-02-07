
using System;

namespace Artemisa.GStreamer
{
	
	
	public class Mp3MediaStream : MediaStream
	{
		
		public Mp3MediaStream (string filename) : base (filename)
		{
			Uri = filename;
		}
		
		public MediaType Type {
			get { return MediaType.Mp3; }
		}
	}
}
