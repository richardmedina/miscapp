
using System;

namespace Reportero.UI.Dialogs
{
	
	
	public class SettingsDialog : CustomDialog
	{
		
		public SettingsDialog ()
		{
			Title = AppSettings.GetFormatedTitle ("Configuración");
		}
	}
}
