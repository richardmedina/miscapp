using System;
using System.Data;
using System.Threading;
using Gtk;
using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI;
using Stprm.CajaFinanciera.UI.Widgets;
using Stprm.CajaFinanciera.UI.Dialogs;

using RickiLib.Widgets;

public partial class MainWindow : Gtk.Window
{
	private EmployeeListView _view_employees;
	private LoanListView _view_loans;
	
	private Gtk.Notebook _notebook;
	
	private bool _isloading = false;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		_view_employees = new EmployeeListView ();
		
		_view_loans = new  LoanListView ();
		
		WindowPosition = Gtk.WindowPosition.Center;
		
		Build ();
			
		Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
		scroll.Add (_view_employees);
		
		_notebook = new Notebook ();
		_notebook.AppendPage (scroll,  new Label ("Trabajadores"));
		
		scroll = new ScrolledWindow ();
		scroll.Add (_view_loans);
		
		_notebook.AppendPage (scroll, new Label ("Préstamos"));
		
		_main_container.Add (_notebook);
		
		Resize (640, 480);
	}
	
	public void SetLoading (bool state)
	{
			_isloading = state;
			GLib.Timeout.Add (50, loadinganim_callback);
	}
	
	public bool loadinganim_callback ()
	{
		if (_isloading) {
			_prg_progress.Show ();
			_prg_progress.Pulse ();
		}
		else
			_prg_progress.Hide ();
		
		return true;
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
		Present ();
		SetLoading (false);
		load_employees ();
	}
	
	private void load_employees ()
	{
		_lbl_msg.Text = "Cargando trabajadores...";
		_isloading = true;
		
		Thread thread = new Thread (new ParameterizedThreadStart (
			delegate (object mydb) {
				int counter = 0;
				Database db = (Database) mydb;
				foreach (Employee emp in Employee.GetStartingWith (db, string.Empty)) {
					Utils.RunOnGtkThread (delegate {
				       	_view_employees.Add (emp);
						_lbl_msg.Text = string.Format ("{0} trabajadores cargados", ++ counter);
					});
				}
				load_loans (db);
				_isloading = false;
			}
		));
		thread.Start (Globals.Db);
		
		
	}
	
	private void load_loans (Database db)
	{
			/*
		Thread thread = new Thread (new ParameterizedThreadStart (
			delegate (object mydb) {*/
				//Database db = (Database) mydb;
				DataSet ds = new DataSet ();
			
				db.QueryToAdapter ("select DATE_FORMAT(pre_fecha,'%d/%m/%Y') as Fecha, pre_folio as Folio, pre_cheque as Cheque, pre_pagare as Pagare, tra_ficha as Ficha, TRIM(CONCAT(tra_nombre, ' ', tra_apepaterno, ' ', tra_apematerno)) as Nombre, CONCAT('$', FORMAT(pre_capital,2)) as Capital, CONCAT('$', FORMAT(pre_interes, 2)) as Intereses, CONCAT('$', FORMAT(pre_capital + pre_interes, 2)) as Total, CONCAT('$', FORMAT(pre_abono,2)) as Abono, CONCAT('$', FORMAT(pre_saldo, 2)) as Saldo from prestamos, trabajadores where prestamos.tra_id = trabajadores.tra_id order by Ficha asc").Fill (ds);
				
				Utils.RunOnGtkThread (delegate {
					_view_loans.LoadDataSet (ds);	
					_view_loans.Populate ();
				});
					
			//}).Start ();
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
