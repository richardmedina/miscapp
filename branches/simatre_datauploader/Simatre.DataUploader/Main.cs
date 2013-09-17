using System;
using System.Net;
using System.Windows.Forms;

namespace Simatre.DataUploader
{
	class MainClass
	{

		public static void Main (string[] args)
		{
			//WebRequest request = FileWebRequest.Create ("");

			RecordumDataRow row = new RecordumDataRow ();
			Console.WriteLine (row);
			Console.ReadLine ();

			Recordum recordum = new Recordum ();
			recordum.ConnectionType = RecordumConnectionType.Remote;
			recordum.SensorId = "fa47740";
			recordum.Username = "admin";
			recordum.Password = "1AQuality";


			if (args.Length == 0) {
				Console.WriteLine ("DataUploader.exe -sensorid <sensorid> -username <username> -password <password> -type <local|remote>");
			} else {

				for (int i = 0; i < args.Length; i++) {
					switch (args [i]) {
						case "-sensorid":
							recordum.SensorId = args [++i];
						break;

						case "-username":
							recordum.Username = args [++i];
						break;

						case "-password":
							recordum.Password = args [++i];
						break;

						case "-type":
							string val = args [++i];
							if (val == "remote")
								recordum.ConnectionType = RecordumConnectionType.Remote;
							else 
								recordum.ConnectionType = RecordumConnectionType.Local;
						break;
					}
				}

			}

			//Console.WriteLine ("Waiting for ..." + recordum.RootUrl);

			Console.WriteLine (recordum.Start ());



			/*
			MainForm form = new MainForm ();

			Application.Run (form);*/
		}
	}
}
