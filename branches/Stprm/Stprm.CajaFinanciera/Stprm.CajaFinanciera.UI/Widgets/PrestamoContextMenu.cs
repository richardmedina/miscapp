
using System;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class PrestamoContextMenu : Gtk.Menu
	{
		private ImageMenuItem _itm_informacion;
		private ImageMenuItem _itm_abonar;
		private ImageMenuItem _itm_suspender;
		
		public PrestamoContextMenu ()
		{
			
			_itm_abonar = new ImageMenuItem ("Abonar...");
			_itm_suspender = new ImageMenuItem ("Suspender");
			_itm_informacion = new ImageMenuItem ("Información...");
			_itm_informacion.Image = Image.NewFromIconName (Stock.Info, IconSize.Menu);
			
			Append (_itm_abonar);
			Append (_itm_suspender);
			Append (new SeparatorMenuItem ());
			Append (_itm_informacion);
			ShowAll ();
		}
		
		public ImageMenuItem ItemAbonar {
			get { return _itm_abonar; }
		}
		
		public ImageMenuItem ItemSuspender {
			get { return _itm_suspender; }
		}
		
		public ImageMenuItem ItemInformacion {
			get { return _itm_informacion; }
		}
	}
}
