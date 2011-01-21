
using System;
using Gtk;
using System.Data;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class DescuentoMovimientosListView : DataSetView
	{
		private Descuento _descuento;
		
		public DescuentoMovimientosListView ()
		{
			RulesHint = true;
		}
		
		public void Load (Descuento descuento)
		{
			_descuento = descuento;
			Load ();
		}
		
		public override void Load ()
		{
			DataSet ds = new DataSet ();
			_descuento.GetMovimientos ().Fill (ds);
			
			LoadDataSet (ds);
			Populate ();
		}
	}
}
