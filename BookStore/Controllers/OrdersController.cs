using BookStore.DAL;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class OrdersController : Controller
    {
        private OrdersRepository ordersRepository = new OrdersRepository();
        private OrdersDetailsRepository ordersDetailsRepository = new OrdersDetailsRepository();
        private BooksRepository booksRepository = new BooksRepository();
        private UsersRepository usersRepository = new UsersRepository();
        // GET: Oders
        public ActionResult Index()
        {
            List<Orders> orders = ordersRepository.getAll();
            List<Orders> ordersResult = new List<Orders>();
            List<Users> users = new List<Users>();
            foreach (Orders order in orders) {
                if(order.state == 1) {
                    ordersResult.Add(order);
                    users.Add((Users)usersRepository.getById(order.usersId));
                }
            }
            ViewData["orders"] = ordersResult;
            ViewData["users"] = users;
            return View();
        }

        // GET: Oders/Details/5
        public ActionResult Details(int id)
        {
            Orders order = (Orders) ordersRepository.getById(id);
            List<Books> books = new List<Books>();
            var ordersDetails = ordersDetailsRepository.getAll();
            foreach(OrdersDetails ordersDetail in ordersDetails) {
                if(ordersDetail.ordersId == order.id) {
                    books.Add((Books)booksRepository.getById(ordersDetail.booksId));
                }
            }
            ViewData["orders"] = order;
            //ViewData["users"] = user;
            ViewData["books"] = books;
            return View("orders");
        }

        public ActionResult view() {
            string role = HttpContext.Request.Cookies.Get("role").Value;
            string idUser = HttpContext.Request.Cookies.Get("user").Value;
            var orders = ordersRepository.findByUserId(int.Parse(idUser));
            ViewData["orders"] = orders;
            return View("product-orders");
            /* Orders order = (Orders)ordersRepository.getById(id);
             Users user = (Users)usersRepository.getById(order.usersId);
             List<Books> books = new List<Books>();
             var ordersDetails = ordersDetailsRepository.getAll();
             foreach (OrdersDetails ordersDetail in ordersDetails) {
                 if (ordersDetail.ordersId == order.id) {
                     books.Add((Books)booksRepository.getById(ordersDetail.booksId));
                 }
             }
             ViewData["orders"] = order;
             ViewData["users"] = user;
             ViewData["books"] = books;
             return View();*/
        }



        // GET: Oders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Oders/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try {
                // TODO: Add insert logic here
                if (HttpContext.Request.Cookies.Get("user") != null && HttpContext.Request.Cookies.Get("role") != null) {
                    string role = HttpContext.Request.Cookies.Get("role").Value;
                    string idUser = HttpContext.Request.Cookies.Get("user").Value;
                    int id = int.Parse(idUser);
                    List<Orders> orders = ordersRepository.findByUserId(id);
                    
                    DateTime date = DateTime.UtcNow.Date;
                    string sqlFormattedDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss");
                    foreach (Orders order in orders) {
                        if (order.state == 0) {
                            OrdersDetails ordersDetails = new OrdersDetails(order.id.ToString(), collection["id"].ToString(),
                                collection["quantity"].ToString(), sqlFormattedDate, sqlFormattedDate);
                            ordersDetailsRepository.create(ordersDetails);
                            return Redirect("../home");
                        }
                    }
                    Orders newOrder = new Orders(null, "0", idUser, sqlFormattedDate, sqlFormattedDate);
                    newOrder = (Orders) ordersRepository.create(newOrder);
                    OrdersDetails newOrdersDetails = new OrdersDetails(newOrder.id.ToString(), collection["id"].ToString(),
                                collection["quantity"].ToString(), sqlFormattedDate, sqlFormattedDate);
                    newOrdersDetails = (OrdersDetails)ordersDetailsRepository.create(newOrdersDetails);
                    return Redirect("../home");
                }
                return Redirect("../login");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); 
                return View();
            }
}

        // GET: Oders/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Oders/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Oders/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Oders/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
