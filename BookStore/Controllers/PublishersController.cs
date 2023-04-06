using BookStore.DAL;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class PublishersController : Controller
    {
        private PublishersRepository publishersRepository = new PublishersRepository();
        private BooksRepository booksRepository = new BooksRepository();
        // GET: Publishers
        public ActionResult Index()
        {
            List<Publishers> publishers = publishersRepository.getAll();
            List<double> percent = new List<double>();
            List<Books> books = booksRepository.getAll();
            double length = books.Count();
            foreach(Publishers publisher in publishers) {
                double dem = 0;
                foreach(Books book in books) {
                    if(book.authorsId == publisher.id) {
                        dem++;
                    }
                }
                if(books.Count > 0)
                percent.Add((dem / length) * 100);
            }
            ViewData["publishers"] = publishers;
            ViewData["percent"] = percent;
            return View("basic-table");
        }

        // GET: Publishers/Details/5
        public ActionResult Details(int id)
        {
            List<Books> books = booksRepository.getAll();
            Publishers publisher =(Publishers) publishersRepository.getById(id);
            List<Books> list = new List<Books>();
            foreach (Books book in books) {
                if (publisher.id == book.publishersId) {
                    list.Add(book);
                }
            }
            ViewData["books"] = list;
            ViewData["publisher"] = publisher;
            return View("view");
        }

        // GET: Publishers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string name = collection["name"].ToString();
                Publishers publishers = new Publishers(null, name);
                publishers = (Publishers) publishersRepository.create(publishers);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Publishers/Edit/5
        public ActionResult Edit(int id)
        {
            Publishers publishers = (Publishers)publishersRepository.getById(id);
            ViewData["publisher"] = publishers;
            return View("edit");
        }

        // POST: Publishers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Publishers publisher = (Publishers)publishersRepository.getById(id);
                publisher.name = collection["name"].ToString();
                publishersRepository.edit(publisher);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Publishers/Delete/5
        public ActionResult Delete(int id)
        {
            Publishers publisher = (Publishers)publishersRepository.getById(id);
            ViewData["publisher"] = publisher;
            return View("delete");
        }

        // POST: Publishers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Publishers publisher = (Publishers)publishersRepository.getById(id);
                bool result = publishersRepository.delete(publisher);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
