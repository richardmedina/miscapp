
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Reflection;
using Reportero.Reports.Drawing;
using Reportero.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reportero.Reports
{
	
	
	public class Report : Canvas
	{
		private DateTime _date_starting;
		private DateTime _date_ending;
		
		private ReportType _report_type;
		
		public static string HeaderCompany;
		public static string HeaderRegion;
		public static string HeaderPlace;
		
		private Font _font_title = FontFactory.GetFont ("Comic sans ms", "UTF8", false, 16, 1, new Color (0x44, 0x44, 0x44));
		private Font _font_sub1 = FontFactory.GetFont ("Arial", "UTF8", false, 14, 1, new Color (0x00, 0, 0));
		private Font _font_sub2 = FontFactory.GetFont ("Arial", "UTF8", false, 12, 1, new Color (0, 0x00, 0));
		private Font _font_sub3 = FontFactory.GetFont ("Arial", "UTF8", false, 10, 0, new Color (0, 0x00, 0));

		
		public Report (DateTime start, DateTime end)
		{
			StartingDate = start;
			EndingDate = end;
		}
		
		public void RunPdfOnExternalApp (string appfilename, string filename)
		{
			Process process = new Process ();
			process.StartInfo.FileName = appfilename;
			process.StartInfo.Arguments = string.Format ("\"{0}\"", filename);
			process.Start ();
		}
		
		public void CreatePdf (string appfilename, string filename, bool run)
		{
			Thread thread = new Thread ((ThreadStart) delegate {
				//_loader.AsyncUpdate (0);
				AsyncCreatePdf (appfilename, filename, run);
			});
			
			thread.Start ();
		}
		
		
		
		protected virtual void AsyncCreatePdf (string appfilename, string filename, bool run) 
		{
			Document document = new Document (PageSize.LETTER);

			PdfWriter writer = PdfWriter.GetInstance (document,
					new FileStream (filename, FileMode.Create));
			
			bool cancel = false;
			// FIXME. this might return on any failed validation?
			if (!HeaderCreate (document))
				cancel = true;
			// TODO. If empty will thrown an Exception and the applicaction will falls..
			else {
				document.Open ();
				if (!BodyCreate (document)) {
					cancel = true;
				} else if (!FooterCreate (document))
				cancel = true;
			}
			
			if (cancel) {
				// FIXME. Permissions must be retreived before do that..
				if (System.IO.File.Exists (filename))
					System.IO.File.Delete (filename);
				return;
			} else {
				document.Close ();
				writer.Close ();
			}
			
			if (run)
				RunPdfOnExternalApp (appfilename, filename);
		}
		
		protected virtual bool BodyCreate (Document document)
		{
			return true;
		}
		
		protected virtual bool HeaderCreate (Document document)
		{
			Document doc = document;
			/*
			Assembly a = Assembly.GetExecutingAssembly ();
			Stream stream = a.GetManifestResourceStream ("reportero_icon_pickup.png");
			Image img = Image.GetInstance (stream);
			img.Alignment = Image.LEFT_BORDER | Image.TEXTWRAP;
			img.ScalePercent (70);
			*/

			Paragraph head_para = new Paragraph ();
			
			head_para.Add (new Paragraph (HeaderCompany, FontTitle));
			head_para.Add (new Paragraph (HeaderRegion, FontSub1));
			head_para.Add (new Paragraph (HeaderPlace, FontSub2));
			head_para.Add (new Paragraph ("Reporte de Historia de Excesos de Velocidad Vehicular por Día", FontSub2));
			head_para.Add (new Paragraph ("(Días limpios de excesos de velocidad incluídos)", FontSub2));
			head_para.Add (new Paragraph (string.Format ("{0} al {1}", StartingDate.ToString ("dd-MM-yyyy"), EndingDate.ToString ("dd-MM-yyyy")), FontSub3));
			HeaderFooter header = new HeaderFooter (head_para, false);
			header.Alignment = HeaderFooter.ALIGN_CENTER;
			doc.Header = header;
			
			return true;
		}
		
		protected virtual bool FooterCreate (Document document)
		{
			return true;
		}
		
		protected Cell CreateCell (string format, params object [] objs)
		{
			string str = string.Format (format, objs);
			Cell cell = new Cell (str);
			cell.VerticalAlignment = Cell.ALIGN_MIDDLE;
			cell.UseAscender = true;
			return cell;
		}
		
		protected Cell CreateFilledCell (string format, params object [] objs)
		{
			Cell cell = CreateCell (format, objs);
			cell.BackgroundColor = new Color (0xCC, 0xCC, 0xCC);
			return cell;
		}
		
		protected void RunOnMainThread (Gtk.ReadyEvent callback)
		{
			Gtk.ThreadNotify notify = new Gtk.ThreadNotify (callback);
			notify.WakeupMain ();
		}
		
		public DateTime StartingDate {
			get { return _date_starting; }
			protected set { _date_starting = value; }
		}
		
		public DateTime EndingDate {
			get { return _date_ending; }
			protected set { _date_ending = value; }
		}

		public ReportType ReportType {
			get { return _report_type; }
			set { _report_type = value; }
		}
		
		protected Font FontTitle {
			get { return  _font_title; }
		}
		
		protected Font FontSub1  {
			get { return _font_sub1; }
		}
		
		protected Font FontSub2 {
			get { return _font_sub2; }
		}
		
		protected Font FontSub3 {
			get { return _font_sub3; }
		}
	}
}
