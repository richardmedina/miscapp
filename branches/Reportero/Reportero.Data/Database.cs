
using System;
using System.Data;
using System.Data.SqlClient;

namespace Reportero.Data
{
	
	
	public class Database
	{
		private SqlConnection _connection;
		
		public void Close ()
		{
			_connection.Close ();
		}
		
		public void Open ()
		{
			_connection = new SqlConnection  (//@"Driver={SQL Server};Server=142.125.145.25\SQLEXPRESS;UID=monitoreovehiculos;PWD=Qwerty;Database=MonitoreoVehiculos");
				@"Server=142.125.145.25;" +
          		"Database=MonitoreoVehiculos;" +
          		"User ID=monitoreovehiculos;" +
          		"Password=Qwerty;");

			Console.WriteLine ("Connecting to database..");
			_connection.Open ();
			Console.WriteLine ("Done");
		}

		public IDataReader Query (string format, params object [] objs)
		{
			string query = string.Format (format, objs);
			IDbCommand command = new SqlCommand (query, _connection);
			
			return command.ExecuteReader ();
		}
		
		public void NonQuery (string format, params object [] objs)
		{
			string query = string.Format (format, objs);
			IDbCommand command = new SqlCommand (query, _connection);
			command.ExecuteNonQuery ();
		}
	}
}
