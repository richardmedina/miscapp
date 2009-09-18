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

namespace Artemisa.UI.Widgets
{
	
	
	public class CopyrightCategory : Gtk.VBox, ICategory
	{
		private Gtk.Entry _entry_author;
		private Gtk.Entry _entry_email;
		private Gtk.Entry _entry_company;
		private Gtk.Entry _entry_copying;
		
		public CopyrightCategory()
		{
			_entry_author = new Entry ();
			_entry_email = new Entry ();
			_entry_company = new Entry ();
			_entry_copying = new Entry ();
			
			Gtk.HBox hbox = new Gtk.HBox(false, 0);
			
			hbox.PackStart (new Label ("Author"), false, false, 0);
			hbox.PackStart (_entry_author);
			PackStart (hbox, false, false, 0);
			ShowAll ();
		}
		
		public void Save ()
		{	
		}
		
		public Gtk.Widget GetWidget () 
		{
			return this;
		}
		
		public string Title {
			get { return "Copyright"; }
		}
	}
}
