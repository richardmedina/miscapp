
using System;
using Gtk;

namespace Reportero.UI.Widgets
{
	
	
	public class SettingsPanel : Gtk.VBox
	{
		
		private string _title;
		private bool _reset_require;
		
		public SettingsPanel()
		{
			Spacing = 5;
			Label label = new Label (
				string.Format ("<big><b>{0}</b></big>",	Title));
			label.UseMarkup = true;
			label.SetAlignment (0f, 0.5f);
			//PackStart (label, false, false, 0);
		}
		
		public virtual bool Save ()
		{
			return true;
		}
		
		public string Title {
			get { return _title; }
			set { _title = value; }
		}
		
		public bool ResetRequire {
			get { return _reset_require; }
			set { _reset_require = value; }
		}
	}
}
