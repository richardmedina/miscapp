
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class Country : Record
	{
		public string Id;
		public string Name;
		
		public Country (Database db) : base (db, RecordType.Country)
		{
		}
		
		
		public override void FillFromReader (IDataReader reader)
		{
			Id = reader["edo_id"].ToString ();
			Name = reader ["edo_nombre"].ToString ();
		}
		
		public static CountryCollection GetFromDatabase (Database db)
		{
			CountryCollection countries = new CountryCollection ();
			IDataReader reader = db.Query ("SELECT * FROM estados");
			
			while (reader.Read ()) {
				Country country = new Country (db);
				country.FillFromReader (reader);
				countries.Add (country);
			}
			
			return countries;
		}
	}
}
