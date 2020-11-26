using BookStore.DAL.Entities.Identity;
using BookStore.DAL.Entities.Store;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.EF
{
    public class StoreContext : IdentityDbContext<ApplicationUser>
    {
        public StoreContext(string conectionString) : base(conectionString) { }
        public StoreContext() : base("StoreContext") { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }

    }
}
