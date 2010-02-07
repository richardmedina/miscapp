
using System;
using System.Threading;
using Artemis.UI;
using Artemis.Addins;
using Artemis.Addins.Samples;
using Gtk;

namespace Artemis
{
	
	
	public class MainClass
	{
	
		public static void Main ()
		{
			AddinCollection plugins = new AddinCollection ();
			plugins.Add (new NotificationAddin ());
			plugins.Add (new SongChangeAdviceAddin ());
			plugins.Add (new AddinManager (plugins));
			Application.Init ();
			MediaEnv env = new MediaEnv ();
			env.Init ();
			foreach (Addin addin in plugins) {
				Console.WriteLine ("Loading plugin.");
				Console.WriteLine ("\n\tName: {0}", addin.Name);
				Console.WriteLine ("\n\tAuthor: {0}", addin.Author);
				Console.WriteLine ("\n\tVersion: {0}", addin.Version);
				Console.WriteLine ("\n\tDescription: {0}", addin.Description);
				
				addin.OnInit (env);
			}
			env.MainWindow.Show ();
			Application.Run ();
		}
	
/*{
			GstPlayer player = new GstPlayer ();
			MediaStream stream = new MediaStream (@"/home/ricki/mp3.mp3");
			player.Play (stream);
			Console.WriteLine ("Playing...");
			Thread.Sleep (3000);
			player.Pause ();
			Console.WriteLine ("Paused");
			Thread.Sleep (3000);
			player.Play (stream);
			Console.WriteLine ("Playing...");
			Thread.Sleep (3000);
			player.Stop ();
			Console.WriteLine ("Stopped");
			Thread.Sleep (3000);
			player.Play (stream);
			Console.WriteLine ("Playing...");			
			Console.ReadLine ();
		}*/		
	}
}
