
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
		
		protected override bool BodyCreate (iTextSharp.text.Document document)
		{
			document.SetPageSize (iTextSharp.text.PageSize.A4.Rotate ());
			
			Table table = new Table (3, 13);
			table.AddCell (new Phrase ("Descripción", 
			
			
			
		
			return true;
		}
	}
}
