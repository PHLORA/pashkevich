using BookStore.DAL.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models.Manager
{
	public class BookModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
		public BookModel(Book bookDAL)
		{
			Id = bookDAL.Id;
			Name = bookDAL.Name;
			Description = bookDAL.Description;
			Price = bookDAL.Price;
			Author = bookDAL.Author.Name;
			Genre = bookDAL.Genre.Name;
		}
		public BookModel() { }
	}
}