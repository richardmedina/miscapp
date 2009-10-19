
using System;
using Gtk;

namespace Reportero.Reports
{
	
	
	public class LoadingWindow : Gtk.Dialog
	{
		private Gtk.ProgressBar _progressbar;
		private Gtk.Label _label;
		
		public LoadingWindow ()
		{
			_label = new Gtk.Label ();
			_label.Text = "Generando reporte...";
			
			_progressbar = new ProgressBar ();
			//_progressbar.Fraction = 0.5;
			//_progressbar.Text = "Running 98 %";
			
			VBox.PackStart (_label, false, false, 0);
			VBox.PackStart (_progressbar, false, false, 0);
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
		}
		
		public string ProgressText {
			get { return _progressbar.Text; }
			set { _progressbar.Text = value; }
		}
		
		public string LabelText {
			get { return _label.Text; }
			set { _label.Text = value; }
		}	
		
		public double Fraction {
			get { return _progressbar.Fraction; }
			set { _progressbar.Fraction = value; }
		}
	}
}
