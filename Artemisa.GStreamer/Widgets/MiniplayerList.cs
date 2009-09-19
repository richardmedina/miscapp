
using System;
using Gtk;

namespace Widgets
{
	
	
	public class MiniplayerList : Gtk.TreeView
	{
		private Gtk.ListStore _store;
		
		public MiniplayerList()
		{
			_store = new ListStore (typeof(String));
			
			Model = _store;
			
			AppendColumn ("Songs", new CellRendererText (), "text", 0);
		}
		
		public void Append (string filename)
		{
			_store.AppendValues (filename);
		}
		
		public void appendMany (params string [] filenames)
		{
			foreach (string filename in filenames)
				Append (filename);
		}
	}
}
