
using System;

namespace Artemisa.Drawing
{
	
	
	public interface IShape
	{
		void Draw ();
		event EventHandler Configure;
		event EventHandler Expose;
	}
}
