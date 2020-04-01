using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
namespace device_management.Models
{
    public class spec
    {
        public int specification_id { get; set; }
         internal Appdb Db { get; set; }
        spec()
        {

        }
        internal spec(Appdb db)
        {
            Db = db;
        }

        async public Task<spec> getSpecificationId(string ram, string screen, string conn, string storage)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "getSpecificationId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@RAM",
                DbType = DbType.String,
                Value = ram,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@storage",
                DbType = DbType.String,
                Value = storage,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@screen_size",
                DbType = DbType.String,
                Value = screen,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@connectivity",
                DbType = DbType.String,
                Value = conn,
            });
            return await ReadSpecification(await cmd.ExecuteReaderAsync());
        }
        async public Task<spec> ReadSpecification(DbDataReader reader)
        {
            var spec1 = new spec();
            if (await reader.ReadAsync())
            {
                spec1.specification_id = reader.GetInt32(0);
                return spec1;
            }
            return null;
        }
    }
}
