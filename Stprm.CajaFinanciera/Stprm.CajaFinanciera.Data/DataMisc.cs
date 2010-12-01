
using System;

namespace Stprm.CajaFinanciera.Data
{


	public static class DataMisc
	{
		
		private static string [] _oper_state = {
			"Retenido (RT)",
			"Descuento sin cobro (DC)",
			"Descuento (DT)",
			"Suspendido (SP)",
			"Pagado (PG)",
			"Cancelado (CA)"
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
