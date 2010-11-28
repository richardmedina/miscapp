
using System;
using Gtk;
using Gdk;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class CurrencyEntry : Gtk.Entry
	{
		
		private decimal _value;

		public CurrencyEntry ()
		{
			
		}
		
		protected override void OnFocusGrabbed ()
		{
			Text = Text.Replace ("$", string.Empty).Replace (",", string.Empty).Replace ("(", string.Empty).Replace (")", string.Empty);
			
			base.OnFocusGrabbed ();
		}
		
		protected override bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			
			if ((evnt.KeyValue > 47 && evnt.KeyValue < 58) 
			    || evnt.Key == Gdk.Key.BackSpace || evnt.Key == Gdk.Key.Delete 
			    || evnt.Key == Gdk.Key.End || evnt.Key == Gdk.Key.Home
			    || evnt.Key == Gdk.Key.KP_Right || evnt.Key == Gdk.Key.KP_Left || evnt.Key == Gdk.Key.KP_Up 
			    || evnt.Key == Gdk.Key.KP_Down || evnt.Key == Gdk.Key.Left || evnt.Key == Gdk.Key.Right
			    || (evnt.Key >= Gdk.Key.KP_0 && evnt.Key <= Gdk.Key.KP_9)
			    || (evnt.KeyValue == 46 && Text.IndexOf (".") == -1)
			    ) 
				return base.OnKeyPressEvent (evnt);
			
			return false;
		}
		
		protected override bool OnFocusOutEvent (Gdk.EventFocus evnt)
		{
			double val;
			
			if (double.TryParse (Text, out val)) {
				Text = string.Format ("{0:C}", val);
			}
			
			return base.OnFocusOutEvent (evnt);
		}
		
		public decimal Value {
			get { return _value; }
			set { 
				_value = value; 
				Text = string.Format ("{0:C}", _value);
			}
		}
	}
}
