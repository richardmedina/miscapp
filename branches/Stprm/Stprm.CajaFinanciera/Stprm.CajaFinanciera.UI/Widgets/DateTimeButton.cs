
using System;
using Gtk;

using RickiLib.Widgets;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class DateTimeButton : Gtk.Button
	{
		
		private DateTime _datetime;
		
		private readonly string [] _months = {
			"Enero",
			"Febrero",
			"Marzo",
			"Abril",
			"Mayo",
			"Junio",
			"Julio",
			"Agosto",
			"Septiembre",
			"Octubre",
			"Noviembre",
			"Diciembre"
		};
		
		public DateTimeButton () : this (DateTime.MinValue)
		{
			Label = "...";
			WidthRequest = 200;
		}
		
		public DateTimeButton (DateTime datetime)
		{
			Date = datetime;
			Relief = ReliefStyle.None;
			WidthRequest = 200;
		}
		
		protected override void OnClicked ()
		{
			SelectDateDialog dialog = new SelectDateDialog ();
			dialog.Date = DateTime.Now;
			
			ResponseType response = (ResponseType) dialog.Run ();
			DateTime date = dialog.Date;
			dialog.Destroy ();
			
			if (response == ResponseType.Ok) {
				Date = date;
			}
		}
		
		public DateTime Date {	
			get { return _datetime; }	
			set { 
				_datetime = value;
				Label = string.Format ("{0} de {1} de {2}",
				                       Date.Day, _months [Date.Month -1], Date.Year);
			}
		}
	}
}
