
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
		
		public ActivityListReport (Leadership leadership)
			: this (leadership, DateTime.Now, DateTime.Now)
		{
		}
		
		public ActivityListReport (Leadership leadership, DateTime start, DateTime end)
			: base (start, end)
		{
			Leader = leadership;
			_loader = new LoadingWindow ();
		}
				
		public void CreatePdf (string appfilename, string filename, bool run)
		{
			Thread thread = new Thread ((ThreadStart) delegate {
				update (0);
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
			head_para.Add (new Paragraph ("Tecnología de Información", font_sub2));
			head_para.Add (new Paragraph ("Reporte de Actividad Vehicular por Coordinación", font_sub2));
			
			HeaderFooter header = new HeaderFooter (head_para, false);
			header.Alignment = HeaderFooter.ALIGN_CENTER;
			doc.Header = header;

			doc.Open ();
			
			doc.AddAuthor ("Software Reportero desarrollado por Ricardo Medina <rmedinalor@pep.pemex.com>");
			doc.AddCreator ("Software Reportero desarrollado por Ricardo Medina <rmedinalo@pep.pemex.com>");						
			
			doc.Add (new Paragraph (Leader.Name, font_sub1));
			
			VehicleUserCollection vehicles = Leader.GetVehicles ();
			
			int counter = 0;
			foreach (VehicleUser vehicle in vehicles) {
				counter ++;
				//update ("", (100 / vehicles.Count) * counter);
				double percent = ((double) 100 / (double) vehicles.Count) * (double)counter;
				update ((int) percent);
				
				Paragraph para = new Paragraph ();						
				para.Add (new Phrase ("Vehículo. ", font_sub2));
				para.Add (new Phrase (vehicle.VehicleId, font_sub3));
				doc.Add (para);
				
				para= new Paragraph ();
				para.Add(new Phrase ("Asignado a. ", font_sub2));
				para.Add (new Phrase (vehicle.Name, font_sub3));
				
				doc.Add (para);
						
				doc.Add (new Paragraph ("Detalles", font_sub2));
						
				int minutes_total = 0;
				int totaldays = (EndingDate - StartingDate).Days;
				for (int i = 0; i <= totaldays; i ++) {
					DateTime current_date = StartingDate.AddDays (i);
					para = new Paragraph ();
					int minutes = vehicle.GetMinutesRunning (current_date);
					minutes_total += minutes;
					
					
					TimeSpan time = TimeSpan.FromMinutes (minutes);
					string detail = string.Format ("     {0}. {1} ", 
						current_date.ToString ("dd-MM-yyyy"), time.ToString ());
					
					para.Add (new Phrase (detail, font_sub3));
					doc.Add (para);
				}
				
				int seconds_avrg = (minutes_total*60)/(totaldays+1);//(totaldays>1?totaldays+1:1);
				TimeSpan total = TimeSpan.FromMinutes (minutes_total);
				
				para = new Paragraph (string.Format ("Actividad Total del Vehículo. {0}", total), font_sub2);
				doc.Add (para);
				para = new Paragraph (string.Format ("Actividad Promedio por día. {0}", TimeSpan.FromSeconds (seconds_avrg)), font_sub2);
				doc.Add (para);
				doc.Add (new Paragraph (" ", font_sub2));
			}
			doc.Add (new Paragraph (string.Format ("{0} Vehiculos contabilizados.", vehicles.Count), font_sub2));
			doc.Close ();
			writer.Close ();
			_loader.Hide ();
			_loader.Destroy ();
			if (run)
				RunPdfOnExternalApp (appfilename, filename);
		}
		
		private void update (double percent)
		{
			update (string.Format ("{0}%", percent), percent);
		}
		
		private void update (string progress_text, double percent)
		{
			Gtk.ThreadNotify notify = new Gtk.ThreadNotify (delegate {
				_loader.ShowAll ();
				_loader.ProgressText = progress_text;
				_loader.Fraction = percent / 100;
			});
			
			notify.WakeupMain ();
		}
		
		private void createPdfTest ()
		{
			Document doc = new Document (PageSize.LETTER);
			PdfWriter writer = PdfWriter.GetInstance(doc, 
				new FileStream("/home/richard/Desktop/Chap0109.pdf", FileMode.Create));
			doc.Open ();
			
			Table table = new Table (2);
			//table.Cellpadding = 3;
			//table.Spacing = 3;
			
			Cell cell = new Cell (new Phrase ("Hello"));
			//table.Alignment = Element.ALIGN_MIDDLE & Element.ALIGN_CENTER;
			cell.VerticalAlignment = Element.ALIGN_MIDDLE;
			cell.HorizontalAlignment = Element.ALIGN_CENTER;
			//cell.Hei
			//cell.UseAscender = true;
			
			table.AddCell (cell);
			
			
			Phrase phrase = new Phrase ("Ricardo Medina López\n");
			phrase.Add (new Phrase ("ricki@"));
			HeaderFooter head = new HeaderFooter (phrase, true);
			doc.Header = head;
			
			
			for (int chapter = 0; chapter < 10; chapter ++) {
				Chapter chap = new ChapterAutoNumber (string.Format ("Index"));
				for (int i = 0; i < 100; i ++)
					chap.Add (new Paragraph ("Hello World, how are you?\n"));
				doc.Add (chap);
				doc.Add (table);
			}
			
			doc.Close ();
			writer.Close ();		
		}
		
		public Leadership Leader {
			get { return _leadership; }
			protected set { _leadership = value; }
		}
	}
}
