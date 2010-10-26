using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Stprm.Data;

namespace Stprm.DataEx
{
    public class Concept : RecordEx
    {
        private int _id;
        private string _name;
        private string _description;

        public Concept(Database db)
            : base(db, RecordTypeEx.Concept)
        {
        }

        public override bool Update ()
        {
            bool result = false;

            IDataReader reader = Db.Query("select * from {0} where id ={1}", ConceptsTableName, Id);

            if (reader.Read())
            {
                fill_from_reader(reader);
                result = true;
            }
            reader.Close();

            return result;
        }

        public override bool Exists()
        {
            bool result = false;

            IDataReader reader = Db.Query("select id from {0} where id= {1}",
                SndConceptsTablename, Id);

            if (reader.Read())
                result = true;

            reader.Close();

            return result;

        }

        public override bool Save()
        {
            bool result = false;

            if (Exists())
            {
                Db.NonQuery("update {0} set nombre='{1}', descripcion='{2}' where id={3}",
                    SndConceptsTablename, Name, Description, Id);
                SaveOperationInfo (DatabaseOperationEx.Modify, SndConceptsTablename, "where id=" + Id.ToString ());
            }
            else
            {
                Db.NonQuery("insert into {0} (nombre,descripcion) values ('{1}', '{2}')",
                    SndConceptsTablename, Name, Description);
                SaveOperationInfo(DatabaseOperationEx.Create, SndConceptsTablename, "where id=" + Id.ToString());
            }
            
            return result;
        }

        protected void fill_from_reader(IDataReader reader)
        {
            //_id = (int)reader["id"];

            if (!int.TryParse(reader["id"].ToString(), out _id))
                _id = 0;

            Name = reader["nombre"].ToString();
            Description = reader["descripcion"].ToString();
        }

        public static ConceptCollection GetAllFromDb (Database db)
        {
            ConceptCollection concepts = new ConceptCollection();

            IDataReader reader = db.Query("Select * from {0}", RecordEx.SndConceptsTablename);

            while (reader.Read())
            {
                Concept concept = new Concept(db);
                concept.fill_from_reader(reader);
                concepts.Add(concept);
            }

            return concepts;
        }

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
