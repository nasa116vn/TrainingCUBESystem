using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Web;
using WebAPI.Controllers;
using WebAPI.Models;

namespace WebAPI.Models
{
    public class StoreModel
    {
        OracleConnection conn = DBUtils.GetDBConnection();
        OracleCommand cmd = new OracleCommand();

        [Key]
        public String STORE_CD { get; set; }
        [Required(ErrorMessage = "Requied Store_name")]
        public String STORE_NAME { get; set; }
        public DateTime? INSERT_DATE { get; set; }
        public String INSERT_USER { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        public String UPDATE_USER { get; set; }
        public int DEL_FLAG { get; set; }

        public StoreModel(string sTORE_CD, string sTORE_NAME, DateTime iNSERT_DATE, string iNSERT_USER,DateTime uPDATE_DATE, string uPDATE_USER, int dEL_FLAG)
        {
            STORE_CD = sTORE_CD;
            STORE_NAME = sTORE_NAME;
            INSERT_DATE = iNSERT_DATE;
            INSERT_USER = iNSERT_USER;
            UPDATE_DATE = uPDATE_DATE;
            UPDATE_USER = uPDATE_USER;
            DEL_FLAG = dEL_FLAG;
        }


        public StoreModel()
        {
            
        }

        //Lay ds Store len giao dien Store
        public List<StoreModel> GetAllStore()
        {
            conn.Open();
            //String sql = "select STORE_CD,STORE_NAME,TO_CHAR(INSERT_DATE,'YYYY-MM-DD HH24:MI:SS')AS INSERT_DATE,INSERT_USER," +
            //    "TO_CHAR(UPDATE_DATE,'YYYY-MM-DD HH24:MI:SS') AS UPDATE_DATE,UPDATE_USER, DEL_FLAG from SVR_STORE";
            String sql = "select* from SVR_STORE";

            cmd.Connection = conn;
            cmd.CommandText = sql;

            List<StoreModel> lsStore = new List<StoreModel>();


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        
                        var store = new StoreModel();
                        store.STORE_CD = reader["STORE_CD"].ToString();
                        store.STORE_NAME = reader["STORE_NAME"].ToString();
                        store.INSERT_DATE = Convert.ToDateTime(reader["INSERT_DATE"].ToString());
                        store.INSERT_USER = reader["INSERT_USER"].ToString();
                        string ngay = reader["UPDATE_DATE"].ToString();
                        if (ngay.Equals(""))
                        {

                            store.UPDATE_DATE = null;
                        }
                        else
                        {
                            store.UPDATE_DATE = Convert.ToDateTime(ngay);
                        }

                        store.UPDATE_USER = reader["UPDATE_USER"].ToString();
                        store.DEL_FLAG = Convert.ToInt32(reader["DEL_FLAG"].ToString());
                        lsStore.Add(store);
                        //Cach khac de lay du lieu len bang nhung chi chuyen duoc 1 doi tuong cuoi 
                        //StoreModel storeRec = new StoreModel()
                        //{
                        //    Store_cd = store_cd,
                        //    Store_name = store_name,
                        //    Insert_date = insert_date,
                        //    Insert_user = insert_user,
                        //    Update_date = update_date,
                        //    Update_user = update_user,
                        //    Del_flag = del_flag

                        //};
                        //ViewBag.Message = storeRec;
                    }
                }
                conn.Close();
            }
            return lsStore;
        }

        //Cap nhat Store
        public int UpdateStore(StoreModel storeEdit, string USERNAME)
        {
            int i;
            conn.Open();
            cmd.Connection = conn;
            if (!storeEdit.STORE_CD.Equals("") || !storeEdit.Equals(""))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "UPDATE_STORE";
                cmd.Parameters.Add("STORE_CD", OracleDbType.Varchar2, storeEdit.STORE_CD, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("STORE_NAME", OracleDbType.Varchar2, storeEdit.STORE_NAME, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2, USERNAME, System.Data.ParameterDirection.Input);
                i = cmd.ExecuteNonQuery();
                conn.Close();

            }
            else
            {
                i = -1;
            }
            return i;
        }

        //Them Store
        public int AddStore(StoreModel store,string username)
        {
            int i;
            foreach (var item in GetAllStore())
            {

                if (item.STORE_CD.Equals(store.STORE_CD))
                {
                    i = -1;
                    return i;
                }
            }
            if (!store.STORE_CD.Equals(""))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "INSERT_STORE";
                cmd.Parameters.Add("STORE_CD", OracleDbType.Varchar2, store.STORE_CD, System.Data.ParameterDirection.Input);
                cmd.Parameters.Add("STORE_NAME", OracleDbType.Varchar2, store.STORE_NAME, System.Data.ParameterDirection.Input);
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

        //Lay danh sach cd va ten cua store
        public List<StoreModel> GetStoreName()
        {
            conn.Open();
            String sql = "select STORE_CD,STORE_NAME  from SVR_STORE";
            cmd.Connection = conn;
            cmd.CommandText = sql;

            List<StoreModel> listStoreName = new List<StoreModel>();

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var storename = new StoreModel();
                        storename.STORE_CD = reader["STORE_CD"].ToString();
                        storename.STORE_NAME = reader["STORE_NAME"].ToString();

                        listStoreName.Add(storename);
                    }
                }
            }
            return listStoreName;

        }

        //Xoa mot store
        public int DeleteStore(string Store_cd)
        {
            int i;
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "REMOVE_STORE";
            cmd.Parameters.Add("STORE_CD", OracleDbType.Varchar2, Store_cd, System.Data.ParameterDirection.Input);

            i = cmd.ExecuteNonQuery();
            conn.Close();
            
            return i;
        }

    }
}