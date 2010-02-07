
using System;
using Artemis.UI;

namespace Artemis.Addins
{
	
	
	public abstract class Addin
	{
		private string _name;
		private string _version;
		private string _author;
		private string _description;
		
		public Addin ()
		{
		}
		
		public virtual void OnInit (MediaEnv env)
		{
		}
		
		public virtual void OnTerminate (MediaEnv env)
		{
		}
		
		public string Name {
			get { return _name; }
			protected set { _name = value; }
		}
		
		public string Version {
			get { return _version; }
			protected set { _version = value; }
		}
		
		public string Author {
			get { return _author; }
			protected set { _author = value; }
		}
		
		public string Description {
			get { return _description; }
			protected set { _description = value; }
		}
	}
}
