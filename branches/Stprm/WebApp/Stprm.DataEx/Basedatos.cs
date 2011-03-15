using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using Stprm.Data;

namespace Stprm.DataEx
{
    public class BaseDatos : IDisposable
    {
        private SqlConnection _connection;

		private string _hostname;
		private string _source;
		private string _userid;
		private string _password;

		public BaseDatos (string hostname, string userid, string password, string source)
		{
			_hostname = hostname;
			_userid = userid;
			_password = password;
			_source = source;
		}
		
        public static Database CreateOldConnection()
        {
            Database db = new Database("Poseidon", "ricki", "09b9085a+", "seccion26");
            db.Open();

            return db;
        }
		
		public static BaseDatos CreateStprmConnection ()
		{
			BaseDatos db = new BaseDatos (@"Poseidon", "ricki", "09b9085a+", "seccion26");
			db.Open ();
			return db;
		}

		public void Close()
		{
			_connection.Close();
		}

		public bool Open()
		{
			string connection_string = string.Format("Server={0};UID={1};PWD={2};Database={3}; Connection Timeout=15;",
				Hostname, UserId, Password, Source);

			_connection = new SqlConnection(connection_string);

			try {
				_connection.Open();
			}
			catch (Exception exception) {
				Console.WriteLine("Exception Connecting: {0}", exception);
				return false;
			}

			return true;
		}

		public IDataReader Query(string format, params object[] objs)
		{
			string query = string.Format(format, objs);
			Console.WriteLine (query);
			
			IDbCommand command = new SqlCommand (query, _connection);

			IDataReader reader = null;

			reader = command.ExecuteReader();
			return reader;
		}

		public void NonQuery(string format, params object[] objs)
		{
			string query = string.Format(format, objs);

			Console.WriteLine (query);
			IDbCommand command = new SqlCommand (query, _connection);

			command.ExecuteNonQuery();
		}

		public IDataAdapter QueryToAdapter(string format, params object[] objs)
		{
			string query = string.Format(format, objs);
			Console.WriteLine (query);
			IDataAdapter adapter = new SqlDataAdapter (query, _connection);

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

		#region IDisposable Members

		public void Dispose()
		{
			Close ();
        }
        #endregion
    }
}
