
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
		
		private double _current_loader_progress = 0;
		private double _progress_unit = 0;
		private int _current_vehicle = 0;
		private int _vehicle_totals = 0;
		
		private bool update_loader (int current, int max)
		{
			double current_value = _current_loader_progress + ((_progress_unit / max) * current);
				
			_loader.AsyncUpdate (string.Format ("{0}%. ({1}/{2})", (int) current_value, _current_vehicle, _vehicle_totals), (int) current_value);
			return !_canceled;
		}
		
		protected override bool BodyCreate (Document document)
		{ 
			Document doc = document;
			
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
			head_para.Add (new Paragraph (string.Format ("{0} al {1}", StartingDate.ToString ("dd-MM-yyyy"), EndingDate.ToString ("dd-MM-yyyy"))));
			
			HeaderFooter header = new HeaderFooter (head_para, false);
			header.Alignment = HeaderFooter.ALIGN_CENTER;
			doc.Header = header;

			doc.Open ();
			
			doc.AddAuthor ("Software Reportero desarrollado por Ricardo Medina <rmedinalor@pep.pemex.com>");
			doc.AddCreator ("Software Reportero desarrollado por Ricardo Medina <rmedinalo@pep.pemex.com>");						
			
			
			VehicleUserCollection vehicles = Leader.GetVehicles ();
			
			SpeedExceedCollection [] collections = new SpeedExceedCollection[vehicles.Count];
			int index = 0;
			
			_progress_unit = (double) 100 / (double) vehicles.Count;
			_vehicle_totals = vehicles.Count;
			
			foreach (VehicleUser vehicle in vehicles) {
				_current_vehicle = index;	
				_current_loader_progress = _progress_unit * index;
				collections [index ++] = vehicle.GetSpeedOvertakenFromRange (StartingDate, EndingDate, update_loader);
				
				// Implement the logic of your chart
				
			}
			
			int row = 0;
			
			Table table = new Table (6);
			table.Padding = 5;
			
			Cell cell = CreateCell (Leader.Name);
			table.AddCell (cell, row, 0);
			
			cell = CreateCell (Leader.GetFullname ());
			cell.Colspan = 5;
			table.AddCell (cell, row++, 1);

			foreach  (SpeedExceedCollection exceeds in collections) {
				int total_times = 0;
				foreach (SpeedExceedItem item in exceeds)
					total_times += item.Times;
				
				//if (exceeds.Count == 0)
				//	continue;
				
				
					table.AddCell (CreateFilledCell ("Vehículo"), row, 0);
					cell = CreateFilledCell (exceeds.Vehicle.VehicleId);
					cell.Colspan = 2;
					table.AddCell (cell, row, 1);
				
					table.AddCell (CreateFilledCell ("Asignado a"), row, 3);
					cell = CreateFilledCell (exceeds.Vehicle.Name);
					cell.Colspan = 2;
					table.AddCell (cell, row ++, 4);

					table.AddCell (CreateCell ("Detalles"), row, 0);
				if (total_times > 0) {
					cell = CreateCell ("");
					cell.Colspan = 5;
					table.AddCell (cell, row ++, 1);
					
					for (int i = 0; i < exceeds.Count && total_times > 0; i ++) {
						if (i == 3)
							break;
						table.AddCell (CreateCell ("Fecha"), row, (i * 2));
						table.AddCell (CreateCell ("Excesos"), row, (i * 2) + 1);
					}
					int novalid = 0;
					int total_exceeds = 0;
					for (int i = 0; i < exceeds.Count; i ++) {
						if (exceeds [i].Times == 0) {
							novalid ++;
							continue;
						}
						total_exceeds += exceeds [i].Times;
						int mod = (i - novalid) % 3;
						if (mod == 0) {
							row ++;
						}
					
						table.AddCell (CreateCell (exceeds [i].Date.ToString ("dd-MM-yyyy")), row, (mod * 2));
						table.AddCell (CreateCell (exceeds [i].Times.ToString ()), row, (mod * 2) + 1);
					}
					
					row ++;
				} else {
						cell = CreateCell ("No se encontró registro de exceso de velocidad para este usuario"); 
						cell.Colspan = 5;
						table.AddCell (cell, row ++, 1);
				}
			}
			
			doc.Add (table);
			doc.Add (new Paragraph (string.Format ("{0} Vehiculos Contabilizados", vehicles.Count)));
	
			RunOnMainThread (delegate {
				_loader.Hide ();
				_loader.Destroy ();
			});
			
			return !_canceled;
		}
		
		public Leadership Leader {
			get { return _leadership; }
			protected set { _leadership = value; }
		}
	}

}
