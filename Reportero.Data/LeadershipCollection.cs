
using System;
using System.Data;

namespace Reportero.Data
{
	
	
	public class LeadershipCollection : RecordCollection <Leadership>
	{
		
		public LeadershipCollection (Database database) : base (database)
		{
		}
		
		public static LeadershipCollection FromDatabase (Database database)
		{
			LeadershipCollection leadcol = new LeadershipCollection (database);
			IDataReader reader = database.Query ("select distinct substring(Alias, 0, charindex('-',Alias)) as leadership from VehicleState where Alias <> '' order by Leadership;");
			
			while (reader.Read ()) {
				Leadership leader = new Leadership (database);
				leader.Name = (string) reader ["leadership"];
				leadcol.Add (leader);
			}
			
			reader.Close ();
			
			return leadcol;
		}		
	}
}
