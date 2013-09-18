using System;

namespace Simatre.Recordum
{
	public class Pollutant
	{
		public float LMP = 0f;
		public MagnitudeCollection Magnitudes;
		public PollutantType Type;

		public Pollutant (PollutantType type)
		{
			Type = type;
			Magnitudes = new MagnitudeCollection ();
		}
	}
}

