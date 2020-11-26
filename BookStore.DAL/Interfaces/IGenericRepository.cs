using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL.Interfaces
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        void Create(T item);
        T FindById(int id);
        IEnumerable<T> Get();
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        IEnumerable<T> Get(Func<T, bool> predicate);
        void RemoveAll(ICollection<T> items);
        void Remove(T item);
        void Update(T item);
        void Save();
    }
}
