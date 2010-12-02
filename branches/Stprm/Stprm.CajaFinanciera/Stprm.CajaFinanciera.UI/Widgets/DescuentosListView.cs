
using System;
using System.Data;
using Gtk;
using Stprm.CajaFinanciera.Data;

using Stprm.CajaFinanciera.UI.Dialogs;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class DescuentosListView : DataSetView
	{
		
		public DescuentosListView ()
		{
			RulesHint = true;
		}
		
		public override void New ()
		{
			GenerarDescuentoDialog dialog = new GenerarDescuentoDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}

		
		public override void EditSelected ()
		{
			string [] fields;
			
			if (GetSelected (out fields)) {
				int id;
				if (int.TryParse (fields [0], out id)) {
					Descuento descuento = new Descuento (Globals.Db);
					descuento.Id = id;
					if (descuento.Update ()) {
						DescuentoMovimientoDialog dialog = new DescuentoMovimientoDialog ();
						dialog.Load (descuento);
						dialog.Run ();
						dialog.Destroy ();
					}
				}
			}
		}

		
		public override void Load ()
		{
			DataSet ds = new DataSet ();
			Descuento.GetCollectionInAdapter (Globals.Db).Fill (ds);
			
			LoadDataSet (ds);
			Populate ();
			Columns [0].Visible = false;
		}
		
		protected override void OnActivated ()
		{
			EditSelected ();
		}

	}
}
