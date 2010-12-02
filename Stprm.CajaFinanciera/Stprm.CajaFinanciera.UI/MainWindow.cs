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
	private AhorroListView _view_ahorros;
	private DescuentosListView _view_descs;
	
	private DataSetView [] _views;
	
	private CuentaBancariaChooser _chooser_cuentas;
	
	private SearchEntry _searchentry_search;
	
	private MainToolbar _toolbar;
	
	
	private Gtk.Notebook _notebook;
	
	private bool _isloading = false;
	
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		_views = new DataSetView [4];
		
		_views [0] = _view_employees = new EmployeeListView ();
		_views [1] = _view_loans = new  LoanListView ();
		_views [2] = _view_ahorros = new AhorroListView ();
		_views [3] = _view_descs = new DescuentosListView ();
		
		_chooser_cuentas = new CuentaBancariaChooser ();
		_chooser_cuentas.Combo.Changed += Handle_chooser_cuentasComboChanged;
		_searchentry_search = new SearchEntry ();
		_searchentry_search.Menu = new TrabajadoresCriteriosMenu (null);
		
		_toolbar = new MainToolbar ();
		_toolbar.ButtonNew.Clicked += Handle_toolbarButtonNewClicked;
		_toolbar.ButtonEdit.Clicked += Handle_toolbarButtonEditClicked;
		_toolbar.ButtonRemove.Clicked += Handle_toolbarButtonRemoveClicked;
		_toolbar.ButtonRefresh.Clicked += Handle_toolbarButtonRefreshClicked;
		Resize (800, 600);
		
		Build ();
		
		Gtk.HBox hbox = new Gtk.HBox (false, 5);
		hbox.PackStart (Factory.Label ("Cuenta Bancaria:"), false, false, 0);
		hbox.PackStart (_chooser_cuentas, false, false, 0);
		
		_eb_cuentas.Add (hbox);
		
		hbox = new HBox (false, 5);
		hbox.PackStart (Factory.Label ("Filtrar:"), false, false, 0);
		hbox.PackStart (_searchentry_search, false, false, 0);
		
		_eb_search.Add (hbox);
		_eb_toolbar.Add (_toolbar);
		
		Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
		scroll.Add (_view_employees);
		
		_notebook = new Notebook ();
		_notebook.TabPos = PositionType.Left;
		_notebook.AppendPage (scroll,  new Label ("Trabajadores"));
		
		scroll = new ScrolledWindow ();
		scroll.Add (_view_loans);
		
		_notebook.AppendPage (scroll, new Label ("Préstamos"));
		
		scroll = new ScrolledWindow ();
		scroll.Add (_view_ahorros);
		_notebook.AppendPage (scroll, new Label ("Ahorros"));
		
		scroll = new ScrolledWindow ();
		scroll.Add (_view_descs);
		_notebook.AppendPage (scroll, new Label ("Descuentos"));
		
		_notebook.Sensitive = false;
		_main_container.Add (_notebook);
		
		Title = Globals.FormatWindowTitle ("Principal");
	}

	private void Handle_toolbarButtonRefreshClicked (object sender, EventArgs e)
	{
		_views [_notebook.Page].Load ();
	}

	private void Handle_toolbarButtonRemoveClicked (object sender, EventArgs e)
	{
		_views [_notebook.Page].RemoveSelected ();
	}

	private void Handle_toolbarButtonEditClicked (object sender, EventArgs e)
	{
		_views [_notebook.Page].EditSelected ();
	}

	private void Handle_toolbarButtonNewClicked (object sender, EventArgs e)
	{
		
		_views [_notebook.Page].New ();
		/*
		Stprm.CajaFinanciera.UI.Dialogs.CustomDialog dialog;	
		//Dialog dialog;
		switch (_notebook.Page) {
			case 0:
				dialog = new EmployeeDialog ();
			break;
		
			case 1:
				dialog = new PrestamoDialog ();
			break;
		
			case 2:
				dialog = new AhorroDialog ();
			break;
			case 3:
				dialog = new GenerarDescuentoDialgo ();
			break;
			
			default:
				dialog = null;//new MessageDialog (this, DialogFlags.Modal, MessageType.Warning, ButtonsType.Ok, "Nada que hacer");
			break;
		}
		
		dialog.Run ();
		dialog.Destroy ();*/
	}

	private void Handle_chooser_cuentasComboChanged (object sender, EventArgs e)
	{
		CuentaBancaria cuenta;
		
		if (_chooser_cuentas.Combo.GetSelected (out cuenta)) {
			Globals.CuentaActual = cuenta;
			_notebook.Sensitive = true;
		}
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
		
		
		if (Authenticate ()) { }
			base.OnShown ();
			Present ();		
			_chooser_cuentas.Combo.Populate ();
			
			SetLoading (false);
		
			new Thread (load_everything).Start ();
			//new Thread (load_everything
		/*} else 
			Application.Quit ();
		*/
	}
	
	private bool Authenticate ()
	{
		bool result = false;
		
		AuthenticationDialog dialog = new AuthenticationDialog();
		dialog.Title = Globals.FormatWindowTitle ("Iniciar sesión");
		dialog.Run ();
		
		dialog.Destroy ();
		
		return result;
	}
	/*
	private void load_employees ()
	{
		_lbl_msg.Text = "Cargando trabajadores...";
		_isloading = true;
		
		load_everything ();
		
		_isloading = false;
	}
	*/
	private void load_everything ()
	{
		
		Utils.RunOnGtkThread (delegate {
			_view_employees.Load ();
			while (Application.EventsPending ())
					Application.RunIteration ();
			_view_loans.Load ();
			while (Application.EventsPending ())
					Application.RunIteration ();
			_view_ahorros.Load ();
			while (Application.EventsPending ())
				Application.RunIteration ();
			_view_descs.Load ();
		});
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
		                                       "¿Seguro que deseas salir de la aplicación?");
		
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
