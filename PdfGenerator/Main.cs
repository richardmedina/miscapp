using System;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace PdfGenerator
{
	class MainClass
	{
		public static void Main(string[] args)
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
			cell.Hei
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
			
		}
	}
}