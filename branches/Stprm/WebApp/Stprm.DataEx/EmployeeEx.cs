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

        public new bool Exists()
        {
            EmployeeEx employee = new EmployeeEx(Db);
            employee.Id = Id;

            return employee.Update();
        }

        public new bool Save()
        {
            bool result = false;

            if (!Exists())
            {
                string arra = ContractualArrangement == Data.ContractualArrangement.Plant ? "PS" : "TS";


                Db.NonQuery("exec sp_AgregarTrabajador '{0}', '{1}', '{2}', '{3}', '{4}'",
                    Id.ToString("000000"), FirstName, MiddleName, LastName, arra);


                result = true;
            }

            return result;
        }
    }
}
