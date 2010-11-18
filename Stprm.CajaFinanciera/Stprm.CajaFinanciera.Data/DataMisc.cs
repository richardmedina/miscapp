
using System;

namespace Stprm.CajaFinanciera.Data
{


	public static class DataMisc
	{
		
		private static string [] _oper_state = {
			"Retenido",
			"Descuento sin cobro",
			"Descuento",
			"Suspendido",
			"Pagado",
			"Cancelado"
		};
		
		public static string OperacionFinancieraEstadoToString (OperacionFinancieraEstado estado)
		{
			return _oper_state [(int) estado];
		}
		
		public static string [] OperacionFinancieraEstados {
			get { return _oper_state; }
		}
	}
}
