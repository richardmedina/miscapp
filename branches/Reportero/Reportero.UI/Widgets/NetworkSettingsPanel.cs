
using System;

namespace Reportero.UI.Widgets
{
	
	
	public class NetworkSettingsPanel : SettingsPanel
	{
		
		private Gtk.Entry _entry_hostname;
		private Gtk.Entry _entry_username;
		private Gtk.Entry _entry_password;
		private Gtk.Entry _entry_source;
		
		public NetworkSettingsPanel()
		{
			Title = "Base de datos";
			ResetRequire = true;
			
			_entry_hostname = new Gtk.Entry (AppSettings.Instance.DbHostname);
			_entry_username = new Gtk.Entry (AppSettings.Instance.DbUserid);
			_entry_password = new Gtk.Entry (AppSettings.Instance.DbPasword);
			_entry_password.Visibility = false;
			_entry_source = new Gtk.Entry (AppSettings.Instance.DbSource);
			
			PackStart (entry_pack ("Direccion", _entry_hostname),
				false, false, 0);
			
			PackStart (entry_pack ("Usuario", _entry_username),
				false, false, 0);
				
			PackStart (entry_pack ("Contraseña", _entry_password),
				false, false, 0);
			
			PackStart (entry_pack ("Base de datos", _entry_source),
				false, false, 0);
			
		}
		
		private Gtk.HBox entry_pack (string label, Gtk.Entry entry)
		{
			Gtk.HBox hbox = new Gtk.HBox (false, 5);
			
			hbox.PackStart (create_label (label, 100), false, false, 0);
			hbox.PackStart (entry);
			
			return hbox;
		}
		
		private Gtk.Label create_label (string text, int hsize)
		{
			Gtk.Label label = new Gtk.Label (text);
			label.SetAlignment (0.0f, 0.5f);
			label.WidthRequest = hsize;
			
			return label;
		}
		
		public override bool Save ()
		{
			return false;
		}
	}
}
