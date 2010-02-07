
using System;
using System.IO;
using Gtk;

namespace Artemis.UI.Dialogs
{
	
	
	public class FolderChooserDialog : CommonDialog
	{
		
		private FolderChooserView _view;
		
		public FolderChooserDialog ()
		{
			Title = "Please select folder to load..";
			Resize (240, 320);
			_view = new FolderChooserView ();
			
			Gtk.ScrolledWindow scroll = new Gtk.ScrolledWindow ();
			scroll.Add (_view);
			
			VBox.PackStart (scroll);
			VBox.ShowAll ();
			
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		public string SelectedPath {
			get { return _view.GetSelectedPath (); }
		}
	}
	
	class FolderChooserView : Gtk.TreeView
	{
		private Gtk.TreeStore _store;
		
		private Gdk.Pixbuf _pixbuf_vol;
		private Gdk.Pixbuf _pixbuf_folder;
	
		public FolderChooserView ()
		{
			_store = new TreeStore (
				typeof (string), // directory name
				typeof (string), // full path
				typeof (Gdk.Pixbuf)
			);
			
			Model = _store;
			
			TreeViewColumn column = new TreeViewColumn ();
			column.Title = "Folders";
			
			
			
			CellRenderer render = new CellRendererPixbuf ();
			column.PackStart (render, false);
			column.AddAttribute (render, "pixbuf", 2);
			
			render = new CellRendererText ();
			column.PackStart (render, true);
			column.AddAttribute (render, "text", 0);
			
			
			AppendColumn (column);
			
			_pixbuf_vol = IconFactory.LookupDefault (Stock.Harddisk).RenderIcon (
				Style, TextDirection.Ltr, StateType.Normal, IconSize.Menu, this, "vol");
				
			_pixbuf_folder = IconFactory.LookupDefault (Stock.Directory).RenderIcon (
				Style, TextDirection.Ltr, StateType.Normal, IconSize.Menu, this, "dir");
			
			loadVolumens ();
			//loadFolders (TreeIter.Zero, "/");
		}
		
		protected override void OnRowCollapsed (Gtk.TreeIter iter, Gtk.TreePath path)
		{
			int childs = _store.IterNChildren (iter);
			Gtk.TreeIter [] iters = new Gtk.TreeIter[childs];
			
			for (int i = 0; i < childs; i ++) {
				Gtk.TreeIter current_iter;
				if (_store.IterNthChild (out current_iter, iter, i))
					iters [i] = current_iter;
			}
			
			foreach (Gtk.TreeIter current_iter in iters) {
				Gtk.TreeIter iter_to_remove = current_iter;
				_store.Remove (ref iter_to_remove);
			}
			
			_store.AppendValues (iter, "Loading", string.Empty, _pixbuf_folder);
			
			//Selection.SelectIter (iter);
			
			base.OnRowCollapsed (iter, path);
		}

				
		protected override void OnRowExpanded (Gtk.TreeIter iter, Gtk.TreePath path)
		{	
			string current_path = (string) _store.GetValue (iter, 1);
			bool exception_suceeded = false;
			Exception exception = null;
			Gtk.TreeIter iter_to_remove;
			try {
				loadFolders (iter, current_path);
			} catch (Exception excep) {
					exception_suceeded = true;
					exception = excep;
			} finally {
				// These must be "loading" iter and will be remove it
				if (_store.IterNthChild (out iter_to_remove, iter, 0))
					_store.Remove (ref iter_to_remove);
			
				Selection.SelectIter (iter);
			}
			
			if (exception_suceeded) {
				MessageDialog dialog = new MessageDialog (null,
					DialogFlags.Modal,
					MessageType.Error,
					ButtonsType.Ok,
					exception.Message);
				dialog.Run ();
				dialog.Destroy ();
			}
			
			base.OnRowExpanded (iter, path);
		}
		
		private void loadVolumens ()
		{
			foreach (string vol in System.IO.Directory.GetLogicalDrives ()) {
				Gtk.TreeIter iter = _store.AppendValues (vol, vol, _pixbuf_vol);
				_store.AppendValues (iter, "Loading...", string.Empty, _pixbuf_folder);
				//loadFolders (TreeIter.Zero, vol);
			}
		}
		
		private void loadFolders (Gtk.TreeIter iter, string fullpath)
		{
			bool iter_valid = _store.IterIsValid (iter);
			
			foreach (string folder in System.IO.Directory.GetDirectories (fullpath)) {
				int last = folder.LastIndexOf (System.IO.Path.DirectorySeparatorChar);
				string name = folder.Substring (last + 1);
				
				if (iter_valid) {
					Gtk.TreeIter insert_iter = _store.AppendValues (iter, name, folder, _pixbuf_folder);
					_store.AppendValues (insert_iter, "Loading..", string.Empty, _pixbuf_folder);
				} else {
					Gtk.TreeIter insert_iter = _store.AppendValues (name, folder, _pixbuf_folder);
					_store.AppendValues (insert_iter, "Loading..", string.Empty, _pixbuf_folder);
				}
			}
			if (iter_valid)
				Selection.SelectIter (iter);
			else {
				TreeIter iter_to_select;
				if (_store.GetIterFirst (out iter_to_select))
					Selection.SelectIter (iter_to_select);
			}
		}
		public string GetSelectedPath ()
		{
			Gtk.TreeIter iter;
			
			if (Selection.GetSelected (out iter)) {
				return (string) _store.GetValue (iter, 1);
			}
			return string.Empty;
		}
	}
}
