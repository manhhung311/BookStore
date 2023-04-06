using BookStore.DAL;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class GenresController : Controller
    {
        private GenresRepository genresRepository = new GenresRepository();
        private BooksGenresRepository BooksGenresRepository = new BooksGenresRepository();
        private BooksRepository booksRepository = new BooksRepository();
        // GET: Genres
        public ActionResult Index()
        {
          
                    var genres = genresRepository.getAll();
                    List<double> percent = new List<double>();
                    List<Books> books = booksRepository.getAll();
                    List<BooksGenres> booksGenres = BooksGenresRepository.getAll();
                    foreach(Genres genre in genres) {
                        double dem = 0;
                        foreach(BooksGenres bookGenre in booksGenres) {
                            if(genre.id == bookGenre.genresId) {
                                dem++;
                            }
                        }
                        if(booksGenres.Count > 0)
                            percent.Add((dem / books.Count())*100);
                    }
                    ViewData["genres"] = genres;
                    ViewData["percent"] = percent;
                    return View("basic-table");
                   

        }

        // GET: Genres/Details/5
        public ActionResult Details(int id)
        {
            Genres genres = (Genres) genresRepository.getById(id);
            List<Books> books = new List<Books>();
            List<BooksGenres> booksGenres = BooksGenresRepository.getAll();
            foreach (BooksGenres booksGenre in booksGenres) {
                if(booksGenre.genresId == genres.id) {
                    books.Add((Books)booksRepository.getById(booksGenre.booksId));
                }
            }
            ViewData["books"] = books;
            ViewData["genre"] = genres;
            return View("view");
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            //if (HttpContext.Request.Cookies.Get("user") != null && HttpContext.Request.Cookies.Get("role") != null) {
                //string role = HttpContext.Request.Cookies.Get("role").Value;
                //if (role.Equals("1")) {
                    return View("create");
               // }
            //}
            return Redirect("../login");
        }

        // POST: Genres/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
               // if (HttpContext.Request.Cookies.Get("user") != null && HttpContext.Request.Cookies.Get("role") != null) {
                    //string role = HttpContext.Request.Cookies.Get("role").Value;
                    //if (role.Equals("1")) {
                        string name = collection["name"].ToString();
                        Genres genres = new Genres(null, name);
                        genres = (Genres)genresRepository.create(genres);
                        return RedirectToAction("Index");
                    //}
                //}
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(int id)
        {
            //if (HttpContext.Request.Cookies.Get("user") != null && HttpContext.Request.Cookies.Get("role") != null) {
               // string role = HttpContext.Request.Cookies.Get("role").Value;
               // if (role.Equals("1")) {
                    Genres genres = (Genres)genresRepository.getById(id);
                    ViewData["genres"] = genres;
                    return View("edit");
               // }
           // }
            return View();
        }

        // POST: Genres/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                //if (HttpContext.Request.Cookies.Get("user") != null && HttpContext.Request.Cookies.Get("role") != null) {
                    //string role = HttpContext.Request.Cookies.Get("role").Value;
                   // if (role.Equals("1")) {
                        Genres genre = (Genres)genresRepository.getById(id);
                        genre.name = collection["name"].ToString();
                        genresRepository.edit(genre);
                        return RedirectToAction("Index");

                   // }
                //}
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(int id)
        {
           // if (HttpContext.Request.Cookies.Get("user") != null && HttpContext.Request.Cookies.Get("role") != null) {
                //string role = HttpContext.Request.Cookies.Get("role").Value;
                //if (role.Equals("1")) {
                    Genres genre = (Genres)genresRepository.getById(id);
                    ViewData["genres"] = genre;
                    return View("delete");
               // }
            //}
            return View();
        }

        // POST: Genres/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //if (HttpContext.Request.Cookies.Get("user") != null && HttpContext.Request.Cookies.Get("role") != null) {
                   // string role = HttpContext.Request.Cookies.Get("role").Value;
                   // if (role.Equals("1")) { 
                        Genres genre = (Genres)genresRepository.getById(id);
                        bool result = genresRepository.delete(genre);
                        return RedirectToAction("Index");
                    //}
               // }
                // TODO: Add delete logic here
                return Redirect("../login");
            }
            catch
            {
                return View();
            }
        }
    }
}
