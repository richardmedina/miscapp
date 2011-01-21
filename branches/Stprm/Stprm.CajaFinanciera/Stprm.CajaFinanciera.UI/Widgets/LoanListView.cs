
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
		
		public bool MostrarPagados = true;
		
		public LoanListView ()
		{	
			RulesHint = true;
			
			_prestamo_context_menu = new PrestamoContextMenu ();
			_prestamo_context_menu.ItemInformacion.Activated += Handle_prestamo_context_menuItemInformacionActivated;
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
				prestamo.Save ();
			}
			dialog.Destroy ();
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
							prestamo_editado.Save ();
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
			if (evnt.Button == 3)
				_prestamo_context_menu.Popup ();
			
			return base.OnButtonPressEvent (evnt);
		}
	}
}
