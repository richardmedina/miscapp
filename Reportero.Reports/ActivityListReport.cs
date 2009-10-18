
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Reportero.Data;

namespace Reportero.Reports
{
	
	
	public class ActivityListReport : ActivityReport
	{
		private Leadership _leadership;
		
		public ActivityListReport (Leadership leadership)
			: this (leadership, DateTime.Now, DateTime.Now)
		{
		}
		
		public ActivityListReport (Leadership leadership, DateTime start, DateTime end)
			: base (start, end)
		{
			Leader = leadership;
		}
		
		protected override void OnShown ()
		{
			createPdf ();
		}
		
		private void createPdf ()
		{
			Document doc = new Document (PageSize.LETTER);
			PdfWriter writer = PdfWriter.GetInstance (doc,
				new FileStream ("/home/richard/Desktop/file.pdf", FileMode.Create));
			doc.Open ();
			
			doc.AddAuthor ("Software Reportero desarrollado por Ricardo Medina <rmedinalor@pep.pemex.com>");
			doc.AddCreator ("Software Reportero desarrollado por Ricardo Medina <rmedinalo@pep.pemex.com>");
			
			Font font_title = FontFactory.GetFont ("Comic sans ms", "UTF8", false, 16, 0, new Color (0x44, 0x44, 0x44));
			Font font_sub1 = FontFactory.GetFont ("Arial", "UTF8", false, 14, 0, new Color (0x00, 0, 0));
			Font font_sub2 = FontFactory.GetFont ("Arial", "UTF8", false, 12, 0, new Color (0, 0x00, 0));
			Font font_sub3 = FontFactory.GetFont ("Arial", "UTF8", false, 10, 0, new Color (0, 0x00, 0));
			
			foreach (Leadership leader in Leadership.FromDatabase (Leader.Db)) {
				doc.Add (new Paragraph (leader.Name, font_sub1));
					foreach (VehicleUser vehicle in leader.GetVehicles ()) {
						Paragraph para = new Paragraph ();
						para.Add (new Phrase ("Vehículo. ", font_sub2));
						para.Add (new Phrase (vehicle.VehicleId, font_sub2));
						doc.Add (para);
						para= new Paragraph ();
						para.Add(new Phrase ("Asignado a. ", font_sub2));
						para.Add (new Phrase (vehicle.Name, font_sub2));
						doc.Add (para);
						
						doc.Add (new Paragraph ("Detalles", font_sub3));
						
						for (DateTime i = StartingDate; i <= EndingDate; i = i.AddDays (1)) {
							para = new Paragraph ();
							
							TimeSpan time = TimeSpan.FromMinutes (vehicle.GetMinutesRunning (i));
							string detail = string.Format ("     {0}. {1} ", 
								i.ToString ("dd-MM-yyyy"), time.ToString ());
								
							
							para.Add (new Phrase (detail, font_sub1));
							doc.Add (para);
							
						}
					}
			}
			
			
			//doc.Add (table);
			doc.Close ();
			writer.Close ();
		}
		
		private void createPdfTest ()
		{
			Document doc = new Document (PageSize.LETTER);
			PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("/home/richard/Desktop/Chap0109.pdf", FileMode.Create));
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
