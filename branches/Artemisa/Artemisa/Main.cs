using System;
using Gtk;
using Artemisa.UI;


namespace Artemisa
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init ();
			MainWindow window = new MainWindow ();
			window.ShowAll ();
			Application.Run ();
		}
	}
}