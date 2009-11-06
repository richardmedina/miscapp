
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Reportero.Data;

namespace Reportero.Reports
{
	
	
	public class SpeedListReport : Report
	{
		private Leadership _leadership;
		private LoadingWindow _loader;
		
		private bool _canceled = false;
		
		public SpeedListReport (DateTime start, DateTime end) : base (start, end)
		{
		}
		
		public SpeedListReport (Leadership leadership, DateTime start, DateTime end)
			: base (start, end)
		{
			Leader = leadership;
			_loader = new LoadingWindow ();
			_loader.Cancel += delegate { _canceled = true; };
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
			
			head_para.Add (new Paragraph ("PEMEX EXPLORACION Y PRODUCCION", font_title));
			head_para.Add (new Paragraph ("Región Sur", font_sub1));
			head_para.Add (new Paragraph ("Activo Integral Samaria-Luna", font_sub2));
			head_para.Add (new Paragraph ("Reporte de Excesos de Velocidad Vehicular por Día", font_sub2));
			
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
			
			Cell cell = createCell (Leader.Name);
			table.AddCell (cell, row, 0);
			
			cell = createCell (Leader.GetFullname ());
			cell.Colspan = 5;
			table.AddCell (cell, row++, 1);
			
			
			int counter = 0;
			double fraction = (double) 100 / (double) vehicles.Count;
			
			double percent = 0;
			foreach (VehicleUser vehicle in vehicles) {
				if (_canceled)
					break;
					
				double current_fraction = fraction * counter;
				percent = current_fraction;
				_loader.AsyncUpdate ((int) percent);
			
				table.AddCell (createCell ("Vehículo"), row, 0);
				cell = createCell (vehicle.VehicleId);
				cell.Colspan = 2;
				table.AddCell (cell, row, 1);
				
				table.AddCell (createCell ("Asignado a"), row, 3);
				cell = createCell (vehicle.Name);
				cell.Colspan = 2;
				table.AddCell (cell, row ++, 4);

				table.AddCell (createCell ("Detalles"), row, 0);
				cell = createCell ("");
				cell.Colspan = 5;
				table.AddCell (cell, row++, 1);
																										
				int total_times = 0;
				int totaldays = (EndingDate - StartingDate).Days;
				
				int x = 0;
				for (x = 0; x < 3 && x < totaldays + 1; x ++) {
					table.AddCell (createCell ("Fecha"), row, (x * 2));
					table.AddCell (createCell ("Excesos"), row, (x * 2) + 1);
				}
				int cells_ingnored = 0;
				double subfraction = fraction / (totaldays + 1);
				
				//current_fraction += subfraction * i;
				
				for (int i = 0; i <= totaldays; i ++) {
					percent = current_fraction + (subfraction * (i+1));
					if (_canceled)
						break;
					DateTime current_date = StartingDate.AddDays (i);
					int times = vehicle.GetTimesSpeedOvertaken (current_date);
					_loader.AsyncUpdate ((int) percent);
					
					if (times == 0) {
						cells_ingnored ++;
						continue;
					}
										
					col = (i- cells_ingnored) % 3;
					if (col == 0)
						row ++;
					
					total_times += times;
					
					cell = createCell (current_date.ToString ("dd-MM-yyyy"));
					table.AddCell (cell, row, col * 2);
					
					cell = createCell (times.ToString());
					cell.HorizontalAlignment |= Cell.ALIGN_CENTER;
					cell.SetHorizontalAlignment ("CENTER");
					cell.UseAscender = true;

					table.AddCell (cell, row, (col * 2) + 1);
					
					_loader.AsyncUpdate ((int) percent);
				}
				
				if (_canceled)
					break;
				
				row ++;
				cell = createCell (" ");
				cell.Colspan = 6;
				table.AddCell (cell, row ++, 0);
				
				double avrg = (double) (total_times) / (double) ((totaldays - cells_ingnored)+1);
				
				cell = createCell ("Cantidad de excesos del Vehiculo");
				cell.Colspan = 5;
				table.AddCell (cell, row, 0);
				cell = createCell (total_times.ToString ());
				cell.SetHorizontalAlignment ("CENTER");
				table.AddCell (cell, row ++, 5);
				
				cell = createCell ("Excesos Promedio por Día");
				cell.Colspan = 5;
				table.AddCell (cell, row, 0);
				cell = createCell (avrg.ToString ("0.00"));
				cell.SetHorizontalAlignment ("CENTER");
				table.AddCell (cell, row ++, 5);
				counter ++;
			}
			if (!_canceled)
				_loader.AsyncUpdate (100);
			doc.Add (table);
			doc.Add (new Paragraph (string.Format ("{0} Vehiculos contabilizados.", vehicles.Count), font_sub2));
			RunOnMainThread (delegate {
				_loader.Hide ();
				_loader.Destroy ();
			});
			return true;
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
