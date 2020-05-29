using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Models;

namespace WebAPI.Models
{
    public class StoreModel
    {
        StoreDBContext1 dBContext1 = new StoreDBContext1();

        //getAll data table Store
        public IEnumerable<SVR_STORE> GetAllStore()
        {
            return dBContext1.SVR_STORE.ToList();
        }

        public SVR_STORE GetStoreByID(String store_cd)
        {
            return dBContext1.SVR_STORE.Where(x => x.STORE_CD == store_cd).FirstOrDefault();
        }
    }
}