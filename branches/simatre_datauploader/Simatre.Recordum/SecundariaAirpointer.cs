using System;

namespace Simatre.Recordum
{
	public class SecundariaAirpointer : Airpointer
	{
		public SecundariaAirpointer () : base ("fa47740")
		{
			//Pollutants = "1,2,3,4,5,6,31,247,33,11745,11751,11763,11769,11775,11757,11811,11739,11733";
			Name = "Secundaria";
		}

		public override PollutantType GetPollutantFromId (int id)
		{
			PollutantType type = PollutantType.AirPressure;

			switch (id) {
				case 1:
					type = PollutantType.NO; 
				break;

				case 2:
					type = PollutantType.NO2;
					break;

				case 3:
					type = PollutantType.NOX;
					break;

				case 4:
					type = PollutantType.CO;
					break;

				case 5:
					type = PollutantType.O3;
					break;

				case 6:
					type = PollutantType.SO2;
					break;

				case 31:
					type = PollutantType.AmbientTemp;
					break;

				case 33:
					type = PollutantType.RoomTemp;
					break;

				case 11745:
						 type = PollutantType.AirPressure;
					break;

				case 11751:
				type = PollutantType.AirTemp;
				break;

				case 11757:
					type = PollutantType.RelativeHumidity;
				break;

				case 11763:
					type = PollutantType.RainAccumulated;
				break;

				case 11769:
					type = PollutantType.RainDuration; 
				break;

				case 11775:
					type = PollutantType.RainIntensity;
				break;

				case 11811:
					type = PollutantType.SupplyVoltage;
				break;

				case 11733:
					type = PollutantType.WindSpeed;
				break;

				case 11739:
					type = PollutantType.WindDirection;
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
					id =  1;
				break;

				case PollutantType.NO2:
					id = 2;
				break;

				case PollutantType.NOX:
					id = 3;
				break;

				case PollutantType.CO:
					id = 4;
				break;

				case PollutantType.O3:
					id = 5;
				break;

				case PollutantType.SO2:
					id = 6;
				break;

				case PollutantType.AmbientTemp:
					id = 31;
				break;

				case PollutantType.Part:
					id = 9;
				break;

				case PollutantType.RoomTemp:
					id = 33;
				break;

				case PollutantType.BattStatus:
					id = 247;
				break;

				case PollutantType.AirPressure:
					id = 11745;
				break;

				case PollutantType.AirTemp:
					id = 11751;
				break;
				case PollutantType.RelativeHumidity:
					id = 11757;
				break;

				case PollutantType.RainAccumulated:
					id = 11763;
				break;

				case PollutantType.RainDuration:
					id = 11769; 
				break;

				case PollutantType.RainIntensity:
					id = 11775;
				break;

				case PollutantType.SupplyVoltage:
					id = 11811;
				break;

				case PollutantType.WindSpeed:
					id = 11733;
				break;

				case PollutantType.WindDirection:
					id = 11739;
				break;
		
			}
			return id;
		}


	}
}

