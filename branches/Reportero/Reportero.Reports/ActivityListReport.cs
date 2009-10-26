
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
	
	
	public class ActivityListReport : ActivityReport
	{
		private Leadership _leadership;
		private LoadingWindow _loader;
		
		private bool _canceled = false;
		
		public ActivityListReport (Leadership leadership)
			: this (leadership, DateTime.Now, DateTime.Now)
		{
		}
		
		public ActivityListReport (Leadership leadership, DateTime start, DateTime end)
			: base (start, end)
		{
			Leader = leadership;
			_loader = new LoadingWindow ();
			_loader.Cancel += delegate { _canceled = true; };
		}
				
		public void CreatePdf (string appfilename, string filename, bool run)
		{
			Thread thread = new Thread ((ThreadStart) delegate {
				_loader.AsyncUpdate (0);
				createPdf (appfilename, filename, run);
			});
			
			thread.Start ();
		}
		
		public void RunPdfOnExternalApp (string appfilename, string filename)
		{
			Process process = new Process ();
			process.StartInfo.FileName = appfilename;
			process.StartInfo.Arguments = string.Format ("\"{0}\"", filename);
			process.Start ();
		}
		
		private void createPdf (string appfilename, string filename, bool run) 
		{
			Document doc = new Document (PageSize.LETTER);

			PdfWriter writer = PdfWriter.GetInstance (doc,
				new FileStream (filename, FileMode.Create));
			
			Font font_title = FontFactory.GetFont ("Comic sans ms", "UTF8", false, 16, 1, new Color (0x44, 0x44, 0x44));
			Font font_sub1 = FontFactory.GetFont ("Arial", "UTF8", false, 14, 1, new Color (0x00, 0, 0));
			Font font_sub2 = FontFactory.GetFont ("Arial", "UTF8", false, 12, 1, new Color (0, 0x00, 0));
			Font font_sub3 = FontFactory.GetFont ("Arial", "UTF8", false, 10, 0, new Color (0, 0x00, 0));

			Assembly a = Assembly.GetExecutingAssembly ();
			Stream stream = a.GetManifestResourceStream ("reportero_icon_pep.png");
			Image img = Image.GetInstance (stream);
			img.Alignment = Image.LEFT_BORDER | Image.TEXTWRAP;
			img.ScalePercent (70);

			Paragraph head_para = new Paragraph ();
			
			head_para.Add (new Paragraph ("PEMEX EXPLORACION Y PRODUCCION", font_title));
			head_para.Add (new Paragraph ("Región Sur", font_sub1));
			head_para.Add (new Paragraph ("Activo Integral Samaria-Luna", font_sub2));
			//head_para.Add (new Paragraph ("Tecnología de Información", font_sub2));
			head_para.Add (new Paragraph ("Reporte de Actividad Vehicular por Día", font_sub2));
			
			HeaderFooter header = new HeaderFooter (head_para, false);
			header.Alignment = HeaderFooter.ALIGN_CENTER;
			doc.Header = header;

			doc.Open ();
			
			doc.AddAuthor ("Software Reportero desarrollado por Ricardo Medina <rmedinalor@pep.pemex.com>");
			doc.AddCreator ("Software Reportero desarrollado por Ricardo Medina <rmedinalo@pep.pemex.com>");						
			//Paragraph para = new Paragraph (
			//	string.Format ("{0}. {1}", Leader.Name, Leader.GetFullname ()), font_sub1);
			//doc.Add (para);
			
			VehicleUserCollection vehicles = Leader.GetVehicles ();
			
			int counter = 0;
			foreach (VehicleUser vehicle in vehicles) {
				if (_canceled)
					break;
				counter ++;
				double percent = ((double) 100 / (double) vehicles.Count) * (double) counter;
				_loader.AsyncUpdate ((int) percent);

				Table table = new Table (6);
				table.Padding = 5;
			
				Cell cell = createCell (Leader.Name);
				table.AddCell (cell, 0, 0);
			
				cell = createCell (Leader.GetFullname ());
				cell.Colspan = 5;
				table.AddCell (cell, 0, 1);
			
				table.AddCell (createCell ("Vehículo"), 1, 0);
				cell = createCell (vehicle.VehicleId);
				cell.Colspan = 2;
				table.AddCell (cell, 1, 1);
				
				table.AddCell (createCell ("Asignado a"), 1, 3);
				cell = createCell (vehicle.Name);
				cell.Colspan = 2;
				table.AddCell (cell, 1, 4);

				table.AddCell (createCell ("Detalles"), 2, 0);
				cell = createCell ("");
				cell.Colspan = 5;
				table.AddCell (cell);
																										
				int minutes_total = 0;
				int totaldays = (EndingDate - StartingDate).Days;
				
				int x = 0;
				for (x = 0; x < 3 && x < totaldays+1; x ++) {
					table.AddCell (createCell ("Fecha"), 3, (x*2));
					table.AddCell (createCell ("Actividad"), 3, (x*2) + 1);
				}
				
				int row = 0;
				
				for (int i = 0; i <= totaldays; i ++) {
					if (_canceled)
						break;
						
					int col = i % 3;
					if (col == 0)
						row ++;
						
					DateTime current_date = StartingDate.AddDays (i);
					
					int minutes = vehicle.GetMinutesRunning (current_date);
					minutes_total += minutes;
					
					TimeSpan time = TimeSpan.FromMinutes (minutes);
					string detail = string.Format ("     {0}. {1} ", 
						current_date.ToString ("dd-MM-yyyy"), time.ToString ());
					
					cell = createCell (current_date.ToString ("dd-MM-yyyy"));
					table.AddCell (cell, row + 3, col * 2);
					
					cell = createCell (time.ToString());
					table.AddCell (cell, row + 3, (col * 2) + 1);
				}
				row += 4;
				cell = createCell (" ");
				cell.Colspan = 6;
				table.AddCell (cell);
				
				int seconds_avrg = (minutes_total * 60) / (totaldays+1);
				TimeSpan total = TimeSpan.FromMinutes (minutes_total);
				
				cell = createCell ("Actividad Total del Vehiculo");
				cell.Colspan = 5;
				table.AddCell (cell, row, 0);
				table.AddCell (createCell (total.ToString ()), row ++, 5);
				
				cell = createCell ("Actividad Promedio por Día");
				cell.Colspan = 5;
				table.AddCell (cell, row, 0);
				table.AddCell (createCell (TimeSpan.FromSeconds (seconds_avrg).ToString ()), row ++, 5);
				
				doc.Add (table);
			}
			doc.Add (new Paragraph (string.Format ("{0} Vehiculos contabilizados.", vehicles.Count), font_sub2));
			
			doc.Close ();
			writer.Close ();
			
			_loader.Hide ();
			_loader.Destroy ();
			if (run && !_canceled)
				RunPdfOnExternalApp (appfilename, filename);
		}
		
		private Cell createCell (string format, params object [] objs)
		{
			string str = string.Format (format, objs);
			Cell cell = new Cell (str);
			cell.VerticalAlignment = Cell.ALIGN_MIDDLE;
			cell.UseAscender = true;
			return cell;
		}
		
		public Leadership Leader {
			get { return _leadership; }
			protected set { _leadership = value; }
		}
	}
}
