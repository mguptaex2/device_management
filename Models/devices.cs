using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace device_management.Models
{
    public class devices
    {
        public string  type{ get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        public string price { get; set; }
        public string serial_number { get; set; }
        public string warranty_year { get; set; }
        public string purchase_date { get; set; }

        public string status { get; set; }
        public Specification specifications { get; set; }
       

        internal Appdb Db { get; set; }

        public devices()
        {
            specifications = new Specification();
            
        }

        internal devices(Appdb db)
        {
            Db = db;
        }
        public static string GetSafeString(MySqlDataReader reader, string colName)
        {
            
            return reader[colName] != DBNull.Value ? reader[colName].ToString() : "";
        }
        private Specification ReadSpecification(MySqlDataReader reader , Specification spec1)
        {
            spec1 = new Specification();
            spec1.RAM = GetSafeString(reader,"RAM");
            spec1.Storage = GetSafeString(reader, "Storage");
            spec1.Screen_size = GetSafeString(reader, "Screen_size");
            spec1.Connectivity = GetSafeString(reader, "Connectivity");

            return spec1;
        }
        public List<devices> GetAllDevices()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "getAllDevices";
            cmd.CommandType = CommandType.StoredProcedure;
            return ReadAll(cmd.ExecuteReader());
     
         }
        public List<devices> getDeviceByStatus(string status)
        {
            using (var cmd = Db.Connection.CreateCommand())
            {

                cmd.CommandText = "call getDevicesByStatus(@status)";
               cmd.Parameters.AddWithValue("@status", status);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                    return ReadAll(reader);
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
                    post.type = GetSafeString(reader, "type");
                    post.brand= GetSafeString(reader, "brand");
                    post.model = GetSafeString(reader, "model");
                    post.color = GetSafeString(reader, "color");
                    post.price = GetSafeString(reader, "price");
                    post.serial_number = GetSafeString(reader, "serial_number");
                    post.warranty_year = GetSafeString(reader, "warranty_year");
                    post.status = GetSafeString(reader, "status");
                    post.purchase_date = Convert.ToDateTime(reader["purchase_date"]).ToString("dd/MM/yyyy");
                    post.specifications = ReadSpecification(reader,post.specifications);


                    posts.Add(post);
                }
            }
            return posts;
        }

       

        

    }
}
