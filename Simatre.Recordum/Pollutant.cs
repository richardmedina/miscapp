using System;

namespace Simatre.Recordum
{
	public class Pollutant
	{
		public float LMP = 0f;
		public MagnitudeCollection Magnitudes;
		public PollutantType Type;
		public string AirpointerId;
		public MeasureUnit Unit;
		

		public Pollutant (string airpointer_id, PollutantType type, MeasureUnit unit)
		{
			AirpointerId = airpointer_id;
			Type = type;
			Unit = unit;
			Magnitudes = new MagnitudeCollection ();
		}

		public Magnitude GetMagnitude (int i)
		{
			Magnitude mag = GetMagnitude (i);
			/*
			switch (unit) {
				case MeasureUnit.PPB:
					val = GetPPBMagnitude (i);
				break;

				case MeasureUnit.PPM:
					val = GetPPMMagnitude (i);
				break;
			}
			*/
			return mag;
		}
		/*
		public Magnitude GetPPMMagnitude (int i)
		{
			Magnitude v = Magnitudes [i];

			if (Unit == MeasureUnit.PPB) {
				v /= 1000;
			}

			return v;
		}

		public Magnitude GetPPBMagnitude (int i)
		{
			Magnitude v = Magnitudes [i];

			if (Unit == MeasureUnit.PPM) {
				v *= 1000;
			}

			return v;
		}
		*/
	}
}

