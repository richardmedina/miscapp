
using System;
using Gtk;
using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class CuentaBancariaCombo : Gtk.ComboBox
	{
		private Gtk.CellRendererText _render;
		private Gtk.ListStore _model;
		
		public CuentaBancariaCombo ()
		{
			_render = new CellRendererText ();
			
			_model = new Gtk.ListStore (typeof (CuentaBancaria),
			                            typeof(string));
			
			PackStart (_render, true);
			AddAttribute (_render, "text", 1);
			
			Model = _model;
			
			
		}
		
		public void Populate (CuentaBancariaCollection cuentas) 
		{
			_model.Clear ();
			foreach (CuentaBancaria cuenta in cuentas) {
					_model.AppendValues (cuenta, string.Format ("{0} ({1})",
				                                    cuenta.Banco, cuenta.Cuenta));
			}
		}
		
		public bool GetSelected (out CuentaBancaria cuenta)
		{
			cuenta = null;
			Gtk.TreeIter iter;
			
			
			if (this.GetActiveIter (out iter)) {
				cuenta = (CuentaBancaria) _model.GetValue (iter, 0);
				return true;
			}
			
			return false;
		}
		
		public void SelectCuenta (CuentaBancaria cuenta)
		{
		
			Gtk.TreeIter iter;
			
			if (_model.GetIterFirst (out iter))
				do {
					CuentaBancaria cuenta_actual = (CuentaBancaria) _model.GetValue (iter, 0);
					if (cuenta.Id == cuenta_actual.Id) {
						SetActiveIter (iter);
						break;
					}
				} while (_model.IterNext (ref iter));
				
		}
	}
}
