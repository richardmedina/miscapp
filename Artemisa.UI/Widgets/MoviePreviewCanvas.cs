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
	
	public enum MoviePreviewState {
		Play,
		Pause,
		Stop
	}
	
	public class MoviePreviewCanvas : Gtk.DrawingArea
	{
		private Gdk.Pixbuf _pixbuf_icon;
		private int _angle = 1;
		
		private int _x = 0;
		private int _y = 0;
		
		private bool x_inc = true;
		private bool y_inc=  true;
		
		private MoviePreviewState _moviepreview_state;
		
		public MoviePreviewCanvas()
		{
			ModifyBg (StateType.Normal, new Gdk.Color (0, 0 ,0));
			_pixbuf_icon = Gdk.Pixbuf.LoadFromResource ("artemisa_icon.png");
			PreviewState = MoviePreviewState.Stop;
		}
		
		protected override void OnShown ()
		{
			base.OnShown ();
			GLib.Timeout.Add (50, animation_timeout);
		}

		
		protected override bool OnExposeEvent (Gdk.EventExpose evnt)
		{
			bool ret = base.OnExposeEvent (evnt);
			
			//int x = (Allocation.Width / 2) - (_pixbuf_icon.Width / 2);
			//int y = (Allocation.Height / 2) - (_pixbuf_icon.Height / 2);
			/*
			evnt.Window.DrawPixbuf (Style.WhiteGC,
			                        _pixbuf_icon,
			                        0, 0, x, y, 
			                        _pixbuf_icon.Width, _pixbuf_icon.Height,
			                        Gdk.RgbDither.None, 0, 0);
			*/
			
			using (Cairo.Context context = 
			       Gdk.CairoHelper.Create (evnt.Window)) {
  
				/*
   cr = cairo_create (image2);
   cairo_translate (cr, h * 0.5, w * 0.5);
   cairo_rotate (cr, M_PI / 2.0);
   cairo_translate (cr, -w * 0.5, -h * 0.5);
   cairo_set_source_surface (cr, image, 0, 0);
   cairo_set_operator (cr, CAIRO_OPERATOR_SOURCE);
   cairo_paint (cr);
   cairo_surface_write_to_png (image2, "Flag-rotate.png");
   cairo_surface_destroy (image);
   cairo_surface_destroy (image2);

				*/
				
				/*Cairo.ImageSurface image = 
						new Cairo.ImageSurface (
					                        _pixbuf_icon.Pixels,
					                        Cairo.Format.Argb32,
					                        _pixbuf_icon.Width, 
					                        _pixbuf_icon.Height,
					                        _pixbuf_icon.Rowstride);
				*/
				Cairo.ImageSurface image = 
					new Cairo.ImageSurface ("/home/richard/Desktop/artemisa_icon.png");
				
				
				//image.Show (context, x, y);
				
				//context.Translate (image.Width*0.5, image.Height*0.5);
				//context.Rotate (Math.PI / 2);
				
				if (x_inc) {
					if (_x < Allocation.Width - image.Width)
						_x += 5;
					else x_inc  = false;
				} else {
					if (_x > 0) _x-=5;
					else x_inc = true;
				}
				
				if (y_inc) {
					if (_y < Allocation.Height - image.Height)
						_y +=5;
					else y_inc = false;
				} else {
					if (_y > 0) _y-=5;
					else y_inc = true;
				}
					
				
				context.SetSource (image, _x, _y);
				//context.Operator =  Cairo.Operator.Source;
				context.Paint ();
				((IDisposable)image).Dispose ();
			}
			
			return ret;
		}
		
		private bool animation_timeout ()
		{
			if (PreviewState == MoviePreviewState.Play)
				QueueDraw ();
			else if (PreviewState == MoviePreviewState.Stop) {
				bool flag = true;
				if (_x == 0 || _y == 0)
					flag = false;
				_x = 0;
				_y = 0;
				if (flag)
					QueueDraw ();
			}
			return true;
		}
		
		public MoviePreviewState PreviewState {
			get { return _moviepreview_state; }
			set { _moviepreview_state = value; }
		}
	}
}
