
using System;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class TabPage
	{
		public string Title;
		
		protected Gtk.Widget WidgetTitle;
		protected Gtk.Widget Widget;
		
		
		public Gtk.Widget GetTitleWidget ()
		{
			return WidgetTitle;
		}
		
		public Gtk.Widget GetWidget ()
		{
			return Widget;
		}
	}
}
