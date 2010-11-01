
using System;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class LoanListView : DataSetView
	{
		
		public LoanListView ()
		{
			RulesHint = true;
		}
		
		public new void Populate ()
		{
			base.Populate ();
			
			for (int i = 6; i < 11; i ++)
				Renders [i].Xalign = 1;
		}
	}
}
