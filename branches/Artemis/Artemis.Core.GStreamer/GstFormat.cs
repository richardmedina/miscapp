using System;

namespace Artemis.Core.GStreamer
{

  

   public enum GstFormat
   {
       GST_FORMAT_UNDEFINED = 0, /* must be first in list */
       GST_FORMAT_DEFAULT = 1,
       GST_FORMAT_BYTES = 2,
       GST_FORMAT_TIME = 3,
       GST_FORMAT_BUFFERS = 4,
       GST_FORMAT_PERCENT = 5
   }
} 
