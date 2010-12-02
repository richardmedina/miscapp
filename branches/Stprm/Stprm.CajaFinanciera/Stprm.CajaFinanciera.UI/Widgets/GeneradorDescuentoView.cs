
using System;
using Gtk;

using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class GeneradorDescuentoView : Gtk.TreeView
	{
		private Gtk.ListStore _model;
		
		private TreeViewColumn [] _columns;
		private CellRendererText [] _renders;
		
		private static string [] _columns_str = {
			"Ficha",
			"Pagare",
			"Folio",
			"nombre",
			"Saldo",
			"Desc.Ctorcenal",
			"Desc.Diario",
			"Sta"
		};
		
		public GeneradorDescuentoView ()
		{
			RulesHint = true;
			_model = new ListStore (typeof (string), // ficha
			                        typeof (string), // pagare
			                        typeof (string), // folio
			                        typeof (string), // nombre
			                        typeof (string), // saldo
			                        typeof (string), // desc.catorcenal
			                        typeof (string), // desc.diario
			                        typeof (string));// STA
			
			Model = _model;
			_renders = new CellRendererText[_columns_str.Length];
			_columns = new TreeViewColumn [_columns_str.Length];
			
			for (int i = 0; i < _columns_str.Length; i ++) {
				_renders [i] = new CellRendererText ();
				
				if (i == 0)// || i > 4)
					_renders [i].Editable = true;
				if (i == 0) {
					_renders [i].Edited += HandleEdited;
				}
				
				_columns [i] = new TreeViewColumn (_columns_str [i], _renders [i], "text", i);
				AppendColumn (_columns [i]);
			}
			
			
			_model.AppendValues (string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
		}

		private void HandleEdited (object o, EditedArgs args)
		{
			TreeIter iter ;
			
			if (_model.GetIterFromString (out iter, args.Path)) {
				UpdateIterFromFicha (iter, args.NewText);
			}
		}
		
		public void UpdateIterFromFicha (Gtk.TreeIter iter, string ficha)
		{
			Employee employee = new Employee(Globals.Db);
			employee.Id = ficha;
			if (employee.Update ()) {
				_model.SetValue (iter, 0, ficha);
				_model.SetValue (iter, 	3, employee.GetFullName ());
				_model.SetValue (iter, 7, "A");
			}
		}
		
		public void UpdateIterFromPrestamo (Prestamo prestamo)
		{
			
		}
	}
}
