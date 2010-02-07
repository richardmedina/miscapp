
using System;

namespace Artemis.Core.GStreamer
{
	
	public enum GstStateChangeReturn
	{
		GST_STATE_CHANGE_FAILURE = 0,
		GST_STATE_CHANGE_SUCCESS = 1,
		GST_STATE_CHANGE_ASYNC = 2,
		GST_STATE_CHANGE_NO_PREROLL = 3
	}
}
