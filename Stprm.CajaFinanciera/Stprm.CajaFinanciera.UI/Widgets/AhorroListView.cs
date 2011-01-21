
using System;
using System.Data;
using Stprm.CajaFinanciera.Data;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.UI.Dialogs;
namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class AhorroListView : DataSetView
	{
		public AhorroListView ()
		{
			RulesHint = true;
		}
		
		public override void New ()
		{
			AhorroDialog dialog = new AhorroDialog ();
			dialog.Run ();
			dialog.Destroy ();
		}
		
		public override void EditSelected ()
		{
			string [] fields;
			
			if (GetSelected (out fields)) {
				int id;
				
				if (int.TryParse (fields [0], out id)) {
					Ahorro ahorro = new Ahorro (Globals.Db);
					ahorro.Id = id;
					if (ahorro.Update ()) {
						AhorroDialog dialog = new AhorroDialog ();
						dialog.Run ();
						dialog.Destroy ();
					}
				}
			}
		}
		
		protected override void OnActivated ()
		{
			EditSelected ();
		}
		
		public override void Load () 
		{
			DataSet ds = new DataSet ();
			Ahorro.GetAllInAdtapter(Globals.Db).Fill (ds);
			LoadDataSet (ds);
			Columns [0].Visible = false;
			Populate ();
		}
	}
}
