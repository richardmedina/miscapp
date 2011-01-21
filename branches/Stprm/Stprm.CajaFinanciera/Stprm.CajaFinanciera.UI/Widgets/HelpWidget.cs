
using System;
using Gtk;
using Gdk;

using RickiLib.Widgets;
namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class HelpWidget : EventBox
	{
		public HelpWidget ()
		{
			ModifyBg (StateType.Normal, new Gdk.Color (255, 255, 255));
			
			Add(Factory.Label ("Próximamente..."));
		}
	}
}
