
using System;
using System.Data;

using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class EmployeeSearchView : DataSetView
	{
		public EmployeeSearchView ()
		{
			AutoSelectable = true;
		}
		
		public void Load (Database db)
		{
			DataSet ds = new DataSet ();
			
			Employee.GetCollectionInAdapter (db).Fill (ds);
			
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
