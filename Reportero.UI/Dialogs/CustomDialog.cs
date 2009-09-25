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
namespace Reportero.UI.Dialogs
{
	
	
	public class CustomDialog : Gtk.Dialog
	{
		
		public CustomDialog()
		{
			WindowPosition = WindowPosition.Center;
			AddButton (Stock.Help, ResponseType.Help);
		}
		
		public new ResponseType Run ()
		{
			ResponseType response;
			do {
				response = (ResponseType) base.Run ();	
			} while (response == ResponseType.Help);
			
			return response;
		}
	}
}
