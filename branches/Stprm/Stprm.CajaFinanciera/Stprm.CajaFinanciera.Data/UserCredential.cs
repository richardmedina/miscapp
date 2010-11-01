
using System;

namespace Stprm.CajaFinanciera.Data
{


	public class UserCredential : Record
	{
		
		public string Id;
		public string Username;
		public string Password;
		public string Name;
		public string Description;
		
		
		public UserCredential (Database db) : base (db, RecordType.UserCredential)
		{
		}
	}
}
