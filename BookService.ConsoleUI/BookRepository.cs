using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookService.Model;
using BookService.Services;

namespace BookService.ConsoleUI {
    public class BookRepository : IRepository<Book> {

        private string FilePath { get; }

        public BookRepository(string filePath) {
            FilePath = filePath;
        }

        #region Public Methods
        public IEnumerable<Book> GetAllItems() {
            FileMode mode = !File.Exists(FilePath) ? FileMode.Create : FileMode.Open;
            List<Book> books = new List<Book>();
            using(BinaryReader reader = new BinaryReader(new FileStream(FilePath, mode))) {
                while(reader.PeekChar() > -1) {
                    string author = reader.ReadString();
                    string title = reader.ReadString();
                    string publishOrganization = reader.ReadString();
                    int pagesNumber = reader.ReadInt32();
                    double price = reader.ReadDouble();
                    books.Add(new Book(author, title, publishOrganization, pagesNumber, price));
                }    
            }
            return books;
        }

        public void Create(Book item) {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            
            Create(new List<Book> {item}, FileMode.Append);
        }

        public void Create(IEnumerable<Book> items) {
            if(items == null)
                throw new ArgumentNullException(nameof(items));
            
           Create(items, FileMode.Append);
        }

        public bool Delete(Book item) {
            if(item == null)
                throw new ArgumentNullException(nameof(item));
            List<Book> books = GetAllItems().ToList();
            bool result = books.Remove(item);
            if (result)
                Create(books, FileMode.Create);
            return result;
        }
        #endregion

        #region Private Methods
        private void WriteBook(Book item, BinaryWriter writer) {
            writer.Write(item.Author);
            writer.Write(item.Title);
            writer.Write(item.PublishOrganization);
            writer.Write(item.PagesNumber);
            writer.Write(item.Price);
        }

        private void Create(IEnumerable<Book> items, FileMode mode) {
            using (BinaryWriter writer = new BinaryWriter(new FileStream(FilePath, mode, FileAccess.Write))) {
                foreach (var item in items) {
                    if (item != null)
                        WriteBook(item, writer);
                }
            }
        }
        #endregion 
    }
}
