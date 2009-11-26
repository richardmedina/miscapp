
using System;
using Gdk;
using Cairo;
using Pango;

using Reportero.Reports.Drawing;

namespace Reportero.Reports
{
	
	
	public class ActivityReportBar : Bar
	{
		private DateTime _date;
		private int _moving_time;
		
		//private int _minutes;
		private string _bartext;
		
		private static double _rootx = 50;
		private static double _rooty = 510;
		private static double _topx = 110;
		private static double _topy = 48;
		
		// Default font sizes
		private int _datefs = 8;
		private int _infofs = 8;
		
		private Cairo.Color _layout_foreground = new Cairo.Color (0.2, 0.5, 0.2);
		
		private string [] dayofweek = {"Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"};
		
		public ActivityReportBar (int position, DateTime date, int moving_time, string text) :
			base (_topx + ((_rootx + 10) * (position)), _topy, _rootx, _rooty)
		{
			_date = date;
			_moving_time = moving_time;
			_bartext = text;
			
			double pixels = (_moving_time * 1.1);
			
			Y = Height - pixels; 
			Height -= (Height - pixels);
		}
		
		public override void Paint (Reportero.Reports.Drawing.CanvasPaintEventArgs args)
		{
			base.Paint (args);
			using (Cairo.Context context = Gdk.CairoHelper.Create (args.Pixmap)) {
				DrawingMisc.ShowLayout (
					context,
					LayoutForeground,
					FontDescription.FromString (DateFontSize.ToString ()),
					X + 10, Y - 15, 
					270,
					_bartext);
					//"Actividad\n{0:00}:{1:00} hrs", 
					//MovingTime.Hours, MovingTime.Minutes);
				
				DrawingMisc.ShowLayout (context,
					LayoutForeground,
					FontDescription.FromString (DateFontSize.ToString ()),
					(double) (X + (Width/2)), (double) Y + Height, 30,
					"{0}\n{1}",
					dayofweek [(int)Date.DayOfWeek], 
					Date.ToString ("dd-MM-yy"));
			}
		}
		
		public DateTime Date {
			get { return _date; }
		}
		/*
		public TimeSpan MovingTime {
			get { return _moving_time; }
		}
		*/
		public Cairo.Color LayoutForeground {
			get { return _layout_foreground; }
			set { _layout_foreground = value; }
		}
		
		public int DateFontSize {
			get { return _datefs; }
			set { _datefs = value; }
		}
		
		public int InfoFontsize {
			get { return _infofs; }
			set { _infofs = value; }
		}
	}
}
