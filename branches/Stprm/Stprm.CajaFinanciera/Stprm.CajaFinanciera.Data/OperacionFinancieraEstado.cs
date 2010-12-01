
using System;

namespace Stprm.CajaFinanciera.Data
{


	public enum OperacionFinancieraEstado
	{
		Retenido 			= 1,
		DescuentoSinCobro 	= 2,
		Descuento			= 3,
		Suspendido			= 4,
		Pagado				= 5,
		Cancelado			= 6
	}
}
