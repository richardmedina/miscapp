
using System;
using Artemis.PlayerEngine;

namespace Artemis.PlayerEngine.GStreamer
{
	
	
	public class GstPlayer : Player
	{
		private Gst _gst;
		private IntPtr _pipeline;
		private IntPtr _src;
		private IntPtr _decoder;
		private IntPtr _dst;
		
		
		
		public GstPlayer ()
		{
			Engine = PlayerEngineType.GStreamer;
			string [] args = new string [0];
			_gst = Gst.Instance;
			_gst.Init ("Artemis", ref args);
			
			_pipeline = _gst.CreatePipeline ("pipeline");
			
			_src = _gst.CreateElementFromFactory ("playbin", "playbin0");
			//_decoder = _gst.CreateElementFromFactory ("mad", "mad0");
			//_dst = _gst.CreateElementFromFactory ("pulsesink", "pulsesink0");
			
			if (!_gst.PipelineAddManyElements (_pipeline, _src)){//, _decoder, _dst)){// ||{
				//!_gst.ElementLinksMany (_src, _decoder, _dst)) {
				throw new MediaPlayerException ("Error Adding or Linking elements to pipeline");	
			}
		}
		
		public override void Play (MediaStream media)
		{
			if (State == PlayerState.Playing || State == PlayerState.Paused)
				Stop ();
				//State = PlayerState.Stopped;
			Console.WriteLine ("Playing : {0}", media.Uri);
			_gst.ElementSetProperty (_src, "uri", media.Uri);
			_gst.ElementSetState (_pipeline, GstState.GST_STATE_PLAYING);
			//string val = String.Empty;
			//Gst.g_object_get (_pipeline, "stream-info", ref val, IntPtr.Zero);
			//Console.WriteLine (val);
			Current = media;
			State = PlayerState.Playing;
		}
		
		public override void Pause ()
		{
			if (State == PlayerState.Playing) {
				_gst.ElementSetState (_pipeline, GstState.GST_STATE_PAUSED);
				State = PlayerState.Paused;
			}
			else if (State == PlayerState.Paused) {
				_gst.ElementSetState (_pipeline, GstState.GST_STATE_PLAYING);
				State = PlayerState.Playing;
			}
		}
		
		public override void Stop ()
		{
			_gst.ElementSetState (_pipeline, GstState.GST_STATE_NULL);
			State = PlayerState.Stopped;
		}
	}
}
