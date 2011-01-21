
using System;
using System.Data;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;
namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class BuscarPrestamoListView : DataSetView
	{
		
		public BuscarPrestamoListView ()
		{
			RulesHint = true;
		}
		
		public bool GetSelectedPrestamo (out Prestamo prestamo)
		{
			string [] fields;
			prestamo = new Prestamo (Globals.Db);
			
			if (GetSelected (out fields)) {
				int id;
				
				if (int.TryParse (fields [0], out id)) {
					prestamo.Id = id;
					if (prestamo.Update ()) {
						return true;
					}
				}
			}
			
			return false;
		}
		
		public override void Load ()
		{
			DataSet ds = new DataSet ();
			
			Prestamo.GetInAdapter (Globals.Db, false).Fill (ds);
			
			LoadDataSet (ds);
			
			foreach (TreeViewColumn col in Columns)
				col.Visible = true;
			
			Populate ();
		}

	}
}
