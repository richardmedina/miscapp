//  
//  Copyright (C) 2009 
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

namespace Artemisa.UI.Widgets
{
	
	
	public class MoviePreviewWidget : Gtk.VBox
	{
		private MoviePreviewCanvas _canvas;
		private MoviePreviewToolbar _toolbar;
		
		public MoviePreviewWidget () : base (false, 5)
		{
			_canvas = new MoviePreviewCanvas ();
			_toolbar = new MoviePreviewToolbar ();
			
			_toolbar.PlayButton.Clicked += 
				delegate { _canvas.PreviewState = MoviePreviewState.Play; };
			_toolbar.PauseButton.Clicked +=
				delegate { _canvas.PreviewState = MoviePreviewState.Pause; };
			_toolbar.StopButton.Clicked +=
				delegate { _canvas.PreviewState = MoviePreviewState.Stop; };
			
			PackStart (_canvas);
			Gtk.HBox hbox = new Gtk.HBox (false, 0);
			hbox.PackStart (_toolbar, true, false, 0);
			PackStart (hbox, false, false, 0);
		}
	}
}
