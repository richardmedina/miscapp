
using System;
using System.Data;
using Gtk;

namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class DataSetView : Gtk.TreeView
	{
		private Gtk.ListStore _store;
		private Gtk.TreeViewColumn [] _columns;
		private Gtk.CellRendererText [] _renders;
		
		private DataSet _dataset;
		
		public string CurrentFilter = string.Empty;
		public int ColumnId = 0;
		
		public bool AutoSelectable = false;
		
		public event EventHandler Activated;
		
		public DataSetView ()
		{
			Activated = onActivated;
		}
		
		public virtual void LoadDataSet (DataSet dataset)
		{
				_dataset = dataset;
				Type [] types = new Type [dataset.Tables [0].Columns.Count];
			
				_renders = new CellRendererText [types.Length];
				_columns = new TreeViewColumn [types.Length];
			
				for (int i = 0; i < types.Length; i ++) {
					types [i] = typeof (string);
					_renders [i] = new CellRendererText ();
					_columns [i] = new TreeViewColumn (dataset.Tables [0].Columns [i].Caption, 
				                                   _renders [i], "text", i);
				}
			
				_store = new ListStore(types);
			
				for (int i =0; i < types.Length; i ++)
					AppendColumn (_columns [i]);
			
				Model = _store;
		}
		
		public virtual bool OnRowAdd (string [] fields)
		{
			_store.AppendValues (fields);
			return true;
		}
				
		public virtual int Populate ()
		{
			_store.Clear ();
			int count = 0;
			
			foreach (DataRow row in _dataset.Tables [0].Rows) {
				string [] fields = new string [_dataset.Tables [0].Columns.Count]	;
				
				for (int i = 0; i < fields.Length; i ++) {
						fields [i] = row [i].ToString ();
				}
				
				if (OnRowAdd (fields))
					count ++;
			}
			
			Gtk.TreeIter iter;
			
			if (AutoSelectable) {
					if (Store.GetIterFirst (out iter))
						Selection.SelectIter (iter);
			}
			
			return count;
		}
		
		protected virtual void OnActivated ()
		{
			Activated (this, EventArgs.Empty);
		}
		
		protected override bool OnKeyPressEvent (Gdk.EventKey evnt)
		{
			if (evnt.Key == Gdk.Key.Return || evnt.Key == Gdk.Key.KP_Enter)
				OnActivated ();
			
			return base.OnKeyPressEvent (evnt);
		}

		
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			if (evnt.Button == 1 && evnt.Type == Gdk.EventType.TwoButtonPress) {
				
				Gtk.TreeIter iter;
				Gtk.TreePath path;
				
				if (GetPathAtPos ((int) evnt.X, (int) evnt.Y, out path)) {
					if (_store.GetIterFromString (out iter, path.ToString ()))
					    OnActivated ();
				}
			}
			
			return base.OnButtonPressEvent (evnt);
		}
		
		public bool GetSelected (out string [] fields)
		{
			fields = new string [Renders.Length];
			Gtk.TreeIter iter;
			
			if (Selection.GetSelected (out iter)) {
				for (int i = 0; i < fields.Length; i ++)
					fields [i] = (string) Store.GetValue (iter, i);
				return true;
			}
			
			return false;
		}
				
		private void onActivated (object sender, EventArgs args)
		{
		}
		
		public new TreeViewColumn [] Columns {
			get { return _columns; }
		}
		
		public CellRendererText [] Renders {
			get { return _renders; }
		}
		
		public ListStore Store {
			get { return _store; }
		}
		
		public DataSet Dataset {
			get { return _dataset; }
		}
	}
}
