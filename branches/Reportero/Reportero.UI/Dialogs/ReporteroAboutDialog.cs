
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
Reportero es una aplicación de genración estadística que se encarga 
de obtener, analizar y sintetizar la información de la base de datos 
de registros vehiculares generada por RASTRAC GPS.

Su finalidad es analizar dicha información y generar reprotes gráficos 
y listados de información por grupos y tipos. Almacenando información
de los usuarios y numeros de contacto para reportar anomalias.

Esta aplicación se encuentra en fase de desarrollo. Por favor reporta 
cualquier problema, desperfecto o funcionamiento incorrecto de la 
aplicación. Solo retroalimentandonos podremos mejorar esta aplicación.

Atte.
Ricardo Medina Lopez
Desarrollador de la aplicación
";
		
		public ReporteroAboutDialog ()
		{
			Authors = new string [] {"Ricardo Medina López <rmedinalo@pep.pemex.com>"};
			Artists = Authors;
			Version = "1.0prev";
			Comments = "Version preliminar.\n" +
				"Todos los derechos reservados\n" +
				"Comentarios, sugerencias o reporte de errores:\n" +
				"Lic. Ricardo Medina López\n" +
				"rmedinalo@pep.pemex.com";
			
			License = license;
			
			WindowPosition = Gtk.WindowPosition.Center;
			
			Icon = Gdk.Pixbuf.LoadFromResource ("reportero_icon_main.png");
			Logo = Gdk.Pixbuf.LoadFromResource ("reportero_icon_main.png");
		}
	}
}
