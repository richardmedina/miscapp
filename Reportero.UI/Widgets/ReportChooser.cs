
using System;
using Gtk;
using Reportero.Data;
using Reportero.UI.Dialogs;

namespace Reportero.UI.Widgets
{
	
	
	public class ReportChooser : Gtk.IconView
	{
		private Gtk.ListStore _store;
		
		private VehicleMenuPopup _vehicle_popup;
		
		public ReportChooser()
		{
			_store = new ListStore (typeof (Gdk.Pixbuf),
				typeof (IRecord), 
				typeof (string));
			
			Model = _store;
			TextColumn = 2;
			PixbufColumn = 0;
			_vehicle_popup = new VehicleMenuPopup ();
			_vehicle_popup.AssignItem.Activated += vehicle_popupActivated;
			_vehicle_popup.StatisticsItem.Activated += vehicle_popupAssignActivated;	
		}
		public void Append (IRecord record)
		{
			Gdk.Pixbuf buf = null;
			string text =  string.Empty;
			
			if (record.Type == RecordType.Leadership) {
				text = ((Leadership)record).Name;
				buf = Gdk.Pixbuf.LoadFromResource ("reportero_icon_lead.png");
			} 
			else if (record.Type == RecordType.VehicleUser) { 
				text = ((VehicleUser)record).VehicleId;
				buf = Gdk.Pixbuf.LoadFromResource ("reportero_icon_pickup.png");
			}
			
			_store.AppendValues (buf.ScaleSimple(54, 54, Gdk.InterpType.Bilinear), record, text);
		}
		
		public bool GetSelected (out IRecord record)
		{
			Gtk.TreeIter iter;
			
			record = null;
			
			if (SelectedItems.Length > 0) {
				if (_store.GetIter (out iter, SelectedItems [0])) {
					record = (IRecord) _store.GetValue (iter, 1);
					return true;
				}
			}
			return false;
		}
		
		public bool GetRecordAtPointer (out IRecord record, int x, int y)
		{
			TreePath path = GetPathAtPos (x, y);
			TreeIter iter;
			record = null;
			if (path != null)
				if (_store.GetIter (out iter, path)) {
					record = (IRecord) _store.GetValue (iter, 1);
					SelectPath (path);
					return true;
				}
			
			return false;
		}
		
		public void GoHome (Database database)
		{
			_store.Clear ();
			foreach (Leadership lead in LeadershipCollection.FromDatabase (database))
				Append (lead);
		}
		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			IRecord record;
			
			if (evnt.Type == Gdk.EventType.TwoButtonPress && evnt.Button == 1) {
				if (GetRecordAtPointer (out record, (int) evnt.X, (int) evnt.Y)) {
					if (record.Type == RecordType.Leadership) {
						_store.Clear ();
						foreach (VehicleUser user in VehicleUserCollection.FromLeadership (record as Leadership))
							Append (user);
						return false;
					} 
					else if (record.Type == RecordType.VehicleUser) {
						assignVehicle (record as VehicleUser);
					}
				}
			} else if (evnt.Button == 3) {
				if (GetRecordAtPointer (out record, (int) evnt.X, (int) evnt.Y))
					if (record.Type == RecordType.VehicleUser)
						_vehicle_popup.Popup ();
			}
			
			return base.OnButtonPressEvent (evnt);
		}
		
		private void assignVehicle (VehicleUser user)
		{
		 	VehicleAssignDialog dialog = new VehicleAssignDialog (user);
			
			if (user.Update ()) {
				dialog.NameEntry.Text = user.Name;
				dialog.IdEntry.Text = user.Id;
			}
				 	
		 	ResponseType response = dialog.Run ();
		 	
		 	user.Name = dialog.NameEntry.Text;
		 	user.Id = dialog.IdEntry.Text;
		 	dialog.Destroy ();
		 	
		 	if (response == ResponseType.Ok)
		 		user.Save ();
		}
		
		private void vehicle_popupActivated (object sender, EventArgs args)
		{
			IRecord record;
			
			if (GetSelected (out record))
				if (record.Type == RecordType.VehicleUser)
					assignVehicle (record as VehicleUser);
			}
		
		private void vehicle_popupAssignActivated (object sender, EventArgs args)
		{
			ActivityReportDialog dialog = new ActivityReportDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}
	}
}
