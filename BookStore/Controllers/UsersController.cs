using BookStore.DAL;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class UsersController : Controller
    {
        UsersRepository usersRepository = new UsersRepository();
        // GET: Users
        public ActionResult Index()
        {
            var users = usersRepository.getAll();
            ViewData["users"] = users;
            return View("basic-table");
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            try {
                Users user = (Users) usersRepository.getById(id);
                return View("profile", user);
            }catch(Exception ex) {
                throw ex;
            }
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Users/Edit/5
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

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            Users user = (Users)usersRepository.getById(id);
            ViewData["user"] = user;
            return View("delete");
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Users user = (Users)usersRepository.getById(id);
                usersRepository.delete(user);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
