
using System;
using Gtk;
using Reportero.UI.Dialogs;

namespace Reportero.UI.Widgets
{
	
	
	public class DateEntry : Gtk.HBox 
	{
		private Label _label;
		private Gtk.Entry _entry;
		private Gtk.Button _button;
		
		private DateTime _date = DateTime.Now;
		
		private DateTime _date_min;
		private DateTime _date_max;
		
		public DateEntry (string text) : this (text, DateTime.Now)
		{
		}
		
		public DateEntry (string text, DateTime date)
		{
			_label = new Label (text);
			_entry = new Entry ();
			_entry.IsEditable = false;
			
			_button = new Button ();
			_button.Image = new Image (Gdk.Pixbuf.LoadFromResource ("reportero_icon_calendar.png"));
			_button.Relief = ReliefStyle.None;
			_button.Clicked += buttonClicked;
			
			MinimunDate = new DateTime (_date.Year, _date.Month, _date.Day);
			MaximunDate = new DateTime (DateTime.MaxValue.Year, DateTime.MaxValue.Month, DateTime.MaxValue.Day);
			
			Date = date;
			
			PackStart (_label, false, false, 0);
			PackStart (_entry);
			PackStart (_button, false, false, 0);
		}
		
		private void buttonClicked (object sender, EventArgs args)
		{
			DateSelectionDialog dialog = new DateSelectionDialog (Date);
			ResponseType response;
			response = dialog.Run ();
			
			DateTime date = dialog.Date;
			dialog.Destroy ();
			
			if (response == ResponseType.Ok)
				Date = date;
			
		}
		
		public Gtk.Label Label {
			get { return _label; }
		}
		
		public Gtk.Entry Entry {
			get { return _entry; }
		}
		
		public Gtk.Button Button {
			get { return _button; }
		}
		
		public DateTime Date {
			get { return _date; }
			set { 
				_date = value; 
				_entry.Text = _date.ToString ("dd.MM.yyyy");
			}
		}

		public DateTime MinimunDate {
			get { return _date_min; }
			set { _date_min = value; }
		}
		
		public DateTime MaximunDate {
			get { return _date_max; }
			set { _date_max = value; }
		}
	}
}
