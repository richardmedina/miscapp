
using System;

namespace Artemis.Core
{
	
	
	public interface IPlayer
	{
		void Play (MediaStream stream);
		void Stop ();
		void Pause ();
		void Seek (double position);
		
		PlayerEngineType Engine { get; }
		MediaStream Current { get; }
	}
}
