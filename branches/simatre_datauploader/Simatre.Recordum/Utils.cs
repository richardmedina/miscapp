using System;
using System.IO;
using System.Globalization;
namespace Simatre.Recordum
{
	public class Utils
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

		public static string DateTimeToRecordumString (DateTime datetime)
		{
			return datetime.ToString (RecordumDateFormat);
		}


		public static string GetFilename ()
		{
			return RecordumPenddingFilename;
		}

		public static void SaveToPendding (AirpointerDataRow row)
		{
			using (StreamWriter sw = new StreamWriter (GetFilename ())) {

			}
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

