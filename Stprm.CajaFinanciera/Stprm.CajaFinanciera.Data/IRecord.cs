
using System;

namespace Stprm.CajaFinanciera.Data
{

	public interface IRecord
	{
		// FIXME. Implement Delete operation
		//void Delete ();
		/*
		
		*/
		bool Update();
		bool Save();
		bool Exists();

		Database Db { get; }
		RecordType Type { get; }
	}
}
