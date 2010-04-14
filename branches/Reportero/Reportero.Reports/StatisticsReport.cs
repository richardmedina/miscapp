
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
			document.Add (new Phrase ("Help", FontTitle)); 
		
			return true;
		}
	}
}