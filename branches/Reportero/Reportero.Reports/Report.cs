
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
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
			
			if (!HeaderCreate (document))
				cancel = true;
			if (!BodyCreate (document))
				cancel = true;
			if (!FooterCreate (document))
				cancel = true;
			
			document.Close ();
			writer.Close ();
			
			if (run && !cancel)
				RunPdfOnExternalApp (appfilename, filename);
		}
		
		protected virtual bool BodyCreate (Document document)
		{
			return true;
		}
		
		protected virtual bool HeaderCreate (Document document)
		{
			return true;
		}
		
		protected virtual bool FooterCreate (Document document)
		{
			return true;
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
	}
}
