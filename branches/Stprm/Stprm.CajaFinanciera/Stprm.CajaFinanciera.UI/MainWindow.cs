using System;
using Gtk;
using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Widgets;
using Stprm.CajaFinanciera.UI.Dialogs;

public partial class MainWindow : Gtk.Window
{
	private EmployeeListView view;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		view = new EmployeeListView ();
		WindowPosition = Gtk.WindowPosition.Center;
		Build ();
			
		Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
		scroll.Add (view);
		
		_main_container.Add (scroll);
		
		Resize (640, 480);
		ShowAll ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		bool result = QuitConfirm ();
		a.RetVal = !result;
		if (result)
			Application.Quit ();
	}
	
	protected override void OnShown ()
	{
		base.OnShown ();
		
		using (Database db = Database.CreateStprmConnection ())
		{
			foreach (Employee emp in Employee.GetStartingWith (db, string.Empty))
			         view.Add (emp);
		}
	}

	protected virtual void OnQuitActionActivated (object sender, System.EventArgs e)
	{
		if (QuitConfirm ())
			Application.Quit ();
	}
	
	private bool QuitConfirm ()
	{
		bool result = false;
		MessageDialog msg = new MessageDialog (this, 
		                                       DialogFlags.Modal, 
		                                       MessageType.Question, 
		                                       ButtonsType.YesNo, 
		                                       false, 
		                                       "Seguro que deseas salir de la aplicación");
		
		ResponseType response = (ResponseType) msg.Run ();
		msg.Destroy ();
		
		if (response == ResponseType.Yes)
			result = true;
		
		return result;
	}
	
	protected virtual void OnAboutActionActivated (object sender, System.EventArgs e)
	{
		CFAboutDialog dialog = new CFAboutDialog ();
		dialog.Run ();
		dialog.Destroy ();
	}
	
	
}
