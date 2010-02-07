
using System;
using Artemis.Addins;
using Artemis.UI;

namespace Artemis.Addins.Samples
{
	
	
	public class NotificationAddin : Addin
	{
		NotificationIcon _icon;
		
		public NotificationAddin ()
		{
			Name = "NotificationIcon";
			Author = "Ricardo Medina <ricki9@gmail.com>";
			Description = "Notification Area Plugin shows an icon to hide and present Artemis Media Player";
			Version = "0.1a";
		}
		
		public override void OnInit (MediaEnv env)
		{
			_icon = new NotificationIcon (env.MainWindow);
		}
		
		public NotificationIcon Icon {
			get { return _icon; }
		}
	}
}
