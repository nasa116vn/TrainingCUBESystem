﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace WebAPI.Controllers
{
    public class DBOracleUtils
    {
        public static OracleConnection GetDBConnection(string host, int port, String sid, String user, String password)
        {
             string connString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = "
                 + host + ")(PORT = " + port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
                 + sid + ")));Password=" + password + ";User ID=" + user;

            OracleConnection conn = new OracleConnection();

            conn.ConnectionString = connString;

            return conn;
        }
    }
}