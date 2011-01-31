
using System;
using System.Data;

namespace Stprm.CajaFinanciera.Data
{

	public class DescuentoMovimiento : Record
	{
		public int Id;
		
		public int TrabajadorInternalId;
		public int DescuentoId;
		public int PrestamoId;
		public decimal Importe;
		public decimal DescuentoDiario;
		
		public string Folio;
		
		public string Sta;
		
		/*
		public int DescuentoId;
		public string TrabajadorId;
		public string Pagare;
		public string Folio;
		public string Nombre;
		public decimal Saldo;
		public decimal DescuentoCatorcenal;
		public decimal DescuentoDiario;
		
		public string Clave;
		public string Periodo;
		public string Anio;
		public DateTime Fecha;
		*/
		public DescuentoMovimiento (Database db) : base (db, RecordType.DescuentoMovimiento)
		{
		}
		
		public static DescuentoMovimiento Parse (Database db, string [] row)
		{
			DescuentoMovimiento movimiento = new DescuentoMovimiento (db);
			
			return movimiento;
		}
		
		public override bool Save ()
		{
			bool result = false;
			
			Console.WriteLine ("Agregando movimiento");
			if (!Exists ()) {
				try {
					Db.NonQuery ("insert into {0} (desc_id) values ({1})",
				             TableDescuentoMovimientos, DescuentoId);
				
					Id = GetLastInsertId ();
					result = true;
				} catch (Exception ex) {
					Console.WriteLine ("DescuentoMovimiento.Save (): {0}", ex.Message);
					Id = 0;
				}
			}
			
			Console.WriteLine ("Agregado, actualizando...{0}", Serialize ());
			
			if (Id > 0) {
				try {
					Db.NonQuery ("UPDATE {0} SET desc_id={1}, tra_id={2}, pre_id={3}, dem_importe={4}, dem_desc_diario={5}, dem_folio_act='{6}' where dem_id={7}",
				             TableDescuentoMovimientos, DescuentoId, TrabajadorInternalId, PrestamoId, Importe, DescuentoDiario, Folio, Id);
					result = true;
				} catch (Exception ex) {
					Console.WriteLine ("DescuentoMovimiento.Save (): {0}", ex.Message);
				}
			}
			
			return result;
		}
		
		public override bool Exists ()
		{
			DescuentoMovimiento mov = new DescuentoMovimiento (Db);
			mov.Id = Id;
			
			return mov.Update ();
		}

	
		public override bool Update ()
		{
			bool result = false;
			
			IDataReader reader = Db.Query ("select * from {0} where dem_id={1}",
			                               TableDescuentoMovimientos, Id);
			
			if (reader.Read ()) {
				FillFromReader (reader);
				result = true;
			}
			reader.Close ();
			
			return result;
		}
		
		public override void FillFromReader (IDataReader reader)
		{
			TrabajadorInternalId = GetInt32 (reader, "tra_id");
			DescuentoId = GetInt32 (reader, "desc_id");
			PrestamoId = GetInt32 (reader, "pre_id");
			Importe = GetDecimal (reader, "dem_importe");
			DescuentoDiario = GetDecimal (reader, "dem_desc_diario");
			Folio = GetString (reader, "dem_folio_act");
		}

		public static DescuentoMovimientoCollection GetCollection (Descuento descuento)
		{
			DescuentoMovimientoCollection movimientos = new DescuentoMovimientoCollection ();
			
			IDataReader reader = descuento.Db.Query ("SELECT * from {0} where desc_id={1}",
			                                         TableDescuentoMovimientos, descuento.Id);
			
			while (reader.Read ()) {
				DescuentoMovimiento mov = new DescuentoMovimiento (descuento.Db);
				mov.FillFromReader (reader);
				movimientos.Add (mov);
			}
			reader.Close ();
			
			return movimientos;
		}
		
		public static IDataAdapter GetCollectionInAdapter (Descuento descuento)
		{
			return descuento.Db.QueryToAdapter ("select dem_folio_act as Folio, tra_ficha as Ficha, tra_nombrecompleto, dem_importe as Importe, dem_desc_diario as 'Desc. Diario' from descuentos_mov,trabajadores where trabajadores.tra_id = descuentos_mov.tra_id and desc_id={1}",
			                                    //"select * from {0} where desc_id = {1}",
			                                        TableDescuentoMovimientos, descuento.Id);
		}
		
		
		public string Serialize ()
		{
			string str = string.Empty;
			Employee employee = new Employee (Db);
			employee.InternalId = TrabajadorInternalId;
			
			if (employee.UpdateFromInternalId()) {
				Descuento descuento = new Descuento (Db);
				descuento.Id = DescuentoId;
			
				if (descuento.Update ()) {
					Categoria categoria = new Categoria (Db);
					categoria.Id = descuento.CategoriaId;
					
					if (categoria.Update ()) {
						if (categoria.Id == "CO") {
						// Corporativo
							str = string.Format ("{0}{1}{2:000000000.00}{3}",
							                     AddZerosTo(employee.Id, 8 - employee.Id.Length), categoria.Concepto, Importe, descuento.Fecha.ToString ("yyyyMMdd"));
						} else {
							// PEP
							Console.WriteLine ("PEP");
							Console.WriteLine (AddZerosTo (Folio, 10 - Folio.Length));
							Console.WriteLine ("PEP_END");
							str = string.Format ("{0}{1}{2}{3}{4}{5}",
							                     AddZerosTo (Folio, 10 - Folio.Length),
							                     AddZerosTo (employee.Id, 8 - employee.Id.Length),
							                     PepFormatName (employee.GetFullName ()),
							                     categoria.Concepto,
							                     Importe.ToString ("000000000.00"),
							                     DescuentoDiario.ToString ("000000000.00"));
						}
					}
				}
			}
						
			return str;
		}
		
		public string PepFormatName (string fullname)
		{
				fullname = fullname.Replace (" ", string.Empty);
			
				if (fullname.Length >= 31)
					fullname = fullname.Substring (0, 30);
				else
					for (int i = fullname.Length; i < 30; i ++)
						fullname += "X";
			
			return fullname;
		}
			

		
		private string AddZerosTo (string text, int howmany)
		{
			
			string nuevaficha = text;
			
			for(int i =0; i < howmany; i ++)
				nuevaficha = "0" + nuevaficha;
			
			return nuevaficha;
		}
		
		/*
		public override string ToString ()
		{
		return string.Format("[DescuentoMovimiento] Ficha = {0}, Pagare = {1}, Folio = {2}, Nombre = {3}, Saldo = {4}, DescuentoCatorcenal = {5}, DescuentDiario = {6}",
			                     Ficha, Folio, Nombre, Saldo, DescuentoCatorcenal, DescuentoDiario);
		}
		*/
	}
	
}
