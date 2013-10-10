using System;

namespace Simatre.Recordum
{
	public class GeronidesAirpointer : Airpointer
	{
		public GeronidesAirpointer () : base ("28c02c8")
		{
			Name = "Geronides";
		}

		public override PollutantType GetPollutantFromId (int id)
		{
			//return base.GetPollutantId (pollutanttype);
			PollutantType type = PollutantType.Unknown;

			switch (id) {
				case 1:
					type =  PollutantType.NO; // ppb
				break;

				case 2:
					type = PollutantType.NO2; // ppb
				break;

				case 3:
					type = PollutantType.NOX; // ppb
				break;

				case 4:
					type = PollutantType.CO; // ppm
				break;

				case 5:
					type = PollutantType.O3; // ppb
				break;

				case 6:
					type = PollutantType.SO2; // ppb
				break;

				case 9:
					type = PollutantType.Part; // µg/m³
				break;

				case 31:
					type = PollutantType.AmbientTemp; // A°Celcius
				break;

				case 33:
					type = PollutantType.RoomTemp; // Celcius
				break;

				case 247:
					type = PollutantType.BattStatus; // digit
				break;

				case 11835:
					type = PollutantType.AirPressure; // hPa
				break;

				case 11841:
					type = PollutantType.AirTemp; // Celcius
				break;
				case 11847:
					type = PollutantType.RelativeHumidity; // hits/cm2h
				break;

				case 11853:
					type = PollutantType.RainAccumulated; // mm
				break;

				case 11859:
					type = PollutantType.RainDuration; // sec 
				break;

				case 11865:
					type = PollutantType.RainIntensity; // hits/cm2h
				break;

				case 11901:
					type = PollutantType.SupplyVoltage; // V
				break;

				case 11823:
					type = PollutantType.WindSpeed; // m/s
				break;

				case 11829:
					type = PollutantType.WindDirection; // °
				break;
		
			}
			return type;
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

				case PollutantType.Part:
					id = 9; // µg/m³
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
					id = 11829; // °
				break;
		
			}
			return id;
		}
	}
}

