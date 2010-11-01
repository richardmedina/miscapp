
using System;

namespace Stprm.CajaFinanciera.Data
{


	public class Category : Record
	{
		public string Id;
		public string Name;
		public string Concept;
		
		
		public Category (Database db) : base (db, RecordType.Category)
		{
		}
	}
}
