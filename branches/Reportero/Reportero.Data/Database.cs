
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
			Console.WriteLine ("Closing Database Connection..");
			_connection.Close ();
		}
		
		public bool Open ()
		{
			string connection_string = string.Format ("Server={0};UID={1};PWD={2};Database={3};",
				Hostname, UserId, Password, Source);
		
			_connection = new SqlConnection  (connection_string);

			Console.WriteLine ("Connecting to database..");
			try {
				_connection.Open ();
			} catch (Exception exception) {
				Console.WriteLine ("Exception Connecting: {0}", exception);
				return false;
			}
				
			Console.WriteLine ("Done with timeout {0}", _connection.ConnectionTimeout);
			return true;
		}

		public IDataReader Query (string format, params object [] objs)
		{
			string query = string.Format (format, objs);
			IDbCommand command = new SqlCommand (query, _connection);
			
			IDataReader reader = null; 
			try {
				reader  = command.ExecuteReader ();
			} catch (Exception e) {
				Console.WriteLine ("Exception. {0}", e.Message);	
			}
			
			return reader;
		}
		
		public void NonQuery (string format, params object [] objs)
		{
			string query = string.Format (format, objs);
			IDbCommand command = new SqlCommand (query, _connection);
			try {
				command.ExecuteNonQuery ();
			} catch (Exception e) {
				Console.WriteLine ("Exception. {0}", e.Message);
			}
		}
		
		public string Hostname {
			get { return _hostname; }
		}
		
		public string UserId {
			get { return _userid; }
		}
		
		public string Password {
			get { return _password; }
		}
		
		public string Source {
			get { return _source; }
		}
	}
}
