using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookStore.DAL {
    public class OrdersRepository:Base<Orders> {
        public OrdersRepository():base() {

        }

        public List<Orders> findByUserId(int id) {
            List<Orders> orders = new List<Orders>();
            string sql = "select * from Orders where usersId = @id and state = 0";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows) {
                while (read.Read()) {
                    orders.Add(new Orders(read["id"].ToString(), read["state"].ToString(), read["usersId"].ToString(),
                        read["createAt"].ToString(), read["updateAt"].ToString()
                        ));
                }
            }
            read.Close();
            return orders;
        }
    }
}