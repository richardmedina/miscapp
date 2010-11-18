
using System;
using Gtk;
using RickiLib.Widgets;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class TrabajadoresCriteriosMenu : Gtk.Menu
	{
		public Gtk.RadioMenuItem _mi_todos;
		public Gtk.RadioMenuItem _mi_nombre;
		public Gtk.RadioMenuItem _mi_apepaterno;
		public Gtk.RadioMenuItem _mi_apematerno;
		
		public TrabajadoresCriteriosMenu (Gtk.AccelGroup accel) : base ()
		{
			_mi_todos = new RadioMenuItem ("Todos");
			_mi_nombre = new RadioMenuItem (_mi_todos, "Nombre");
			_mi_apepaterno = new RadioMenuItem (_mi_todos, "Apellido Paterno");
			_mi_apematerno = new RadioMenuItem (_mi_todos, "Apellido Materno");
			
			Insert (_mi_todos, -1);
			Insert (new SeparatorMenuItem (), -1);
			Insert (_mi_nombre, -1);
			Insert (_mi_apepaterno, -1);
			Insert (_mi_apematerno, -1);
			ShowAll ();
		}
	}
}
