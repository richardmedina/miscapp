
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
			                        typeof (string), // STA
			                        typeof (Prestamo)); // Prestamo al que pertenece el descuento
			
			Model = _model;
			_renders = new CellRendererText[_columns_str.Length];
			_columns = new TreeViewColumn [_columns_str.Length];
			
			for (int i = 0; i < _columns_str.Length; i ++) {
				_renders [i] = new CellRendererText ();
				
				if (i == 0) {
					_renders [i].Editable = true;
					_renders [i].Edited += HandleEdited;
				}
				
				if (i == 1) {
					_renders [i].Editable = true;
					_renders [i].Edited += HandleEditedValueCol1;
				}
				
				if (i == 4) {
					_renders [i].Editable = true;
					_renders [i].Edited += HandleEditedValueCol4;
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
		
		public void ChangePrestamosStatus ()
		{
			Gtk.TreeIter iter;
			
			if (_model.GetIterFirst (out iter))
				do {
					Prestamo prestamo = (Prestamo) _model.GetValue (iter, 8);
					if (prestamo.Id > 0) {
						if ((string) _model.GetValue (iter, 7) == "B") {
							prestamo.Suspender ();
						}
						else {
							if (prestamo.Status == OperacionFinancieraEstado.Suspendido)
								prestamo.Reactivar (OperacionFinancieraEstado.DescuentoSinCobro);
						
							prestamo.Status = OperacionFinancieraEstado.DescuentoSinCobro;
						}
						Console.WriteLine ("Salvando prestamo...");
						prestamo.Folio = (string) _model.GetValue (iter, 2);
						prestamo.Save ();
						Globals.MainWindow.PrestamosView.UpdatePrestamo (prestamo);
					}
				} while (_model.IterNext (ref iter));
		}
		
		private void HandleEditedText (object sender, EditedArgs args)
		{
			TreeIter iter ;
			
			if (args.NewText.ToUpper() == "A" || args.NewText.ToUpper () == "B") {
				if (_model.GetIterFromString (out iter, args.Path)) {
					if (args.NewText.ToUpper () == "B") {
						_model.SetValue (iter, 6, "0.00");
						_model.SetValue (iter, 5, "0.00");
						_model.SetValue (iter, 4, "0.00");
						_model.SetValue (iter, 7, args.NewText.ToUpper ());
					} else {
					
						Prestamo prestamo;
						string ficha = (string) _model.GetValue (iter, 0);
				
						Employee employee = new Employee (Globals.Db);
						employee.Id = ficha;
				
						if (employee.Update ()) {
							if (Prestamo.GetFromPagare (Globals.Db, employee, (string) _model.GetValue (iter, 1), out prestamo)) {
								UpdateIter (iter, employee, prestamo);
							}
						}
						_model.SetValue (iter, 7, args.NewText.ToUpper ());
					}
				}
			}
				//args.NewText.ToUpper () == "A";
		}
		
		private void HandleEdited (object o, EditedArgs args)
		{
			TreeIter iter ;
			
			if (_model.GetIterFromString (out iter, args.Path)) {
				UpdateIterFromFicha (iter, args.NewText);
			}
		}
		
		private void HandleEditedValueCol1 (object sender, EditedArgs args)
		{
			Gtk.TreeIter iter;
			
			if (_model.GetIterFromString (out iter, args.Path)) {
				string ficha = (string) _model.GetValue (iter, 0);
				
				Employee employee = new Employee (Globals.Db);
				employee.Id = ficha;
				
				if (employee.Update ()) {
					Prestamo prestamo;
					
					if (employee.GetPrestamoFromPagare (employee, args.NewText, out prestamo))
						UpdateIter (iter, employee, prestamo);
					
					/*foreach (Prestamo prestamo in employee.GetPrestamos ()) {
						if (prestamo.Pagare == args.NewText) {
							//_model.SetValue (iter, 1, args.NewText);
							UpdateIter (iter, employee, prestamo);
							break;
						}
						
					}*/
				}
			}
		}
		// DescuentoCatorcenal
		private void HandleEditedValueCol4 (object sender, EditedArgs args)
		{
			TreeIter iter;
			decimal val;
			
				
			if (decimal.TryParse (args.NewText, out val))
				if (_model.GetIterFromString (out iter, args.Path)) {
					_model.SetValue (iter, 4, val.ToString ("0.00"));
				}
		}
		
		// DescuentoDiario
		private void HandleEditedValueCol5 (object sender, EditedArgs args)
		{
			TreeIter iter;
			decimal val;
			
				
			if (decimal.TryParse (args.NewText, out val))
				if (_model.GetIterFromString (out iter, args.Path)) {
					if (val == 0) {
						_model.SetValue (iter, 7, "B");
					}
					_model.SetValue (iter, 5, val.ToString ("0.00"));
					_model.SetValue (iter, 6, (val > 0 ? (val / 14) : val).ToString ("0.00")); 
				}
		}
		
		private void HandleEditedValueCol6 (object sender, EditedArgs args)
		{
			TreeIter iter;
			decimal val;
			
				
			if (decimal.TryParse (args.NewText, out val))
				if (_model.GetIterFromString (out iter, args.Path)) {
					_model.SetValue (iter, 6, val.ToString ("0.00"));
					_model.SetValue (iter, 5, (val * 14).ToString ("0.00"));
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
				PrestamoCollection prestamos = employee.GetPrestamos ();
				//string pagare = prestamos.Count == 1? prestamos [0].Pagare : string.Empty;
				
				//Console.WriteLine (prestamos.Count + " " + employee.GetFullName ());
				if (prestamos.Count == 0) {
					MessageDialog dialog = new MessageDialog (Globals.MainWindow,
					                                          DialogFlags.Modal,
					                                          MessageType.Error,
					                                          ButtonsType.Ok,
					                                          "El trabajador no tiene adeudos");
					dialog.Run ();
					dialog.Destroy ();
				}
				
				if (prestamos.Count == 1)
					UpdateIter (iter, employee, prestamos [0]);
				
				else if (prestamos.Count > 1) {
					UpdateIter (iter, employee, null);
				}
			}
			Console.WriteLine ("UpdateIter.End");
		}
		
		public void AppendPrestamo (Prestamo prestamo)
		{
			Employee employee = new Employee (Globals.Db);
			employee.InternalId = prestamo.TrabajadorInternalId;
			
			if (employee.UpdateFromInternalId ()) {
			
				Gtk.TreeIter iter = AppendRowIfLastEmpty ();
				
				UpdateIter (iter, employee, prestamo);
				
				AppendRowIfLastEmpty ();
			}
		}
		
		public void UpdateIter (Gtk.TreeIter iter, Employee employee, Prestamo prestamo)
		{
			SetPrestamoToIter (iter, prestamo);
			
			if (prestamo == null) {
				UpdateIter (iter, employee.Id, employee.GetFullName (),
				            string.Empty, string.Empty,
				            0m, 0m, 0m, "A");
				return;
			}
			
			decimal descuento_catorcenal = (prestamo.Capital + prestamo.Interes);
			decimal descuento_diario = 0;
			string folio = prestamo.Folio;
			
			Console.WriteLine ("Prestamo.Folio: {0}", prestamo.Folio);
			
			if (folio == string.Empty)
				folio = prestamo.Pagare + prestamo.Fecha.Year.ToString ("0000");
			else {
				folio = prestamo.Folio;
			}
			
			if (prestamo.Status == OperacionFinancieraEstado.Suspendido) {
					string anio = folio.Substring (folio.Length - 4);
					Console.WriteLine ("Anio : {0}", anio);
			}
			
			if (descuento_catorcenal > 0 && prestamo.NumPagos > 0) {
				descuento_catorcenal /= prestamo.NumPagos;
				
				if (descuento_catorcenal > prestamo.Saldo)
					descuento_catorcenal = prestamo.Saldo;
				if (descuento_catorcenal > 0)
					descuento_diario = descuento_catorcenal / Globals.DiasCatorcenal;
			}
			
			UpdateIter (iter, employee.Id, employee.GetFullName (), 
			            prestamo.Pagare, folio, 
			            prestamo.Saldo, descuento_catorcenal,
			            descuento_diario, "A");
		}
		
		public void SetPrestamoToIter (Gtk.TreeIter iter, Prestamo prestamo)
		{
			if (prestamo == null) {
				prestamo = new Prestamo (Globals.Db);
				prestamo.Id = 0;
			}
			
			_model.SetValue (iter, 8, prestamo);
		}
		
		public void UpdateIter (Gtk.TreeIter iter, string ficha, string nombre, 
								string pagare, string folio, 
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
			
			Gtk.TreeIter iter = _model.AppendValues (fields);
			SetPrestamoToIter (iter, null);
			return iter;
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
		
		public DescuentoMovimientoCollection GetDescuentoMovimientos (Categoria categoria)
		{
			DescuentoMovimientoCollection movs = new DescuentoMovimientoCollection ();
			
			TreeIter iter;
			
			if (_model.GetIterFirst (out iter))
				do {
					if (!IterIsEmpty (iter)) {
						Prestamo prestamo = (Prestamo) _model.GetValue (iter, 8);
						DescuentoMovimiento mov = new DescuentoMovimiento (Globals.Db);
						string [] row = GetRow (iter);
						
						foreach (string field in row)
							Console.Write ("'{0}' ", field);
						Console.WriteLine ();
					
						Employee employee = new Employee (Globals.Db);
						employee.Id = row [0];
						if (employee.Update ()) {
							mov.TrabajadorInternalId = employee.InternalId;
						}
						mov.PrestamoId = prestamo.Id;
						mov.Folio = row [2];
						
						mov.Importe = decimal.Parse (row [5]);
						
					if (categoria.Concepto == "1244")
							mov.Importe = decimal.Parse (row [4]);
						
						mov.DescuentoDiario = decimal.Parse (row [6]);
						movs.Add (mov);
				}
			} while (_model.IterNext (ref iter));
			Console.WriteLine ("Regresando movs...");
			return movs;
		}
				
		public string [] GetRow (Gtk.TreeIter iter)
		{
			string [] row = new string [Columns.Length];
			
			for (int i = 0; i < row.Length; i ++) {
				row [i] = (string) _model.GetValue (iter, i);
			}
			
			return row;
		}
		
		protected override bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			if (evnt.Key == Gdk.Key.Delete)
				RemoveSelected ();
			
			return base.OnKeyPressEvent (evnt);
		}

	}
}
