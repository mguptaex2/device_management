using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
namespace device_management.Models
{
    public class val
    {
        public int device_id { get; set; }
        public int device_brand_id { get; set; }
        public int device_type_id { get; set; }
        public int status_id { get; set; }
        public int specification_id { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string price { get; set; }
        public string serial_number { get; set; }
        public string warranty_year { get; set; }
        public string purchase_date { get; set; }
        public string entry_date { get; set; }
    }
    public class logicinsert
    {
        public Appdb Db { get; }
        public logicinsert(Appdb db)
        {
            Db = db;
        }
        async public Task addDevice(val v)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "insertdevice";
            cmd.CommandType = CommandType.StoredProcedure;
            BindDevice(cmd, v);
            await cmd.ExecuteNonQueryAsync();
        }
        private void BindDevice(MySqlCommand cmd, val v)
        {
            cmd.Parameters.Add(new MySqlParameter("device_type_id", v.device_type_id));
            cmd.Parameters.Add(new MySqlParameter("device_brand_id", v.device_brand_id));
            cmd.Parameters.Add(new MySqlParameter("model", v.model));
            cmd.Parameters.Add(new MySqlParameter("color", v.color));
            cmd.Parameters.Add(new MySqlParameter("price", v.price));
            cmd.Parameters.Add(new MySqlParameter("serial_number", v.serial_number));
            cmd.Parameters.Add(new MySqlParameter("warranty_year", v.warranty_year));
            cmd.Parameters.Add(new MySqlParameter("purchase_date", v.purchase_date));
            cmd.Parameters.Add(new MySqlParameter("status_id", v.status_id));
            cmd.Parameters.Add(new MySqlParameter("specification_id", v.specification_id));
            cmd.Parameters.Add(new MySqlParameter("entry_date", v.entry_date));
        }
    }
    public class logicupdate
    {
        public Appdb Db { get; }
        public logicupdate(Appdb db)
        {
            Db = db;
        }
        async public Task updateDevice(val v1)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "updatedevice";
            cmd.CommandType = CommandType.StoredProcedure;
            BindDeviceput(cmd, v1);
            await cmd.ExecuteNonQueryAsync();
        }
        private void BindDeviceput(MySqlCommand cmd, val v1)
        {
            cmd.Parameters.Add(new MySqlParameter("device_id", v1.device_id));
            cmd.Parameters.Add(new MySqlParameter("device_type_id", v1.device_type_id));
            cmd.Parameters.Add(new MySqlParameter("device_brand_id", v1.device_brand_id));
            cmd.Parameters.Add(new MySqlParameter("model", v1.model));
            cmd.Parameters.Add(new MySqlParameter("color", v1.color));
            cmd.Parameters.Add(new MySqlParameter("price", v1.price));
            cmd.Parameters.Add(new MySqlParameter("serial_number", v1.serial_number));
            cmd.Parameters.Add(new MySqlParameter("warranty_year", v1.warranty_year));
            cmd.Parameters.Add(new MySqlParameter("purchase_date", v1.purchase_date));
            cmd.Parameters.Add(new MySqlParameter("status_id", v1.status_id));
            cmd.Parameters.Add(new MySqlParameter("specification_id", v1.specification_id));
            cmd.Parameters.Add(new MySqlParameter("entry_date", v1.entry_date));
        }
    }

    public class devices
    {
        public int device_id { get; set; }
        public string type { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string price { get; set; }
        public string serial_number { get; set; }
        public string warranty_year { get; set; }
        public string purchase_date { get; set; }

        public string status { get; set; }


        public Specification specifications { get; set; }
        public string assign_date { get; set; }
        public string return_date { get; set; }
        public name assign_to { get; set; }
        public name assign_by { get; set; }

        internal Appdb Db { get; set; }

        public devices()
        {
            specifications = new Specification();
            assign_to = new name();
            assign_by = new name();
        }

        internal devices(Appdb db)
        {
            Db = db;
        }
        public static string GetSafeString(MySqlDataReader reader, string colName)
        {

            return reader[colName] != DBNull.Value ? reader[colName].ToString() : "";
        }

        public static int GetInt(MySqlDataReader reader, string colName)
        {
            return reader[colName] != DBNull.Value ? (int)reader[colName] : default;
        }
        public Specification ReadSpecification(MySqlDataReader reader)
        {
           var spec1 = new Specification();
            spec1.RAM = GetSafeString(reader, "RAM");
            spec1.Storage = GetSafeString(reader, "Storage");
            spec1.Screen_size = GetSafeString(reader, "Screen_size");
            spec1.Connectivity = GetSafeString(reader, "Connectivity");

            return spec1;
        }
        private name ReadName(MySqlDataReader reader, name name1, string prefix)
        {
            name1 = new name();
            name1.first_name = GetSafeString(reader, prefix + "_first_name");
            name1.middle_name = GetSafeString(reader, prefix + "_middle_name");
            name1.last_name = GetSafeString(reader, prefix + "_last_name");
            return name1;
        }

        public List<devices> SortAlldevices(String SortColumn, String SortDirection)
        {
            using var cmd = Db.Connection.CreateCommand();

            cmd.CommandText = "select ad.assign_date as assign_date,ad.return_date as return_date, " +
                "u1.first_name as assign_by_first_name,u1.middle_name as assign_by_middle_name," +
                "u1.last_name as assign_by_last_name,u.first_name as assign_to_first_name, " +
                "u.middle_name as assign_to_middle_name, u.last_name as assign_to_last_name, " +
                "d.device_id as device_id, d.model as model, d.color as color,d.price as price," +
                " d.serial_number as serial_number, d.purchase_date as purchase_date," +
                " d.entry_date as entry_date, d.warranty_year as warranty_year, sf.RAM as RAM," +
                " sf.connectivity as connectivity, sf.storage as storage, sf.screen_size as screen_size," +
                "s.status as status, dt.type as type, db.brand as brand from device as d" +
                " inner join device_type as dt inner join device_brand as db inner join status as s " +
                "inner join  specification as sf on d.device_type_id = dt.device_type_id and" +
                " d.device_brand_id = db.device_brand_id and d.status_id = s.status_id and" +
                " d.specification_id = sf.specification_id left join assign_device as ad" +
                " on d.device_id = ad.device_id left join  user as u  on ad.user_id = u.user_id" +
                " left join user as u1 " +
                "on  u1.user_id = ad.assign_by order by " + @SortColumn + " " + @SortDirection;


            return ReadAll(cmd.ExecuteReader());


        }

        public List<devices> GetAllDevices(int limit1, int offset1)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = " call getAllDevice(@limit1,@offset1)";
            cmd.Parameters.AddWithValue("@limit1", limit1);
            cmd.Parameters.AddWithValue("@offset1", offset1);
            // cmd.CommandType = CommandType.StoredProcedure;
            return ReadAll(cmd.ExecuteReader());

        }
        public List<devices> getDeviceBySearch(string search)
        {
            using (var cmd = Db.Connection.CreateCommand())
            {

                cmd.CommandText = "call getDevicesBySearch(@search)";
                cmd.Parameters.AddWithValue("@search", search);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                    return ReadAll(reader);
            }
        }


        //delete device
        public int Delete()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "delbyid";
            cmd.CommandType = CommandType.StoredProcedure;
            Bindid(cmd);
            cmd.ExecuteNonQuery();
            return 1;
        }
        private void Bindid(MySqlCommand cmd)
        {
            var device_idParam = new MySqlParameter("device_id", device_id);
            if (cmd.Parameters.Contains("device_id"))
            {
                cmd.Parameters["device_id"].Value = device_id;
            }
            else
            {
                cmd.Parameters.Add(device_idParam);
            }
        }

        private List<devices> ReadAll(MySqlDataReader reader)
        {
            var posts = new List<devices>();
            using (reader)
            {
                while (reader.Read())
                {
                    var post = new devices();
                    post.device_id = GetInt(reader, "device_id");
                    post.type = GetSafeString(reader, "type");
                    post.brand = GetSafeString(reader, "brand");
                    post.model = GetSafeString(reader, "model");
                    post.color = GetSafeString(reader, "color");
                    post.price = GetSafeString(reader, "price");
                    post.serial_number = GetSafeString(reader, "serial_number");
                    post.warranty_year = GetSafeString(reader, "warranty_year");
                    post.status = GetSafeString(reader, "status");
                    post.purchase_date = Convert.ToDateTime(reader["purchase_date"]).ToString("dd/MM/yyyy");
                    post.specifications = ReadSpecification(reader);
                    post.assign_date = GetSafeString(reader, "assign_date");
                    post.return_date = GetSafeString(reader, "return_date");
                    post.assign_by = ReadName(reader, post.assign_by, "assign_by");
                    post.assign_to = ReadName(reader, post.assign_to, "assign_to");
                    posts.Add(post);
                }
            }
            return posts;
        }



    }
}
