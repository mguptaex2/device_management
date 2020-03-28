
using System;
using System.Threading.Tasks;
using device_management.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace device_management.Controllers
{

    [Route("dm/[controller]")]
    public class DeviceController : Controller
    {

        public DeviceController(Appdb db)
        {
            Db = db;
        }

        [HttpGet]
        public IActionResult GetAllDevices()
        {
            
            Db.Connection.Open();
            var query = new devices(Db);
            var result = query.GetAllDevices();
            Db.Connection.Close();
            return Ok(result);
        }
        [HttpGet]
        [Route("{status}")]

        public IActionResult getDeviceswithStatus(String Status)
        {
            Db.Connection.Open();
            var query = new devices(Db);
            var result = query.getDeviceByStatus(Status);
            Db.Connection.Close();
            return Ok(result);

        }
        public Appdb Db { get; }
    }
}
