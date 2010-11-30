
using System;
using System.Data;
using Gtk;

using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class MovimientosListView : DataSetView
	{

		public MovimientosListView ()
		{
		}
		
		public void LoadMovimientos (Prestamo prestamo)
		{
			DataSet ds = new DataSet ();
			
			prestamo.GetMovimientosInAdapter ().Fill (ds);
			
			LoadDataSet (ds);
			Populate ();
			Columns [0].Visible = false;
		}
	}
}
