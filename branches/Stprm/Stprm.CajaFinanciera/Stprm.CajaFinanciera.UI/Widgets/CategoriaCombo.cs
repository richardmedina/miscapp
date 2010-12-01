
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
		
		public void Populate () 
		{
			_model.Clear ();
			
			foreach (Categoria categoria in Categoria.GetCollection (Globals.Db)) {
				_model.AppendValues (categoria, string.Format ("{0} ({1})", categoria.Nombre, categoria.Id));
			}
		}
		
		public void Select (Categoria categoria)
		{
			Gtk.TreeIter iter;
			
			if (_model.GetIterFirst (out iter))
				do {
					Categoria categoria_actual = (Categoria) _model.GetValue (iter, 0);
					if (categoria.Id == categoria_actual.Id) {
						SetActiveIter (iter);
						break;
					}
				} while (_model.IterNext (ref iter));
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
