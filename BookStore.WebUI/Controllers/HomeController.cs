using BookStore.DAL.Entities.Store;
using BookStore.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
	public class HomeController : Controller
	{
		StoreService service = new StoreService("StoreContext");
		public ActionResult Index()
		{
			return View(service.Books.Get().ToList());
		}
	}
}