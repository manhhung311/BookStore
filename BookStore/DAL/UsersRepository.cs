using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookStore.DAL {
    public class UsersRepository : Base<Users> {
        public UsersRepository():base() {
          
        }

        public Users login(string username, string password) {
            string sql = "select * from Users where username = @username and password = @password";
            Users user = null;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows) {
                read.Read();
                user = new Users(read["id"].ToString(), read["name"].ToString(), read["username"].ToString(),
                    read["password"].ToString(), read["address"].ToString(), read["email"].ToString(), read["phoneNumber"].ToString(),
                    read["role"].ToString(), read["createAt"].ToString(), read["updateAt"].ToString()
                    );
            }
            return user;
        }
    }
}