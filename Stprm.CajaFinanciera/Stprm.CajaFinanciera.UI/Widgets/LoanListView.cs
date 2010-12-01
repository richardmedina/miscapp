
using System;
using System.Data;
using Gtk;
using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Dialogs;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class LoanListView : DataSetView
	{
		
		public LoanListView ()
		{
			RulesHint = true;
		}
		
		public new void Populate ()
		{
			base.Populate ();
			
			for (int i = 7; i < 12; i ++)
				Renders [i].Xalign = 1;
			
			Columns [0].Visible = false;
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
			Prestamo.GetInAdapter (Globals.Db).Fill (ds);
			LoadDataSet (ds);
			Populate ();
		}
		
		protected override void OnActivated ()
		{
			EditSelected ();
			base.OnActivated ();
		}
	}
}
