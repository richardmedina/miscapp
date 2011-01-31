
using System;
using System.Data;
using Gtk;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class PrestamoMovimientosListView : DataSetView
	{		
		
		public PrestamoMovimientosListView ()
		{
		}
		
		public void LoadMovimientos (Prestamo prestamo)
		{
			DataSet ds = new DataSet ();
			
			prestamo.GetPrestamoMovimientosInAdapter ().Fill (ds);
			
			LoadDataSet (ds);
			Populate ();
			Columns [0].Visible = false;
		}
	}
}
