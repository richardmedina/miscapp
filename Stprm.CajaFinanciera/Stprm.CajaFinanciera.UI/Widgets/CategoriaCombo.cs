
using System;
using Gtk;

using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class CategoriaCombo : Gtk.ComboBox
	{
		private CellRendererText _render;
		private Gtk.ListStore _model;
		
		public CategoriaCombo ()
		{
			_model = new ListStore (typeof (Categoria),
			                        typeof(string));
			
			_render = new CellRendererText ();
			PackStart (_render, true);
			
			AddAttribute (_render, "text", 1);
			
			Model = _model;
		}

		public bool GetSelected (out Categoria categoria)
		{
			categoria = null;
			Gtk.TreeIter iter;
			
			
			if (this.GetActiveIter (out iter)) {
				categoria = (Categoria) _model.GetValue (iter, 0);
				return true;
			}
			
			return false;
		}
	}
}
