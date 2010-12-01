
using System;
using Gtk;
using RickiLib.Widgets;
using Stprm.CajaFinanciera.UI.Widgets;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class AhorroDialog : CustomDialog
	{
		private Notebook _notebook;
		private AhorroDetallesWidget _adw_detalles;
		
		public AhorroDialog ()
		{
			Title = Globals.FormatWindowTitle ("Ahorro");
			_notebook = new Notebook ();
			_adw_detalles = new AhorroDetallesWidget ();
			
			_notebook.AppendPage (_adw_detalles, Factory.Label ("Detalles"));
			
			VBox.PackStart (_notebook);
			
			VBox.ShowAll ();
			
			AddButton (Stock.Close, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
	}
}
