
using System;
using Gtk;
using Stprm.CajaFinanciera.UI.Dialogs;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class MainToolbar : Gtk.Toolbar
	{
		private Gtk.ToolButton _btn_new;
		private Gtk.ToolButton _btn_edit;
		private Gtk.ToolButton _btn_remove;
		private Gtk.ToolButton _btn_refresh;
		
		
		private Gtk.ToolButton _btn_help;
		private Gtk.ToolButton _btn_about;
		
		
		public MainToolbar ()
		{
			
			IconSize = IconSize.LargeToolbar;
			//ToolbarStyle = ToolbarStyle.Both;
			WidthRequest = 250;
			
			_btn_new = new ToolButton (Stock.New);
			_btn_edit = new ToolButton (Stock.Edit);
			_btn_remove = new ToolButton (Stock.Remove);
			_btn_refresh = new ToolButton (Stock.Refresh);
			
			_btn_help = new ToolButton (Stock.Help);
			_btn_about = new ToolButton (Stock.About);
			_btn_about.Clicked += Handle_btn_aboutClicked;
			
			Insert (_btn_new, -1);
			Insert (_btn_edit, -1);
			Insert (_btn_remove, -1);
			Insert (_btn_refresh, -1);
			Insert (new SeparatorToolItem (), -1);
			Insert (_btn_help, -1);
			Insert (_btn_about, -1);
		}

		private void Handle_btn_aboutClicked (object sender, EventArgs e)
		{
			CFAboutDialog dialog = new CFAboutDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}
		
		public ToolButton ButtonNew {
			get { return _btn_new; }
		}
		
		public ToolButton ButtonEdit {
			get { return _btn_edit; }	
		}
		
		public ToolButton ButtonRemove {
			get { return _btn_remove; }
		}
		
		public ToolButton ButtonRefresh {
			get {return _btn_refresh; }
		}
	}
}
