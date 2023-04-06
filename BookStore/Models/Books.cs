using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models {
    public class Books {
        public Books(string id, string name, string title, string price, string quantity, string description, string image,
            string authorsId, string publishersId, string createAt, string updateAt) {
            this.id = (id == null)?-1:int.Parse(id);
            this.title = title;
            this.quantity = quantity != null ? int.Parse(quantity):-1;
            this.description = description;
            this.image = image;
            this.authorsId = authorsId != null?int.Parse(authorsId):-1;
            this.publishersId = publishersId != null ? int.Parse(publishersId):-1;
            this.createAt = createAt;
            this.updateAt = updateAt;
            this.price = int.Parse(price);
            this.name = name;
        }
        public int id { get; set; }
        public string name { get; set; }
        public string title { get; set; }

        public int price { get; set; }
        public int quantity { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public int authorsId { get; set; }
        public int publishersId { get; set; }
        public string createAt { get; set; }
        public string updateAt { get; set; }

    }
}