using BookStore.DAL;
using BookStore.DTO;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class BooksController : Controller {
        BooksRepository booksRepository = new BooksRepository();
        AuthorsRepository authorsRepository = new AuthorsRepository();
        PublishersRepository publishersRepository = new PublishersRepository();
        GenresRepository genresRepository = new GenresRepository();
        BooksGenresRepository booksGenresRepository = new BooksGenresRepository();
        // GET: Products
        public ActionResult Index() {

            /*var books = booksRepository.getAll();*/
            var list = booksRepository.getAll();
            return View("products", list);
        }


        public ActionResult View(int id) {

            /*var books = booksRepository.getAll();*/
            Books book = (Books)booksRepository.getById(id);
            Authors author = (Authors)authorsRepository.getById(book.authorsId);
            Publishers publisher = (Publishers)publishersRepository.getById(book.publishersId);
            /*List<Genres> list = new List<Genres>();
            var genres = genresRepository.getAll();
            foreach(Genres genre in genres) {
                if(genre.)
            }*/
            ViewData["books"] = book;
            ViewData["author"] = author;
            ViewData["publisher"] = publisher;
            return View("view");
        }

        // GET: Products/Details/5
        public ActionResult Details(int id) {

            Books book = (Books)booksRepository.getById(id);
            Authors author = (Authors) authorsRepository.getById(book.authorsId);
            Publishers publisher = (Publishers)publishersRepository.getById(book.publishersId);
            /*List<Genres> list = new List<Genres>();
            var genres = genresRepository.getAll();
            foreach(Genres genre in genres) {
                if(genre.)
            }*/
            ViewData["books"] = book;
            ViewData["author"] = author;
            ViewData["publisher"] = publisher;
            return View("product-detail");
        }

        // GET: Products/Create
        public ActionResult Create() {
            var authors = authorsRepository.getAll();
            var publishers = publishersRepository.getAll();
            var genres = genresRepository.getAll();
            ViewData["authors"] = authors;
            ViewData["publishers"] = publishers;
            ViewData["genres"] = genres;
            return View("add-products");
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            try {
                // TODO: Add insert logic here
                string name = collection["name"].ToString();
                string title = collection["title"].ToString();
                string price = collection["price"].ToString();
                string quantity = collection["quantity"].ToString();
                string description = collection["description"].ToString();
                string authorsId = collection["authorsId"].ToString();
                string publishersId = collection["publishersId"].ToString();
                string[] genresArray = collection["genres"].Split(',');
                string img = "";
                if (Request != null) {
                    HttpPostedFileBase file = Request.Files["image"];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName)) {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        string filename = file.FileName;
                        /*DateTime date = DateTime.UtcNow.Date;
                        string strdate = date.Date.ToString("yyyy-MM-dd HH:mm:ss");*/
                        string strdate = DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString().Split('.')[0];
                        img = strdate + "." + filename.Split('.')[1];
                        string path = Path.Combine(Server.MapPath("~/Asset/imageUpload"),
                                      Path.GetFileName(img));
                        System.Diagnostics.Debug.WriteLine(path);
                        file.SaveAs(path);
                    }
                }
                var authors = authorsRepository.getAll();
                var publishers = publishersRepository.getAll();
                var genres = genresRepository.getAll();
               
                ViewData["authors"] = authors;
                ViewData["publishers"] = publishers;
                ViewData["genres"] = genres;
                /*return View("Index");*/
                DTOMessage message = new DTOMessage();
                if (!img.Equals("")) {
                    DateTime date = DateTime.UtcNow.Date;
                    string sqlFormattedDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss");
                    Books book = new Books(null, name, title, price, quantity, description, img, authorsId, publishersId, sqlFormattedDate, sqlFormattedDate);
                    book = (Books)booksRepository.create(book);
                    if (book != null) {
                        if (genresArray.Length > 0) {
                            foreach (string genre in genresArray) {
                                BooksGenres booksGenres = new BooksGenres(book.id.ToString(), genre);
                                booksGenresRepository.create(booksGenres);
                            }
                        }
                        message.status = 201;
                        message.message = "OK";
                        return View("add-products", message);
                    }
                }
                message.status = 400;
                message.message = "Tạo Lỗi";
                return View("add-products", message);
            }
            catch {
                return RedirectToAction("Create");
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int id) {
            Books book = (Books)booksRepository.getById(id);
            Authors author = (Authors)authorsRepository.getById(book.authorsId);
            Publishers publisher = (Publishers)publishersRepository.getById(book.publishersId);
            var authors = authorsRepository.getAll();
            var publishers = publishersRepository.getAll();
            var genres = genresRepository.getAll();
            ViewData["book"] = book;
            ViewData["author"] = author;
            ViewData["publisher"] = publisher;
            ViewData["authors"] = authors;
            ViewData["publishers"] = publishers;
            ViewData["genres"] = genres;   
            return View("edit");
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection) {
            try { 
                string name = collection["name"].ToString();
                string title = collection["title"].ToString();
                string price = collection["price"].ToString();
                string quantity = collection["quantity"].ToString();
                string description = collection["description"].ToString();
                string authorsId = collection["authorsId"].ToString();
                string publishersId = collection["publishersId"].ToString();
                string[] genresArray = collection["genres"].Split(',');
                string img = "";
                if (Request != null) {
                    HttpPostedFileBase file = Request.Files["image"];
                    if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName)) {
                        string fileName = file.FileName;
                        string fileContentType = file.ContentType;
                        byte[] fileBytes = new byte[file.ContentLength];
                        file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        string filename = file.FileName;
                        string strdate = DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString().Split('.')[0];
                        img = strdate + "." + filename.Split('.')[1];
                        string path = Path.Combine(Server.MapPath("~/Asset/imageUpload"),
                                      Path.GetFileName(img));
                        System.Diagnostics.Debug.WriteLine(path);
                        file.SaveAs(path);
                    }
                }
                    DTOMessage message = new DTOMessage();
                    Books book = (Books)booksRepository.getById(id);
                    book.name = name;
                    book.title = title;
                    book.price = int.Parse(price);
                    book.quantity = int.Parse(quantity);
                    book.description = description;
                    book.authorsId = int.Parse(authorsId);
                    book.publishersId = int.Parse(publishersId);
                    if (!img.Equals("")) {
                        /*DateTime date = DateTime.UtcNow.Date;
                        string sqlFormattedDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss");*/
                        book.image = img;
                        bool result = booksRepository.edit(book);
                        if (book != null) {
                            message.status = 201;
                            message.message = "OK";
                            return View("add-products", message);
                        }
                    }
                else {
                    DateTime date = DateTime.UtcNow.Date;
                        string sqlFormattedDate = date.Date.ToString("yyyy-MM-dd HH:mm:ss");
                    book.updateAt = sqlFormattedDate;
                    bool result = booksRepository.edit(book);
                    if (result) {

                        message.status = 201;
                        message.message = "OK";
                        return View("add-products", message);
                    }
                }
                message.status = 400;
                message.message = "Tạo Lỗi";
                Books bookById = (Books)booksRepository.getById(id);
                Authors author = (Authors)authorsRepository.getById(book.authorsId);
                Publishers publisher = (Publishers)publishersRepository.getById(book.publishersId);
                var authors = authorsRepository.getAll();
                var publishers = publishersRepository.getAll();
                var genres = genresRepository.getAll();
                ViewData["book"] = bookById;
                ViewData["author"] = author;
                ViewData["publisher"] = publisher;
                ViewData["authors"] = authors;
                ViewData["publishers"] = publishers;
                ViewData["genres"] = genres;
                return View("add-products", message);
            }
            catch
            {
                return RedirectToAction("Edit");
    
            }
    }

        // GET: Products/Delete/5
        public ActionResult Delete(int id)
        {
            Books book = (Books)booksRepository.getById(id);
            Authors author = (Authors)authorsRepository.getById(book.authorsId);
            Publishers publisher = (Publishers)publishersRepository.getById(book.publishersId);
            ViewData["book"] = book;
            ViewData["author"] = author;
            ViewData["publisher"] = publisher;
            return View("delete");
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Books book = (Books)booksRepository.getById(id);
                List<BooksGenres> genres = booksGenresRepository.getAll();
                foreach(BooksGenres genre in genres) {
                    if(genre.booksId == book.id) {
                        booksGenresRepository.delete(genre);
                    }
                }
                bool result = booksRepository.delete(book);
                if (result) {
                    // xóa thành công
                    DTOMessage message = new DTOMessage();
                    message.message = "Đã xóa";
                    message.status = 201;
                    ViewData["message"] = message;
                    var list = booksRepository.getAll();
                    return View("products", list);
                }
                
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
