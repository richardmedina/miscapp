
using System;

namespace Stprm.CajaFinanciera.Data
{


	public enum OperacionFinancieraEstado
	{
		Retenido 			= 0,
		DescuentoSinCobro 	= 1,
		Descuento			= 2,
		Suspendido			= 3,
		Pagado				= 4,
		Cancelado			= 5
	}
}
