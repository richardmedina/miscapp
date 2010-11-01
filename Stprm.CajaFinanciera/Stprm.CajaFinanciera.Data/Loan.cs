
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class Loan : Record
	{
		public int Id;
		public string Folio;
		
		public DateTime Fecha;
		public DateTime FechaIniCobro;
		
		public double _capital;
		public double Intereses;
		public double Cargo;
		public double Abono;
		public double Saldo;
		public int PorcentajeInteres;
		
		public DateTime PreFechaSusp;
		public double AbonoCapital;
		public double AbonoInteres;
		
		public string Cheque;
		public string Pagare;
		
		
		public Loan (Database db) : base (db, RecordType.Loan)
		{
		}
		
		
		public override bool Update ()
		{
			bool result = true;
			
			return result;
		}

		public override void FillFromReader (IDataReader reader)
		{
			if (!int.TryParse (reader ["pre_id"].ToString (), out Id))
				Id = 0;
			Folio = reader ["pre_folio"].ToString ();
			Cheque = reader ["pre_folio"].ToString ();
			Pagare = reader ["pre_pagare"].ToString ();
		}

	}
}
