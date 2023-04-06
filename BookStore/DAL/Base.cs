using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace BookStore.DAL {
    public abstract class Base<Type> : IBase<Type> {

        public static SqlConnection conn;
        public Base() {
            if (conn == null) {
                conn = new SqlConnection();
            }
            if (conn.State == System.Data.ConnectionState.Closed) {
                string connectString =  ConfigurationManager.ConnectionStrings["SQLSERVER"].ConnectionString;
                Console.WriteLine(connectString);
                conn = new SqlConnection(connectString);
                conn.Open();
            }
        }
        public Object getById(int id) {
            object data = null; ;
            try {
                var a = typeof(Type);
                string sql = "select ";
                PropertyInfo[] name = a.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (PropertyInfo k in name) {
                    sql += k.Name + ", ";
                }
                sql = sql.Remove(sql.Length - 2);
                sql += " from " + a.Name + " where id = " + id + "; ";
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader read = command.ExecuteReader();
                object[] obj = new object[name.Length];
                if (read.HasRows) {
                    read.Read();
                    int i = 0;
                    foreach (PropertyInfo k in name) {
                        obj[i] = read[k.Name].ToString();
                        i++;
                    }
                }
                read.Close();
                data = Activator.CreateInstance(typeof(Type), obj);
                Console.WriteLine(sql);
                return data;
            }
            catch (SqlException ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return data;
            }
        }
        public object create(Type type) {
            object data = null;
            string sql = "";
            bool check = false;
            try {
                sql = "insert into " + type.GetType().Name + " ( ";
                PropertyInfo[] name = type.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (PropertyInfo k in name) {
                    if (!k.Name.Equals("id")) {
                        sql += k.Name + ",";
                    }
                    else check = true;
                }
                sql = sql.Remove(sql.Length - 1);
                /*sql += " ) OUTPUT INSERTED.ID values ('";*/
                if (check) sql += ") OUTPUT INSERTED.ID values ('";
                else sql += ") values ('";
                foreach (PropertyInfo k in name) {
                    if (!k.Name.Equals("id"))
                        sql += k.GetValue(type) + "', '";
                }
                sql = sql.Remove(sql.Length - 3);
                sql += " );";
                System.Diagnostics.Debug.WriteLine(sql);
                SqlCommand command = new SqlCommand(sql, conn);
                string id = null;
                if (check) { 
                    id = command.ExecuteScalar().ToString(); 
                }
                else {
                    command.ExecuteScalar();
                }

                object[] obj = new object[name.Length];
                    int i = 0;
                    foreach (PropertyInfo k in name) {
                        obj[i] = k.GetValue(type).ToString();
                        i++;
                    }
                    if(check)
                        obj[0] = id;
                data = Activator.CreateInstance(typeof(Type), obj);
                return data;
            }catch(SqlException ex) {
                System.Diagnostics.Debug.WriteLine("Exception: " +ex.Message);
                return data;
            }
        }
        public bool delete(Type type) {
            try {
                string sql = "delete from " + type.GetType().Name + " where id = '";
                PropertyInfo[] name = type.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                sql += name[0].GetValue(type) + "';";
                SqlCommand command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
                return true;
            } catch (SqlException ex) {
                return false;
            }
        }

        public bool edit(Type type) {
            try {
                string sql = "update " + type.GetType().Name + " set ";
                PropertyInfo[] name = type.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (PropertyInfo k in name) {
                    if(!k.Name.Equals("id"))
                        sql += k.Name + " = '" + k.GetValue(type) + "' ,";
                }
                sql = sql.Remove(sql.Length - 1);
                sql += " where id = " + name[0].GetValue(type) + "";
                System.Diagnostics.Debug.WriteLine(sql);
                SqlCommand command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
                return true;
            } catch (SqlException ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public ArrayList find(string name) {
            throw new NotImplementedException();
        }


        public List<Type> getAll() {
            object data = null;
            var list = new List<Type>();
            try {
                var a = typeof(Type);
                string sql = "select ";
                PropertyInfo[] name = a.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (PropertyInfo k in name) {
                    sql += k.Name + ", ";
                }
                sql = sql.Remove(sql.Length - 2);
                sql += " from " + a.Name;
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataReader read = command.ExecuteReader();
                object[] obj = new object[name.Length];
                if (read.HasRows) {
                    while (read.Read()) {
                        int i = 0;
                        foreach (PropertyInfo k in name) {
                            obj[i] = read[i].ToString();
                            i++;
                        }
                        data = Activator.CreateInstance(typeof(Type), obj);
                        list.Add((Type)data);
                    }
                }
                read.Close();
                Console.WriteLine(sql);
                return list;
            }
            catch (Exception ex) {
                //throw ex;
                return list;
            }
        }

    }
}