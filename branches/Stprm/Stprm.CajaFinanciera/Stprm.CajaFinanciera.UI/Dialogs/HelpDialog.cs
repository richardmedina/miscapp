
using System;
using Gtk;


using Stprm.CajaFinanciera.UI.Widgets;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class HelpDialog : CustomDialog
	{
		private HelpWidget _widget_help;
		
		public HelpDialog ()
		{
			_widget_help = new HelpWidget ();
			VBox.PackStart (_widget_help);
			
			VBox.ShowAll ();
			
			AddButton (Stock.Close, ResponseType.Cancel);
			SetResponseSensitive (ResponseType.Help, false);
		}
	}
}
