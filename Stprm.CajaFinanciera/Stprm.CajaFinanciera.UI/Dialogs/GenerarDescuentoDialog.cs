
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
		
		public override ResponseType Run ()
		{
			ResponseType resp = ResponseType.DeleteEvent;
			
			while ((resp = base.Run ())  == ResponseType.DeleteEvent) {
				MessageDialog message = new MessageDialog (this,
				                                           DialogFlags.Modal, 
				                                           MessageType.Warning,
				                                           ButtonsType.YesNo,
				                                           "Seguro que desea cerrar la ventana y perder los cambios realizados?");
				message.Title = Globals.FormatWindowTitle ("Aviso");
				ResponseType response = (ResponseType) message.Run ();
				message.Destroy ();
				if (response == ResponseType.Yes) {
					resp = ResponseType.Cancel;
					break;
				}
			}
			
			
			return resp;
		}

	}
}
