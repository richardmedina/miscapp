
using System;
using Gtk;

using Reportero.UI.Widgets;
namespace Reportero.UI.Dialogs
{
	
	
	public class SettingsDialog : CustomDialog
	{
		
		private PanelTabulator _tabulator;
		
		public SettingsDialog ()
		{
			Title = AppSettings.Instance.GetFormatedTitle ("Configuración");
			
			_tabulator = new PanelTabulator (new PanelCollection ());
			_tabulator.Panels.Add (new NetworkSettingsPanel ());
			_tabulator.Panels.Add (new AppearanceSettingsPanel ());
			_tabulator.Panels.Add (new PlottingSettingsPanel ());
			
			_tabulator.ReloadPanels ();
			
			VBox.PackStart (_tabulator);
			
			VBox.ShowAll ();
			
			AddButton (Stock.Apply, ResponseType.Apply);
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
	}
}
