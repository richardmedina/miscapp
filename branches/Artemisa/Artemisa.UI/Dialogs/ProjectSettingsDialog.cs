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

using Artemisa.UI.Widgets;

namespace Artemisa.UI.Dialogs
{
	
	
	public class ProjectSettingsDialog : CustomDialog
	{
		private Categorizer _categorizer;
		
		public ProjectSettingsDialog ()
		{
			Categorizer categorizer = new Categorizer ();
			//categorizer.Categories.Add (new CopyrightCategory ());
			categorizer.CategoryActivated += onCategoryActivated;
			
			VBox.PackStart (categorizer);
			VBox.ShowAll ();
			
			AddButton (Stock.Apply, ResponseType.Apply);
			AddButton (Stock.Cancel, ResponseType.Cancel);
			AddButton (Stock.Ok, ResponseType.Ok);
		}
		
		private void onCategoryActivated (object sender,
		                                  CategorizerEventArgs args)
		{
			Console.WriteLine ("Current Category: {0}", 
			                   args.Category.Title);
		}
	}
}
