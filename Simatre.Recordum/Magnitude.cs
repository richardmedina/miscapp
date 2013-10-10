using System;

namespace Simatre.Recordum
{
	public class Magnitude
	{
		public DateTime Date;
		public float Amount;
		public MeasureUnit Unit;

		public Magnitude (DateTime datetime, float amount, MeasureUnit unit)
		{
			Date = datetime;
			Amount = amount;
			Unit = unit;
		}


		public float GetValue (MeasureUnit unit)
		{
			float val = 0f;

			switch (unit) {
				case MeasureUnit.PPB:
					val = GetPPBValue ();
				break;

				case MeasureUnit.PPM:
					val = GetPPMValue ();
				break;
			}

			return val;
		}

		public float GetPPMValue ()
		{
			float v = Amount;

			if (Unit == MeasureUnit.PPB) {
				v /= 1000;
			}

			return v;
		}

		public float GetPPBValue ()
		{
			float v = Amount;

			if (Unit == MeasureUnit.PPM) {
				v *= 1000;
			}

			return v;
		}

		public override string ToString ()
		{
			return string.Format ("[Magnitude]");
		}
	}
}

