using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers.Api
{
    public class DeviceApiController : ApiController
    {
        Device device = new Device();
        // GET: api/DeviceApi
        public IEnumerable<Device> Get()
        {
            return device.GetAllDevices();
        }

        // GET: api/DeviceApi?IMEI=140
        public Device Get(string IMEI)
        {
            var deviceApi = device.GetAllDevices().Find(x => x.IMEI.Equals(IMEI));
            return deviceApi;
        }

        // POST: api/DeviceApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DeviceApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DeviceApi/5
        public void Delete(int id)
        {
        }
    }
}
