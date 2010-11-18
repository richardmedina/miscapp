
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
	}
}
