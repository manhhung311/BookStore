using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DTO {
    public class DTOMessage {
        public DTOMessage() { }
        public int status { get; set; }
        public string message { get; set; }
    }
}