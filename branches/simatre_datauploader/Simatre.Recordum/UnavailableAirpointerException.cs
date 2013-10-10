using System;

namespace Simatre.Recordum
{
	public class UnavailableAirpointerException : AirpointerException
	{
		public Airpointer Airpoiner;

		public UnavailableAirpointerException (Airpointer airpointer) : base (
			string.Format ("[{0}] Airpointer no esta disponible",
			DateTime.Now.ToString (Utils.RecordumDateFormat)))
		{
			this.Airpoiner = airpointer;
		}
	}
}

