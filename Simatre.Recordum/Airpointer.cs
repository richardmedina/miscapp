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

		public string Pollutants = "1,2,3,4,5,6";
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

		public string Start ()
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

			Console.WriteLine ("Waiting for {0}...", url);

			WebRequest request = WebRequest.Create (url);

			var response = request.GetResponse ();
			string data;

			using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
				data = reader.ReadToEnd ();
			}


			XmlDocument doc = new XmlDocument ();
			doc.LoadXml (data);

			XmlNodeList nodes  = doc.GetElementsByTagName ("ParameterDetails");


			for (int i = 0; i  < nodes.Count; i ++) {
				XmlNode node = nodes [i];

				Console.WriteLine ("Posicion : {0}", node.Attributes ["Position"].Value);

				foreach (XmlNode subnode in node.ChildNodes) {
					Console.WriteLine ("\tSubnode: {0}", subnode.Name);
				}

			}

			//"$this->Sensor/http_if/download.php?loginstring=$this->Username&user_pw=$this->Password&tstart=$this->StartingDate&tend=$this->EndingDate&$this->Interval=$this->AvgStr&dec=POINT&null=NULL";//&colT=3,2"

			return data;
		}

		public void Info ()
		{

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
			return string.Format ("loginstring={0}&user_pw={1}&tstart={2}&tend={3}&{4}={5}&dec=POINT&null=NULL&nohtml&dec=point&del=semi&format=csv", 
			                            Username, 
			                            Password,
			                            DateStart.ToString ("yyyy-MM-dd,HH:mm:ss"),
			                            DateEnd.ToString ("yyyy-MM-dd,HH:mm:ss"),
			                            Interval,
			                            Pollutants);
		}

	}
}
