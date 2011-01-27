
using System;
using Gtk;

using Stprm.CajaFinanciera.Data;
namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class PrestamoContextMenu : Gtk.Menu
	{
		private ImageMenuItem _itm_informacion;
		private ImageMenuItem _itm_abonar;
		private ImageMenuItem _itm_suspender;
		private ImageMenuItem _itm_reactivar;
		
		public PrestamoContextMenu ()
		{
			
			_itm_abonar = new ImageMenuItem ("Abonar...");
			_itm_suspender = new ImageMenuItem ("Suspender");
			_itm_reactivar = new ImageMenuItem ("Reactivar");
			_itm_informacion = new ImageMenuItem ("Información...");
			_itm_informacion.Image = Image.NewFromIconName (Stock.Info, IconSize.Menu);
			
			
			Append (_itm_abonar);
			Append (new SeparatorMenuItem ());
			Append (_itm_suspender);
			Append (_itm_reactivar);
			Append (new SeparatorMenuItem ());
			Append (_itm_informacion);
			ShowAll ();
		}
		
		public void Sensitivizar (Prestamo prestamo)
		{
			ItemAbonar.Sensitive = true;
			ItemSuspender.Sensitive = true;
			ItemReactivar.Sensitive = true;
			ItemInformacion.Sensitive = true;
			
			if (prestamo.Status == OperacionFinancieraEstado.Suspendido)
				_itm_suspender.Sensitive = false;
			else
				_itm_reactivar.Sensitive = false;
			
		}
		
		public ImageMenuItem ItemAbonar {
			get { return _itm_abonar; }
		}
		
		public ImageMenuItem ItemSuspender {
			get { return _itm_suspender; }
		}
		
		public ImageMenuItem ItemReactivar {
			get { return _itm_reactivar; }
		}
		
		public ImageMenuItem ItemInformacion {
			get { return _itm_informacion; }
		}
	}
}
