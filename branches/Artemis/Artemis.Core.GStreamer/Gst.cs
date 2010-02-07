using System; 
using System.Runtime.InteropServices;


namespace Artemis.Core.GStreamer 
{

   public class Gst
   {

		// Win32 dll files
       //const string gstDynamicLib = "libgstreamer-0.10.dll";
       //const string gObjDynamicLib = "libgobject-2.0-0.dll";
       const string gstDynamicLib = "libgstreamer-0.10.so";// "libgstreamer-0.10.dll";
       const string gObjDynamicLib = "libgobject-2.0.so"; //"libgobject-2.0-0.dll";


       static Gst instance = null;
       static readonly object padlock = new object();

       Gst()
       {
       }

       public static Gst Instance
       {
           get
           {
               lock (padlock)
               {
                   if (instance == null)
                   {
                       instance = new Gst ();
                   }
                   return instance;
               }
           }
       }



       [DllImport(gstDynamicLib)]
       static extern bool gst_element_seek_simple(IntPtr element, int format,
                                                        int seek_flags, Int64 seek_pos);
       
       
       [DllImport(gstDynamicLib)]
       static extern void gst_object_unref(IntPtr pipeline);


       [DllImport(gstDynamicLib)]
       static extern void gst_init(ref int argc, ref IntPtr argv);

       [DllImport(gstDynamicLib)]
       static extern bool gst_init_check(ref int argc, ref IntPtr argv);


       [DllImport(gstDynamicLib)]
       static extern void gst_version(out uint major, out uint minor, out uint micro, out uint nano);


       [DllImport(gstDynamicLib)]
       static extern IntPtr gst_pipeline_get_bus(IntPtr pipeline);


       [DllImport(gstDynamicLib)]
       static extern IntPtr gst_pipeline_new(string NamePipeLine);


       [DllImport(gstDynamicLib)]
       static extern IntPtr gst_element_factory_make(string nameBin, string stuff);


       [DllImport(gObjDynamicLib)]
       static extern void g_object_set_property(IntPtr handle, IntPtr prop, ref GLib.Value value);


       [DllImport(gstDynamicLib)]
       static extern bool gst_bin_add(IntPtr bin, IntPtr element);

       [DllImport(gstDynamicLib)]
       static extern bool gst_element_link(IntPtr src, IntPtr destElement);


       [DllImport(gstDynamicLib)]
       static extern int gst_element_set_state(IntPtr element, GstState state);



       [DllImport(gObjDynamicLib)]
      public  static extern bool g_object_get(IntPtr ptr, string prop, ref string value, IntPtr NullTerminated);


       public GstStateChangeReturn ElementSetState(IntPtr element, GstState state)
       {
           return (GstStateChangeReturn)gst_element_set_state(element, state);
       }

       public void ObjectUnref(IntPtr obj)
       {
           gst_object_unref(obj);
       }


       public  bool ElementLink(IntPtr srcElement, IntPtr destElement)
       {
           return gst_element_link(srcElement, destElement);
       }


       public  bool ElementLinksMany(params IntPtr[] destElement)
       {

           for (int i = 0; i < destElement.Length - 1; i++)
           {
               if (!gst_element_link(destElement[i], destElement[i + 1]))
                   return false;
           }

           return true;
       }

       public  bool PipelineAddElement(IntPtr pipeline, IntPtr element)
       {
           return gst_bin_add(pipeline, element);
       }


       public  bool PipelineAddManyElements(IntPtr pipeline, params IntPtr[] elements)
       {
           foreach (IntPtr ptr in elements)
           {
               if (!gst_bin_add(pipeline, ptr))
                   return false;
           }
           return true;
       }



       public  void ElementSetProperty(IntPtr handle, string prop, string value)
       {
           //gst_element_set_property(handle, prop, value);
           IntPtr property_name_as_native = GLib.Marshaller.StringToPtrGStrdup(prop);
           GLib.Value val = new GLib.Value(value);
           g_object_set_property(handle, property_name_as_native, ref val);
           GLib.Marshaller.Free(property_name_as_native);


       }


       public  IntPtr GetBus(IntPtr pipeline)
       {
           return gst_pipeline_get_bus(pipeline);
       }

		

       public  bool ElementSeekSimple(IntPtr element, GstFormat format, GstSeekFlags seek_flags, Int64 seek_pos)
       {
           return gst_element_seek_simple(element, (int)format, (int)seek_flags, seek_pos);
       }


       public  bool GObjectGetProperty(IntPtr ptr, string prop, ref string value)
       {
           return g_object_get(ptr, prop, ref value, IntPtr.Zero);
       }


       public  IntPtr CreatePipeline(string name)
       {
           return gst_pipeline_new(name);
       }


       public  IntPtr CreateElementFromFactory(string nameBin, string stuff)
       {
           return gst_element_factory_make(nameBin, stuff);
       }


        bool do_init(string progname, ref string[] args, bool check)
       {

           bool res = false;
           string[] progargs = new string[args.Length + 1];

           progargs[0] = progname;
           args.CopyTo(progargs, 1);

           GLib.Argv argv = new GLib.Argv(progargs);
           IntPtr buf = argv.Handle;
           int argc = progargs.Length;

           if (check)
               res = gst_init_check(ref argc, ref buf);
           else
               gst_init(ref argc, ref buf);

           if (buf != argv.Handle)
               throw new Exception("init returned new argv handle");

           // copy back the resulting argv, minus argv[0], which we're
           // not interested in.

           if (argc <= 1)
               args = new string[0];
           else
           {
               progargs = argv.GetArgs(argc);
               args = new string[argc - 1];
               Array.Copy(progargs, 1, args, 0, argc - 1);
           }

           return res;
       }


       public  bool Init(string progname, ref string[] args)
       {
           if (Environment.OSVersion.Platform == PlatformID.Win32Windows || Environment.OSVersion.Platform == PlatformID.Win32NT)
           {
               string path = Environment.GetEnvironmentVariable("PATH");
               string currenDir = Environment.CurrentDirectory;
               path += ";" + currenDir + @"\GStreamer\0.10\deps;" + currenDir + @"\GStreamer\0.10\bin";
               Environment.SetEnvironmentVariable("PATH", path);
               Environment.SetEnvironmentVariable("GST_PLUGIN_PATH", currenDir + @"\GStreamer\0.10\lib\gstreamer-0.10");
           }
           return do_init(progname, ref args, false);
       }


       public  string Version
       {
           get
           {
               uint major = 0, minor = 0, micro = 0, nano = 0;
               gst_version(out major, out minor, out micro, out nano);
               return major + "." + minor + "." + micro;
           }
       }

       public void UnrefPipeline(IntPtr pipeline)
       {
           gst_object_unref(pipeline);
       }


   }

} 
