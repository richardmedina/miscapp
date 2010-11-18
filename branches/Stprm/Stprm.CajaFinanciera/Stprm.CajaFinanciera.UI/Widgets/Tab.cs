
using System;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class Tab : Gtk.Notebook
	{
		private TabPageCollection _tabpages;
		
		public Tab ()
		{
			_tabpages = new TabPageCollection ();
		}
		
		public void Append (TabPage tabpage)
		{
				
		}
		
		public TabPageCollection TabPages {
			get { return _tabpages; }
		}
	}
}
