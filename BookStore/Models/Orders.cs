using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models {
    public class Orders {
        public Orders(string id, string state, string usersId, string createAt, string updateAt) {
            this.id = id != null ? int.Parse(id) : -1;
            this.state = state != null ? int.Parse(state) : -1;
            this.usersId = usersId != null ? int.Parse(usersId) : -1;
            this.createAt = DateTime.Parse(createAt);
            this.updateAt = DateTime.Parse(updateAt);
        }
        public int id { get; set; }
        public int state { get; set; }
        public int usersId { get; set; }
        public DateTime createAt { get; set; }
        public DateTime updateAt { get; set; }
    }
}