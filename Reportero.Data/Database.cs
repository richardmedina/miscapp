
using System;
using System.Data;
using System.Data.SqlClient;

namespace Reportero.Data
{
	
	
	public class Database
	{
		private SqlConnection _connection;
		
		private string _hostname;
		private string _source;
		private string _userid;
		private string _password;
		
		public Database (string hostname, string userid, string password, string source)
		{
			_hostname = hostname;
			_userid = userid;
			_password = password;
			_source = source;
		}
		
		public void Close ()
		{
			_connection.Close ();
		}
		
		public void Open ()
		{
			//string conn
		
			_connection = new SqlConnection  (@"Server=142.125.145.25;UID=monitoreovehiculos;PWD=Qwerty;Database=MonitoreoVehiculos");
				

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
