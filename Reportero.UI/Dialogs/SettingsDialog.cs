
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
			_tabulator.Panels.Add (new ReportSettingsPanel ());
			
			_tabulator.ReloadPanels ();
			
			VBox.PackStart (_tabulator);
			
			VBox.ShowAll ();
			
			AddButton (Stock.Apply, ResponseType.Apply);
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		public override ResponseType Run ()
		{
			bool error_saving = false;
			ResponseType response;
			
			do {
				response = base.Run ();
				error_saving = false;
				
				if (response == ResponseType.Ok)
					error_saving = !OnApply ();
				
			} while (response == ResponseType.Ok && error_saving);
			
			return response;
		}
		
		protected override bool OnApply ()
		{
			base.OnApply ();
			
			foreach (SettingsPanel panel in Tabulator)
				if (!panel.Save ()) {
					MessageDialog dialog = new MessageDialog (this,
						DialogFlags.Modal,
						MessageType.Error,
						ButtonsType.Ok,
						"Por favor verifique que los datos sean correctos en el apartado:\n\t<b>{0}</b>",
						panel.Title);
						
					dialog.Run ();
					dialog.Destroy ();
					return false;
				}
			
			AppSettings.Instance.Serialize (AppSettings.Filename);
			return true;
		}

		public PanelTabulator Tabulator {
			get { return _tabulator; }
		}
	}
}
