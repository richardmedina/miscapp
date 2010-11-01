
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
		
		public DataSetView ()
		{	
		}
		
		public void LoadDataSet (DataSet dataset)
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
		
		public void Populate ()
		{
			foreach (DataRow row in _dataset.Tables [0].Rows) {
				string [] fields = new string [_dataset.Tables [0].Columns.Count]	;
				
				for (int i = 0; i < fields.Length; i ++) {
						fields [i] = row [i].ToString ();
				}
				
				_store.AppendValues (fields);
			}
		}
		
		public new TreeViewColumn [] Columns {
			get { return _columns; }
		}
		
		public CellRendererText [] Renders {
			get { return _renders; }
		}
	}
}
