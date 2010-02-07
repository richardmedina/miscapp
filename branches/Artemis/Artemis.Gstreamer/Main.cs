
using System;
namespace Artemis.GStreamer
{
	
	
	public class MainClass
	{
		
		public static void Main ()
		{
			string[] args = new string [0];
			Gst gst  = Gst.Instance;
			
			gst.Init ("test", ref args);
			Console.WriteLine(gst.Version);
			
			IntPtr pipeline = gst.CreatePipeline ("pipeline");
			Console.ReadLine ();
			Console.WriteLine ("Creating filesrc..");
			IntPtr filesrc = gst.CreateElementFromFactory ("filesrc", "filesrc");
			Console.WriteLine ("Done");
			
			Console.WriteLine ("Creating mad..");
			IntPtr mad = gst.CreateElementFromFactory ("mad", "mad");
			Console.WriteLine ("Done");
			
			IntPtr audioConv = gst.CreateElementFromFactory ("alsasink", "alsasink0");
			/*
			Console.WriteLine ("Creating AudioConvert..");
			IntPtr audioConv = gst.CreateElementFromFactory ("audioconvert", "audioconvert");
			Console.WriteLine ("Done");
			
			
			IntPtr audioResampl = gst.CreateElementFromFactory ("audioresample", "audioresample");
			IntPtr audioOut = gst.CreateElementFromFactory ("autoaudiosink", "autoaudiosink");*/
			
			Console.ReadLine ();
			
			Console.WriteLine ("Adding to pipeline");
			bool resState = gst.PipelineAddManyElements (pipeline, filesrc, mad, audioConv);//, audioOut);
			Console.WriteLine ("Done: {0}", resState);
			
			Console.WriteLine ("Linking in pipeline");
			bool resState2 = gst.ElementLinksMany (filesrc, mad, audioConv);//, audioResampl, audioOut);
			Console.WriteLine ("Sone: {0}", resState2);
			
			gst.ElementSetProperty (filesrc, "location", @"/home/ricki/mp3.mp3");
			GstStateChangeReturn absaa = gst.ElementSetState (pipeline, GstState.GST_STATE_PLAYING);
			//Console.ReadLine ();
			System.Threading.Thread.Sleep (3000);
			absaa = gst.ElementSetState (pipeline, GstState.GST_STATE_PAUSED);
			System.Threading.Thread.Sleep (3000);
			absaa = gst.ElementSetState (pipeline, GstState.GST_STATE_PLAYING);
			System.Threading.Thread.Sleep (3000);
		/*
			Gtk.Application.Init ();
			MiniPlayerWindow win = new MiniPlayerWindow ();
			win.ShowAll ();
			Gtk.Application.Run ();
		*/
		}
	}
}
