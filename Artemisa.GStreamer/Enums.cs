using System;

namespace Artemisa.GStreamer {


   public enum GstState
   {
       GST_STATE_VOID_PENDING = 0,
       GST_STATE_NULL = 1,
       GST_STATE_READY = 2,
       GST_STATE_PAUSED = 3,
       GST_STATE_PLAYING = 4
   };


   public enum GstStateChangeReturn
   {
       GST_STATE_CHANGE_FAILURE = 0,
       GST_STATE_CHANGE_SUCCESS = 1,
       GST_STATE_CHANGE_ASYNC = 2,
       GST_STATE_CHANGE_NO_PREROLL = 3
   };

   public enum GstFormat
   {
       GST_FORMAT_UNDEFINED = 0, /* must be first in list */
       GST_FORMAT_DEFAULT = 1,
       GST_FORMAT_BYTES = 2,
       GST_FORMAT_TIME = 3,
       GST_FORMAT_BUFFERS = 4,
       GST_FORMAT_PERCENT = 5
   };


   public enum GstSeekFlags
   {
       GST_SEEK_FLAG_NONE = 0,
       GST_SEEK_FLAG_FLUSH = (1 << 0),
       GST_SEEK_FLAG_ACCURATE = (1 << 1),
       GST_SEEK_FLAG_KEY_UNIT = (1 << 2),
       GST_SEEK_FLAG_SEGMENT = (1 << 3),
       GST_SEEK_FLAG_SKIP = (1 << 4)
   } ;


} 
