
using System;
using System.Data;
using System.IO;

namespace Reportero.Data
{
	
	
	public class Leadership : IRecord
	{
		private string _name;
		private Database _database;
		
		private static readonly string equivs_filename = "equivs.txt";
		
		public Leadership (Database database)
		{
			_database = database;
		}
		
		// in order its anatomy, IRecord interface
		// can't be implemented. The internal system
		// does not possible.
		public void Save ()
		{
			
		}
		
		public bool Update ()
		{
			return false;
		}
		
		public bool Exists ()
		{
			return true;
		}
		
		public static LeadershipCollection FromDatabase (Database database)
		{
			LeadershipCollection leadcol = new LeadershipCollection (database);
			IDataReader reader = database.Query ("select distinct substring(Alias, 0, charindex('-',Alias)) as leadership from VehicleState where Alias <> '' order by Leadership;");
			
			while (reader.Read ()) {
				Leadership leader = new Leadership (database);
				leader.Name = (string) reader ["leadership"];
				leadcol.Add (leader);
			}
			
			reader.Close ();
			
			return leadcol;
		}
		
		public string GetFullname () 
		{
			string fullname = GetFullname (equivs_filename, Name);
			
			if (fullname.Trim ().Length > 0) 
				return fullname;
			
			return "Sin Equivalencia";
			/*	
			switch (Name) {
				case "MEDYSA":
					return "Mantenimiento a Equipo Dinámico y Sistemas Auxiliares";
				
				case "CAEC":
					return "Coordinación de Asuntos Externos y Comunicaciones";
					
				case "CCYM":
					return "Coordinación de Construcción y Mantenimiento";
				
				case "CDE":
					return "Coordinación de Diseños y Explotación";
				
				case "COPIE":
						return "Coordinación de Operación de Pozos e Instalaciones de Explotación";
				
				case "CTI":
					return "Coordinación de Tecnología de la Información";
				 				
 				case "RTDH":
 					return "Representación de Transporte y Distribución de Hidrocarburos";
 				
 				case "SIPAC":
					return "Coordinación de Seguridad  Industrial  y Protección Ambiental";
				
				default:
					return string.Format ("Sin equivalencia");
			}*/
		}
		
		public static string GetFullname (string filename, string shortname)
		{
			if (File.Exists (filename)) {
				using (StreamReader reader = new StreamReader (filename)) {
					string [] args = new string [0];
					for (string line = reader.ReadLine ();
						line != null; line = reader.ReadLine ()) {
						args = line.Split (":".ToCharArray ());
						if (args [0] == shortname)
							return args [1];
					}
				}
			}
			
			return string.Empty;
		}

		
		public VehicleUserCollection GetVehicles ()
		{
			VehicleUserCollection vehicles = new VehicleUserCollection (Db);
			
			
			 IDataReader reader = Db.Query ("select distinct Vehicle_ID, alias from VehicleState where alias like '{0}-%'",
				Name);
			//IDataReader reader = Db.Query ("select * from V where Vehiculo like '{0}%' order by Vehiculo;",
			//	Name);
			
			while (reader.Read ()) {
				VehicleUser vehicle = new VehicleUser (Db);
				//vehicle.Id = (string) reader ["Ficha"];
				//vehicle.Category = (string) reader ["Categoria"];
				//vehicle.Name = (string) reader ["Nombre"];
				vehicle.VehicleId = (string) reader ["alias"];
				vehicles.Add (vehicle);
			}
			
			reader.Close ();
			
			foreach (VehicleUser vehicle in vehicles)
				vehicle.Update ();
			
			return vehicles;
		}
		
		public RecordType Type {
			get { return RecordType.Leadership; }
		}
		
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		
		public Database Db {
			get { return _database; }
		}
	}
}
