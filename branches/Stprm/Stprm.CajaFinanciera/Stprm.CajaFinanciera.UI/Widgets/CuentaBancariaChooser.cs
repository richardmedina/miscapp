
using System;
using Gtk;

using Stprm.CajaFinanciera.Data;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class CuentaBancariaChooser : Gtk.HBox
	{
		private CuentaBancariaCombo _combo;
		private Gtk.Button _button_block;
			
		public CuentaBancariaChooser () : base (false, 5)
		{
			_combo = new CuentaBancariaCombo ();
			_combo.Changed += _combo_Changed;
			
			_button_block = new Button (Stock.Refresh);
			_button_block.Label = string.Empty;
			_button_block.Image = Image.NewFromIconName (Stock.Refresh, IconSize.Button);
			_button_block.Clicked += button_block_Click;
			
			PackStart (_combo, false, false, 0);
			PackStart (_button_block, false, false, 0);
		}
		
		private void _combo_Changed (object sender, EventArgs args)
		{
			CuentaBancaria cuenta;
			
			if (Combo.GetSelected (out cuenta))
				Globals.CuentaActual = cuenta;
			
			_combo.Sensitive = false;
		}
		
		private void button_block_Click (object seder, EventArgs args)
		{
			ToggleSelectable ();
		}
		
		public void ToggleSelectable ()
		{
			_combo.Sensitive = !_combo.Sensitive;
		}
		
		public CuentaBancariaCombo Combo {
			get { return _combo; }	
		}
		
		public Button BlockButton {
			get { return _button_block; }
		}
	}
}
