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
using Gtk;

namespace Artemisa.UI.Widgets
{
	
	
	public class MoviePreviewToolbar : Gtk.HBox
	{
		private Button _btn_play;
		private Button _btn_pause;
		private Button _btn_stop;
			
		public MoviePreviewToolbar()
		{
			Spacing = 5;
			
			_btn_play = new Button (Stock.MediaPlay);
			_btn_pause = new Button (Stock.MediaPause);
			_btn_stop = new Button (Stock.MediaStop);
			
			_btn_play.Relief = ReliefStyle.None;
			_btn_pause.Relief = ReliefStyle.None;
			_btn_stop.Relief = ReliefStyle.None;
			
			PackStart (_btn_play, false, false, 0);
			PackStart (_btn_pause, false, false, 0);
			PackStart (_btn_stop, false, false, 0);
		}
		
		public Button PlayButton {
			get { return _btn_play; }
		}
		
		public Button PauseButton {
			get { return _btn_pause; }
		}
		
		public Button StopButton {
			get { return _btn_stop; }
		}
	}
}
