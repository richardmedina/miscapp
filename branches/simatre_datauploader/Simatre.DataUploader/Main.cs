using System;
using System.IO;
using System.Net;
using System.Threading;
using Simatre.Recordum;
using System.Collections.Generic;

namespace Simatre.DataUploader
{
	class MainClass
	{
		static string target = "secundaria";
		static string hostname = string.Empty;
		static ConnectionType conn_type = ConnectionType.Remote;
		static string username = string.Empty;
		static string password = string.Empty;
		static bool verbose = false;


		public static void Main (string[] args)
		{
			domain (args);
		}

		static void  domain (string[] args)
		{

			if (args.Length == 0) {
				Console.WriteLine ("*.exe -target secundaria|jeronides -hostname localhostnameorip.com -username user -password pass -local-only -verbose");
				return;
			}

			for (int i = 0; i < args.Length; i ++) {
				if (args [i] == "-target") {
					if (i + 1 >= args.Length) {
						Console.WriteLine ("Argument TARGET missing..");
						return;
					}
					target = args [i + 1];
				}

				if (args [i] == "-hostname") {
					if ((i + 1) >= args.Length) {
						Console.WriteLine ("Argument HOSTNAME missing..");
						return;
					}
					hostname = args [i + 1];
				}

				if (args [i] == "-username") {
					if ((i + 1) >= args.Length) {
						Console.WriteLine ("Argument HOSTNAME missing..");
						return;
					}
					username = args [i + 1];
				}

				if (args [i] == "-local-only") {
					conn_type = ConnectionType.Local;
				}

				if (args [i] == "-verbose") {
					verbose = true;
				}
			}

			Thread thread = new Thread (timer);
			thread.Start ();

			Console.WriteLine ("Presiona una tecla para terminar...");
			Console.ReadLine ();
			thread.Abort ();
		}

		static void timer ()
		{
			
			Airpointer airpointer = null;

			if (target == "jeronides")
				airpointer = new JeronidesAirpointer ();
			else if (target == "secundaria")
				airpointer = new SecundariaAirpointer ();
			

			airpointer.ConnectionType = conn_type;

			
			airpointer.Username = "admin";
			airpointer.Password = "1AQuality";
			airpointer.VerboseMode = verbose;
			airpointer.Init ();

			bool waiting = false;

			while (true) {
				if(!waiting)
					Console.WriteLine ("[{0}] Consultando airpointer...", DateTime.Now.ToString (Utils.RecordumDateFormat));

				DateTime start = DateTime.Now - new TimeSpan (2, 0, 0);
				DateTime end = DateTime.Now - new TimeSpan (1, 0, 0);

				airpointer.DateStart  = start;
				airpointer.DateEnd = end;

				PollutantCollection pollutants;

				try {
					pollutants = airpointer.Start ();
					SendPollutantValues (pollutants);
					waiting = false;
				} 
				catch (UnavailableAirpointerException e) {
					if(!waiting) {
						Console.WriteLine (e.Message);
						Console.WriteLine ("Esperando que se normalice la conexión...");
					}
					waiting = true;
				}
				catch (AirpointerException e) {
					DateTime d = DateTime.Now;
					Console.WriteLine (Utils.DateTimeToRecordumString (d) + ":" + e.Message);

					if (airpointer.VerboseMode)
						Console.Write ("Esperando 1 segundo...");

					Thread.Sleep (1000);

					if (airpointer.VerboseMode)
						Console.WriteLine ("Hecho");

					continue;
				}

				if (airpointer.VerboseMode)
					Console.Write ("Esperando 5 segundos..");

				Thread.Sleep (5000);

				if (airpointer.VerboseMode)
					Console.WriteLine ("Hecho");
			}
		}

		public static void SendPollutantValues (PollutantCollection pollutants)
		{
			List<string> pendding = new List<string> ();
			string [] queries = pollutants.GetQueryString ();

			try {
				Console.Write ("[{0}] Registrando valores..", DateTime.Now.ToString (Utils.RecordumDateFormat));
				foreach (string query in queries) {
					string send_request = "http://zeus/simatre_mysql/simatre_save.php?" + query;
					string response = string.Empty;

					if (pollutants.Airpointer.VerboseMode)
						Console.WriteLine (send_request);

					if (Utils.GetResponse (send_request, out response)) {
						if (pollutants.Airpointer.VerboseMode)
							Console.WriteLine (response);
					} else {
						pendding.Add (send_request);
						if (pollutants.Airpointer.VerboseMode) {
							Console.WriteLine ("Error enviando datos..");
						}
					}
				}
				Console.WriteLine ("Hecho");
			} 
			catch (Exception e) {
				Console.WriteLine ("ERROR. Realizando copia local para envio temporizado");
				Utils.SaveToPendding (pendding.ToArray ());
			}

			if (pendding.Count > 0)
				Utils.SaveToPendding (pendding.ToArray ());
		}

		public static void main (string[] args)
		{
			Airpointer airpointer = new SecundariaAirpointer ();
			airpointer.ConnectionType = ConnectionType.Remote;

			airpointer.Username = "admin";
			airpointer.Password = "1AQuality";

			if (args.Length == 0) {
				Console.WriteLine ("DataUploader.exe -sensorid <sensorid> -username <username> -password <password> -type <local|remote>");
			} else {

				for (int i = 0; i < args.Length; i++) {
					switch (args [i]) {
					case "-sensorid":
						airpointer.Id = args [++i];
						break;

					case "-username":
						airpointer.Username = args [++i];
						break;

					case "-password":
						airpointer.Password = args [++i];
						break;

					case "-type":
						string val = args [++i];
						if (val == "remote")
							airpointer.ConnectionType = ConnectionType.Remote;
						else 
							airpointer.ConnectionType = ConnectionType.Local;
						break;
					
						case "-verbose":
							airpointer.VerboseMode = true;
						break;
					}
				}
			}

			airpointer.Init ();
			PollutantCollection pollutants;

			try {
				pollutants = airpointer.Start ();
			} catch (AirpointerException e) {
				Console.WriteLine ("MyError {0}", e);
				return;
			}

			string [] queries = pollutants.GetQueryString ();
			foreach (string query in queries)
				Console.WriteLine (query);

			Console.ReadLine ();
		}
	}
}
