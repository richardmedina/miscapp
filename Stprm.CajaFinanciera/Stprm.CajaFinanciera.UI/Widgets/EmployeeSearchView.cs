
using System;
using System.Data;

using RickiLib.Widgets;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class EmployeeSearchView : DataSetView
	{
		public EmployeeSearchView ()
		{
			AutoSelectable = true;
		}
		
		public override void Load ()
		{
			DataSet ds = new DataSet ();
			
			Employee.GetCollectionForSearchingInAdapter (Globals.Db).Fill (ds);
			
			LoadDataSet (ds);
		}
		
		public override bool OnRowAdd (string[] fields)
		{
			for (int i = 0; i < fields.Length; i ++)
				if (fields [i].ToLower ().IndexOf (CurrentFilter.ToLower ()) > -1) {
					return base.OnRowAdd (fields);
				}
			
			return false;
		}
	}
}
