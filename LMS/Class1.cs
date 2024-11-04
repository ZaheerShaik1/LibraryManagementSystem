using System;
using System.Collections.Generic;
using System.Linq;

namespace LMSFinal
{
    public class Book
    {
        public int Id { get; set; }
        //public string Title { get; set; }=string.Empty;
        public string Title=  string.Empty;
        public string Author { get; set; }=string.Empty ;
        public DateTime DateAdded { get; set; }
        public bool IsIssued { get; set; }
        public int? RackId { get; set; }
        public Student? IssuedTo { get; set; }
    }

    public class Rack
    {
        public int Id { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }

    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Book> IssuedBooks { get; set; } = new List<Book>();
    }
    class Program1
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            for (int i = 1; i <= 4; i++)
            {
                Rack rack = new Rack { Id = i };
                library.AddRack(rack);
            }

            Console.Write("Enter the number of unique books you want to add: ");
            int numberOfBooks = int.Parse(Console.ReadLine() ?? "0");

            for (int i = 0; i < numberOfBooks; i++)
            {
                Console.Write("Enter Book Title: ");
                string? bookTitle = Console.ReadLine();
                Console.Write("Enter Author Name: ");
                string? authorName = Console.ReadLine();
                Console.Write("Enter Rack ID for the Book: ");
                int rackId = int.Parse(Console.ReadLine() ?? "1");

                Book book = new Book
                {
                        Id = library.GetNextBookId(),
                        Title = bookTitle,
                        Author = authorName,
                        DateAdded = DateTime.Now,
                        RackId = rackId
                 };
                 library.AddBook(book);

            }

            // Create and add students
            Console.Write("Enter the number of students you want to add: ");
            int numberOfStudents = int.Parse(Console.ReadLine() ?? "0");

            for (int i = 0; i < numberOfStudents; i++)
            {
                Console.Write("Enter Student ID: ");
                int studentId = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Enter Student Name: ");
                string studentName = Console.ReadLine();
                Student student = new Student { Id = studentId, Name = studentName };
                library.AddStudent(student);
            }

            // Example operations
            while (true)
            {
                Console.WriteLine("\nLibrary Management System");
                Console.WriteLine("1. Issue Book");
                Console.WriteLine("2. Find Available Books");
                Console.WriteLine("3. Find Issued Books");
                Console.WriteLine("4. List Books in Racks");
                Console.WriteLine("5. Find Age of a Book");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        // Issue Book
                        Console.Write("Enter Book ID to issue: ");
                        int issueBookId = int.Parse(Console.ReadLine() ?? "0");
                        Console.Write("Enter Student ID: ");
                        int issueStudentId = int.Parse(Console.ReadLine() ?? "0");
                        library.IssueBook(issueBookId, issueStudentId);
                        break;

                    case "2":
                        // Find Available Books
                        Console.WriteLine("Available Books:");
                        var availableBooks = library.FindAvailableBooks();
                        foreach (var book in availableBooks)
                        {
                            Console.WriteLine($"- {book.Title} by {book.Author}");
                        }
                        break;

                    case "3":
                        // Find Issued Books
                        Console.WriteLine("Issued Books:");
                        var issuedBooks = library.FindIssuedBooks();
                        foreach (var book in issuedBooks)
                        {
                            Console.WriteLine($"- {book.Title} by {book.Author}");
                        }
                        break;

                    case "4":
                        // List Books in Racks
                        Console.WriteLine("Books in Racks:");
                        foreach (var rack in library.GetAllRacks())
                        {
                            Console.WriteLine($"Rack {rack.Id}:");
                            var booksInRack = library.GetBooksInRack(rack.Id);
                            foreach (var book in booksInRack)
                            {
                                Console.WriteLine($"  - {book.Title} by {book.Author}");
                            }
                        }
                        break;

                    case "5":
                        // Find Age of a Book
                        Console.Write("Enter Book ID to find its age: ");
                        int bookId = int.Parse(Console.ReadLine() ?? "0");
                        int age = library.FindBookAge(bookId);
                        if (age >= 0)
                        {
                            Console.WriteLine($"The age of the book is {age} days.");
                        }
                        else
                        {
                            Console.WriteLine("Book not found.");
                        }
                        break;

                    case "6":
                        // Exit
                        return;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
    }

   

    public class Library
    {
        private List<Book> books = new List<Book>();
        private List<Rack> racks = new List<Rack>();
        private List<Student> students = new List<Student>();

        public void AddRack(Rack rack)
        {
            racks.Add(rack);
        }

        public void AddBook(Book book)
        {
            books.Add(book);
            // Add the book to the corresponding rack
            var rack = racks.FirstOrDefault(r => r.Id == book.RackId);
            rack?.Books.Add(book);
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public int GetNextBookId()
        {
            return books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
        }

        public void IssueBook(int bookId, int studentId)
        {
            Book? book = books.FirstOrDefault(b => b.Id == bookId && !b.IsIssued);
            Student? student = students.FirstOrDefault(s => s.Id == studentId);

            if (book != null && student != null)
            {
                book.IsIssued = true;
                student.IssuedBooks.Add(book);
                Console.WriteLine("Book issued successfully.");
            }
            else
            {
                Console.WriteLine("Book not available or student not found.");
            }
        }

        public List<Book> FindAvailableBooks()
        {
            return books.Where(b => !b.IsIssued).ToList();
        }

        public List<Book> FindIssuedBooks()
        {
            return books.Where(b => b.IsIssued).ToList();
        }

        public List<Rack> GetAllRacks()
        {
            return racks;
        }

        public List<Book> GetBooksInRack(int rackId)
        {
            return racks.FirstOrDefault(r => r.Id == rackId)?.Books ?? new List<Book>();
        }

        public int FindBookAge(int bookId)
        {
            Book? book = books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                return (DateTime.Now - book.DateAdded).Days;
            }
            return -1; // Book not found
        }
    }
}
