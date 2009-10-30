
using System;
using Gtk;
using Reportero.Data;
using Reportero.UI.Dialogs;
using Reportero.Reports;

namespace Reportero.UI.Widgets
{
	
	
	public class ReportChooser : Gtk.IconView
	{
		private Gtk.ListStore _store;
		
		private VehicleMenuPopup _vehicle_popup;
		private LeadershipMenuPopup _leadership_popup;
		
		private Database _database;
		
		public ReportChooser (Database database)
		{
			_store = new ListStore (typeof (Gdk.Pixbuf),
				typeof (IRecord), 
				typeof (string),
				typeof (string));
			
			_database = database;
			
			Model = _store;
			
			TextColumn = 2;
			PixbufColumn = 0;
			TooltipColumn = 3;
			
			_vehicle_popup = new VehicleMenuPopup ();
			_vehicle_popup.AssignItem.Activated += vehicle_popupAssignActivated;
			_vehicle_popup.StatisticsItem.Activated += vehicle_popupStatisticsActivated;
			_vehicle_popup.StatisticsSpeedItem.Activated += vehicle_popupStatisticsSpeedActivated;
			_vehicle_popup.AboutItem.Activated += aboutdialog_show;
			
			_leadership_popup = new LeadershipMenuPopup ();
			_leadership_popup.ExploreItem.Activated += leadershipExploreActivated;
			_leadership_popup.StatisticsItem.Activated += leadershipStatisticsActivated;
			_leadership_popup.StatisticsSpeedItem.Activated += leadershipStatisticsSpeedActivated;
			_leadership_popup.AboutItem.Activated += aboutdialog_show;
			
		}
		
		public void Append (IRecord record)
		{
			Gdk.Pixbuf buf = null;
			string text =  string.Empty;
			string tooltip = string.Empty;
			if (record.Type == RecordType.Leadership) {
				text = ((Leadership)record).Name;
				buf = Gdk.Pixbuf.LoadFromResource ("reportero_icon_lead.png");
				tooltip = string.Format ("<b>{0}</b> ({1} Vehiculos)\n{2}",
					(record as Leadership).Name,
					(record as Leadership).GetVehicles ().Count,
					(record as Leadership).GetFullname ());
			} 
			else if (record.Type == RecordType.VehicleUser) { 
				text = ((VehicleUser)record).VehicleId;
				buf = Gdk.Pixbuf.LoadFromResource ("reportero_icon_pickup.png");
				tooltip = string.Format ("<b>Vehiculo.</b> {0}\n" +
					"<b>Resguardo.</b> {1}\n" +
					"<b>Ficha.</b> {2}",
					(record as VehicleUser).VehicleId,
					(record as VehicleUser).Name,
					(record as VehicleUser).Id);
			}
			
			_store.AppendValues (buf.ScaleSimple(54, 54, Gdk.InterpType.Bilinear), record, text, tooltip);
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
		
		public void GoHome ()
		{
			_store.Clear ();
			foreach (Leadership lead in Leadership.FromDatabase (Db))
				Append (lead);
		}
		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			IRecord record;
			
			if (evnt.Type == Gdk.EventType.TwoButtonPress && evnt.Button == 1) {
				if (GetRecordAtPointer (out record, (int) evnt.X, (int) evnt.Y)) {
					if (record.Type == RecordType.Leadership) {
						LoadVehicles (record as Leadership);
						return false;
					} 
					else if (record.Type == RecordType.VehicleUser) {
						AssignVehicle (record as VehicleUser);
					}
				}
			} else if (evnt.Button == 3) {
				if (GetRecordAtPointer (out record, (int) evnt.X, (int) evnt.Y))
					if (record.Type == RecordType.VehicleUser)
						_vehicle_popup.Popup ();
					else if (record.Type == RecordType.Leadership)
						_leadership_popup.Popup ();
			}
			
			return base.OnButtonPressEvent (evnt);
		}
		
		protected override bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			if (evnt.Key == Gdk.Key.Return||evnt.Key == Gdk.Key.KP_Enter) {
				IRecord record;
				if (GetSelected (out record)) {
					if (record.Type == RecordType.Leadership)
						LoadVehicles (record as Leadership);
					if (record.Type == RecordType.VehicleUser)
						AssignVehicle (record as VehicleUser);
				}
			}
			
			if (evnt.Key == Gdk.Key.Escape)
				GoHome ();
			if (evnt.Key == Gdk.Key.Menu) {
				IRecord record;
				if (GetSelected (out record)) {
					if (record.Type == RecordType.VehicleUser)
						_vehicle_popup.Popup ();
					else if (record.Type == RecordType.Leadership)
						_leadership_popup.Popup ();
				}
			}
		
			return base.OnKeyPressEvent (evnt);
		}
		
		protected override bool OnMotionNotifyEvent (Gdk.EventMotion evnt)
		{
			return base.OnMotionNotifyEvent (evnt);
		}


		public void LoadVehicles (Leadership leadership)
		{
			_store.Clear ();
			foreach (VehicleUser vehicle in leadership.GetVehicles ())
				Append (vehicle);
		}
		
		public void AssignVehicle (VehicleUser user)
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
		
		private void vehicle_popupAssignActivated (object sender, EventArgs args)
		{
			IRecord record;
			
			if (GetSelected (out record))
				if (record.Type == RecordType.VehicleUser)
					AssignVehicle (record as VehicleUser);
			}
		
