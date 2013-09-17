using System;
using System.IO;
using System.Globalization;
namespace Simatre.DataUploader
{
	public class Utils
	{
		public static string RecordumPenddingFilename = "recordum.pendding.txt";

		public static readonly string RecordumDateFormat = "yyyy-MM-dd,HH:mm:ss"; 


		public static RecordumPollutantType PollutantTypeFromString (string str)
		{
			return RecordumPollutantType.Unknown;
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


		public static string GetFilename ()
		{
			return RecordumPenddingFilename;
		}

		public static void SaveToPendding (RecordumDataRow row)
		{
			using (StreamWriter sw = new StreamWriter (GetFilename ())) {

			}
		}


		public static bool RecordumSend (RecordumDataRow row)
		{
			throw new NotImplementedException ();
		}

		public static bool RecordumSendPendding ()
		{
			throw new NotImplementedException ();
		}
	}
}

