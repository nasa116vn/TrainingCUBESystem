using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAPI.Models;
using WebAPI.Controllers;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

namespace WebAPI.Models
{
    public class Device
    {

        OracleConnection conn = DBUtils.GetDBConnection();
        OracleCommand cmd = new OracleCommand();

        [Key]
        public String IMEI { get; set; }
        public String TOKEN { get; set; }
        [Required(ErrorMessage = "Requied MODEL")]
        public String MODEL { get; set; }
        public String MAKER { get; set; }
        public int BUMON_ID { get; set; }
        public String BUMON_NAME { get; set; }
        public String STORE_CD { get; set; }
        public String STORE_NAME { get; set; }
        public DateTime? INSERT_DATE { get; set; }
        public String INSERT_USER { get; set; }
        public String UPDATE_USER { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        public int DEL_FLAG { get; set; }

        public Device(string iMEI, string tOKEN, string mODEL, string mAKER, int bUMON_ID, string bUMON_NAME, string sTORE_CD, string sTORE_NAME, DateTime? iNSERT_DATE, string iNSERT_USER, string uPDATE_USER, DateTime? uPDATE_DATE, int dEL_FLAG)
        {
            IMEI = iMEI;
            TOKEN = tOKEN;
            MODEL = mODEL;
            MAKER = mAKER;
            BUMON_ID = bUMON_ID;
            BUMON_NAME = bUMON_NAME;
            STORE_CD = sTORE_CD;
            STORE_NAME = sTORE_NAME;
            INSERT_DATE = iNSERT_DATE;
            INSERT_USER = iNSERT_USER;
            UPDATE_USER = uPDATE_USER;
            UPDATE_DATE = uPDATE_DATE;
            DEL_FLAG = dEL_FLAG;
        }

        public Device()
        {

        }

        //Lay danh sach Device len giao dien Device
        public List<Device> GetAllDevices()
        {
            conn.Open();
            String sql = "select d.IMEI, TOKEN, MODEL, MAKER, BUMON_NAME, STORE_NAME, t.INSERT_DATE, t.INSERT_USER, t.UPDATE_DATE,t.UPDATE_USER,d.DEL_FLAG,b.BUMON_ID,s.STORE_CD " +
                "FROM SVR_DEVICE_SETTING t join SVR_DEVICE d on t.IMEI = d.IMEI join SVR_STORE s on s.STORE_CD = t.STORE_CD join SVR_BUMON b on b.BUMON_ID = t.BUMON_ID";

            cmd.Connection = conn;
            cmd.CommandText = sql;

            List<Device> lsDevices = new List<Device>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var device = new Device();
                        device.IMEI = reader["IMEI"].ToString();
                        device.TOKEN = reader["TOKEN"].ToString();
                        device.MAKER = reader["MAKER"].ToString();
                        device.MODEL = reader["MODEL"].ToString();
                        device.BUMON_ID = Convert.ToInt32(reader["BUMON_ID"].ToString());
                        device.BUMON_NAME = reader["BUMON_NAME"].ToString();
                        device.STORE_CD = reader["STORE_CD"].ToString();
                        device.STORE_NAME = reader["STORE_NAME"].ToString();
                        device.INSERT_DATE = Convert.ToDateTime(reader["INSERT_DATE"].ToString());
                        device.INSERT_USER = reader["INSERT_USER"].ToString();
                        device.UPDATE_USER = reader["UPDATE_USER"].ToString();
                        string ngay = reader["UPDATE_DATE"].ToString();
                        if(ngay.Equals(""))
                        {
                            device.UPDATE_DATE = null;
                        }
                        else
                        {
                            device.UPDATE_DATE = Convert.ToDateTime(ngay);
                        }
                       
                        device.DEL_FLAG = Convert.ToInt32(reader["DEL_FLAG"].ToString());

                        lsDevices.Add(device);

                    }
                }
                conn.Close();
            }
            return lsDevices;
        }
        
        //Them moi Device
        public int AddDevice(Device device, string username)
        {
            int i;
            foreach (var item in GetAllDevices())
            {

                if (item.IMEI.Equals(device.IMEI))
                {
                    i = -1;
                    return i;
                }
            }
            if (!device.IMEI.Equals("") || device.IMEI != null)
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "INSERT_DEVICE";
                cmd.Parameters.Add("IMEI", OracleDbType.Varchar2, device.IMEI, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("MODEL", OracleDbType.Varchar2, device.MODEL, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("MAKER", OracleDbType.Varchar2, device.MAKER, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2, username, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("STORE_CD", OracleDbType.Varchar2, device.STORE_CD, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("BUMON_ID", OracleDbType.Int32, device.BUMON_ID, System.Data.ParameterDirection.Input);
                i = cmd.ExecuteNonQuery();

                conn.Close();
               
                    return i;
               
               
            }
            else
            {
                i = -1;
            }

            return i;
        }

        //Xoa Device
        public int DeleteDevice(string IMEI)
        {
            int i;
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "REMOVE_DEVICE";
            cmd.Parameters.Add("IMEI", OracleDbType.Varchar2, IMEI, System.Data.ParameterDirection.Input);

            i = cmd.ExecuteNonQuery();
            conn.Close();
            
            return i;
        }


        //Cap nhat Device
        public int UpdateDevice(Device deviceEdit, string username)
        {
            conn.Open();
            int i;
            if (deviceEdit.MODEL != null || !deviceEdit.MODEL.Equals("") || deviceEdit.MAKER != null || deviceEdit.MAKER.Equals(""))
            {
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = "UPDATE_DEVICE";
                cmd.Parameters.Add("IMEI", OracleDbType.Varchar2, deviceEdit.IMEI, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("MODEL", OracleDbType.Varchar2, deviceEdit.MODEL, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("MAKER", OracleDbType.Varchar2, deviceEdit.MAKER, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("STORE_CD", OracleDbType.Varchar2, deviceEdit.STORE_CD, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("BUMON_ID", OracleDbType.Int32, deviceEdit.BUMON_ID, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2, username, System.Data.ParameterDirection.Input);

                i = cmd.ExecuteNonQuery();
                conn.Close();
                return i;
            }
            else
            {
                i = -1;
            }
            return i;

        }

    }
}