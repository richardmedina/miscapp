using System;

namespace Simatre.Recordum
{
	public class GeronidesAirpointer : Airpointer
	{
		public GeronidesAirpointer () : base ("28c02c8")
		{
		}

		public override int GetPollutantId (PollutantType pollutanttype)
		{
			//return base.GetPollutantId (pollutanttype);
			int id = 0;

			switch (pollutanttype) {
				case PollutantType.NO:
					id =  1; // ppb
				break;

				case PollutantType.NO2:
					id = 2; // ppb
				break;

				case PollutantType.NOX:
					id = 3; // ppb
				break;

				case PollutantType.CO:
					id = 4; // ppm
				break;

				case PollutantType.O3:
					id = 5; // ppb
				break;

				case PollutantType.SO2:
					id = 6; // ppb
				break;

				case PollutantType.AmbientTemp:
					id = 31; // A°Celcius
				break;

				case PollutantType.RoomTemp:
					id = 33; // Celcius
				break;

				case PollutantType.BattStatus:
					id = 247; // digit
				break;

				case PollutantType.AirPressure:
					id = 11835; // hPa
				break;

				case PollutantType.AirTemp:
					id = 11841; // Celcius
				break;
				case PollutantType.RelativeHumidity:
					id = 11847; // hits/cm2h
				break;

				case PollutantType.RainAccumulated:
					id = 11853; // mm
				break;

				case PollutantType.RainDuration:
					id = 11859; // sec 
				break;

				case PollutantType.RainIntensity:
					id = 11865; // hits/cm2h
				break;

				case PollutantType.SupplyVoltage:
					id = 11901; // V
				break;

				case PollutantType.WindSpeed:
					id = 11823; // m/s
				break;

				case PollutantType.WindDirection:
					id = 11739; // °
				break;
		
			}
			return id;
		}
	}
}

