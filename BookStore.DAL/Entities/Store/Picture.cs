using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Entities.Store
{
	public class Picture
	{
		[Key]
		[ForeignKey("Book")]
		public int Id { get; set; }
		public string Name { get; set; }
		public byte[] Image { get; set; }

		public Book Book { get; set; }
	}
}
