
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
					EditPrestamo (prestamo);
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
					EditPrestamo (prestamo);
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
					AddPrestamo (prestamo);
					//Populate ();
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
		
		public void EditPrestamo (Prestamo prestamo)
		{
			TreeIter iter;
			string [] row = GetPrestamoAsRow (prestamo);
			
			if (BuscarPrestamo (prestamo, out iter)) {
			//if (Selection.GetSelected (out iter)) {
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
							if (prestamo_editado.Save ())
								EditPrestamo (prestamo_editado);
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
