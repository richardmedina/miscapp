using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stprm.DataEx
{
    public class RecordEx : Stprm.Data.Record
    {
        private readonly static string _tablename_benefits = "Beneficios";
        private readonly static string _tablename_sndconcepts = "Conceptos";
       
        private RecordTypeEx _recordtypeex;

        private string _user_creator = string.Empty;
        private string _user_modifier = string.Empty;
        private string _host_creator = string.Empty;
        private string _host_modifier = string.Empty;

        private DateTime _created_date;
        private DateTime _modified_date;

        public RecordEx(Stprm.Data.Database db, RecordTypeEx typeex)
            : base(db, (Data.RecordType) 0)
        {
            _recordtypeex = typeex;
        }

        public void SaveOperationInfo(DatabaseOperationEx operation, string tablename, string where)
        {
            if (operation == DatabaseOperationEx.Create)
            {
                Db.NonQuery("Update {0} set usuario_cr='{1}', host_cr='{2}', fecha_creado='{3}' {4}",
                    tablename, UserCreator, HostCreator, CreatedDate.ToString ("yyyyMMdd"), where);
                SaveOperationInfo(DatabaseOperationEx.Modify, tablename, where);
            }

            if (operation == DatabaseOperationEx.Modify)
            {
                Db.NonQuery("update {0} set usuario_modif='{1}', host_modif='{2}', fecha_modificado='{3}' {4}",
                    tablename, UserModifier, HostModifier, ModifiedDate.ToString ("yyyyMMdd"), where);
            }
        }

        public new RecordTypeEx Type {
            get { return _recordtypeex; }
        }
        public static string BenefitsTablename
        {
            get { return _tablename_benefits; }
        }

        public static string SndConceptsTablename
        {
            get { return _tablename_sndconcepts; }
        }

        

        public string UserCreator
        {
            get { return _user_creator; }
            set { _user_creator = value; }
        }

        public string UserModifier
        {
            get { return _user_modifier; }
            set { _user_modifier = value; }
        }

        public string HostCreator
        {
            get { return _host_creator; }
            set { _host_creator = value; }
        }

        public string HostModifier
        {
            get { return _host_modifier; }
        }

        public DateTime CreatedDate
        {
            get { return _created_date; }
            set { _created_date = value; }
        }

        public DateTime ModifiedDate
        {
            get { return _modified_date; }
            set { _modified_date = value; }
        }
    }
}
