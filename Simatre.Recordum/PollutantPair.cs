using System;

namespace Simatre.Recordum
{
	public class PollutantPair
	{
		public int Id;
		public PollutantType Type;


		public PollutantPair (int id, PollutantType type)
		{
			Id = id;
			Type = type;
		}
	}
}

