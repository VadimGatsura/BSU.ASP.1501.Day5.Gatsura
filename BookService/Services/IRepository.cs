using System;
using System.Collections.Generic;

namespace BookService.Services {
    public interface IRepository<T> where T : IEquatable<T> {
        /// <summary>Read all items in repository</summary>
        /// <returns>All items from repository</returns>
        IEnumerable<T> GetAllItems(); 
        /// <summary>Add item to repository</summary>
        /// <param name="item">Item to add</param>
        void Create(T item);
        /// <summary>Add items to repository</summary>
        /// <param name="items">Items to add</param>
        void Create(IEnumerable<T> items);
        /// <summary>Delete item from repository</summary>
        /// <param name="item">Item for removal</param>
        /// <returns>Result of removal</returns>
        bool Delete(T item);
    }

}
