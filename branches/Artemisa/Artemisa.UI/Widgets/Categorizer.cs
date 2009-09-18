//  
//  Copyright (C) 2009 Ricardo Medina
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

using System;
using Gtk;
using System.Collections.Generic;

namespace Artemisa.UI.Widgets
{
	
	
	public class Categorizer : Gtk.TreeView
	{
		private CategoryCollection _categories;
		private Gtk.ListStore _store;
		
		private event CategorizerEventHandler _category_activated;
		
		public Categorizer () : this (new CategoryCollection ())
		{
		}
		
		public Categorizer (CategoryCollection categories)
		{
			_category_activated = onCategoryActivated;
			_categories = categories;
			
			_store = new Gtk.ListStore (typeof (ICategory),
			                            typeof(string));
			
			
			Model = _store;
			
			TreeViewColumn column = new TreeViewColumn ("Category", 
			                                            new CellRendererText (), 
			                                            "text", 
			                                            1);
			
			AppendColumn (column);
			HeadersVisible = true;
			
			Selection.Changed += onSelectionChanged;
			
			foreach (ICategory cat in Categories)
					Append (cat);
		}
		
		public void Append (ICategory category)
		{
			_store.AppendValues (category, 
			                     category.Title);
		}
		
		protected virtual void OnCategoryActivated (ICategory category)
		{
			_category_activated (this, 
			                     new CategorizerEventArgs(category));
		}
		
/*
		protected override bool OnButtonPressEvent (Gdk.EventButton evnt)
		{
			TreePath path;
			
			if (GetPathAtPos ((int) evnt.X, (int) evnt.Y, out path)) {
				TreeIter iter;
				if (_store.GetIterFromString (out iter, path.ToString ())) {
					ICategory cat = (ICategory) _store.GetValue (iter, 0);
				    OnCategoryActivated (cat);
				}
			}
			return base.OnButtonPressEvent (evnt);
		}
		
		protected override void OnShown ()
		{
			foreach (ICategory category in Categories)
				Append (category);
			
			base.OnShown ();
		}
*/
		
		private void onCategoryActivated (object sender,
		                                  CategorizerEventArgs args)
		{
		}
		
		private void onSelectionChanged (object sender, EventArgs args)
		{
			Gtk.TreeIter iter;
		
			if (Selection.GetSelected (out iter)) {
				ICategory cat = (ICategory) _store.GetValue (iter, 0);
				OnCategoryActivated (cat);
			}
		}

		public CategoryCollection Categories {
			get { return _categories; }
		}
		
		public event CategorizerEventHandler CategoryActivated {
			add { _category_activated += value; }
			remove { _category_activated -= value; }
		}
	}
}
