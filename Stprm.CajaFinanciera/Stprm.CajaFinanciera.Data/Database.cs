
using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Stprm.CajaFinanciera.Data
{


	public class Database : IDisposable
	{
		private MySqlConnection _connection;

		private string _hostname;
		private string _source;
		private string _userid;
		private string _password;
		
		private UserCredential _user_credential;

		public Database(string hostname, string userid, string password, string source)
		{
			_hostname = hostname;
			_userid = userid;
			_password = password;
			_source = source;
		}
		
		public static Database CreateStprmConnection ()
		{
			Database db = new Database (@"localhost", "ricki", "09b9085a+", "caja2");
			db.Open ();
			return db;
		}

		public void Close()
		{
			_connection.Close();
		}

		public bool Open()
		{
			Console.WriteLine ("Connecting...");
			string connection_string = string.Format("Server={0};UID={1};PWD={2};Database={3}; Connection Timeout=15;Allow Zero Datetime=true",
				Hostname, UserId, Password, Source);

			_connection = new MySqlConnection(connection_string);

			try {
				_connection.Open();
			}
			catch (Exception exception) {
				Console.WriteLine("Exception Connecting: {0}", exception);
				return false;
			}
			
			Console.WriteLine ("Connected!");

			return true;
		}

		public IDataReader Query(string format, params object[] objs)
		{
			string query = string.Format(format, objs);
			IDbCommand command = new MySqlCommand (query, _connection);

			IDataReader reader = null;

			reader = command.ExecuteReader();
			return reader;
		}

		public void NonQuery(string format, params object[] objs)
		{
			string query = string.Format(format, objs);

			IDbCommand command = new MySqlCommand (query, _connection);

			command.ExecuteNonQuery();
		}

		public IDbDataAdapter QueryToAdapter(string format, params object[] objs)
		{
			string query = string.Format(format, objs);

			IDbDataAdapter adapter = new MySqlDataAdapter (query, _connection);

			return adapter;
		}

		public string Hostname
		{
			get { return _hostname; }
		}

		public string UserId
		{
			get { return _userid; }
		}

		public string Password
		{
			get { return _password; }
		}

		public string Source
		{
			get { return _source; }
		}
		
		public UserCredential UserCredential {
			get { return _user_credential; }
			set { _user_credential = value; }
		}

		#region IDisposable Members

		public void Dispose()
		{
			Close ();
		}

		#endregion
	}
}
