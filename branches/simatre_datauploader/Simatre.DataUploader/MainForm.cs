using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Simatre.DataUploader
{
	public class MainForm : Form
	{

		private Label label;
		private TextBox textbox;

		private Timer timer;
		private BackgroundWorker worker;

		public MainForm ()
		{
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			Size = new System.Drawing.Size (640, 480);

			Text = "Airpointer Sinchronizer";


			timer = new Timer ();
			timer.Interval = 3000;
			timer.Tick += HandleTick;

			label = new Label ();
			label.Text = "Inicializando...";
			label.Location = new Point (10, 40);

			textbox = new TextBox ();
			textbox.ReadOnly = true;
			textbox.Multiline = true;
			textbox.WordWrap = false;
			textbox.ScrollBars = ScrollBars.Both;

			textbox.Location = new Point (10, 40);
			textbox.Size = new System.Drawing.Size (Size.Width - 20, Size.Height - 40);


			Controls.Add (textbox);


			textbox.Select (0, 0);


			worker = new BackgroundWorker ();
			worker.DoWork += delegate(object sender, DoWorkEventArgs e) {
				Console.WriteLine ("Iniciando conexion");
				Recordum recordum = new Recordum();
				recordum.Username = "admin";
				recordum.Password = "1AQuality";
				recordum.Start ();
				Console.WriteLine ("Hecho");
			};

			worker.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e) {
				PushMessage ("El proceso ha terminado");

			};

			worker.RunWorkerAsync ();

			//timer.Start ();
		}

		void HandleTick (object sender, EventArgs e)
		{


			PushMessage ("Timer Tick...Stops");
			//timer.Stop ();
			PushMessage ("textbox");
		}

		public void PushMessage (string message)
		{
			textbox.Text = message + "\n\r" + textbox.Text;
		}
	}
}

