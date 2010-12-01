
using System;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class GenerarDescuentoDialog : CustomDialog
	{

		public GenerarDescuentoDialog ()
		{
			Title = Globals.FormatWindowTitle ("Generador de descuentos");
			VBox.ShowAll ();
			
			AddButton (Stock.Close, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
	}
}
