using BookStore.DAL.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebUI.Models
{
	public class BookViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public string Author { get; set; }
		public string Genre { get; set; }
		public byte[] Image { get; set; }
		public BookViewModel(Book bookDAL)
		{
			Id = bookDAL.Id;
			Name = bookDAL.Name;
			Description = bookDAL.Description;
			Price = bookDAL.Price;
			Author = bookDAL.Author.Name;
			Genre = bookDAL.Genre.Name;
			Image = bookDAL.Picture.Image;
		}
		public BookViewModel() { }
	}
}