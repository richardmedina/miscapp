
using System;
using Gtk;


namespace Reportero.UI.Widgets
{
	
	
	public class PanelTabulator : Gtk.Notebook
	{
		
		private PanelCollection _panels;
		
		public PanelTabulator (PanelCollection panels)
		{
			_panels = panels;
		}
		
		public void ReloadPanels ()
		{
			foreach (SettingsPanel panel in Panels)
				AppendPanel (panel);
		}
		
		public void AppendPanel (SettingsPanel panel)
		{
			AppendPage (panel, new Gtk.Label (panel.Title));
		}
		
		public PanelCollection Panels {
			get { return _panels; }
		}
	}
}
