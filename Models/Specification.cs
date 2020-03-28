using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace device_management.Models
{
    public class Specification
    {
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string Screen_size { get; set; }
        public string Connectivity { get; set; }


/*
        internal Appdb Db { get; set; }

        public Specification()
        {
        }

        internal Specification(Appdb db)
        {
            Db = db;
        }
        */
     
    }
}


   

