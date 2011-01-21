
using System;
using System.Data;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class PlazoListView : DataSetView
	{

		public PlazoListView ()
		{
			AutoSelectable = true;
		}
		
		public void Load (Database db)
		{
			DataSet ds = new DataSet ();
			
			PlazoPago.GetCollectionInAdapter (Globals.Db).Fill (ds);
			
			LoadDataSet (ds);
		}
	}
}
