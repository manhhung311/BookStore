using BookStore.DAL;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            //UsersRepository userRepository = new UsersRepository();
            //Users user = (Users) userRepository.getById(3);
            //Users newUser = new Users("4", "manhhung31198", "1234566575675412");
            /*userRepository.delete(user);*/
            /*user.password = "1234345325";
            if (userRepository.edit(user))*/
            //return View("index", user);
            /*else return View("About");*/
            BooksRepository booksRepository = new BooksRepository();
            var list = booksRepository.getAll();
            return View("products", list);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}