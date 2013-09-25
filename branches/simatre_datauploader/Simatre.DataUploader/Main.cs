using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Simatre.Recordum;

namespace Simatre.DataUploader
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			//WebRequest request = FileWebRequest.Create ("");
			/*
			RecordumDataRow row = new RecordumDataRow ();
			Console.WriteLine (row);
			Console.WriteLine ("Presiona una tecla para continuar...");
			Console.ReadLine ();
			*/
			Airpointer airpointer = new Airpointer ();
			airpointer.ConnectionType = ConnectionType.Remote;

			airpointer.SensorId = "28c02c8";
			airpointer.SensorId = "fa47740";

			airpointer.Username = "admin";
			airpointer.Password = "1AQuality";


			if (args.Length == 0) {
				Console.WriteLine ("DataUploader.exe -sensorid <sensorid> -username <username> -password <password> -type <local|remote>");
			} else {

				for (int i = 0; i < args.Length; i++) {
					switch (args [i]) {
					case "-sensorid":
						airpointer.SensorId = args [++i];
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
					}
				}

			}

			//Console.WriteLine ("Waiting for ..." + recordum.RootUrl);

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

			//foreach (float f in pollutants.GetMagnitudes (0, MeasureUnit.PPB)) {
			/*
			Console.Write ("{0}:", Utils.DateTimeToRecordumString(pollutants [0].Magnitudes [0].Date));

			for (int i = 0; i  < pollutants.Count; i ++) {

				Console.Write ("@{0}={1}", 
				               pollutants [i].Type, 
				               pollutants [i].Magnitudes [0].GetPPMValue ());
			}
			*/
			Console.ReadLine ();

			/*using (StreamWriter sw = new StreamWriter ("data.xml"))
				sw.Write (data);

			Console.WriteLine (data.Length + " bytes written");

			/*
			MainForm form = new MainForm ();

			Application.Run (form);*/
		}
	}
}
