
using System;
using Gtk;

using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class CajaFinancieraComboBox : Gtk.ComboBox
	{
		private static readonly string [] _titulos = {"Prestamos", "Ahorros"};
		
		public CajaFinancieraComboBox () : base (_titulos)
		{
		}
		
		public CajaFinancieraTipo GetSelected ()
		{
			CajaFinancieraTipo caja = CajaFinancieraTipo.Prestamos;
			
			for (int i = 0; i < _titulos.Length; i ++)
				if (_titulos [i] == ActiveText)
					caja = (CajaFinancieraTipo) i;
			
			return caja;
		}
		
		public void Select (CajaFinancieraTipo caja)
		{
			Active = (int) caja;	
		}
	}
}
