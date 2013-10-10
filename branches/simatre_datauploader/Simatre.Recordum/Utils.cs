using System;
using System.IO;
using System.Globalization;
using System.Net;

namespace Simatre.Recordum
{
	public static class Utils
	{
		public static string RecordumPenddingFilename = "recordum.pendding.txt";

		public static readonly string RecordumDateFormat = "yyyy-MM-dd,HH:mm:ss"; 


		public static PollutantType PollutantTypeFromString (string str)
		{
			return PollutantType.CO;
		}

		public static bool DateTimeFromRecordumString (string str, out DateTime datetime)
		{
			string pattern = RecordumDateFormat;

			DateTime dt;

			if (DateTime.TryParseExact(str, pattern, CultureInfo.InvariantCulture, 
				DateTimeStyles.None,
				out dt)) {

				datetime = dt;
				return true;
			}

			return false;
		}

		public static bool GetResponse (string url, out string response)
		{
			response = string.Empty;
			string data = string.Empty;

			try {
				WebRequest req = WebRequest.Create (url);
				WebResponse res = req.GetResponse ();

				using (StreamReader reader = new StreamReader (res.GetResponseStream ()))
				data = reader.ReadToEnd ();
				response = data;
			} catch (Exception e) {
				response = e.Message;
				return false;
			}

			return true;
		}

		public static string DateTimeToRecordumString (DateTime datetime)
		{
			return datetime.ToString (RecordumDateFormat);
		}


		public static string GetFilename ()
		{
			return RecordumPenddingFilename;
		}

		public static void SaveToPendding (string [] row)
		{/*
			using (StreamWriter sw = new StreamWriter (GetFilename ())) {
				sw.WriteLine (row);
			}*/
		}


		public static bool RecordumSend (AirpointerDataRow row)
		{
			throw new NotImplementedException ();
		}

		public static bool RecordumSendPendding ()
		{
			throw new NotImplementedException ();
		}
	}
}

