using BookStore.DAL.EF;
using BookStore.DAL.Entities.Store;
using BookStore.DAL.Interfaces;
using BookStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Services
{
    public class StoreService
    {
        private StoreContext db;
        public StoreService(string connectionString)
        {
            db = new StoreContext(connectionString);
        }
        private GenericRepository<Book> BooksRepository;
        private GenericRepository<Author> AuthorsRepository;
        private GenericRepository<Genre> GenresRepository;
        public IGenericRepository<Book> Books
        {
            get
            {
                if (BooksRepository == null)
                {
                    BooksRepository = new GenericRepository<Book>(db);
                }
                return BooksRepository;
            }
        }
        public IGenericRepository<Author> Authors
        {
            get
            {
                if (AuthorsRepository == null)
                {
                    AuthorsRepository = new GenericRepository<Author>(db);
                }
                return AuthorsRepository;
            }
        }
        public IGenericRepository<Genre> Genres
        {
            get
            {
                if (GenresRepository == null)
                {
                    GenresRepository = new GenericRepository<Genre>(db);
                }
                return GenresRepository;
            }
        }
    }
}
