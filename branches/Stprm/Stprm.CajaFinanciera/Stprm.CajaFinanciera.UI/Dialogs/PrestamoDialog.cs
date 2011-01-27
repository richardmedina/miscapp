
using System;
using Stprm.CajaFinanciera.Data;
using Stprm.CajaFinanciera.UI.Widgets;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Dialogs
{


	public class PrestamoDialog : CustomDialog
	{
		private Gtk.Notebook _notebook;
		private PrestamoDetallesWidget _pdw_prestamo;
		private PrestamoMovimientosListView _lv_movimientos;
		
		public PrestamoDialog ()
		{
			Title = Globals.FormatWindowTitle ("Prestamo");
			_pdw_prestamo = new PrestamoDetallesWidget ();
			_lv_movimientos = new PrestamoMovimientosListView ();
			
			_notebook = new Notebook ();
			_notebook.AppendPage (_pdw_prestamo, new Label ("Detalles"));
			
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_lv_movimientos);
			
			_notebook.AppendPage (scroll, new Label ("Movimientos"));
			
			VBox.PackStart (_notebook);
			
			ShowAll ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		protected override bool OnValidate (out string message)
		{
			Prestamo prestamo = GetAsPrestamo ();
			bool result = base.OnValidate (out message);
			
			Console.WriteLine ("InternalID = {0}", prestamo.TrabajadorInternalId);
			
			Employee employee = new Employee (Globals.Db);
			employee.InternalId = prestamo.TrabajadorInternalId;
			
			if (prestamo.TrabajadorInternalId == 0 || !employee.UpdateFromInternalId ()) {
				message = "Verifique la ficha del trabajador";
				result = false;
			}
			
			else if (prestamo.Cheque.Trim () == string.Empty) {
				message = "Por favor establezca el numero de cheque";
				result = false;
			}
			
			else if (prestamo.Pagare.Trim () == string.Empty) {
				message = "Por favor establezca el numero de pagaré";
				result = false;
			}
			
			else if (prestamo.NumPagos == 0) {
				message = "Por favor establezca el plazo";
				result = false;
			}
			
			else if (prestamo.Capital == 0) {
				message = "Por favor establezca el monto del préstamo";
				result = false;
			}
			
			else if (prestamo.Id == 0) {
				if (Prestamo.ChequeExiste (Globals.Db, prestamo.Cheque, Globals.CuentaActual)) {
					message = "El cheque ya está registrado en la cuenta seleccionada";
					result = false;
				}
			
				else if (Prestamo.PagareExiste (Globals.Db, prestamo.Pagare, prestamo.Fecha.Year)) {
					message = "El pagaré existe, por favor verifiquelo";
					result = false;
				}
			}
			
			Console.WriteLine ("Result: {0}",result);
			return result;
		}
		
		
		public void UpdateFromPrestamo (Prestamo prestamo)
		{
			_pdw_prestamo.UpdateFromPrestamo (prestamo);
			_lv_movimientos.LoadMovimientos (prestamo);
		}
		
		public Prestamo GetAsPrestamo ()
		{
			return _pdw_prestamo.GetAsPrestamo ();
		}
	}
}
