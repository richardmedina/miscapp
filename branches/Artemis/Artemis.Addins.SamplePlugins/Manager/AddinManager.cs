
using System;
using Artemis.Addins;
using Artemis.UI;
using Artemis.UI.Widgets;
using Gtk;

namespace Artemis.Addins.Samples
{
	
	
	public class AddinManager : Addin
	{
		private AddinCollection _addins;
		private MediaEnv _env;
		
		private Gtk.ImageMenuItem _itm_addman;
		
		private AddinManagerWindow _wnd_man;
		
		public AddinManager (AddinCollection addins)
		{
			_addins = addins;
		}
		
		public override void OnInit (MediaEnv env)
		{
			_env = env;
			
			_wnd_man = new AddinManagerWindow ();
			
			_itm_addman = new ImageMenuItem ("Addin Manager...");
			_itm_addman.Image = Image.NewFromIconName (Stock.Preferences, IconSize.Menu);
			
			_env.MainWindow.Menubar.Addins.Append (_itm_addman);
			_itm_addman.Activated += itm_addmanActivated;
			_itm_addman.Show ();
		}
		
		private void itm_addmanActivated (object sender, EventArgs args)
		{
			_wnd_man.Resize (640, 480);
			_wnd_man.Modal = true;
			//_wnd_man.Parent = _env.MainWindow;
			_wnd_man.WindowPosition = WindowPosition.CenterOnParent;
			_wnd_man.ShowAll ();
		}
		
		public AddinCollection Addins {
			get { return _addins; }
			set { _addins = value; }
		}
		
		public MediaEnv Env {
			get { return _env; }
			set { _env = value; }
		}
	}
}
