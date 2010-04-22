
using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Reportero.Reports
{
	
	
	public class StatisticsReport : Report
	{
		private int _month;
		private static readonly string [] _months = {
			"Enero", "Febrero",	"Marzo", "Abril", "Mayo", "Junio",
			"Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
		};
		
		public StatisticsReport (int month) : base (DateTime.Now, DateTime.Now)
		{
			_month = month;
		}
		
		protected override bool HeaderCreate (iTextSharp.text.Document document)
		{
			
			return base.HeaderCreate (document);
		}

		
		protected override bool BodyCreate (iTextSharp.text.Document document)
		{
			//document.SetPageSize (PageSize.A4.);
			
			document.Add (new Paragraph (string.Format ("INFORME ESTADÍSTICO VEHICULAR DEL MES DE {0}.", _months [_month].ToUpper ())));
			
			Table table = new Table (3, 12);
			table.Cellpadding = 5;
			int row = 0;
			
			Cell cell = CreateCell ("Equipos Instalados", FontSub1);
			
			table.AddCell (cell, row, 0); 
			table.AddCell (CreateCell ("Cantidad", FontSub1), row, 1);
			table.AddCell (CreateCell ("Porcentaje", FontSub1), row ++, 2);
			
			table.AddCell (CreateCell ("Vehiculos con exceso  (más de 80 Km/h)", FontSub2), row ++, 0);
			table.AddCell (CreateCell ("Vehiculos que excedieron menos de dos días", FontSub2), row ++, 0);
			table.AddCell (CreateCell ("Vehiculos que excedieron menos de 7 días", FontSub2), row ++, 0);
			
			
			document.Add (table);
		
			return true;
		}
	}
}
