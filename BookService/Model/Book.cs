using System;
using System.Globalization;

namespace BookService.Model {
    public sealed class Book : IEquatable<Book>, IComparable<Book> {

        #region Public Fields
        public string Author { get; }
        public string Title { get; }
        public string PublishOrganization { get; }
        public int PagesNumber { get; }
        public double Price { get; }
        #endregion


        public Book(string author, string title, string publishOrganization, int pagesNumber, double price) {
            Author = author;
            Title = title;
            PublishOrganization = publishOrganization;
            PagesNumber = pagesNumber;
            Price = price;
        }


        #region Public Methods
        public int CompareTo(Book other) {
            if(other == null) return 1;
            return PagesNumber.CompareTo(other.PagesNumber);
        }

       
        public override bool Equals(object obj) {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Book) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = Author?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Title?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (PublishOrganization?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ PagesNumber;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();
                return hashCode;
            }
        }

        public bool Equals(Book other) {
            if(ReferenceEquals(null, other)) return false;
            if(ReferenceEquals(this, other)) return true;
            return string.Equals(Author, other.Author) && string.Equals(Title, other.Title) && string.Equals(PublishOrganization, other.PublishOrganization) && PagesNumber == other.PagesNumber && Price.Equals(other.Price);
        }

        public override string ToString() => $"Author: {Author}, Title: {Title}\n\tPublishOrganization: {PublishOrganization}, PagesNumber: {PagesNumber}, Price: {Price.ToString("C", CultureInfo.GetCultureInfo("be-BY"))}";

        #endregion
    }
}
