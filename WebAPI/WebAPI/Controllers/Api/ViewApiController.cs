using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers.Api
{
    public class ViewApiController : ApiController
    {
        ViewDB view = new ViewDB();
        // GET: api/ViewApi
        public IEnumerable<ViewDB> Get()
        {
            return view.GetAllView();
        }

        // GET: api/ViewApi?IMEI=5
        public ViewDB Get(string IMEI)
        {
            return view.GetAllView().Find(x =>x.IMEI.Equals(IMEI));
        }


        // POST: api/ViewApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ViewApi/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/ViewApi/5
        public void Delete(int id)
        {
        }
    }
}
