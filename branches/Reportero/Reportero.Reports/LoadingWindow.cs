
using System;
using Gtk;

namespace Reportero.Reports
{
	
	
	public class LoadingWindow : Gtk.Dialog
	{
		private Gtk.ProgressBar _progressbar;
		private Gtk.Label _label;
		
		private bool _canceled = false;
		
		public LoadingWindow ()
		{
			Modal = true;
			
			_label = new Gtk.Label ();
			_label.Text = "Generando reporte...";
			
			_progressbar = new ProgressBar ();
			
			VBox.PackStart (_label, false, false, 0);
			VBox.PackStart (_progressbar, false, false, 0);
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
		}
		
		protected override void OnResponse (Gtk.ResponseType response_id)
		{
			base.OnResponse (response_id);
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
		
		public bool canceled {
			get { return _canceled; }
			set { _canceled = value; }
		}
	}
}
