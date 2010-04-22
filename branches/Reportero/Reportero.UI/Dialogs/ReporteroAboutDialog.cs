
using System;
using Gtk;

namespace Reportero.UI.Dialogs
{
	
	
	public class ReporteroAboutDialog : Gtk.AboutDialog
	{
	
static string license = 
@"
Reportero - Generador de Estadísticas
*****
Reportero es una aplicación de generación estadística que se encarga 
de obtener, analizar y sintetizar la información de la base de datos 
de registros vehiculares generada por RASTRAC.

Su finalidad es analizar dicha información y generar reprotes gráficos 
y listados de información por grupos y tipos. Almacenando información
de los usuarios y numeros de contacto para reportar anomalías.

Esta aplicación se encuentra en etapa de desarrollo. Por favor reporta 
cualquier problema o desperfecto o comentario de la aplicación
aplicación será bienvenido. 

Solo retroalimentandonos podremos mejorar esta aplicación.

Saludos.

Atte.
Ricardo Medina Lopez <ricardo.medina@pemex.com>
Desarrollador de la aplicación
";
		
		public ReporteroAboutDialog ()
		{
			Authors = new string [] {"Ricardo Medina López <ricardo.medina@pemex.com>"};
			Artists = Authors;
			Version = "0.67";
			Comments = "Version preliminar.\n" +
				"Todos los derechos reservados\n" +
				"Comentarios, sugerencias o reporte de errores:\n" +
				"Lic. Ricardo Medina López\n" +
				"ricardo.medina@pemex.com";
			
			License = license;
			
			WindowPosition = Gtk.WindowPosition.Center;
			
			Icon = Gdk.Pixbuf.LoadFromResource ("reportero_icon_main.png");
			Logo = Gdk.Pixbuf.LoadFromResource ("reportero_icon_main.png");
		}
	}
}
