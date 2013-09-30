using System;

namespace Simatre.Recordum
{
	public enum PollutantType
	{
		Unknown = 0,
		NO, 
		NO2,  
		NOX,
		CO,
		O3,
		SO2,
		Part = 9,
		AmbientTemp = 31,
		RoomTemp = 33,
		BattStatus = 247,
		AirPressure = 11745,
		AirTemp = 11751,
		RelativeHumidity = 11757,
		RainAccumulated = 11763,
		// One type is missing
		RainDuration = 11769,
		RainIntensity = 11775,
		SupplyVoltage = 11811,
		WindSpeed = 11733,
		WindDirection = 11739

	} 
}

