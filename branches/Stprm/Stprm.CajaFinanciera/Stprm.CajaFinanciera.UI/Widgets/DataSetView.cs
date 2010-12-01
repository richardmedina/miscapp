
using System;
using System.Data;
using Gtk;

using Stprm.CajaFinanciera.UI;
namespace Stprm.CajaFinanciera.UI.Widgets
{


	public class DataSetView : Gtk.TreeView
	{
		private Gtk.ListStore _store;
		private Gtk.TreeViewColumn [] _columns = new Gtk.TreeViewColumn [0];
		private Gtk.CellRendererText [] _renders = new Gtk.CellRendererText [0];
		
		private DataSet _dataset;
		
		public string CurrentFilter = string.Empty;
		public int ColumnId = 0;
		
		public bool AutoSelectable = false;
		
		public event EventHandler Activated;
		
		public DataSetView ()
		{
			Activated = onActivated;
		}
		
		public virtual void New ()
		{
		}
		
		public virtual void RemoveSelected ()
		{
			TreeIter iter;
			
			if (Selection.GetSelected (out iter)) {
				TreeIter iter_for_removing = iter;
				Store.IterNext (ref iter);
				Store.Remove (ref iter_for_removing);
				Selection.SelectIter (iter);
			}
		}
		
		public virtual void EditSelected ()
		{
		}
		
		public virtual void Load ()
		{
		}
		
		public virtual void LoadDataSet (DataSet dataset)
		{
			
			foreach (TreeViewColumn col in Columns) {
				RemoveColumn (col);
			}
					
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
			
			if (Globals.ViewResponsiveLoading)
				while (Application.EventsPending ())
					Application.RunIteration ();
			
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
			if (evnt.Key == Gdk.Key.Return || evnt.Key == Gdk.Key.KP_Enter) {
				OnActivated ();
			}
			
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
		
		private int sort_func (TreeModel model, TreeIter a, TreeIter b)
		{
			int sort_id;
			SortType sort_type;
			
			Console.WriteLine ("Sorting...");
			if (_store.GetSortColumnId (out sort_id, out sort_type)) {
				string astr = (string) _store.GetValue (a, sort_id);
				string bstr = (string) _store.GetValue (b, sort_id);
				
				if (sort_id == 1)
					return byInt (astr, bstr) * -1;
				if (sort_id == 2)
					return byString (astr, bstr) * -1;
				if (sort_id == 3)
					return byCurrency (astr, bstr) * -1;
				if (sort_id == 4)
					return byDateTime (astr, bstr) * -1;
				
			}
			
			return 0;
		}
		
		private int byDateTime (string a, string b)
		{
			DateTime adate = DateTime.Parse (a);
			DateTime bdate = DateTime.Parse (b);
			
			if (adate > bdate)
				return -1;
			if (bdate > adate)
				return 1;
			
			return 0;
		}
		
		private int byCurrency (string a, string b)
		{
			a = a.Replace ("$", string.Empty).Replace ("(", string.Empty).Replace (")", string.Empty).Replace (",", string.Empty);
			b = b.Replace ("$", string.Empty).Replace ("(", string.Empty).Replace (")", string.Empty).Replace (",", string.Empty);
			
			double adouble = double.Parse (a);
			double bdouble = double.Parse (b);
			if (adouble == bdouble)
				return 0;
			if (adouble > bdouble)
				return -1;
			
			return 1;
		}
		
		private int byString (string a, string b)
		{
			return string.Compare (a, b);	
		}
		
		private int byInt (string a, string b)
		{
			int aint = int.Parse (a);
			int bint = int.Parse (b);
				
			if (aint == bint)
				return 0;
			if (aint > bint)
				return -1;
			
			return 1;
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
