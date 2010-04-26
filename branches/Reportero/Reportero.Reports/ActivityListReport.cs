
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Reportero.Data;

//using Gtk;

namespace Reportero.Reports
{
	
	
	public class ActivityListReport : Report
	{
		private Leadership _leadership;
		private LoadingWindow _loader;
		
		private bool _canceled = false;
		
		public ActivityListReport (Leadership leadership, ReportType type)
			: this (leadership, DateTime.Now, DateTime.Now, type)
		{
		}
		
		public ActivityListReport (Leadership leadership, DateTime start, DateTime end, ReportType type)
			: base (start, end)
		{
			Leader = leadership;
			_loader = new LoadingWindow ();
			_loader.Cancel += delegate { _canceled = true; };
			ReportType = type;
		}
				
		protected override bool BodyCreate (Document document)
		{
			Document doc = document;//new Document (PageSize.LETTER);
			
			Font font_title = FontFactory.GetFont ("Comic sans ms", "UTF8", false, 16, 1, new Color (0x44, 0x44, 0x44));
			Font font_sub1 = FontFactory.GetFont ("Arial", "UTF8", false, 14, 1, new Color (0x00, 0, 0));
			Font font_sub2 = FontFactory.GetFont ("Arial", "UTF8", false, 12, 1, new Color (0, 0x00, 0));
			//Font font_sub3 = FontFactory.GetFont ("Arial", "UTF8", false, 10, 0, new Color (0, 0x00, 0));

			Assembly a = Assembly.GetExecutingAssembly ();
			Stream stream = a.GetManifestResourceStream ("reportero_icon_pep.png");
			Image img = Image.GetInstance (stream);
			img.Alignment = Image.LEFT_BORDER | Image.TEXTWRAP;
			img.ScalePercent (70);

			Paragraph head_para = new Paragraph ();
			
			head_para.Add (new Paragraph (HeaderCompany, font_title));
			head_para.Add (new Paragraph (HeaderRegion, font_sub1));
			head_para.Add (new Paragraph (HeaderPlace, font_sub2));
			string rtypestr = "Actividad";
			if (ReportType == ReportType.InactivityList)
				rtypestr = "Inactividad";
			
			head_para.Add (new Paragraph ("Reporte de " + rtypestr + " Vehicular por Día", font_sub2));
			head_para.Add (new Paragraph (string.Format ("{0} al {1}", StartingDate.ToString ("dd-MM-yyyy"), EndingDate.ToString ("dd-MM-yyyy"))));
			
			HeaderFooter header = new HeaderFooter (head_para, false);
			header.Alignment = HeaderFooter.ALIGN_CENTER;
			doc.Header = header;

			doc.Open ();
			
			doc.AddAuthor ("Software Reportero desarrollado por Ricardo Medina <rmedinalor@pep.pemex.com>");
			doc.AddCreator ("Software Reportero desarrollado por Ricardo Medina <rmedinalo@pep.pemex.com>");						
			
			int col = 0;
			int row = 0;
			
			VehicleUserCollection vehicles = Leader.GetVehicles ();

			Table table = new Table (6);
			table.Padding = 5;
			
			Cell cell = CreateCell (Leader.Name);
			table.AddCell (cell, row, 0);
			
			cell = CreateCell (Leader.GetFullname ());
			cell.Colspan = 5;
			table.AddCell (cell, row++, 1);
			
			
			int counter = 0;
			double fraction = (double) 100 / (double) vehicles.Count;
			
			foreach (VehicleUser vehicle in vehicles) {
				if (_canceled)
					break;
				
				double current_fraction = fraction * counter;
				double percent = current_fraction;
				
				_loader.AsyncUpdate ((int) percent);
			
				table.AddCell (CreateFilledCell ("Vehículo"), row, 0);
				cell = CreateFilledCell (vehicle.VehicleId);
				cell.Colspan = 2;
				table.AddCell (cell, row, 1);
				
				table.AddCell (CreateFilledCell ("Asignado a"), row, 3);
				cell = CreateFilledCell (vehicle.Name);
				cell.Colspan = 2;
				table.AddCell (cell, row ++, 4);

				table.AddCell (CreateCell ("Detalles"), row, 0);
				cell = CreateCell ("");
				cell.Colspan = 5;
				table.AddCell (cell, row++, 1);
																										
				int minutes_total = 0;
				int totaldays = (EndingDate - StartingDate).Days;
				
				int x = 0;
				for (x = 0; x < 3 && x < totaldays + 1; x ++) {
					table.AddCell (CreateCell ("Fecha"), row, (x * 2));
					table.AddCell (CreateCell (rtypestr), row, (x * 2) + 1);
				}
				
				double subfraction = fraction / (totaldays + 1);
				
				for (int i = 0; i <= totaldays; i ++) {
					percent = current_fraction + (subfraction * (i+1));
					if (_canceled)
						break;
					DateTime current_date = StartingDate.AddDays (i);
					int minutes_8hours = 8 * 60;
					int minutes = vehicle.GetMinutesRunning (current_date);
					if (ReportType == ReportType.InactivityList)
						minutes = minutes_8hours - minutes;
					
					minutes_total += minutes;

					_loader.AsyncUpdate ((int) percent);
					
					col = i % 3;
					if (col == 0)
						row ++;

					TimeSpan time = TimeSpan.FromMinutes (minutes);
					
					cell = CreateCell (current_date.ToString ("dd-MM-yyyy"));
					table.AddCell (cell, row, col * 2);
					
					cell = CreateCell (time.ToString());
					table.AddCell (cell, row, (col * 2) + 1);
				}
				row ++;
				cell = CreateCell (" ");
				cell.Colspan = 6;
				table.AddCell (cell, row ++, 0);
				
				int seconds_avrg = (minutes_total * 60) / (totaldays+1);
				
				TimeSpan total = TimeSpan.FromMinutes (minutes_total);
				
				cell = CreateCell (rtypestr + " Total del Vehiculo");
				cell.Colspan = 5;
				table.AddCell (cell, row, 0);
				table.AddCell (CreateCell (total.ToString ()), row ++, 5);
				
				cell = CreateCell (rtypestr + " Promedio por Día");
				cell.Colspan = 5;
				table.AddCell (cell, row, 0);
				table.AddCell (CreateCell (TimeSpan.FromSeconds (seconds_avrg).ToString ()), row ++, 5);
				counter ++;
			}
			
			if (!_canceled) {
				_loader.AsyncUpdate (100);
			
				doc.Add (table);
				doc.Add (new Paragraph (string.Format ("{0} Vehiculos contabilizados.", vehicles.Count), font_sub2));
			}
			
			RunOnMainThread (delegate {
				_loader.Hide ();
				_loader.Destroy ();
			});
			
			return !_canceled;
		}
		/*
		private Cell CreateCell (string format, params object [] objs)
		{
			string str = string.Format (format, objs);
			Cell cell = new Cell (str);
			cell.VerticalAlignment = Cell.ALIGN_MIDDLE;
			cell.UseAscender = true;
			return cell;
		}
		*/
		public Leadership Leader {
			get { return _leadership; }
			protected set { _leadership = value; }
		}
	}
}
