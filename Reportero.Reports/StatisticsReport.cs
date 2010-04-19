
using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reportero.Reports
{
	
	
	public class StatisticsReport : Report
	{
		
		public StatisticsReport () : base (DateTime.Now, DateTime.Now)
		{
		}
		
		protected override bool HeaderCreate (iTextSharp.text.Document document)
		{
			
			return base.HeaderCreate (document);
		}

		
		protected override bool BodyCreate (iTextSharp.text.Document document)
		{
			//document.SetPageSize (PageSize.A4.);
			
			Table table = new Table (3, 13);
			table.AddCell (new Phrase ("Descripción", FontSub1));
			document.Add (table);
		
			return true;
		}
	}
}
