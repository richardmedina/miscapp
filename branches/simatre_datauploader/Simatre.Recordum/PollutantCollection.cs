using System;
using System.Collections.Generic;
using System.Xml;

namespace Simatre.Recordum
{
	public class PollutantCollection : List<Pollutant>
	{
		public string AirPointerId;
		public Airpointer Airpointer;

		public PollutantCollection (Airpointer airpointer)
		{
			Airpointer = airpointer;
		}

		public int MagnitudesCount ()
		{
			return this [0].Magnitudes.Count;
		}

		public string [] GetQueryString ()
		{
			string [] queries = new string [this [0].Magnitudes.Count];

			for (int j = 0; j < this[0].Magnitudes.Count; j ++) {
				string query = string.Empty;


				for (int i = 0; i < this.Count; i ++) {	
					if (i == 0) query = "Airpointer=" + Airpointer.Id + "&DateTime=" + Utils.DateTimeToRecordumString (this [0].Magnitudes [j].Date);
					query += string.Format ("&{0}={1}", 
					                              this[i].Type, 
					                              this [i].Magnitudes [j].GetPPMValue ());
					queries [j] = query;
				}
			}

			return queries;
		}

		public static PollutantCollection ParseXML (Airpointer airpointer, XmlDocument doc)
		{
			PollutantCollection pollutants = new PollutantCollection (airpointer);

			//XmlNodeList nodes  = doc.GetElementsByTagName ("ParameterDetails");



			foreach (XmlNode node in doc.GetElementsByTagName ("ParameterDetails")) {

				//Console.WriteLine ("Posicion : {0}", node.Attributes ["Position"].Value);


				//Pollutant p = new Pollutant (AirPointerId, 

				int pollutant_id = 0;
				MeasureUnit measure_unit = MeasureUnit.PPM;
				

			//	Pollutant p = new Pollutant (AirPointerId, 

				foreach (XmlNode subnode in node.ChildNodes) {
				//	Console.WriteLine ("\tSubnode {0}: {1}", subnode.Name, subnode.InnerText);

					if (subnode.Name.ToLower () == "id") {
						//Console.WriteLine ("Id found");
						string id_str = subnode.InnerText;

						if (!int.TryParse (id_str, out pollutant_id))
							pollutant_id = 0;
					}

					if (subnode.Name.ToLower () == "unit") {
						string unit = subnode.InnerText.ToLower ();

						if (unit == "ppb") {
							measure_unit = MeasureUnit.PPB;
						}
					}
				}

				Pollutant pollutant = new Pollutant (airpointer, 
				                                     (PollutantType) pollutant_id, 
				                                     measure_unit);

				pollutants.Add (pollutant);
			}

			XmlNodeList nodes = doc.GetElementsByTagName ("SData");

			for (int i = 0; i < nodes.Count; i ++) {
				XmlNode node = nodes [i];

				string d = node.Attributes ["d"].Value;
				string t = node.Attributes ["t"].Value;

				DateTime datetime;

				if (Utils.DateTimeFromRecordumString	 (string.Format ("{0},{1}", d, t), out datetime)) {
					//Console.WriteLine ("SData ({0}):{1}", datetime, node.InnerText);
					string [] values = node.InnerText.Split (";".ToCharArray ());

					for (int j= 0; j < values.Length; j ++) {
						//Console.WriteLine ("\tVal: {0}", values [j]);
						float f;

						if (!float.TryParse (values [j], out f) || f < 0f) {
							f = 0f;
						}

						Magnitude mag = new Magnitude (datetime, f, pollutants [j].Unit);

						pollutants [j].Magnitudes.Add (mag);
					}
				}
			}
			
				

			return pollutants;
		}

	}
}
