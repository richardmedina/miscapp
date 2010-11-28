
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class Prestamo : Record
	{
		
		public int Id;
		public int PlazoId;
		public int TrabajadorId;
		public string Folio;
		public string FolioOriginal;
		
		public DateTime Fecha;
		public DateTime FechaIniCobro;
		
		public decimal Capital;
		public decimal Interes;
		public decimal Cargo;
		public decimal Abono;
		
		public decimal Saldo;
		public int PorcentajeInteres;
		public int Status;
		
		public DateTime FechaSusp;
		public int NumPagos;
		public decimal AbonoCapital;
		public decimal AbonoInteres;
		public string Cheque;
		public string Pagare;
		
		public int CuentaId;

		public Prestamo (Database db) : base (db, RecordType.Prestamo)
		{
		}
		
		public static PrestamoCollection GetCollection (Database db)
		{
			PrestamoCollection prestamos = new PrestamoCollection ();
			
			IDataReader reader = db.Query ("select pre_id from prestamos");
			
			while (reader.Read ()) {
				Prestamo prestamo = new Prestamo (db);
				prestamo.Id = GetInt32 (reader, "pre_id");
				prestamos.Add (prestamo);
			}
			
			reader.Close ();
			
			foreach (Prestamo prestamo in prestamos)
				prestamo.Update ();
			
			return prestamos;
		}
		
		public override bool Update ()
		{
			bool result = false;
			
			IDataReader reader = Db.Query ("SELECT pla_id, tra_id, pre_folio, pre_folio_original, pre_fecha, pre_fecha_inicobro, pre_capital, pre_interes, pre_cargo, pre_abono, pre_saldo, pre_porcentaje_interes as pre_porc, pre_status, pre_fecha_susp, pre_num_pagos, pre_abono_capital, pre_abono_interes, pre_cheque, pre_pagare, cue_id from {0} where pre_id = {1}",
			                               TablePrestamos, Id);
			
			if (reader.Read ()) {
					FillFromReader (reader);
					result = true;
			}
			reader.Close ();
			
			return result;
		}
		
		public static IDataAdapter GetInAdapter (Database db)
		{
			return db.QueryToAdapter ("select pre_id as Id, DATE_FORMAT(pre_fecha,'%d/%m/%Y') as Fecha, pre_folio as Folio, pre_cheque as Cheque, pre_pagare as Pagare, tra_ficha as Ficha, TRIM(CONCAT(tra_nombre, ' ', tra_apepaterno, ' ', tra_apematerno)) as Nombre, CONCAT('$', FORMAT(pre_capital,2)) as Capital, CONCAT('$', FORMAT(pre_interes, 2)) as Intereses, CONCAT('$', FORMAT(pre_capital + pre_interes, 2)) as Total, CONCAT('$', FORMAT(pre_abono,2)) as Abono, CONCAT('$', FORMAT(pre_saldo, 2)) as Saldo from prestamos, trabajadores where prestamos.tra_id = trabajadores.tra_id order by Ficha asc");
		}
		
		public override void FillFromReader (IDataReader reader)
		{
			//Id = GetInt32 (reader, "pre_id");
			PlazoId = GetInt32 (reader, "pla_id");
			TrabajadorId = GetInt32 (reader, "tra_id");
			Folio = GetString (reader, "pre_folio");
			FolioOriginal = GetString (reader, "pre_folio_original");
			Fecha = GetDateTime (reader, "pre_fecha");
			FechaIniCobro = GetDateTime (reader, "pre_fecha_inicobro");
			Capital = GetDecimal (reader, "pre_capital");
			Interes = GetDecimal (reader, "pre_interes");
			Cargo = GetDecimal (reader, "pre_cargo");
			Abono = GetDecimal (reader, "pre_abono");
			Saldo = GetDecimal (reader, "pre_saldo");
			PorcentajeInteres = GetInt32 (reader, "pre_porc");
			Status = GetInt32 (reader, "pre_status");
			FechaSusp = GetDateTime (reader, "pre_fecha_susp");
			NumPagos = GetInt32 (reader, "pre_num_pagos");
			AbonoCapital = GetDecimal (reader, "pre_abono_capital");
			AbonoInteres = GetDecimal (reader, "pre_abono_interes");
			Cheque = GetString (reader, "pre_cheque");
			Pagare = GetString (reader, "pre_pagare");
		}
	}
}

