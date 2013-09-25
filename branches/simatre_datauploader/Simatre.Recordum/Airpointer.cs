using System;
using System.Net;
using System.IO;
using System.Xml;

namespace Simatre.Recordum
{
	public class Airpointer
	{

		public string Username;
		public string Password;

		public string SensorId = "";

		public DateTime DateStart = DateTime.Now - new TimeSpan(5, 0, 0, 0);
		public DateTime DateEnd = DateTime.Now - new TimeSpan (4, 0, 0, 0);

		public string Pollutants = "1,2,3,4,5,6" + ",31,247,33,11745,11751,11763,11769,11775,11757,11811,11739,11733";
		public string Interval = "avg3";

		public ConnectionType ConnectionType;

		public string RootUrl = "http://portal.recordum.com/instrument/UI";
				//"http://10.0.0.140/start.php";

		private readonly string url_local = "http://10.0.0.140/start.php";
		private readonly string url_remote = "http://portal.recordum.com/instrument/UI";

		public readonly string [] PollutantName = {"NO", "NO2", "NOX", "CO", "O3", "SO2"};


		public Airpointer ()
		{
			ConnectionType = ConnectionType.Local;
		}

		public PollutantCollection Start ()
		{
			//Console.WriteLine ("Recordum.Start");

			/*string url = string.Format ("{0}/{1}/http_if/download.php?loginstring={2}&user_pw={3}&tstart={4}&tend={5}&{6}={7}&dec=POINT&null=NULL&colt=3,2", 
			                            RootUrl, 
			                            SensorId, 
			                            Username, 
			                            Password,
			                            DateStart.ToString ("yyyy-MM-dd,HH:mm:ss"),
			                            DateEnd.ToString ("yyyy-MM-dd,HH:mm:ss"),
			                            Interval,
			                            Pollutants);

		*/

			string url = GetUrlRequest ();
			string data = string.Empty;


			WebRequest request = WebRequest.Create (url);

			try {
				var response = request.GetResponse ();
				using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
					data = reader.ReadToEnd ();
				}

			} catch (Exception) {
				throw new AirpointerException (string.Format ("Airpointer {0} does not respond", SensorId));
			}
	
			XmlDocument doc = new XmlDocument ();

			try {
				doc.LoadXml (data);
			} catch (Exception) {
				throw new AirpointerException (string.Format ("Airpointer {0} xml file is corrupt", SensorId));
			}

			
			PollutantCollection pollutants;
			try {
				pollutants = PollutantCollection.ParseXML (SensorId, doc);
			} catch (Exception) {
				throw new AirpointerException (string.Format ("Parsing error {0} in xml data file", SensorId));
			}

			return pollutants;
		}




		public void Info ()
		{
			throw new NotImplementedException ();
		}

		public string GetUrlRequest ()
		{
			string querystr = GetQueryString ();

			if (ConnectionType == ConnectionType.Local) {
				return string.Format ("{0}?{1}", url_local, querystr);
			}

			return string.Format ("{0}/{1}/http_if/download.php?{2}", url_remote, SensorId, querystr);
		}

		public string GetQueryString ()
		{
			return string.Format ("loginstring={0}&user_pw={1}&tstart={2}&tend={3}&{4}={5}&dec=POINT&null=NULL&nohtml&dec=point&del=semi", 
			                            Username, 
			                            Password,
			                            DateStart.ToString ("yyyy-MM-dd,HH:mm:ss"),
			                            DateEnd.ToString ("yyyy-MM-dd,HH:mm:ss"),
			                            Interval,
			                            Pollutants);
		}

	}
}