		private void vehicle_popupStatisticsActivated (object sender, EventArgs args)
		{
			DateTime startdate;
			DateTime enddate;
			
			if (getDateRange (out startdate, out enddate)) {
				IRecord record;
				if (GetSelected (out record)) {
					ActivityGraphicReport report = new ActivityGraphicReport (record as VehicleUser, startdate, enddate);
					
					ReportDialog dialog = new ReportDialog (report);
					
					dialog.Title = AppSettings.Instance.GetFormatedTitle ("Reporte de Actividad");
					dialog.Run ();
					dialog.Destroy ();
				}
			}
		}
		
		
		private void leadershipExploreActivated (object sender, EventArgs args)
		{
			IRecord record;
			
			if (GetSelected (out record)) {
				if (record.Type == RecordType.Leadership)
					LoadVehicles (record as Leadership);
			}
		}
		
		private void leadershipStatisticsActivated (object sender, EventArgs args)
		{
			IRecord record;
			
			DateTime start;
			DateTime end;
			
			if (getDateRange (out start, out end)) {
				if (GetSelected (out record)) {
					if (record.Type == RecordType.Leadership) {
						FileChooserDialog dialog = new FileChooserDialog (
							AppSettings.Instance.GetFormatedTitle ("Crear  y guardar reporte..."),
							null,
							FileChooserAction.Save);
						
						dialog.CurrentName = 
							string.Format ("{0} ({1} al {2}).pdf",
								(record as Leadership).Name,
								start.ToString ("dd.MM.yy"), end.ToString ("dd.MM.yyyy"));
						
						dialog.AddButton (Stock.Cancel, ResponseType.Cancel);
						dialog.AddButton (Stock.Save, ResponseType.Ok);
						
						ResponseType response = (ResponseType) dialog.Run ();
						string filename = dialog.Filename;
						dialog.Destroy ();
						
						if (response == ResponseType.Ok) {
							ActivityListReport report = new ActivityListReport (record as Leadership, start, end);
							report.CreatePdf (
								AppSettings.Instance.PdfAppLoader, 
								filename, 
								AppSettings.Instance.PdfRunOnGenerated);
						}
					}
				}
			}
		}
		
		private void vehicle_popupStatisticsSpeedActivated (object sender, EventArgs args)
		{
			IRecord record;
			
			if (GetSelected (out record)) {
				if (record.Type == RecordType.VehicleUser) {
					DateRangeSelectionDialog dialog = new DateRangeSelectionDialog ();
					ResponseType response = dialog.Run ();
					DateTime start = dialog.StartingDateEntry.Date;
					DateTime end = dialog.EndingDateEntry.Date;
					dialog.Destroy ();
					if (response == ResponseType.Ok)
					do {
						string str = string.Format (
							"{0} rebasó {1} veces el {2}.",
							(record as VehicleUser).VehicleId,
							(record as VehicleUser).GetTimesSpeedOvertaken (start).ToString (),
							start.ToString ("dd-MM-yyyy")
						);
						MessageDialog msg = new MessageDialog (null, 
						DialogFlags.Modal, 
						MessageType.Info,
						ButtonsType.Ok,
						str
						);
					
						msg.Run  ();
						msg.Destroy ();
						start = start.AddDays (1);
					}while (start < end);
					
				}
			}
		}
		
		private void leadershipStatisticsSpeedActivated (object sender, EventArgs args)
		{
			IRecord record;
			
			DateTime start;
			DateTime end;
			
			if (getDateRange (out start, out end)) {
				if (GetSelected (out record)) {
					if (record.Type == RecordType.Leadership) {
						FileChooserDialog dialog = new FileChooserDialog (
							AppSettings.Instance.GetFormatedTitle ("Crear  y guardar reporte..."),
							null,
							FileChooserAction.Save);
						
						dialog.CurrentName = 
							string.Format ("{0} ({1} al {2}).pdf",
								(record as Leadership).Name,
								start.ToString ("dd.MM.yy"), end.ToString ("dd.MM.yyyy"));
						
						dialog.AddButton (Stock.Cancel, ResponseType.Cancel);
						dialog.AddButton (Stock.Save, ResponseType.Ok);
						
						ResponseType response = (ResponseType) dialog.Run ();
						string filename = dialog.Filename;
						dialog.Destroy ();
						
						if (response == ResponseType.Ok) {
							SpeedListReport report = new SpeedListReport (record as Leadership, start, end);
							report.CreatePdf (
								AppSettings.Instance.PdfAppLoader, 
								filename, 
								AppSettings.Instance.PdfRunOnGenerated);
						}
					}
				}
			}

		}
		
		private void aboutdialog_show (object sender, EventArgs args)
		{
			ReporteroAboutDialog dialog = new ReporteroAboutDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}
		
		private bool getDateRange (out DateTime date1, out DateTime date2)
		{
			DateRangeSelectionDialog dlg = new DateRangeSelectionDialog ();
			ResponseType response = (ResponseType) dlg.Run ();
			date1 = dlg.StartingDateEntry.Date;
			date2 = dlg.EndingDateEntry.Date;
			dlg.Destroy ();
			
			if (response == ResponseType.Ok)
				return true;
			
			return false;
		}
		
		
		public Database Db {
			get { return _database; }
		}
	}
}
