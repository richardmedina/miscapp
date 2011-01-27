
using System;
using System.IO;
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
		
		public void Export (string filename) 
		{
			try {
				using (StreamWriter sw = new StreamWriter (filename)) {
				
				}
			} catch (Exception exception) {
				Console.WriteLine ("Exception : {1}", exception.Message);	
			}
		}
		
		public override void Load ()
		{
			DataSet ds = new DataSet ();
			_descuento.GetMovimientosInAdapter ().Fill (ds);
			
			LoadDataSet (ds);
			Populate ();
		}
	}
}
