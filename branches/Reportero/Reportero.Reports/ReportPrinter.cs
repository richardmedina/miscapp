
using System;
using Gtk;
using Cairo;

namespace Reportero.Reports
{
	
	
	public class ReportPrinter : Gtk.PrintOperation
	{
		private Cairo.Surface _surface;
		
		public ReportPrinter (Cairo.Surface surface)
		{
			_surface = surface;
		}
		
		protected override void OnBeginPrint (Gtk.PrintContext context)
		{
			base.OnBeginPrint (context);
			NPages = 10;
		}
		
		protected override void OnDrawPage (Gtk.PrintContext context, int page_nr)
		{
			base.OnDrawPage (context, page_nr);
			
			_surface.Show (context.CairoContext, 0, 0);
		}
		
		protected override void OnEndPrint (Gtk.PrintContext context)
		{
			base.OnEndPrint (context);
		}
		
		public Surface Surface {
			get { return _surface; }
		}
	}
}
