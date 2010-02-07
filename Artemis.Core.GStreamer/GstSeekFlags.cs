
using System;

namespace Artemis.Core.GStreamer
{
	
	public enum GstSeekFlags
	{
		GST_SEEK_FLAG_NONE 		= 0,
		GST_SEEK_FLAG_FLUSH 	= (1 << 0),
		GST_SEEK_FLAG_ACCURATE 	= (1 << 1),
		GST_SEEK_FLAG_KEY_UNIT 	= (1 << 2),
		GST_SEEK_FLAG_SEGMENT 	= (1 << 3),
		GST_SEEK_FLAG_SKIP 		= (1 << 4)
	}
}
