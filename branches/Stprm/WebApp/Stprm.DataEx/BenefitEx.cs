using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Stprm.Data;

namespace Stprm.DataEx
{
    public class BenefitEx : RecordEx
    {
        private int _employeeid;
        private string _ben_type;
        private string _ben_name;
        
        public BenefitEx(Database db) : base (db, RecordTypeEx.Benefit)
        {
        }


        public int EmployeeId
        {
            get { return _employeeid; }
            set { _employeeid = value; }
        }
    }
}
