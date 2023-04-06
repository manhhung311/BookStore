using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models {
    public class Users {

        public Users(string id, string name, string username,
            string password, string address, string email, string phoneNumber,
            string role, string creatAt, string updateAt) {
            this.id = (id != null)?int.Parse(id):-1;
            this.username = username;
            this.password = password;
            this.name = name;
            this.address = address;
            this.createAt = creatAt;
            this.updateAt = updateAt;
            this.role = int.Parse(role);
            this.phoneNumber = phoneNumber;
            this.email = email;
        }
        public Users() { }
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }

        public string password { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public int role { get; set; }
        public string createAt { get; set; }
        public string updateAt { get; set; }
    }
}