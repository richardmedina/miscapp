
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
		
		public bool Open ()
		{
			//string conn
			
			string connection_string = string.Format ("Server={0};UID={1};PWD={2};Database={3};",
				Hostname, UserId, Password, Source);
		
			_connection = new SqlConnection  (connection_string);

			Console.WriteLine ("Connecting to database..");
			try {
				_connection.Open ();
			} catch (Exception exception) {
				Console.WriteLine ("ERROR Connecting + {0}", exception);
				return false;
			}
				
			Console.WriteLine ("Done");
			return true;
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
			try {
				command.ExecuteNonQuery ();
			} catch (SqlException) {
				if (Open ()) {
					command.ExecuteNonQuery ();
				}
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
