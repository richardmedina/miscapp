
using System;
using System.IO;
using Gtk;

namespace Reportero.UI.Widgets
{
	
	
	public class ReportSettingsPanel : SettingsPanel
	{
		
		private Gtk.ComboBox _cmb_action;
		private Gtk.FileChooserButton _btn_applauncher;
		
		public ReportSettingsPanel()
		{
			Title = "Reportes";
			_cmb_action = new ComboBox (new string [] {"Ver Reporte", "Ninguna"});
			
			_cmb_action.Active = 1;
			if (AppSettings.Instance.PdfRunOnGenerated)
				_cmb_action.Active = 0;
			
			_btn_applauncher = new FileChooserButton ("Seleccionar Visor PDF", FileChooserAction.Open, string.Empty);
			_btn_applauncher.SetFilename (AppSettings.Instance.PdfAppLoader);
			_btn_applauncher.Title = AppSettings.Instance.PdfAppLoader;
			
			Gtk.HBox hbox = new Gtk.HBox (false, 5);
			
			hbox.PackStart (createLabel ("Acción al generar reporte"), false, false, 0);
			hbox.PackStart (_cmb_action, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (createLabel ("Visor de reportes PDF"), false, false, 0);
			hbox.PackStart (_btn_applauncher);
			
			PackStart (hbox, false, false, 0);
		}
		
		public override bool Save ()
		{
			if (!File.Exists (_btn_applauncher.Filename)) 
				return false;
			
			AppSettings.Instance.PdfAppLoader = _btn_applauncher.Filename;
			
			if (_cmb_action.Active == 0)
				AppSettings.Instance.PdfRunOnGenerated = true;
			else
				AppSettings.Instance.PdfRunOnGenerated = false;
		
			return base.Save ();
		}

		
		private Gtk.Label createLabel (string text) 
		{
			Gtk.Label label = new Gtk.Label (text);
			label.SetAlignment (0f, 0.5f);
			label.WidthRequest = 170;
			
			return label;
		}
	}
}
