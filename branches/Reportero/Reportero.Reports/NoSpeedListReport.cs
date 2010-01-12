
using System;
using System.IO;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reportero.Reports
{
	
	
	public class NoSpeedListReport : Report
	{
		
		public NoSpeedListReport (DateTime date1, DateTime date2) : base (date1, date2)
		{
		}
		
		protected override bool HeaderCreate (iTextSharp.text.Document document)
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
			
			HeaderFooter header = new HeaderFooter (head_para, false);
			header.Alignment = HeaderFooter.ALIGN_CENTER;
			doc.Header = header;
			
			return true;
		}

		protected override bool BodyCreate (iTextSharp.text.Document document)
		{
			//document.Open ();
			document.Add (new Paragraph ("HELLO WORLD"));
			return true;
		}

	}
}
