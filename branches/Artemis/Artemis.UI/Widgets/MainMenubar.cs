
using System;
using Gtk;

namespace Artemis.UI.Widgets
{
	
	
	public class MainMenubar : Gtk.MenuBar
	{
		private FileMenu _mnu_file;
		
		private CommonMenu _mnu_addins;
		
		private HelpMenu _mnu_help;
		
		private Gtk.AccelGroup _accel;
		
		public MainMenubar(Gtk.AccelGroup accel)
		{
			_accel = accel;
			
			_mnu_file = new FileMenu (_accel);
			_mnu_addins = new CommonMenu ("_Addins", _accel);
			_mnu_help = new HelpMenu (accel);
			
			Append (_mnu_file.MenuItem);
			Append (_mnu_addins.MenuItem);
			Append (_mnu_help.MenuItem);
		}
		
		public FileMenu File {
			get { return _mnu_file; }
		}
		
		public CommonMenu Addins {
			get { return _mnu_addins; }
		}
		
		public HelpMenu Help {
			get { return _mnu_help; }
		}
		
		public AccelGroup Accel {
			get { return _accel; }
		}
	}
}
