
using System;
using Gtk;

namespace Artemis.UI.Widgets
{
	
	
	public class SearchEntry : Gtk.Frame
	{
		private Gtk.Entry entry;
		private Gtk.Image imageFind;
		private Gtk.Image imageClear;
		private Gtk.EventBox eventbox;
		private Gtk.EventBox ebImageFind;
		private Gtk.EventBox ebImageClear;
		private Gtk.Menu menu;
		private Gdk.Color background;
		private Gdk.Cursor cursorHand;
		
		public SearchEntry () : 
			this (new Gtk.Image (Stock.Find, IconSize.Menu), null)
		{
		}
		
		public SearchEntry (Gtk.Image img, Gtk.Menu find_menu)
		{
			base.WidthRequest = 200;
			
			background = new Gdk.Color (255, 255, 255);
			cursorHand = new Gdk.Cursor (Gdk.CursorType.Hand2);

			entry = new Entry ();
			entry.HasFrame = false;
			entry.Changed += entry_Changed;
			
			imageFind = img;
			imageClear = new Image (Stock.Clear, IconSize.Menu);
			
			menu = find_menu;
			
			eventbox = new EventBox ();
			ebImageFind = new EventBox ();
			
			ebImageFind.ButtonPressEvent += ebImageFind_ButtonPressEvent;
			ebImageFind.EnterNotifyEvent += ebImageFind_EnterNotifyEvent;
			
			ebImageClear = new EventBox ();
			ebImageClear.EnterNotifyEvent += ebImageClear_EnterNotifyEvent;
			ebImageClear.ButtonPressEvent += ebImageClear_ButtonPressEvent;
			ebImageClear.Shown += ebImageClear_Shown;			
			eventbox.ModifyBg (
				entry.State, 
				background
			);
			
			ebImageFind.ModifyBg (
				entry.State, 
				background
			);
			
			ebImageClear.ModifyBg (
				entry.State, 
				background
			);

			ebImageFind.Add (imageFind);
			ebImageClear.Add (imageClear);
			
			Gtk.HBox hbox = new HBox (false, 0);
			hbox.PackStart (ebImageFind, false, false, 0);
			hbox.PackStart (entry, true, true, 0);
			hbox.PackStart (ebImageClear, false, false, 0);
			
			eventbox.Add (hbox);
			
			base.Add (eventbox);
		}
		
		protected virtual void OnImageFindClicked 
			(Gdk.EventButton event_button)
		{
			if (menu != null) {
				menu.Popup (
					null,
					null,
					menu_MenuPositionFunc,
					event_button.Button,
					event_button.Time
				);
			}

		}
						
		private void ebImageFind_ButtonPressEvent (object sender,
			ButtonPressEventArgs args)
		{
			OnImageFindClicked (args.Event);
		}
		
		private void menu_MenuPositionFunc (Menu menu, out int x, out int y, out bool push_in)
		{
			int bx, by;
			base.Parent.GdkWindow.GetRootOrigin (out bx, out by);
			
			x = bx + Allocation.X;
			y = by + Allocation.Y + Allocation.Height;
			
			push_in = false;
		}
		
		private void ebImageFind_EnterNotifyEvent (object sender,
			EnterNotifyEventArgs args)
		{
			ebImageFind.GdkWindow.Cursor = cursorHand;
		}
		
		private void ebImageClear_EnterNotifyEvent (object sender,
			EnterNotifyEventArgs args)
		{
			ebImageClear.GdkWindow.Cursor = cursorHand;
		}
		
		private void ebImageClear_ButtonPressEvent (object sender,
			ButtonPressEventArgs args)
		{
			entry.Text = string.Empty;
		}
		
		protected override void OnShown ()
		{
			base.OnShown ();
			//entry_Changed (entry, EventArgs.Empty);	
			//if (entry.Text.Length == 0)
			//	ebImageClear.Hide ();
		}
				
		private void entry_Changed (object sender,
			EventArgs args)
		{
			ebImageClear.Visible = !(entry.Text.Length == 0); 
		}
		
		private void ebImageClear_Shown (object sender, EventArgs args)
		{
			entry_Changed (entry,EventArgs.Empty);
		}
		
		public Gtk.Entry Entry {
			get {
				return entry;
			}
		}
		
		public Gtk.Menu Menu {
			get {
				return menu;
			}
			set {
				menu = value;
				
			}
		}
	}
}