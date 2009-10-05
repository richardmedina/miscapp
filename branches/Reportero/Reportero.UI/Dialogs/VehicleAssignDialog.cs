
using System;
using Gtk;
using Reportero.Data;

namespace Reportero.UI.Dialogs
{
	
	
	public class VehicleAssignDialog : CustomDialog
	{
		private Gtk.Entry _entry_pemexid;
		private Gtk.Entry _entry_name;
		
		private VehicleUser _vehicle;
		
		public VehicleAssignDialog (VehicleUser vehicle)
		{
			_vehicle = vehicle;
		
			Title = AppSettings.Instance.GetFormatedTitle ("Asignación de vehículo");
			Resize (300, 170);
			VBox.Spacing = 5;
			_entry_pemexid = new Entry ();
			_entry_name = new Entry ();
			
			_entry_pemexid.Changed += entry_pemexid_Changed;
			_entry_name.Changed += entry_changed;
			_entry_pemexid.Changed += entry_changed;
			
			Label label = new Gtk.Label (string.Format ("Asignación del vehículo:\n{0}", 
				vehicle.VehicleId));
			VBox.PackStart (label, false, false, 10);
			
			Gtk.HBox hbox = new HBox (false, 5);
			
			VBox.PackStart (hbox, false, false, 0);
			label = new Label ("Nombre");
			label.WidthRequest = 70;
			
			hbox.PackStart (label, false, false, 0);
			hbox.PackStart (_entry_name);
			
			hbox = new HBox (false, 5);
			label = new Label ("Ficha");
			label.WidthRequest = 70;
			
			hbox.PackStart (label, false, false, 0);
			hbox.PackStart (_entry_pemexid);
			VBox.PackStart (hbox, false, false, 0);
			
			VBox.ShowAll ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Save, ResponseType.Ok);
			
			SetResponseSensitive (ResponseType.Ok, false);
		}

		private void entry_pemexid_Changed (object sender, EventArgs args)
		{
			string val = _entry_pemexid.Text;
			string final = string.Empty;
			
			foreach (char c in val.ToCharArray ())
				if (c >= '0' && c <= '9')
					final += c.ToString ();
			
			_entry_pemexid.Text = final;
		}
		
		private void entry_changed (object sender, EventArgs args)
		{
			SetResponseSensitive (ResponseType.Ok,
				_entry_name.Text.Trim ().Length > 0 &&
				_entry_pemexid.Text.Trim ().Length > 0);
		}
		
		public Gtk.Entry IdEntry {
			get { return _entry_pemexid; }
		}
		
		public Gtk.Entry NameEntry {
			get { return _entry_name; }
		}
		
		public VehicleUser Vehicle {
			get { return _vehicle; }
		}
	}
}
