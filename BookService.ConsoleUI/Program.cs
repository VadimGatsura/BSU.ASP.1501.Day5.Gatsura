using System.Collections.Generic;
using System.IO;
using BookService.Model;
using BookService.Services;
using static System.Console;

namespace BookService.ConsoleUI {
    class Program {
        static void Main(string[] args) {
            BookListService service = new BookListService(new BookRepository("books"));

            if (!File.Exists("books"))
                service.AddBooks(new List<Book> {
                    new Book("J. K. Rowling", "Harry Potter and the Prisoner of Azkaban", "Махаон", 528, 184100),
                    new Book("J. K. Rowling", "Harry Potter and the Philosopher's Stone", "МАХАОН", 432, 172100),
                    new Book("Stephen King", "The Green Mile", "ACT", 384, 106100),
                    new Book("Margaret Mitchell", "Gone with the Wind", "Эксмо", 1280, 173100),
                    new Book("Arthur Conan Doyle", "A Study in Scarlet", "ACT", 304, 123700),
                    new Book("George Martin", "A Storm of Swords", "ACT", 960, 127300),
                    new Book("Борис Васильев", "А зори здесь тихие...", "Мартин", 432, 102200),
                    new Book("Alexandre Dumas", "Le comte de Monte Cristo", "ACT", 1216, 166700),
                    new Book("Harper Lee", "To Kill a Mockingbird", "ACT", 416, 90100)
                });

            IEnumerable<Book> find = service.Find(book => book.PublishOrganization.ToLowerInvariant() == "махаон");
            WriteLine("\tFind by publish organisation Махаон\n");

            foreach(var book in find) {
                WriteLine($"{book}\n");
            }

            IEnumerable<Book> sort = service.Sort(book => book.Price);
            WriteLine("\n\n\tSort by price\n");
            foreach(var book in sort) {
                WriteLine($"{book}\n");    
            }

            sort = service.Sort(book => book.PagesNumber);
            WriteLine("\n\n\tSort by number of pages\n");
            foreach (var book in sort) {
                WriteLine($"{book}\n");
            }

            WriteLine("\n\n\ttest exceptions\n");
            try {
                service.AddBook(new Book("Margaret Mitchell", "Gone with the Wind", "Эксмо", 1280, 173100));
            } catch(BookListServiceException ex) {
                WriteLine(ex.Message);
            }
            try {
                service.RemoveBook(new Book("Margaret Mitchell", "Gone with the Wind", "ACT", 1280, 173100));
            } catch(BookListServiceException ex) {
                WriteLine(ex.Message);
            }

            WriteLine("\n\n\tTest remove: 'Gone with the Wind' and '...the Prisoner of Azkaban'");
            service.RemoveBook(new Book("Margaret Mitchell", "Gone with the Wind", "Эксмо", 1280, 173100));
            service.RemoveBook(new Book("J. K. Rowling", "Harry Potter and the Prisoner of Azkaban", "Махаон", 528, 184100));

            foreach(var book in service.Books) {
                WriteLine($"{book}\n");
            }

            WriteLine("\n\n\tTest add book: 'Gone with the Wind' and '...the Prisoner of Azkaban' and 'Godfather'");
            service.AddBooks(new List<Book> {
                new Book("Margaret Mitchell", "Gone with the Wind", "Эксмо", 1280, 173100),
                new Book("J. K. Rowling", "Harry Potter and the Prisoner of Azkaban", "Махаон", 528, 184100),
                new Book("Mario Puzo", "Godfather", "Айрис-пресс", 288, 256300)
            });

            foreach (var book in service.Books) {
                WriteLine($"{book}\n");
            }

            service.RemoveBook(new Book("Mario Puzo", "Godfather", "Айрис-пресс", 288, 256300));

            ReadLine();
        }
    }
}
