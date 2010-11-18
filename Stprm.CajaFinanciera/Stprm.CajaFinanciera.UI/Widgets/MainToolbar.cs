
using System;
using Gtk;


namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class MainToolbar : Gtk.Toolbar
	{
		private Gtk.ToolButton _btn_new;
		private Gtk.ToolButton _btn_edit;
		
		
		private Gtk.ToolButton _btn_help;
		private Gtk.ToolButton _btn_about;
		
		
		public MainToolbar ()
		{
			
			IconSize = IconSize.Menu;
			
			_btn_new = new ToolButton (Stock.New);
			_btn_edit = new ToolButton (Stock.Edit);
			
			_btn_help = new ToolButton (Stock.Help);
			_btn_about = new ToolButton (Stock.About);
			
			Insert (_btn_new, -1);
			Insert (_btn_edit, -1);
			Insert (new SeparatorToolItem (), -1);
			Insert (_btn_help, -1);
			Insert (_btn_about, -1);
		}
		
		public ToolButton ButtonNew {
			get { return _btn_new; }
		}
		
		public ToolButton ButtonEdit {
			get { return _btn_edit; }	
		}
	}
}
