using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace WebAPI.Controllers
{
    public class DBUtils
    {
        public static OracleConnection GetDBConnection()
        {
            string host = "192.168.11.115";
            int port = 1522;
            string sid = "orcl19c";
            string user = "thuanphat";
            string password = "123456";

            return DBOracleUtils.GetDBConnection(host, port, sid, user, password);
        }
    }
}