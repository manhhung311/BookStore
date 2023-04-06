using BookStore.DAL;
using BookStore.DTO;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class RegisterController : Controller
    {
        // GET: register
        public ActionResult Index()
        {
            return View("signup");
        }

        // GET: register/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: register/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: register/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string name = collection["name"].ToString();
                string address = collection["address"].ToString();
                string username = collection["username"].ToString();
                string password = collection["password"].ToString();
                string email = collection["email"].ToString();
                string phone = collection["phone"].ToString();
                DateTime date = DateTime.UtcNow.Date;
                string sqlFormattedDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss");
                Users user = new Users(null, name, username, password, address,
                    email, phone, "0", sqlFormattedDate, sqlFormattedDate
                );
                UsersRepository usersRepository = new UsersRepository();
                var userResult = usersRepository.create(user);
                if (userResult != null) {
                    return Redirect("../Login");
                }
                else {
                    DTOMessage err = new DTOMessage();
                    err.status = 403;
                    err.message = "Tài Khoản này đã được đăng ký vui lòng kiểm tra lại";
                    return View("signup", err);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }

        // GET: register/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: register/Edit/5
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

        // GET: register/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: register/Delete/5
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
