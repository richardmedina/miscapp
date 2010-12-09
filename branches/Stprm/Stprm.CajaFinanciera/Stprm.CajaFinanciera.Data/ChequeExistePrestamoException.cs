
using System;

namespace Stprm.CajaFinanciera.Data
{


	public class ChequeExistePrestamoException : Exception
	{
		private string _cheque;
		private  CuentaBancaria _cuenta;
		
		public ChequeExistePrestamoException (string cheque, CuentaBancaria cuenta) : base ("Numero de Cheque actualmente asignado a otro prestamo")
		{
			_cheque = cheque;
			_cuenta = cuenta;
		}
		
		public string Cheque {
			get { return _cheque; }
		}
		
		public CuentaBancaria Cuenta {
			get { return _cuenta; }	
		}
	}
}
