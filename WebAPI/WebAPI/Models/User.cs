using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using WebAPI.Controllers;
using System.Data.Common;

namespace WebAPI.Models
{
    public class User
    {

        OracleConnection conn = DBUtils.GetDBConnection();
        OracleCommand cmd = new OracleCommand();

        private String username;
        private String password;
        private int companyID;
        private int bumonID;
        private DateTime insert_date;
        private String insert_user;
        private DateTime update_date;
        private String update_user;
        private int del_flag; 

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int CompanyID { get => companyID; set => companyID = value; }
        public int BumonID { get => bumonID; set => bumonID = value; }
        public DateTime Insert_date { get => insert_date; set => insert_date = value; }
        public string Insert_user { get => insert_user; set => insert_user = value; }
        public DateTime Update_date { get => update_date; set => update_date = value; }
        public string Update_user { get => update_user; set => update_user = value; }
        public int Del_flag { get => del_flag; set => del_flag = value; }



        public User()
        {

        }

        public User(string username, string password, int companyID, int bumonID, DateTime insert_date, string insert_user, DateTime update_date, string update_user, int del_flag)
        {
            Username = username;
            Password = password;
            CompanyID = companyID;
            BumonID = bumonID;
            Insert_date = insert_date;
            Insert_user = insert_user;
            Update_date = update_date;
            Update_user = update_user;
            Del_flag = del_flag;
        }
        
        //Lay danh sach Users de check login ben HomeController
        public List<User> ListUsers()
        {
            List<User> lsUser = new List<User>();
            conn.Open();
            string sql = "select* from SVR_USER";
           

            cmd.Connection = conn;
            cmd.CommandText = sql;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        var user = new User();
                        user.Username = reader["USERNAME"].ToString();
                        user.Password = reader["PASSWORD"].ToString();
                        user.CompanyID = Convert.ToInt32(reader["COMPANY_ID"].ToString());
                        user.BumonID = Convert.ToInt32(reader["BUMON_ID"].ToString());
                        user.Insert_date = Convert.ToDateTime(reader["INSERT_DATE"].ToString());
                        user.Insert_user = reader["INSERT_USER"].ToString();
                        user.Update_date = Convert.ToDateTime(reader["UPDATE_DATE"].ToString());
                        user.Update_user = reader["UPDATE_USER"].ToString();
                        user.Del_flag = Convert.ToInt32(reader["DEL_FLAG"].ToString());
               
                        //Co the chinh lai gan user vao lsit de dung khi can
                        lsUser.Add(user);
                    }
                }
                conn.Close();
            }
            return lsUser;
        }        

        //Xu ly doi mat khau
        public void ChangePass(String newPass, String username)
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "UPDATE_PASS";
            cmd.Parameters.Add("USERNAME", OracleDbType.Varchar2, username, System.Data.ParameterDirection.Input);
            cmd.Parameters.Add("NEWPASS", OracleDbType.Varchar2, newPass, System.Data.ParameterDirection.Input);
            object o = cmd.ExecuteScalar();
            conn.Close();
        }
    }
}