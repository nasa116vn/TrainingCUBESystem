using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Web;
using WebAPI.Controllers;

namespace WebAPI.Models
{
    public class ViewDB
    {
        OracleConnection conn = DBUtils.GetDBConnection();
        OracleCommand cmd = new OracleCommand();


        [Key]
        public int LOG_ID { get; set; }
        public String IMEI { get; set; }
        [Required(ErrorMessage = "Requied BUMON_NAME")]
        public String BUMON_NAME { get; set; }
        public String STORE_NAME { get; set; }
        public String PRODUCT_NAME { get; set; }
        public DateTime VIEW_DATE { get; set; }
        public int VIEWS { get; set; }
        public DateTime INSERT_DATE { get; set; }
        public DateTime UPDATE_DATE { get; set; }

        public ViewDB(int lOG_ID,string iMEI, string bUMON_NAME, string sTORE_NAME, string pRODUCT_NAME, DateTime vIEW_DATE, int vIEWS, DateTime iNSERT_DATE, DateTime uPDATE_DATE)
        {
            LOG_ID = lOG_ID;
            IMEI = iMEI;
            BUMON_NAME = bUMON_NAME;
            STORE_NAME = sTORE_NAME;
            PRODUCT_NAME = pRODUCT_NAME;
            VIEW_DATE = vIEW_DATE;
            VIEWS = vIEWS;
            INSERT_DATE = iNSERT_DATE;
            UPDATE_DATE = uPDATE_DATE;
        }

        public ViewDB()
        {

        }



        // Lay danh sach View len giao dien View
        public List<ViewDB> GetAllView()
        {
            conn.Open();
            String sql = "select v.LOG_ID, v.IMEI,b.BUMON_NAME, t.STORE_NAME, p.TITLE ,v.VIEW_DATE,v.VIEWS, v.INSERT_DATE,v.UPDATE_DATE from SVR_VIEW v join SVR_BUMON b on v.BUMON_ID = b.BUMON_ID join SVR_STORE t on v.STORE_CD = t.STORE_CD join SVR_DEVICE d on v.IMEI = d.IMEI join SVR_PRODUCT p on v.PRODUCT_ID = p.PRODUCT_ID";

            cmd.Connection = conn;
            cmd.CommandText = sql;
            List<ViewDB> lsview = new List<ViewDB>();
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var views = new ViewDB();
                        views.LOG_ID = Convert.ToInt32(reader["LOG_ID"].ToString());
                        views.IMEI = reader["IMEI"].ToString();
                        views.BUMON_NAME = reader["BUMON_NAME"].ToString();
                        views.STORE_NAME = reader["STORE_NAME"].ToString();
                        views.PRODUCT_NAME = reader["TITLE"].ToString();
                        views.VIEW_DATE = Convert.ToDateTime(reader["VIEW_DATE"].ToString());
                        views.VIEWS = Convert.ToInt32(reader["VIEWS"].ToString());
                        views.INSERT_DATE = Convert.ToDateTime(reader["INSERT_DATE"].ToString());
                        views.UPDATE_DATE = Convert.ToDateTime(reader["UPDATE_DATE"].ToString());

                        lsview.Add(views);
                    }
                }
                conn.Close();
            }
            return lsview;
        }

        //Xu ly chung
        //Ham lay danh sach IMEI tu bang device dung tren giao dien Views
        public List<Device> GetListIMEI()
        {
            List<Device> lsIMEIDevices = new List<Device>();
            conn.Open();
            String sql = "select IMEI from SVR_DEVICE";

            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var device = new Device();
                        device.IMEI = reader["IMEI"].ToString();

                        lsIMEIDevices.Add(device);
                    }
                }
                conn.Close();
            }


            return lsIMEIDevices;
        }
    

    }
}