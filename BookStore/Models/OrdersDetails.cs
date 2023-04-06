using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models {
    public class OrdersDetails {
        public OrdersDetails(string ordersId, string booksId, string amount, string createAt, string updateAt) {
            this.ordersId = int.Parse(ordersId);
            this.booksId = int.Parse(booksId);
            this.amount = int.Parse(amount);
            this.createAt = DateTime.Parse(createAt);
            this.updateAt = DateTime.Parse(updateAt);
        }
        public int ordersId { get; set; }
        public int booksId { get; set; }
        public int amount { get; set; }
        public DateTime createAt {get; set;}
        public DateTime updateAt { get; set;}
    }
}