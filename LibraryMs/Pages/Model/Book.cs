

using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibraryMs.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int ISBN { get; set; }
        public DateTime Date { get; set; }
        public int RackId { get; set; }

        public Book(int id, string title, string author, int isbn, int rackId)
        {
            Id = id;
            Title = title;
            Author = author;
            ISBN = isbn;
            Date = DateTime.Now;
            RackId = rackId;
        }

        
    }
}
