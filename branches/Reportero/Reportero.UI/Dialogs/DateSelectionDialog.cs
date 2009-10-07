
using System;
using Gtk;
using Reportero.UI.Dialogs;

namespace Reportero.UI.Dialogs
{
	
	
	public class DateSelectionDialog : CustomDialog
	{
		private Gtk.Calendar _calendar;
		
		public DateSelectionDialog () :
			this (DateTime.Now)
		{
		}
		
		public DateSelectionDialog (DateTime date)
		{
			_calendar = new Calendar ();
			_calendar.DaySelectedDoubleClick += calendarDaySelectedDoubleClick;
			
			VBox.PackStart (_calendar);
			VBox.ShowAll ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		private void calendarDaySelectedDoubleClick (object sender, EventArgs args)
		{
			Respond (ResponseType.Ok);
		}
		
		public Calendar Calendar {
			get { return _calendar; }
		}
		
		public DateTime Date {
			get { return _calendar.Date; }
		}		
	}
}
