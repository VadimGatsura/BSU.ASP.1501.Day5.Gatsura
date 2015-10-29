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

        public BookListService(IRepository<Book> repository) {
            Repository = repository;
        }

        #region Public methods
        public void AddBook(Book book) {
            if(book == null)
                throw new ArgumentNullException(nameof(book));
            if(Repository.GetAllItems().Contains(book)) {
                throw new AddBookException($"BookListService contains {book}");   
            }
            Repository.Create(book);
        }

        public void RemoveBook(Book book) {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (!Repository.Delete(book)) {
                throw new RemoveBookException($"BookListService doesn't contain {book}");    
            }    
        }

        public IEnumerable<Book> Sort(Func<Book, object> keySelector) => Repository.GetAllItems().OrderBy(keySelector);
        public IEnumerable<Book> Find(Predicate<Book> match) => Repository.GetAllItems().ToList().FindAll(match);

        #endregion
    }
}
