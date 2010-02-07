
using System;

namespace Artemisa.GStreamer
{
	
	
	public class MediaPlayer : IPlayer
	{
		private GstreamerInterop _engine;
		
		private string _filename;
		private bool _initialized = false;

		private static GstPipeline pipeline;
		private static IntPtr filesrc;
		private static IntPtr mad;
		private static IntPtr audioConv;
		private static IntPtr audioResampl;
		private static IntPtr audioOut;
		
		public MediaPlayer () : this (string.Empty)
		{
		}
		
		public MediaPlayer (string filename)
		{
			_engine = GstreamerInterop.Instance;
			_filename = filename;
		}
		
		public void InitEngine ()
		{
			string[] args = new string [0];
			_engine.Init("test", ref args);
			Console.WriteLine(_engine.Version);

			pipeline = _engine.CreatePipeline ("pipeline");
			filesrc = _engine.CreateElementFromFactory ("filesrc", "filesrc");
			mad = _engine.CreateElementFromFactory ("mad", "mad");
			audioConv = _engine.CreateElementFromFactory ("audioconvert", "audioconvert");
			audioResampl = _engine.CreateElementFromFactory ("audioresample", "audioresample");
			audioOut = _engine.CreateElementFromFactory ("autoaudiosink", "autoaudiosink");
			bool resState = _engine.PipelineAddManyElements (pipeline, filesrc, mad, audioConv, audioResampl, audioOut);
			bool resState2 = _engine.ElementLinksMany (filesrc, mad, audioConv, audioResampl, audioOut);

			if (!resState)
				throw new MediaPlayerException ("Init Failed. Exception merging elements");
			if (!resState2)
				throw new MediaPlayerException ("Init Failed. Exception Linking elements");
			
			_initialized = true;
		}
		
		public void Play ()
		{
			if (!_initialized)
				InitEngine ();
			
			_engine.ElementSetProperty (filesrc, "location", Filename);
			GstStateChangeReturn absaa = _engine.ElementSetState (pipeline, GstState.GST_STATE_PLAYING);
		}
		
		public void Stop ()
		{
		}
		
		public void Pause ()
		{
		}
		
		public void Seek (long position)
		{
		}
		
		private IntPtr createMediaPipe (MediaType media_type)
		{
			IntPtr pipe = IntPtr.Zero;
			
			switch (media_type) {
				case MediaType.Mp3:
					pipe = createMp3MediaPipe ();
				break;
			}
			
			return pipe;
		}
		
		private IntPtr createMp3MediaPipe ()
		{
			pipeline = _engine.CreatePipeline ("pipeline");
			filesrc = _engine.CreateElementFromFactory ("filesrc", "filesrc");
			mad = _engine.CreateElementFromFactory ("mad", "mad");
			audioConv = _engine.CreateElementFromFactory ("audioconvert", "audioconvert");
			audioResampl = _engine.CreateElementFromFactory ("audioresample", "audioresample");
			audioOut = _engine.CreateElementFromFactory ("autoaudiosink", "autoaudiosink");
			bool resState = _engine.PipelineAddManyElements (pipeline, filesrc, mad, audioConv, audioResampl, audioOut);
			bool resState2 = _engine.ElementLinksMany (filesrc, mad, audioConv, audioResampl, audioOut);

			if (!resState)
				throw new MediaPlayerException ("Init Failed. Exception adding elements to pipeline");
			if (!resState2)
				throw new MediaPlayerException ("Init Failed. Exception Linking elements in pipeline");
			
			_initialized = true;
			return pipeline;
		}
		
		public string Filename {
			get { return _filename; }
			set { _filename = value; }
		}
		
		public MediaStream Current {
			get { return _currrent; }
			set { _current = value; }	
		}	
	}
}
