using System;

namespace Simatre.Recordum
{
	public class AirpointerDataRow
	{

		public string SensorId;

		public DateTime StartDate;
		public DateTime EndDate;

		public float NO;
		public float NO2;
		public float NOX;
		public float CO;
		public float O3;
		public float SO2;


		public AirpointerDataRow ()
		{
		}

		public override string ToString ()
		{
			// @RecordumDataRow@aaaa-mm-dd,hh:MM:ss@SensorId@NO@NO2@NOX@CO@O3@SO2
			return string.Format ("@RecordumDataRow@" 
			                      + SensorId 
			                      + "@" + StartDate.ToString (Utils.RecordumDateFormat)
			                      + "@" + EndDate.ToString (Utils.RecordumDateFormat)
			                      + "@" + NO.ToString ("0.0000") 
			                      + "@" + NO2.ToString ("0.0000")
			                      + "@" + NOX.ToString ("0.0000")
			                      + "@" + CO.ToString ("0.0000")
			                      + "@" + O3.ToString ("0.0000")
			                      + "@" + SO2.ToString ("0.0000")
			                      );
		}
	}
}

