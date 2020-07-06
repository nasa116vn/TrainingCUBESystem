using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using WebAPI.Controllers;

namespace WebAPI.Models
{
    public class Bumon
    {
        OracleConnection conn = DBUtils.GetDBConnection();
        OracleCommand cmd = new OracleCommand();

        public int bumon_id { get; set; }
        public String bumon_name { get; set; }

        public Bumon(int bumon_id, string bumon_name)
        {
            this.bumon_id = bumon_id;
            this.bumon_name = bumon_name;
        }

        public Bumon()
        {

        }

        //Lay danh sach id va ten cua bumon
        public List<Bumon> GetBumonName()
        {
            conn.Open();
            String sql = "select BUMON_ID,BUMON_NAME  from SVR_BUMON";
            cmd.Connection = conn;
            cmd.CommandText = sql;

            List<Bumon> listBumonName = new List<Bumon>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var bumonname = new Bumon();
                        bumonname.bumon_id = Convert.ToInt32(reader["BUMON_ID"].ToString());
                        bumonname.bumon_name = reader["BUMON_NAME"].ToString();

                        listBumonName.Add(bumonname);
                    }
                }
            }
            return listBumonName;
        }
    }
}