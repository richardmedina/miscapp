using System;

namespace Simatre.Recordum
{
	public class Pollutant
	{
		public float LMP = 0f;
		public MagnitudeCollection Magnitudes;
		public PollutantType Type;
		public string AirpointerId;
		private MeasureUnit Unit;

		public Pollutant (string airpointer_id, PollutantType type, MeasureUnit unit)
		{
			AirpointerId = airpointer_id;
			Type = type;
			Unit = unit;
			Magnitudes = new MagnitudeCollection ();
		}
	}
}

