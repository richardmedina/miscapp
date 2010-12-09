
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{


	public class Prestamo : Record
	{
		
		public int Id;
		public int PlazoId;
		public int TrabajadorInternalId;
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
		public OperacionFinancieraEstado Status;
		
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
		
		public override bool Save ()
		{
			bool result = false;
			
			if (!Exists ()) {
				Db.NonQuery ("insert into {0} (pla_id) values (0)",
				          TablePrestamos);
				Id = GetLastInsertId ();
			}
			
			
			Abono = GetAbono ();
			Saldo = (Capital + Interes) - GetAbono ();
			if (Id > 0) {
				Db.NonQuery ("UPDATE {0} SET pla_id ={1}, tra_id={2}, pre_folio='{3}', pre_folio_original='{4}', pre_fecha='{5}', pre_fecha_inicobro ='{6}', pre_capital={7}, pre_interes={8}, pre_cargo={9}, pre_porcentaje_interes={10}, pre_abono={11}, pre_saldo={12}, pre_status={13}, pre_fecha_susp={14}, pre_num_pagos={15}, pre_abono_capital={16}, pre_abono_interes={17}, pre_cheque='{18}', pre_pagare='{19}' where pre_id={20}",
				             TablePrestamos, PlazoId, TrabajadorInternalId, Folio, FolioOriginal, Fecha.ToString ("yyyyMMdd"), FechaIniCobro == DateTime.MinValue? "00000000" : FechaIniCobro.ToString ("YYYYmmdd"), Capital, Interes, Cargo, PorcentajeInteres, Abono, Saldo, (int) Status, FechaSusp == DateTime.MinValue ? "00000000" : FechaSusp.ToString ("yyyyMMdd"), NumPagos, AbonoCapital, AbonoInteres, Cheque, Pagare, Id);
				
				Employee employee = new Employee (Db);
				employee.InternalId = TrabajadorInternalId;
				
				if (employee.UpdateFromInternalId ()) {
					employee.Saldo = employee.GetAbono ();
					employee.Save ();
					result = true;
				}
			}
			
			return result;
		}
		
		public static bool ChequeExiste (Database db, string cheque, out CuentaBancaria cuenta)
		{
			cuenta = new CuentaBancaria (db);
			bool result = false;
			
			//IDataReader reader = db.Query ("SELECT 
			
			return result;
		}
		
		public override bool Exists ()
		{
			Prestamo prestamo = new Prestamo (Db);
			prestamo.Id = Id;
			
			return prestamo.Update ();
		}
		
		public decimal GetAbono ()
		{
			decimal abono = 0m;
			IDataReader reader = Db.Query ("select sum(prem_abono) as saldo from {0} where tra_id = {1} and pre_id={2}",
			                               TablePrestamoMovimientos, TrabajadorInternalId, Id);
			
			if (reader.Read ()) {
				abono = GetDecimal (reader, "saldo");
			}
			
			reader.Close ();
			return abono;
		}
		
		
		public IDataAdapter GetMovimientosInAdapter ()
		{
			return PrestamoMovimiento.GetMovimientosInAdapter (this);	
		}
		
		public static IDataAdapter GetInAdapter (Database db)
		{
			return db.QueryToAdapter ("select pre_id as Id, DATE_FORMAT(pre_fecha,'%d/%m/%Y') as Fecha, pre_folio as Folio, pre_cheque as Cheque, pre_pagare as Pagare, tra_ficha as Ficha, TRIM(CONCAT(tra_nombre, ' ', tra_apepaterno, ' ', tra_apematerno)) as Nombre, CONCAT('$', FORMAT(pre_capital,2)) as Capital, CONCAT('$', FORMAT(pre_interes, 2)) as Intereses, CONCAT('$', FORMAT(pre_capital + pre_interes, 2)) as Total, CONCAT('$', FORMAT(pre_abono,2)) as Abono, CONCAT('$', FORMAT(pre_saldo, 2)) as Saldo, CASE pre_status WHEN 1 THEN 'RT' WHEN 2 THEN 'DC' WHEN 3 THEN 'DT' WHEN 4 THEN 'SP' WHEN 5 THEN 'PG' END as Estado from {0}, {1} where {0}.tra_id = {1}.tra_id and {0}.pre_status <> 5 order by Ficha asc",
			                          TablePrestamos, TableEmployees);
		}
		
		public static IDataAdapter GetCollectionForEmployee (Database db, int tra_id)
		{
			return db.QueryToAdapter ("select DATE_FORMAT(pre_fecha,'%d/%m/%Y') as Fecha, pre_folio as Folio, pre_cheque as Cheque, pre_pagare as Pagare, CONCAT('$', FORMAT(pre_capital,2)) as Capital, CONCAT('$', FORMAT(pre_interes, 2)) as Intereses, CONCAT('$', FORMAT(pre_capital + pre_interes, 2)) as Total, CONCAT('$', FORMAT(pre_abono,2)) as Abono, CONCAT('$', FORMAT(pre_saldo, 2)) as Saldo, CASE pre_status WHEN 1 THEN 'RT' WHEN 2 THEN 'DC' WHEN 3 THEN 'DT' WHEN 4 THEN 'SP' WHEN 5 THEN 'PG' END as Estado from {0}, {1} where prestamos.tra_id = trabajadores.tra_id and prestamos.tra_id={2} order by pre_fecha asc", 
			                          TablePrestamos, TableEmployees, tra_id);
		}
		
		public override void FillFromReader (IDataReader reader)
		{
			//Id = GetInt32 (reader, "pre_id");
			PlazoId = GetInt32 (reader, "pla_id");
			TrabajadorInternalId = GetInt32 (reader, "tra_id");
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
			Status = (OperacionFinancieraEstado) GetInt32 (reader, "pre_status");
			FechaSusp = GetDateTime (reader, "pre_fecha_susp");
			NumPagos = GetInt32 (reader, "pre_num_pagos");
			AbonoCapital = GetDecimal (reader, "pre_abono_capital");
			AbonoInteres = GetDecimal (reader, "pre_abono_interes");
			Cheque = GetString (reader, "pre_cheque");
			Pagare = GetString (reader, "pre_pagare");
			CuentaId = GetInt32 (reader, "cue_id");
		}
	}
}

