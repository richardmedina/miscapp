
using System;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class CustomDialog : RickiLib.Widgets.CustomDialog
	{

		private event EventHandler _help_request;
		
		
		public CustomDialog ()
		{
			WindowPosition = WindowPosition.CenterOnParent;
			BorderWidth = 5;
			VBox.Spacing = 5;
			Resize (640, 480);
			Resizable = true;
			
			_help_request = onHelpRequest;
		}
		
		public new virtual ResponseType Run ()
		{
			ResponseType response;
			
			do {
				response = (ResponseType) base.Run ();
				if (response == ResponseType.Help)
					OnHelpRequest ();
			}while (response == ResponseType.Help);
			
			return response;
		}
		
		protected virtual void OnHelpRequest ()
		{
			_help_request (this, EventArgs.Empty);	
		}
		
		private void onHelpRequest (object sender, EventArgs args)
		{	
		}
	}
}
