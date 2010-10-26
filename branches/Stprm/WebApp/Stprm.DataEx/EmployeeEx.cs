using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Stprm.Data;

namespace Stprm.DataEx
{
    public class EmployeeEx :  Employee
    {
        public EmployeeEx(Database db)
            : base(db)
        {
        }

        public new bool Save()
        {
            bool result = false;

            if (!Exists())
            {
                Db.NonQuery("INSERT INTO RR_CO_EMPLE (FICHA,NOM_EMPLE, NOMBRE, APE_PAT, APE_MAT) VALUES ({0}, '{1}', '{2}', '{3}', '{4}')",
                    Id.ToString ("000000"), GetFullName(), FirstName, MiddleName, LastName);

                Db.NonQuery("INSERT INTO RR_DERECHOHABIENCIA (FICHA, NUM_FAM, REGIMEN_CONT, REGIMEN_TIT, NOMBRES, AP_PATERNO, AP_PATERNO, NOM_NACIO)",
                    Id, 0, DataMisc.ContractualArrangementToString(ContractualArrangement), DataMisc.ContractualArrangementToString(ContractualArrangement), 
                    FirstName, MiddleName, LastName, "-1");



            }

            return result;
        }
    }
}
