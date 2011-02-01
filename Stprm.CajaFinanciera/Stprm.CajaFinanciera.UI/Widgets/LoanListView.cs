
using System;
using System.Data;
using Gtk;
using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Dialogs;

using RickiLib.Widgets;
namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class LoanListView : DataSetView
	{
		private PrestamoContextMenu _prestamo_context_menu;
		
		public bool MostrarPagados = false;
		
		public LoanListView ()
		{	
			RulesHint = true;
			
			_prestamo_context_menu = new PrestamoContextMenu ();
			_prestamo_context_menu.ItemInformacion.Activated += Handle_prestamo_context_menuItemInformacionActivated;
			_prestamo_context_menu.ItemReactivar.Activated += Handle_prestamo_context_menuItemReactivarActivated;
			_prestamo_context_menu.ItemSuspender.Activated += Handle_prestamo_context_menuItemSuspenderActivated;
		}

		private void Handle_prestamo_context_menuItemSuspenderActivated (object sender, EventArgs e)
		{
			string [] row;
			
			if (GetSelected (out row)) {
				Prestamo prestamo = new Prestamo (Globals.Db);
				prestamo.Id = int.Parse (row [0]);
				if (prestamo.Update ()) {
					prestamo.Suspender ();
					prestamo.Save ();
					UpdatePrestamo (prestamo);
				}
			}			
		}

		private void Handle_prestamo_context_menuItemReactivarActivated (object sender, EventArgs e)
		{
			string [] row;
			
			if (GetSelected (out row)) {
				Prestamo prestamo = new Prestamo (Globals.Db);
				prestamo.Id = int.Parse (row [0]);
				if (prestamo.Update ()) {
					prestamo.Reactivar (OperacionFinancieraEstado.Retenido);	
					prestamo.Save ();
					UpdatePrestamo (prestamo);
				}
			}
		}

		private void Handle_prestamo_context_menuItemInformacionActivated (object sender, EventArgs e)
		{
			EditSelected ();
		}
		
		public new void Populate ()
		{
			base.Populate ();
			
			for (int i = 7; i < 12; i ++)
				Renders [i].Xalign = 1;
		}
		
		public override void New ()
		{
			PrestamoDialog dialog = new PrestamoDialog ();
			if (dialog.Run () == ResponseType.Ok) {
				Prestamo prestamo = dialog.GetAsPrestamo ();
				if (prestamo.Save ()) {
					/*******************
					 * Monto inicial del prestamo
					 * ****************/
					PrestamoMovimiento prestamo_mov = new PrestamoMovimiento (Globals.Db);
					prestamo_mov.PrestamoId = prestamo.Id;
					prestamo_mov.TrabajadorInternalId = prestamo.TrabajadorInternalId;
					prestamo_mov.Fecha = prestamo.Fecha;
					prestamo_mov.Concepto = string.Format ("Prestamo CH={0} PA={1}", 
						prestamo.Cheque, prestamo.Pagare);
					prestamo_mov.Cargo = prestamo.Cargo;
					prestamo_mov.CargoCapital = prestamo.Capital;
						
					prestamo_mov.Save ();
					/*********************
					 * Intereses 
					 * ******************/
					prestamo_mov = new PrestamoMovimiento (Globals.Db);
							
					prestamo_mov.PrestamoId = prestamo.Id;
					prestamo_mov.TrabajadorInternalId = prestamo.TrabajadorInternalId;
					prestamo_mov.Fecha = prestamo.Fecha;
					prestamo_mov.Concepto = string.Format ("Intereses", 
						prestamo.Cheque, prestamo.Pagare);
					prestamo_mov.Cargo = prestamo.Cargo;
					prestamo_mov.CargoCapital = prestamo.Capital;
					prestamo_mov.Save ();
				
					AddPrestamo (prestamo);
					Employee employee = new Employee (Globals.Db);
					employee.InternalId = prestamo.TrabajadorInternalId;
					
					if(employee.UpdateFromInternalId ())
						Globals.MainWindow.ViewEmployees.UpdateTrabajador (employee);
				}
			}
			dialog.Destroy ();
		}
		
		public void AddPrestamo (Prestamo prestamo)
		{
			string [] row = GetPrestamoAsRow (prestamo);
			this.Dataset.Tables [0].Rows.Add (row);
			AddRow (row);
		}
		
		public string []  GetPrestamoAsRow (Prestamo prestamo)
		{
			Employee employee = new Employee (Globals.Db);
			employee.InternalId = prestamo.TrabajadorInternalId;
			employee.UpdateFromInternalId ();
			
			return new string [] {prestamo.Id.ToString (),
											  prestamo.Fecha.ToString ("dd/MM/yyyy"),
			                                  prestamo.Folio,
			                                  prestamo.Cheque,
			                                  prestamo.Pagare,
			                                  employee.Id,
			                                  employee.GetFullName (),
			                                  prestamo.Capital.ToString ("C"),
			                                  prestamo.Interes.ToString ("C"),
			                                  (prestamo.Capital + prestamo.Interes).ToString ("C"),
			                                  prestamo.Abono.ToString ("C"),
			                                  prestamo.Saldo.ToString ("C"),
			                                  DataMisc.OperacionFinancieraEstadoToShortString (prestamo.Status)};	
		}
		
		public void UpdatePrestamo (Prestamo prestamo)
		{
			TreeIter iter;
			
			if (BuscarPrestamo (prestamo, out iter)) {
				string [] row = GetPrestamoAsRow (prestamo);
				Store.SetValues (iter, row);
			}
		}
		
		public bool BuscarPrestamo (Prestamo prestamo, out Gtk.TreeIter iter)
		{
			bool result = false;
			Gtk.TreeIter xiter;
			
			iter = TreeIter.Zero;
		
			if (Store.GetIterFirst (out xiter)) {
				do {
					if (prestamo.Id.ToString () == (string) Store.GetValue (xiter, 0)) {
						iter = xiter;
						result = true;
						break;
					}
				} while (Store.IterNext (ref xiter));
			}
			
			return result;
		}
		
		public override void EditSelected ()
		{
			string [] fields;
			int id;
			
			if (GetSelected (out fields)) {
				Console.WriteLine ("{0}..", fields [1]);
				if (int.TryParse (fields [0], out id)) {
					Console.WriteLine ("Id : {0}", id);
					Prestamo prestamo = new Prestamo (Globals.Db);
					prestamo.Id = id;
					
					if (prestamo.Update ()) {
						PrestamoDialog dialog = new PrestamoDialog ();
						dialog.UpdateFromPrestamo (prestamo);
						if (dialog.Run () == ResponseType.Ok) {
							Prestamo prestamo_editado = dialog.GetAsPrestamo ();
							if (prestamo_editado.Save ()) {
								UpdatePrestamo (prestamo_editado);
								
								Employee employee = new Employee (Globals.Db);
								employee.InternalId = prestamo.TrabajadorInternalId;
								if (employee.UpdateFromInternalId ()) {
									Console.WriteLine("Actualizando empleado {0}", employee.GetFullName ());
									Globals.MainWindow.ViewEmployees.UpdateTrabajador (employee);
								}
							}
						}
						dialog.Destroy ();
					}
				}
			}
		}
		
		public override void Load ()
		{
			DataSet ds = new DataSet ();
			Prestamo.GetInAdapter (Globals.Db, MostrarPagados).Fill (ds);
			LoadDataSet (ds);
			Columns [0].Visible = false;
			Populate ();
		}
		
		protected override void OnActivated ()
		{
			EditSelected ();
			base.OnActivated ();
		}
		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			if (evnt.Button == 3) {
				string [] row;
				
				if (GetSelected (out row)) {
					Prestamo prestamo = new Prestamo (Globals.Db);
					prestamo.Id = int.Parse (row [0]);
					if (prestamo.Update ()) {
						_prestamo_context_menu.Sensitivizar (prestamo);
						_prestamo_context_menu.Popup ();
					}
				}
			}
			
			return base.OnButtonPressEvent (evnt);
		}
		
	}
}
