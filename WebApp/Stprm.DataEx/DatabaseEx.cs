using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Stprm.Data;

namespace Stprm.DataEx
{
    public class DatabaseEx : Database
    {
        private UserDataEx _userdata;

        public DatabaseEx(string hostname, string userid, string password, string dbsource)
            : base(hostname, userid, password, dbsource)
        {
        }

        public UserDataEx UserData
        {
            get { return _userdata; }
            set { _userdata = value; }
        }
    }
}
