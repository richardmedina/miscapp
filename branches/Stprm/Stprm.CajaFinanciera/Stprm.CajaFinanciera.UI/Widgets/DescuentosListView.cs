
using System;
using System.Data;
using Gtk;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class DescuentosListView : DataSetView
	{
		
		public DescuentosListView ()
		{
		}
		
		public void Load (Cobro cobro)
		{
			DataSet ds = new DataSet();
			
			cobro.GetDescuentosInAdapter ().Fill (ds);
			
			LoadDataSet (ds);
			Populate ();
		}
	}
}
