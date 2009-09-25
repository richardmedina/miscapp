
using System;
using Gtk;
using Reportero.Data;

namespace Reportero.UI.Widgets
{
	
	
	public class ReportChooser : Gtk.IconView
	{
		private Gtk.ListStore _store;
		
		public ReportChooser()
		{
			_store = new ListStore (typeof (Gdk.Pixbuf),
				typeof (Leadership), 
				typeof (string));
			
			Model = _store;
			TextColumn = 2;
			PixbufColumn = 0;
			
		}
		
		public void Append (Leadership leadership)
		{
			Gdk.Pixbuf buf = Gdk.Pixbuf.LoadFromResource ("reportero_icon_pickup.png");
			_store.AppendValues (buf.ScaleSimple(54, 54, Gdk.InterpType.Bilinear), leadership, leadership.Name);
		}
		
		public bool GetSelected (out Leadership leadership)
		{
			Gtk.TreeIter iter;
			
			leadership = null;
			
			if (SelectedItems.Length > 0) {
				if (_store.GetIter (out iter, SelectedItems [0])) {
					leadership = (Leadership) _store.GetValue (iter, 1);
					return true;
				}
			}
			return false;
		}
		
		public bool GetLeadershipAtPointer (out Leadership leader, int x, int y)
		{
			TreePath path = GetPathAtPos (x, y);
			TreeIter iter;
			leader = null;
			if (path != null)
				if (_store.GetIter (out iter, path)) {
					leader = (Leadership) _store.GetValue (iter, 1);
					SelectPath (path);
					return true;
				}
			
			return false;
		}
	}
}
