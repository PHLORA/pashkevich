using BookStore.DAL.Entities.Store;
using BookStore.DAL.Services;
using BookStore.WebUI.Models;
using BookStore.WebUI.Models.Manager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class ManagerController : Controller
    {
        StoreService service = new StoreService("StoreContext");
        public ActionResult Index()
        {
            List<BookViewModel> list = new List<BookViewModel>();
            foreach (Book item in service.Books.Get().Where(b=>b.isDel==false))
                list.Add(new BookViewModel(item));
            return View(list);
        }
        [HttpPost]
        public ActionResult Create(BookModel book, HttpPostedFileBase upload)
        {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(upload.InputStream))
                {
                    imageData = binaryReader.ReadBytes(upload.ContentLength);
                }
               var picture = new Picture { Image = imageData, Name = upload.FileName };
            Book bookNew = new Book
            {
                Name = book.Name,
                Price = book.Price,
                Description = book.Description,
                Picture = picture
            };
            if (service.Authors.Find(a => a.Name == book.Author).FirstOrDefault() == null)
                bookNew.Author = new Author { Name = book.Author };
            else
                bookNew.Author = service.Authors.Find(a => a.Name == book.Author).FirstOrDefault();
            if (service.Genres.Find(g => g.Name == book.Genre).FirstOrDefault() == null)
                bookNew.Genre = new Genre { Name = book.Genre };
            else
                bookNew.Genre = service.Genres.Find(g => g.Name == book.Genre).FirstOrDefault();
            service.Books.Create(bookNew);
            return RedirectToAction("Index");
		}
        public ActionResult Create()
		{
            return View();
		}
        public ActionResult Edit(int id)
		{
            return View(new BookModel(service.Books.FindById(id)));
		}
        [HttpPost]
        public ActionResult Edit(BookModel book)
		{
            Book bookUp = service.Books.FindById(book.Id);
            bookUp.Description = book.Description;
            bookUp.Name = book.Name;
            bookUp.Price = book.Price;
            if (service.Authors.Find(a => a.Name == book.Author).FirstOrDefault() == null)
                bookUp.Author = new Author { Name = book.Author };
            else
                bookUp.Author = service.Authors.Find(a => a.Name == book.Author).FirstOrDefault();
            if (service.Genres.Find(g => g.Name == book.Genre).FirstOrDefault() == null)
                bookUp.Genre = new Genre { Name = book.Genre };
            else
                bookUp.Genre = service.Genres.Find(g => g.Name == book.Genre).FirstOrDefault();
            service.Books.Update(bookUp);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
		{
            var bookDel = service.Books.FindById(id);
            bookDel.isDel = true;
            service.Books.Update(bookDel);
            //if (service.Authors.Find(a => a.Name == bookDel.Author.Name).FirstOrDefault().Books.Where(b => b.isDel == false).Count() == 0)
               // service.Authors.Remove(service.Authors.Find(a => a.Name == bookDel.Author.Name).FirstOrDefault());
            return RedirectToAction("Index");
        }


    }
}