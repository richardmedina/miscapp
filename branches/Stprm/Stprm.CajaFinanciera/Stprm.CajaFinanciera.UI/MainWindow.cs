using System;
using Gtk;
using Stprm.CajaFinanciera.Data;

public partial class MainWindow : Gtk.Window
{
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		WindowPosition = Gtk.WindowPosition.CenterAlways;
		Resize (640, 480);
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	protected override void OnShown ()
	{
		base.OnShown ();
		
		string message = "Connection failed";
		Database db = Database.CreateStprmConnection ();
		
		
		if (db.Open ()) {
			message = "Connection success";
			Employee employee = new Employee (db);
			employee.Ficha = "420085";
			employee.Update ();
			
			Console.WriteLine ("Employee : {0}", employee.GetFullName ());
			
			foreach (Employee emp in Employee.GetStartingWith (db, "Ricardo"))
			         Console.WriteLine ("Match :  {0}", emp.GetFullName ());
		}
		
		db.Close ();
		
		MessageDialog dlg = new MessageDialog (this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, message);
		
		dlg.Run ();
		dlg.Destroy ();
	}

}
