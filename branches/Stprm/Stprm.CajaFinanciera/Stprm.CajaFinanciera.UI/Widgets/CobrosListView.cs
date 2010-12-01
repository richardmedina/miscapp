
using System;
using System.Data;
using Gtk;
using Stprm.CajaFinanciera.Data;

using Stprm.CajaFinanciera.UI.Dialogs;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class CobrosListView : DataSetView
	{

		public CobrosListView ()
		{
			RulesHint = true;
		}
		
		public override void New ()
		{
			GenerarDescuentoDialog dialog = new GenerarDescuentoDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}

		
		public override void Load ()
		{
			DataSet ds = new DataSet ();
			
			Cobro.GetCollectionInAdapter (Globals.Db).Fill (ds);
			
			LoadDataSet (ds);
			Populate ();
			Columns [0].Visible = false;
		}
		
		public override void EditSelected ()
		{
			string [] fields;
			
			if (GetSelected (out fields)) {
				Cobro cobro = new Cobro (Globals.Db);
				int id;
				if (int.TryParse (fields [0], out id)) {
					cobro.Id = id;
					DescuentoDialog dialog = new DescuentoDialog ();
					dialog.LoadDescuentos (cobro);
					dialog.Run ();
					dialog.Destroy ();
				}
			}
		}
		
		protected override void OnActivated ()
		{
			EditSelected ();
			base.OnActivated ();
		}

	}
}
