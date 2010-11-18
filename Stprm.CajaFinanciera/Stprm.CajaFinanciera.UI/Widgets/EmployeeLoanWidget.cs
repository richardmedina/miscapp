
using System;
using Gtk;
using System.Data;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class EmployeeLoanWidget : CustomVBox
	{
		private DataSetView _view_loans;
		
		public EmployeeLoanWidget ()
		{
			_view_loans = new DataSetView ();
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			
			scroll.Add (_view_loans);
			PackStart (scroll);
		}
		
		public void UpdateFromEmployee (Employee employee)
		{
			DataSet ds = new DataSet ();
			employee.Db.QueryToAdapter ("select DATE_FORMAT(pre_fecha,'%d/%m/%Y') as Fecha, pre_folio as Folio, pre_cheque as Cheque, pre_pagare as Pagare, CONCAT('$', FORMAT(pre_capital,2)) as Capital, CONCAT('$', FORMAT(pre_interes, 2)) as Intereses, CONCAT('$', FORMAT(pre_capital + pre_interes, 2)) as Total, CONCAT('$', FORMAT(pre_abono,2)) as Abono, CONCAT('$', FORMAT(pre_saldo, 2)) as Saldo from prestamos, trabajadores where prestamos.tra_id = trabajadores.tra_id and tra_ficha='{0}'order by pre_fecha asc", employee.Id).Fill (ds);
			_view_loans.LoadDataSet (ds);
			_view_loans.Populate ();
			
			for (int i = 4; i < 9; i ++)
				_view_loans.Renders [i].Xalign = 1;
		}
	}
}
