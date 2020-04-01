
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
        [Route("page")]
        public IActionResult GetAllDevices()
        {
            int limit1 = Convert.ToInt32(HttpContext.Request.Query["limit1"]);
            int offset1 = Convert.ToInt32(HttpContext.Request.Query["offset1"]);
            Db.Connection.Open();
            var query = new devices(Db);
            var result = query.GetAllDevices(limit1, offset1);
            Db.Connection.Close();
            return Ok(result);
        }
        [HttpGet]
        [Route("{search}")]

        public IActionResult getDeviceswithSearch(String search)
        {
            Db.Connection.Open();
            var query = new devices(Db);
            var result = query.getDeviceBySearch(search);
            Db.Connection.Close();
            return Ok(result);

        }
        [HttpGet]
        [Route("sort/sortcolumn={SortColumn}&sortdirection={SortDirection}")]

        public IActionResult getDeviceswithSorting(String SortColumn, String SortDirection)
        {
            Db.Connection.Open();
            var query = new devices(Db);
            var result = query.SortAlldevices(SortColumn, SortDirection);
            Db.Connection.Close();
            return Ok(result);

        }

        [HttpDelete]
        [Route("del/{device_id}")]
        public IActionResult DeleteOne(int device_id)
        {
            Db.Connection.Open();
            devices query = new devices(Db);
            query.device_id = device_id;
            query.Delete();
            Db.Connection.Close();
            return Ok();
        }

        [HttpPost]
        [Route("add")]
        async public Task<IActionResult> Post([FromBody]val body)
        {
            Db.Connection.Open();
            var que = new logicinsert(Db);
            await que.addDevice(body);
            Db.Connection.Close();
            return Ok();
        }

        [HttpPut]
        [Route("update/{device_id}")]
        async public Task<IActionResult> Put(int device_id, [FromBody]val body)
        {
            Db.Connection.Open();
            var query = new logicupdate(Db);
            body.device_id = device_id;
            await query.updateDevice(body);
            Db.Connection.Close();
            return Ok();
        }



        //Specification
        [HttpGet("specid")]
        async public Task<IActionResult> get_specification_id()
        {
            string ram = (HttpContext.Request.Query["ram"]);
            string screen = (HttpContext.Request.Query["screen"]);
            string conn = (HttpContext.Request.Query["connec"]);
            string stor = (HttpContext.Request.Query["storage"]);
            await Db.Connection.OpenAsync();
            var result = new spec(Db);
            var data = await result.getSpecificationId(ram, screen, conn, stor);
            return new OkObjectResult(data);
        }

        [HttpGet("specification")]
        public async Task<IActionResult> GetAllSpecification()
        {
            await Db.Connection.OpenAsync();
            var query = new Specification(Db);
            var result = await query.getAllSpecifications();
            return new OkObjectResult(result);
        }


        [HttpPost]
        [Route("addspecification")]
        async public Task<IActionResult> Postspec([FromBody]Specification body)
        {
            Db.Connection.Open();
            var que = new insertspec(Db);
            await que.addspecification(body);
            // item.Db = Db;
            //var result = item.Insert();
            Db.Connection.Close();
            return Ok();
        }
        [HttpPut]
        [Route("updatespecification/{specification_id}")]
        async public Task<IActionResult> Putspec(int specification_id, [FromBody]Specification body)
        {
            Db.Connection.Open();
            var query = new updatespec(Db);
            body.specification_id = specification_id;
            await query.updatespecification(body);
            Db.Connection.Close();
            return Ok();
        }
        public Appdb Db { get; }
    }
}
