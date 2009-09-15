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
		
		public Categorizer (CategoryCollection categories)
		{
			_store = new Gtk.ListStore (typeof (ICategory),
			                            typeof(string));
			
			
			Model = _store;
			
			TreeViewColumn column = new TreeViewColumn ("Category", new CellRendererText (), "text", 1);
			
			AppendColumn (column);
			HeadersVisible = true;
			
			_categories = categories;
		}
		
		public CategoryCollection Categories {
			get { return _categories; }
		}
	}
}
