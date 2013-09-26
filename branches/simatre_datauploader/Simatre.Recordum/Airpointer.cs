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

		public string Id = "";

		public DateTime DateStart = DateTime.Now - new TimeSpan(0, 2, 0, 0);
		public DateTime DateEnd = DateTime.Now - new TimeSpan (0, 1, 0, 0);

		//public string Pollutants = "1,2,3,4,5,6" + ",31,247,33,11745,11751,11763,11769,11775,11757,11811,11739,11733";

		public PollutantPairCollection PollutantPairs;
		 
		public string Interval = "avg3";

		public ConnectionType ConnectionType;

		public string RootUrl = "http://portal.recordum.com/instrument/UI";
				//"http://10.0.0.140/start.php";

		private readonly string url_local = "http://10.0.0.140/start.php";
		private readonly string url_remote = "http://portal.recordum.com/instrument/UI";

		public readonly string [] PollutantName = {"NO", "NO2", "NOX", "CO", "O3", "SO2"};

		public bool VerboseMode = false;

		public Airpointer (string airpointerid)
		{
			Id = airpointerid;
			ConnectionType = ConnectionType.Local;
			PollutantPairs = new PollutantPairCollection ();
		}

		public void Init ()
		{
			string [] ptypes = PollutantType.GetNames (typeof(PollutantType));
			PollutantPair [] pairs = new PollutantPair [ptypes.Length -1];


			for (int i = 0; i < ptypes.Length; i ++) {
				PollutantType pt = (PollutantType)Enum.Parse (typeof (PollutantType), ptypes[i]);
				int pollutant_id = GetPollutantId (pt);

				if (pollutant_id == 0) continue;//throw new AirpointerException ("PollutantId " + Id + " can't be zero");

				PollutantPair pair = new PollutantPair (pollutant_id, pt);  
				pairs [i-1] = pair;
			}

			if (pairs.Length == 0) throw new AirpointerException (Id + ": Specify parameters");

			PollutantPairs.AddRange (pairs);
		}

		public PollutantCollection Start ()
		{
			string url = GetUrlRequest ();
			string data = string.Empty;


			WebRequest request = WebRequest.Create (url);

			try {
				var response = request.GetResponse ();
				using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
					data = reader.ReadToEnd ();
				}

			} catch (Exception) {
				throw new AirpointerException (string.Format ("Airpointer {0} does not respond", Id));
			}
	
			XmlDocument doc = new XmlDocument ();

			try {
				doc.LoadXml (data);
			} catch (Exception) {
				throw new AirpointerException (string.Format ("Airpointer {0} xml file is corrupt", Id));
			}

			
			PollutantCollection pollutants;
			try {
				pollutants = PollutantCollection.ParseXML (this, doc);
			} catch (Exception) {
				throw new AirpointerException (string.Format ("Parsing error {0} in xml data file", Id));
			}

			return pollutants;
		}

		public virtual int GetPollutantId (PollutantType pollutanttype)
		{
			return 0;
		}
		public virtual PollutantType GetPollutantFromId (int id)
		{
			return PollutantType.CO;
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

			return string.Format ("{0}/{1}/http_if/download.php?{2}", url_remote, Id, querystr);
		}

		public string GetQueryString ()
		{

			string pols = string.Empty;


			for (int i = 0; i < PollutantPairs.Count; i ++) {
				PollutantPair pair = PollutantPairs [i];
				if (i > 0)
					pols += ",";

				pols += pair.Id.ToString ();

			}

			Console.WriteLine ("ready to get -{0}-", pols);

			return string.Format ("loginstring={0}&user_pw={1}&tstart={2}&tend={3}&{4}={5}&dec=POINT&null=NULL&nohtml&dec=point&del=semi", 
			                            Username, 
			                            Password,
			                            DateStart.ToString ("yyyy-MM-dd,HH:mm:ss"),
			                            DateEnd.ToString ("yyyy-MM-dd,HH:mm:ss"),
			                            Interval,
			                            pols);
		}

	}
}

