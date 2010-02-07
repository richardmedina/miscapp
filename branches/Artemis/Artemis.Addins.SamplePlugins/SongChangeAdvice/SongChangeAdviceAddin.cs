
using System;
using Artemis.Addins;
using Artemis.Core;
using Artemis.UI;
using Artemis.UI.Dialogs;
using Gtk;

namespace Artemis.Addins.Samples
{
	
	
	public class SongChangeAdviceAddin : Addin
	{
		private MediaEnv _env;
		
		public SongChangeAdviceAddin ()
		{
			Name = "Song Change Advice";
			Author = "Ricardo Medina <ricki9@gmail.com>";
			Description = "throws a Message dialog displaying information of playing song";
			Version = "0.1a";
		}
		
		public override void OnInit (Artemis.UI.MediaEnv env)
		{
			_env = env;
			_env.Player.StateChanged += envPlayerStateChanged;
			base.OnInit (env);
		}

		private void envPlayerStateChanged (object sender, EventArgs args)
		{
			if (_env.Player.State == PlayerState.Playing) {
				string info = string.Format ("Playing\n<b>{0}</b>", Env.Player.Current.Name);
			//	Console.WriteLine (info);
			
			
				MessageDialog dialog = new MessageDialog (Env.MainWindow,
					DialogFlags.DestroyWithParent,
					MessageType.Info,
					ButtonsType.Ok,
					true,
					info);
			
				dialog.ShowAll ();
				GLib.Timeout.Add (2000, delegate { dialog.Hide (); dialog.Destroy (); dialog.Destroy (); return false; });
			}
		}
		
		public MediaEnv Env {
			get { return _env; }
			set { _env = value; }
		}

	}
}
