
using System;
using System.IO;
using Gtk;

namespace Reportero.UI.Widgets
{
	
	
	public class ReportSettingsPanel : SettingsPanel
	{
		
		private Gtk.ComboBox _cmb_action;
		private Gtk.FileChooserButton _btn_applauncher;
		
		private Gtk.Entry _entry_company;
		private Gtk.Entry _entry_region;
		private Gtk.Entry _entry_place;
		
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
			
			_entry_company = new Entry (AppSettings.Instance.ReportHeaderCompany);
			_entry_region = new Entry (AppSettings.Instance.ReportHeaderRegion);
			_entry_place = new Entry (AppSettings.Instance.ReportHeaderPlace);
			
			Gtk.HBox hbox = new Gtk.HBox (false, 5);
			
			hbox.PackStart (createLabel ("Acción al generar reporte"), false, false, 0);
			hbox.PackStart (_cmb_action, false, false, 0);
			
			PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 5);
			hbox.PackStart (createLabel ("Visor de reportes PDF"), false, false, 0);
			hbox.PackStart (_btn_applauncher);
			PackStart (hbox, false, false, 0);
			
			Frame frame = new Frame ("Personalizar Cabecera de Reportes");
			
			Gtk.VBox vbox = new Gtk.VBox (false, 0);
			vbox.BorderWidth = 5;
			
			hbox = new HBox (false, 0);
			hbox.PackStart (createLabel ("Empresa"), false, false, 0);
			hbox.PackStart (_entry_company);
			vbox.PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 0);
			hbox.PackStart (createLabel ("Región"), false, false, 0);
			hbox.PackStart (_entry_region);
			
			vbox.PackStart (hbox, false, false, 0);
			
			hbox = new HBox (false, 0);
			hbox.PackStart (createLabel ("Activo"), false, false, 0);
			hbox.PackStart (_entry_place);
			
			vbox.PackStart (hbox, false, false, 0);
			
			frame.Add (vbox);
			
			PackStart (frame, false, false, 0);
			
			
			
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
			
			AppSettings.Instance.ReportHeaderCompany = _entry_company.Text;
			AppSettings.Instance.ReportHeaderRegion = _entry_region.Text;
			AppSettings.Instance.ReportHeaderPlace = _entry_place.Text;
		
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
