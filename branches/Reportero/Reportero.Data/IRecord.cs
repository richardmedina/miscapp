
using System;

namespace Reportero.Data
{
	
	
	public interface IRecord
	{
		// FIXME. Implement Delete operation
		//void Delete ();
		bool Update ();
		void Save ();
		bool Exists ();
		
		Database Db { get; }
		RecordType Type { get; }
	}
}
