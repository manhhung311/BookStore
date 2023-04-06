using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models {
    public class BooksGenres {
        public BooksGenres(string booksId, string genresId) {
            this.booksId = booksId != null? int.Parse(booksId): -1;
            this.genresId = genresId!=null? int.Parse(genresId): -1;
        }
        public int booksId { get; set; }
        public int genresId { get; set; }
    }
}