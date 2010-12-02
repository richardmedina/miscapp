
using System;
using Gtk;

using Stprm.CajaFinanciera.UI.Widgets;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class GenerarDescuentoDialog : CustomDialog
	{
		private GeneradorDescuentosWidget _widget_gen_descs;

		public GenerarDescuentoDialog ()
		{
			Title = Globals.FormatWindowTitle ("Generador de descuentos");
			
			_widget_gen_descs = new GeneradorDescuentosWidget ();
			
			VBox.PackStart (_widget_gen_descs);
			
			VBox.ShowAll ();
			AddButton (Stock.Close, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
	}
}
