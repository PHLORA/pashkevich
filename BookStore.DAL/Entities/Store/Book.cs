using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities.Store
{
	public class Book
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public bool isDel { get; set; }
		public virtual Author Author { get; set; }
		public virtual Genre Genre { get; set; }
		public virtual Picture Picture { get; set; }
	}
}
