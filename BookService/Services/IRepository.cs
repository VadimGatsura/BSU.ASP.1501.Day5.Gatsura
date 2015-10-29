using System.Collections.Generic;

namespace BookService.Services {
    public interface IRepository<T> where T : class {
        IEnumerable<T> GetAllItems(); 
        void Create(T item);
        void Create(IEnumerable<T> items);
        bool Delete(T item);
    }

}
