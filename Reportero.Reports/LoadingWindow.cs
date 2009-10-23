
using System;
using Gtk;

namespace Reportero.Reports
{
	
	
	public class LoadingWindow : Gtk.Dialog
	{
		private Gtk.ProgressBar _progressbar;
		private Gtk.Label _label;
		
		private Gtk.Button _btn_cancel;
		
		private bool _canceled = false;
		
		private event EventHandler _cancel;
		
		public LoadingWindow ()
		{
			Modal = true;
			
			_label = new Gtk.Label ();
			_label.Text = "Generando reporte...";
			
			_progressbar = new ProgressBar ();
			
			_cancel = onCancel;
			
			VBox.PackStart (_label, false, false, 0);
			VBox.PackStart (_progressbar, false, false, 0);
			
			_btn_cancel = (Gtk.Button) AddButton (Stock.Cancel, ResponseType.Cancel);
			_btn_cancel.Clicked += delegate {
				OnCancel ();
			};
		}
		
		public void AsyncUpdate (double percent)
		{
			AsyncUpdate (string.Format ("{0}%", percent), percent);
		}
		
		public void AsyncUpdate (string progress_text, double percent)
		{
			Gtk.ThreadNotify notify = new Gtk.ThreadNotify (delegate {
				Update (progress_text, percent);
			});
			
			notify.WakeupMain ();
		}
		
		public void Update (string progress_text, double percent)
		{
			ShowAll ();
			ProgressText = progress_text;
			Fraction = percent / 100;
		}
		
		public void Update (double percent)
		{
			Update (string.Format ("{0}%", percent), percent);
		}
		
		protected virtual void OnCancel ()
		{
			_cancel (this, EventArgs.Empty);
		}
		
		protected override void OnRealized ()
		{
			base.OnRealized ();
			GdkWindow.Functions = Gdk.WMFunction.Move |
				Gdk.WMFunction.Resize;
		}
		
		protected override void OnResponse (Gtk.ResponseType response_id)
		{
			OnCancel ();
			base.OnResponse (response_id);
		}

		private void onCancel (object sender, EventArgs args)
		{
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
		
		public bool Canceled {
			get { return _canceled; }
			set { _canceled = value; }
		}
		
		public event EventHandler Cancel {
			add { _cancel += value; }
			remove { _cancel -= value; }
		}
		
		public Gtk.Button CancelButton {
			get { return _btn_cancel; }
		}
	}
}
