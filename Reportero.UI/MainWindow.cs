
using System;
using System.Data;
using Gtk;
using Reportero.UI.Dialogs;
using Reportero.UI.Widgets;
using Reportero.Data;
using Reportero.Reports;


namespace Reportero.UI
{
	
	
	public class MainWindow : Gtk.Window
	{
		private Gtk.VBox _vbox;
		
		private ReportMenubar _menubar;
		private ReportToolbar _toolbar;
		private ReportChooser _chooser;
		
		private Database _database;
		
		public MainWindow () : base (WindowType.Toplevel)
		{
			Report.HeaderCompany = AppSettings.Instance.ReportHeaderCompany;
			Report.HeaderRegion = AppSettings.Instance.ReportHeaderRegion;
			Report.HeaderPlace = AppSettings.Instance.ReportHeaderPlace;
		
			Title = AppSettings.Instance.GetFormatedTitle ("Principal");
			WindowPosition = WindowPosition.Center;
			Icon = Gdk.Pixbuf.LoadFromResource ("reportero_icon_main.png");
			Resize (1024, 768);
			
			_database = new Database (
				AppSettings.Instance.DbHostname, 
				AppSettings.Instance.DbUsername, 
				AppSettings.Instance.DbPasword, 
				AppSettings.Instance.DbSource);
			
			_menubar = new ReportMenubar ();
			
			_toolbar = new ReportToolbar ();
			_toolbar.AssignButton.Clicked += toolbarAssignButtonClicked;
			_toolbar.HomeButton.Clicked += toolbarHomeButtonActivated;
			
			_chooser = new ReportChooser (Db);
			//_chooser.ButtonPressEvent += chooserButtonPressEvent;
			
			
			_vbox = new VBox (false, 0);
			
			_vbox.PackStart (_menubar, false, false, 0);
			_vbox.PackStart (_toolbar, false, false, 0);
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_chooser);
			_vbox.PackStart (scroll);
			
			_chooser.GrabFocus ();
			
			Add (_vbox);			
		}
		
		protected override void OnShown ()
		{
			base.OnShown ();
			if (AppSettings.Instance.EnableConfiguration) {
				MessageDialog dialog = new MessageDialog (this,
					DialogFlags.Modal, MessageType.Info, ButtonsType.Ok,
					"<b>Información</b>.\nLa aplicacion ha iniciado en <b>modo desconectado</b>"
				);
				dialog.Run ();
				dialog.Destroy ();
			} else if (!_database.Open ()) {
				MessageDialog dialog = new MessageDialog ( this,
					DialogFlags.Modal, MessageType.Error, ButtonsType.Ok,
					"<b>Error al iniciar</b>.\nLa aplicación terminará ahora.");
			
				dialog.Run ();
				dialog.Destroy ();
			}
			//_chooser.GoHome ();
		}
		
		protected override bool OnDeleteEvent (Gdk.Event evnt)
		{
			Application.Quit ();
			return false;
		}
		
		private void toolbarHomeButtonActivated (object sender, EventArgs args)
		{
			_chooser.GoHome ();
		}
		
		private void toolbarAssignButtonClicked (object sender, EventArgs args)
		{
			IRecord record;
				
			if (_chooser.GetSelected (out record)) {
				if (record.Type == RecordType.VehicleUser)
			 		_chooser.AssignVehicle (record as VehicleUser);
			 }
		}
		
		public Database Db {
			get { return _database; }
		}
	}
}