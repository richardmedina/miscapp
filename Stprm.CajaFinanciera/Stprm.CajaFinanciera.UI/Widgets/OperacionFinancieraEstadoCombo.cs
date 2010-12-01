
using System;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class OperacionFinancieraEstadoCombo : Gtk.ComboBox
	{

		public OperacionFinancieraEstadoCombo () : base (DataMisc.OperacionFinancieraEstados)
		{
			Active = 0;
		}
		
		public OperacionFinancieraEstado Estado {
			get { return (OperacionFinancieraEstado) (Active + 1); }
			set { Active = (int) (value -1); }
		}
	}
}
