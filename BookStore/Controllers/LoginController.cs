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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if(HttpContext.Request.Cookies.Get("user") != null && HttpContext.Request.Cookies.Get("role") != null) {
                string role = HttpContext.Request.Cookies.Get("role").Value;
                if(role.Equals("1")) {
                    return Redirect("./HomeAdmin");
                }
            }
            return View("login");
        }

        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            try
            {
                // TODO: Add insert logic here
                UsersRepository usersRepository = new UsersRepository();
                string username = collection["username"].ToString();
                string password = collection["password"].ToString();
                var user = usersRepository.login(username, password);
                if (user != null) {
                    HttpContext.Response.Cookies.Add(new HttpCookie("user", user.id.ToString()));
                    HttpContext.Response.Cookies.Add(new HttpCookie("role", user.role.ToString()));
                    if(user.role == 1)
                        return Redirect("../HomeAdmin");
                    else 
                        return Redirect("../Home");
                }
                else {
                    DTOMessage err = new DTOMessage();
                    err.status = 401;
                    err.message = "Tài khoản hoặc mật khẩu không đúng";
                    return View("login", err);
                }
            }
            catch(Exception ex)
            {
                return View("index");
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
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

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
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
