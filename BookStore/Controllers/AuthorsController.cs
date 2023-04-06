using BookStore.DAL;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private AuthorsRepository authorsRepository = new AuthorsRepository();
        private BooksRepository booksRepository = new BooksRepository();
        // GET: Authors
        public ActionResult Index()
        {
            List<Authors> authors = authorsRepository.getAll();
            List<double> percent = new List<double>();
            List<Books> books = booksRepository.getAll();
            double length = books.Count();
            foreach (Authors author in authors) {
                double dem = 0;
                foreach (Books book in books) {
                    if (book.authorsId == author.id) {
                        dem++;
                    }
                }
                if(books.Count > 0)
                    percent.Add((dem / length) * 100);
            }
            ViewData["authors"] = authors;
            ViewData["percent"] = percent;
            return View("basic-table");
        }

        // GET: Authors/Details/5
        public ActionResult Details(int id)
        {
            List<Books> books = booksRepository.getAll();
            Authors author = (Authors)authorsRepository.getById(id);
            List<Books> list = new List<Books>();
            foreach (Books book in books) {
                if(author.id == book.authorsId) {
                    list.Add(book);
                }
            }
            ViewData["books"] = list;
            ViewData["author"] = author;
            return View("view");
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View("create");
        }

        // POST: Authors/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                string name = collection["name"].ToString();
                Authors authors = new Authors(null, name);
                authors = (Authors) authorsRepository.create(authors);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int id)
        {
            Authors authors = (Authors)authorsRepository.getById(id);
            ViewData["author"] = authors;
            return View("edit");
        }

        // POST: Authors/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Authors authors = (Authors)authorsRepository.getById(id);
                authors.name = collection["name"].ToString();
                authorsRepository.edit(authors);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int id)
        {
            Authors authors = (Authors)authorsRepository.getById(id);
            ViewData["author"] = authors;
            return View("delete");
        }

        // POST: Authors/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Authors authors = (Authors)authorsRepository.getById(id);
                bool result = authorsRepository.delete(authors);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
