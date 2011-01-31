
using System;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class CustomDialog : RickiLib.Widgets.CustomDialog
	{

		private event EventHandler _help_request;
		
		
		public CustomDialog ()
		{
			WindowPosition = WindowPosition.CenterAlways;
			BorderWidth = 5;
			VBox.Spacing = 5;
			Resize (640, 480);
			Resizable = true;
			
			Icon = Gdk.Pixbuf.LoadFromResource ("CajaFinanciera.png");
			
			_help_request = onHelpRequest;
		}
		
		public new virtual ResponseType Run ()
		{
			ResponseType response;
			string message;
			
			do {
				response = (ResponseType) base.Run ();
				if (response == ResponseType.Help)
					OnHelpRequest ();
				
				if (response == ResponseType.Ok)
						if (!OnValidate (out message)) {
							MessageDialog msg = new MessageDialog (Globals.MainWindow,
					                                       DialogFlags.Modal, MessageType.Error,
					                                       ButtonsType.Ok, message);
							msg.Title = Globals.FormatWindowTitle ("Error");
							msg.Run ();
							msg.Destroy ();
							response = ResponseType.Help;
							continue;
						}
			}while (response == ResponseType.Help);
			
			return response;
		}
		
		protected virtual bool OnValidate (out string message)
		{
			message = string.Empty;
			return true;
		}
		
		protected virtual void OnHelpRequest ()
		{
			HelpDialog dialog = new HelpDialog();
			dialog.Run ();
			dialog.Destroy ();
			_help_request (this, EventArgs.Empty);	
		}
		
		private void onHelpRequest (object sender, EventArgs args)
		{	
		}
	}
}
