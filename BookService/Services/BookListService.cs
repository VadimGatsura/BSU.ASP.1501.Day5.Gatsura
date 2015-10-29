using System;
using System.Collections.Generic;
using System.Linq;
using BookService.Model;

namespace BookService.Services {

    public class BookListServiceException : Exception {public BookListServiceException(string message) : base(message) {} }
    public sealed class AddBookException : BookListServiceException { public AddBookException(string message) : base(message) { } }
    public sealed class RemoveBookException : BookListServiceException { public RemoveBookException(string message) : base(message) { } }


    public sealed class BookListService {
        private IRepository<Book>  Repository { get; }

        /// <summary>Get all books from repository</summary>
        public IEnumerable<Book> Books => Repository.GetAllItems();

        public BookListService(IRepository<Book> repository) {
            Repository = repository;
        }

        #region Public methods
        /// <summary>Add book to repository
        /// <remarks>If the book already in the repository thrown <see cref="AddBookException"/></remarks>
        /// </summary>
        /// <param name="book">Book to add</param>
        public void AddBook(Book book) {
            if(book == null)
                throw new ArgumentNullException(nameof(book));
            if(Repository.GetAllItems().Contains(book)) {
                throw new AddBookException($"BookListService contains {book}");   
            }
            Repository.Create(book);
        }
        /// <summary>Add books to repository
        /// <remarks>If the any book from <see cref="books"/> already in the repository thrown <see cref="AddBookException"/> and no book is added</remarks>
        /// </summary>
        /// <param name="books">Books to add</param>
        public void AddBooks(IEnumerable<Book> books) {
            if(books == null)
                throw new ArgumentNullException(nameof(books));
            var enumerable = books as IList<Book> ?? books.ToList();
            foreach(var book in enumerable.Where(book => Repository.GetAllItems().Contains(book))) {
                throw new AddBookException($"BookListService contains {book}");
            }
            Repository.Create(enumerable);
        }
        /// <summary>Remove book from repository
        /// <remarks>If the book isn't in the repository thrown <see cref="RemoveBookException"/></remarks>
        /// </summary>
        /// <param name="book">Book for removal</param>
        public void RemoveBook(Book book) {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (!Repository.Delete(book)) {
                throw new RemoveBookException($"BookListService doesn't contain {book}");    
            }    
        }

        /// <summary>Sorts the elements in ascending order according to a key</summary>
        /// <param name="keySelector">A function to extract a key from an element</param>
        /// <returns>A <see cref="IEnumerable{Book}"/> whose elements are sorted according to a key</returns>
        public IEnumerable<Book> Sort(Func<Book, object> keySelector) => Repository.GetAllItems().OrderBy(keySelector);
        /// <summary>Retrieves all the element that match the conditions by the specified predicate</summary>
        /// <param name="match">The <see cref="Predicate{Book}"/> delegate that defines the conditions of the elements to search for</param>
        /// <returns>A <see cref="IEnumerable{Book}"/> containing all the elements that match the conditions defined by the specified predicate, if found; otherwise, an empty <see cref="IEnumerable{Book}"/></returns>
        public IEnumerable<Book> Find(Predicate<Book> match) => Repository.GetAllItems().ToList().FindAll(match);

        #endregion
    }
}
