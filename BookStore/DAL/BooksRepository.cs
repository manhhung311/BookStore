﻿using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.DAL {
    public class BooksRepository:Base<Books> {
        public BooksRepository():base() {

        }
    }
}