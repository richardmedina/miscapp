
using System;
using Gtk;

using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class GeneradorDescuentosView : Gtk.TreeView
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
		
		public GeneradorDescuentosView ()
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
				
				if (i == 0) {
					_renders [i].Editable = true;
					_renders [i].Edited += HandleEdited;
				}
				
				if (i == 5) {
					_renders [i].Editable = true;
					_renders [i].Edited += HandleEditedValueCol5;
				}
				
				if (i == 6) {
					_renders [i].Editable = true;
					_renders [i].Edited += HandleEditedValueCol6;
				}
				
				if (i == 7) {
					_renders [i].Editable = true;
					_renders [i].Edited += HandleEditedText;
				}
				
				_columns [i] = new TreeViewColumn (_columns_str [i], _renders [i], "text", i);
				AppendColumn (_columns [i]);
			}
			
			AppendRow ();
		}
		
		private void HandleEditedText (object sender, EditedArgs args)
		{
			TreeIter iter ;
			
			if (args.NewText.Length == 1)
				if (_model.GetIterFromString (out iter, args.Path)) {
					_model.SetValue (iter, 7, args.NewText);
				}
		}
		
		private void HandleEditedValueCol5 (object sender, EditedArgs args)
		{
			TreeIter iter;
			decimal val;
			
				
			if (decimal.TryParse (args.NewText, out val))
				if (_model.GetIterFromString (out iter, args.Path)) {
					_model.SetValue (iter, 5, val.ToString ("0.00"));
				}
		}
		
		private void HandleEditedValueCol6 (object sender, EditedArgs args)
		{
			TreeIter iter;
			decimal val;
			
				
			if (decimal.TryParse (args.NewText, out val))
				if (_model.GetIterFromString (out iter, args.Path)) {
					_model.SetValue (iter, 6, val.ToString ("0.00"));
				}
		}

		private void HandleEdited (object o, EditedArgs args)
		{
			TreeIter iter ;
			
			if (_model.GetIterFromString (out iter, args.Path)) {
				UpdateIterFromFicha (iter, args.NewText);
			}
		}
		
		public void RemoveSelected ()
		{
			TreeIter iter;
			
			if(Selection.GetSelected (out iter)) {
				_model.Remove (ref iter);
				AppendRowIfLastEmpty ();
			}
		}
		
		public void UpdateIterFromFicha (Gtk.TreeIter iter, string ficha)
		{
			Console.WriteLine ("UpdateFromFicha");
			Employee employee = new Employee(Globals.Db);
			employee.Id = ficha;
			if (employee.Update ()) {
				UpdateIter (iter, employee.Id, string.Empty, string.Empty, employee.GetFullName (), 0m, 0m, 0m, "A");
			}
			Console.WriteLine ("UpdateIter.End");
		}
		
		public void AppendPrestamo (Prestamo prestamo)
		{
			Employee employee = new Employee (Globals.Db);
			employee.InternalId = prestamo.TrabajadorInternalId;
			
			if (employee.UpdateFromInternalId ()) {
			
				Gtk.TreeIter iter = AppendRowIfLastEmpty ();
				
				UpdateIter (iter,
				            employee.Id,
							prestamo.Pagare,
							prestamo.Cheque + DateTime.Today.ToString ("yyyy"),
							employee.GetFullName (),
							prestamo.Saldo,
							(prestamo.Capital + prestamo.Interes) / prestamo.NumPagos,
							(((prestamo.Capital + prestamo.Interes) / prestamo.NumPagos) / Globals.DiasCatorcenal),
							"A");
				
				AppendRowIfLastEmpty ();
			}
		}
		
		public void UpdateIter (Gtk.TreeIter iter, string ficha, string pagare,
		                        string folio, string nombre, 
		                        decimal saldo, decimal desc_catorcenal, 
		                        decimal desc_diario, string status)
		{
			Console.WriteLine ("UpdateIter");
			if (_model.IterIsValid (iter)) {
				_model.SetValue (iter, 0, ficha);
				_model.SetValue (iter, 1, pagare);
				_model.SetValue (iter, 2, folio);
				_model.SetValue (iter, 3, nombre);
				_model.SetValue (iter, 4, saldo.ToString ("0.00"));
				_model.SetValue (iter, 5, desc_catorcenal.ToString ("0.00"));
				_model.SetValue (iter, 6, desc_diario.ToString ("0.00"));
				_model.SetValue (iter, 7, status);
			}
			
			AppendRowIfLastEmpty ();
			
			Console.WriteLine ("UpdateIter.End");
		}
		
		public bool IterGetLast (out Gtk.TreeIter last_iter)
		{
			Console.WriteLine ("IterGetLast");
			bool result = true;
			Gtk.TreeIter iter = TreeIter.Zero;
			last_iter = TreeIter.Zero;
			
			if (_model.GetIterFirst (out iter)) {
				last_iter = iter;
				do {
					last_iter = iter;
				}while (_model.IterNext (ref iter));
			} else result = false;
			Console.WriteLine ("IterGetLast.End");
			return result;
		}
		
		public bool IterIsEmpty (Gtk.TreeIter iter)
		{
			
			bool result = true;
			for (int i = 0; i < _columns_str.Length; i ++) {
				
				if (_model.IterIsValid (iter)) {
					string field = (string) _model.GetValue (iter, i);
					
					if (field.Length > 0)
						result = false;
				}
			}
			
			return result;
		}
		
		public Gtk.TreeIter AppendRow ()
		{
			string [] fields = new string [_columns_str.Length];
			
			for (int i = 0; i < fields.Length; i ++)
				fields [i] = string.Empty;
			
			return _model.AppendValues (fields);
		}
		
		public Gtk.TreeIter AppendRowIfLastEmpty ()
		{
			Gtk.TreeIter iter;
			if (IterGetLast (out iter)) {
				if (!IterIsEmpty (iter)) {
			    	return AppendRow ();
				}
			} else return AppendRow ();
			
			IterGetLast (out iter);
			
			return iter;
		}
	}
}
