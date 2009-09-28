
using System;
using System.Data;
using Gtk;
using Reportero.UI.Dialogs;
using Reportero.UI.Widgets;
using Reportero.Data;


namespace Reportero.UI
{
	
	
	public class MainWindow : Gtk.Window
	{
		private Gtk.VBox _vbox;
		private ReportChooser _chooser;
		private ReportToolbar _toolbar;
		
		private Database _database;
		
		public MainWindow () : base (WindowType.Toplevel)
		{
			Title = "Reportero";
			WindowPosition = WindowPosition.Center;
			Resize (800, 600);
			
			_database = new Database (
				AppSettings.DbHostname, 
				AppSettings.DbUserid, 
				AppSettings.DbPasword, 
				AppSettings.DbSource);
			
			_toolbar = new ReportToolbar ();
			_toolbar.AssignButton.Clicked += delegate {
				IRecord record;
				
				if (_chooser.GetSelected (out record)) {
					if (record.Type == RecordType.Leadership)
				 		assignVehicle (record as Leadership);
				 }
			};
			
			_toolbar.HomeButton.Clicked += delegate {
				_chooser.GoHome (_database);
			};
			
			_chooser = new ReportChooser ();
			//_chooser.ButtonPressEvent += chooserButtonPressEvent;
			
			
			_vbox = new VBox (false, 5);
			_vbox.PackStart (_toolbar, false, false, 0);
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_chooser);
			_vbox.PackStart (scroll);
			
			Add (_vbox);			
		}
		
		protected override void OnShown ()
		{
			base.OnShown ();
			_database.Open ();
			
			foreach (Leadership leader in LeadershipCollection.FromDatabase (_database))
				_chooser.Append (leader);

		}
		
		protected override bool OnDeleteEvent (Gdk.Event evnt)
		{
			Application.Quit ();
			return false;
		}
		
		/*
		[GLib.ConnectBefore]
		private void chooserButtonPressEvent (object sender, ButtonPressEventArgs args)
		{
			if (args.Event.Type == Gdk.EventType.TwoButtonPress) {// &&
				//args.Event.Button == 0) {
					IRecord record;
					if (_chooser.GetRecordAtPointer (out record, 
						(int) args.Event.X, (int) args.Event.Y))
						if (record.Type == RecordType.Leadership)
							assignVehicle (record as Leadership);
			}
		}
		*/
		private void assignVehicle (Leadership leader)
		{
		 	VehicleAssignDialog dialog = new VehicleAssignDialog (leader);
		 	VehicleUser user = new VehicleUser (Database);
				 	
			user.VehicleId = leader.Name;
			if (user.Update ()) {
				dialog.NameEntry.Text = user.Name;
				dialog.IdEntry.Text = user.Id;
			}
				 	
		 	ResponseType response = dialog.Run ();
		 	user.Name = dialog.NameEntry.Text;
		 	user.Id = dialog.IdEntry.Text;
				 	
		 	dialog.Destroy ();
		 	if (response == ResponseType.Ok) {
		 		user.Save ();
		 	}
		}
		
		public Database Database {
			get { return _database; }
		}
	}
}