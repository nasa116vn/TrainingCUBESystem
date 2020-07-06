using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers.Api
{
    public class StoreApiController : ApiController
    {
        StoreModel store = new StoreModel();
        // GET: api/StoreApi
        public IEnumerable<StoreModel> Get()
        {
            return store.GetAllStore();
        }

        // GET: api/StoreApi?STORE_CD=1001
        public StoreModel Get(string STORE_CD)
        {
            var storeApi = store.GetAllStore().Find(x => x.STORE_CD.Equals(STORE_CD));
            return storeApi;
        }

        // POST: api/StoreApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/StoreApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/StoreApi/5
        public void Delete(string store_cd)
        {
            store.DeleteStore(store_cd);
        }
    }
}
