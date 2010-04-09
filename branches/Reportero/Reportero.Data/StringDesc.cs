
using System;
using System.Collections.Generic;
using System.IO;

namespace Reportero.Data
{
	
	
	public class StringDesc : Dictionary<string, string>
	{
		public void Load (string filename)
		{	
			try {
				using (StreamReader reader = new StreamReader (filename)) {
					string line;
					
					for (line = reader.ReadLine (); 
						line != null; 
						line = reader.ReadLine ()) {
						string [] parts = line.Split (":".ToCharArray ());
						Add (parts [0], parts [1]);
					}
				}
			} catch (Exception e) { Console.WriteLine (e); }
		}
	}
}
