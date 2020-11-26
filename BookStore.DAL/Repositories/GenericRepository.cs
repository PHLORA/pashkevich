using BookStore.DAL.EF;
using BookStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Repositories
{
        public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            private StoreContext context;
            private DbSet<T> dbSet;
            public GenericRepository(StoreContext db)
            {
                context = db;
                dbSet = context.Set<T>();
            }
            public IEnumerable<T> Get()
            {
                return dbSet.AsNoTracking().ToList();
            }
            public IEnumerable<T> Get(Func<T, bool> predicate)
            {
                return dbSet.AsNoTracking().Where(predicate).ToList();
            }
            public T FindById(int id)
            {
                return dbSet.Find(id);
            }
            public void Create(T item)
            {
                dbSet.Add(item);
                context.SaveChanges();
            }
            public void Update(T item)
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
            public void Remove(T item)
            {
                dbSet.Remove(item);
                context.SaveChanges();
            }
            public void RemoveAll(ICollection<T> items)
            {
                dbSet.RemoveRange(items);
                context.SaveChanges();
            }
            public void Save()
            {
                context.SaveChanges();
            }
            public IEnumerable<T> Find(Func<T, Boolean> predicate)
            {
                return dbSet.Where(predicate).ToList();
            }
            private bool disposed = false;
            public virtual void Dispose(bool disposing)
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                        context.Dispose();
                    }
                    this.disposed = true;
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
    }
