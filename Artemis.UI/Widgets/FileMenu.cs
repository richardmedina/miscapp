
using System;
using Gtk;

namespace Artemis.UI.Widgets
{
	
	
	public class FileMenu : CommonMenu
	{
	
		private Gtk.ImageMenuItem _itm_newplaylist;
		private Gtk.ImageMenuItem _itm_clearplaylist;
		
		private Gtk.ImageMenuItem _itm_importfile;
		private Gtk.ImageMenuItem _itm_importdir;
		
		private Gtk.ImageMenuItem _itm_quit;
		
		public FileMenu (AccelGroup accel) : base ("_Media", accel)
		{
			
			
			_itm_newplaylist = new ImageMenuItem ("New playlist");
			_itm_newplaylist.Image = Image.NewFromIconName (Stock.New, IconSize.Menu);
			
			_itm_clearplaylist = new ImageMenuItem ("Clear current playlist");
			_itm_clearplaylist.Image = Image.NewFromIconName (Stock.Clear, IconSize.Menu);
			
			_itm_importfile = new ImageMenuItem ("Import local files...");
			_itm_importfile.Image = Image.NewFromIconName (Stock.Open, IconSize.Menu);
			_itm_importdir = new ImageMenuItem ("Import local directory...");
			
			_itm_quit = new ImageMenuItem (Stock.Quit, Accel);
			_itm_quit.Activated += itm_quitActivated;
			
			Append (_itm_newplaylist);
			Append (_itm_clearplaylist);
			Append (new SeparatorMenuItem ());
			Append (_itm_importfile);
			Append (_itm_importdir);
			Append (new SeparatorMenuItem ());
			Append (_itm_quit);
			
		}
		
		private void itm_quitActivated (object sender, EventArgs args)
		{
			Application.Quit ();
		}
		
		public ImageMenuItem NewPlaylist {
			get { return _itm_newplaylist; }
		}
		
		public ImageMenuItem ClearPlaylist {
			get { return _itm_clearplaylist; }
		}
		
		public ImageMenuItem ImportLocalFiles {
			get { return _itm_importfile; }
		}
		
		public ImageMenuItem ImportLocalDirectory {
			get { return _itm_importdir; }
		}
		
		public ImageMenuItem Quit {
			get { return _itm_quit; }
		}
	}
}
