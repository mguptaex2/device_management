using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace device_management.Models
{
    public class Specification
    {

        public int specification_id { get; set; }

        public string RAM { get; set; }
        public string Storage { get; set; }
        public string Screen_size { get; set; }

        public string Connectivity { get; set; }

        internal Appdb Db { get; set; }

        public Specification()
        {
        }

        internal Specification(Appdb db)
        {
            Db = db;
        }

        public async Task<List<Specification>> getAllSpecifications()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"select * from specification;";

            return await ReadSpecifications(await cmd.ExecuteReaderAsync());
        }







        public async Task<List<Specification>> ReadSpecifications(DbDataReader reader)
        {
            var specifications = new List<Specification>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var spec1 = new Specification()
                    {
                        specification_id = reader.GetInt32(0),
                        RAM = reader.GetString(1),
                        Storage = reader.GetString(2),
                        Screen_size = reader.GetString(3),
                        Connectivity = reader.GetString(4)


                    };
                    specifications.Add(spec1);
                }
            }
            return specifications;
        }


    }
    public class insertspec
    {
        public Appdb Db { get; }
        public insertspec(Appdb db)
        {
            Db = db;
        }
        async public Task addspecification(Specification s)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "addSpecification";
            cmd.CommandType = CommandType.StoredProcedure;
            Bindspec(cmd, s);
            await cmd.ExecuteNonQueryAsync();
        }
        private void Bindspec(MySqlCommand cmd, Specification s)
        {
            cmd.Parameters.Add(new MySqlParameter("RAM", s.RAM));
            cmd.Parameters.Add(new MySqlParameter("storage", s.Storage));
            cmd.Parameters.Add(new MySqlParameter("screen_size", s.Screen_size));
            cmd.Parameters.Add(new MySqlParameter("connectivity", s.Connectivity));
        }
    }
    public class updatespec
    {
        public Appdb Db { get; }
        public updatespec(Appdb db)
        {
            Db = db;
        }
        async public Task updatespecification(Specification s1)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "updatespecification";
            cmd.CommandType = CommandType.StoredProcedure;
            Bindspecs(cmd, s1);
            await cmd.ExecuteNonQueryAsync();
        }
        private void Bindspecs(MySqlCommand cmd, Specification s1)
        {
            cmd.Parameters.Add(new MySqlParameter("specification_id", s1.specification_id));
            cmd.Parameters.Add(new MySqlParameter("RAM", s1.RAM));
            cmd.Parameters.Add(new MySqlParameter("storage", s1.Storage));
            cmd.Parameters.Add(new MySqlParameter("screen_size", s1.Screen_size));
            cmd.Parameters.Add(new MySqlParameter("connectivity", s1.Connectivity));
        }
    }
}











