using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models {
    public class Publishers {
        public Publishers(string id, string name) {
            this.id = id!=null ?int.Parse(id):-1;
            this.name = name;
        }
        public int id { get; set; }
        public string name { get; set; }
    }
}