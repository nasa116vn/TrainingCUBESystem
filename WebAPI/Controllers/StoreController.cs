using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class StoreController : ApiController
    {
        StoreModel store = new StoreModel();

        [HttpGet]
        public IEnumerable<SVR_STORE> GetAllstore()
        {
            return store.GetAllStore();
        }

        [HttpGet]
        public SVR_STORE GetStoreByID(String CD)
        {
            return store.GetStoreByID(CD);
        }
    }
}
