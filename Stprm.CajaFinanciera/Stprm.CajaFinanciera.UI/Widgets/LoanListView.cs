
using System;
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
		
		protected override void OnActivated ()
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
						dialog.Run ();
						dialog.Destroy ();
					}
				}
			}
			base.OnActivated ();
		}
	}
}
