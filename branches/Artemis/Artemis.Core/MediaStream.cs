
using System;
using System.IO;

namespace Artemis.Core
{
	
	
	public class MediaStream
	{
		private string _uri;
		private string _location;
		
		private string _artist;
		private string _album;
		private string _name;
		private int _length;
		
		//private Gdk.Pixbuf _gdk_cover;
		
		public MediaStream (string uri, string location)
		{
			_uri = uri;
			_location = location;
			if (File.Exists (location)) {
				int index = location.LastIndexOf (Path.DirectorySeparatorChar);
				_name = location.Substring (index + 1);
			}
		}
		
		public virtual bool Load ()
		{
			_artist = string.Empty;
			_album = string.Empty;
			_name = string.Empty;
			_length = 100;
			
			return true;
		}
		
		public virtual bool Save ()
		{
			return true;
		}
		/*
		public static bool operator == (MediaStream stream1, MediaStream stream2)
		{
			return stream1.Location == stream2.Location;
		}
		
		public static bool operator != (MediaStream stream1, MediaStream stream2)
		{
			return stream1.Location != stream2.Location;
		}
		*/
		
		public string Uri {
			get { return _uri; }
		}
		
		public string Location {
			get { return _location; }
		}
		
		public string Artist {
			get { return _artist; }
			set { _artist = value; }
		}
		
		public string Album {
			get { return _album; }
			set { _album = value; }
		}
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		public int Length {
			get { return _length; }
		}
	}
}
