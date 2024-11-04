namespace LibraryMs.Model
{
    public class LibraryManager
    {
        private static readonly LibraryManager _instance = new LibraryManager();
        public static LibraryManager Instance => _instance;

        public List<Book> books = new List<Book>();
        private List<Rack> racks = new List<Rack>();
        private List<Student> students = new List<Student>();

        public int idAuto = 1;
        public int sno = 1;

        private LibraryManager() { } // Private constructor to prevent instantiation

        public void AddRack(Rack rack) { racks.Add(rack); }
        public void AddBook(Book book) { books.Add(book); }
        public int GetNextBookId() { return idAuto++; }
        public List<Book> GetAllBooks() { return books; }

        public void AddStudent(Student student) { students.Add(student); }
        public int GetNextStudentSno() { return sno++; }
        public List<Student> GetAllStudents() { return students; }

        // Dictionary to track issued books and their durations
        private Dictionary<int, (int StudentId, int Duration)> issuedBooks = new Dictionary<int, (int, int)>();

        public void IssueBook(int bookId, int studentId, int duration)
        {
            Book? book = books.FirstOrDefault(b => b.Id == bookId && !issuedBooks.ContainsKey(b.Id));
            Student? student = students.FirstOrDefault(s => s.Id == studentId);

            if (book != null && student != null)
            {
                issuedBooks[book.Id] = (studentId, duration); // Track issued book and duration
                Console.WriteLine($"Book '{book.Title}' issued to '{student.Name}' for {duration} days.");
            }
            else
            {
                Console.WriteLine("Book not available or student not found.");
            }
        }

        public List<(Book Book, int Duration)> GetIssuedBooks()
        {
            var issuedList = new List<(Book, int)>();
            foreach (var entry in issuedBooks)
            {
                var book = books.FirstOrDefault(b => b.Id == entry.Key);
                if (book != null)
                {
                    issuedList.Add((book, entry.Value.Duration));
                }
            }
            return issuedList;
        }

        public List<Book> FindAvailableBooks()
        {
            return books.Where(b => !issuedBooks.ContainsKey(b.Id)).ToList();
        }

        public List<Book> FindIssuedBooks()
        {
            return books.Where(b => issuedBooks.ContainsKey(b.Id)).ToList();
        }

        public List<Rack> GetAllRacks()
        {
            return racks;
        }
        public List<(Book Book, int StudentId, DateTime IssueDate, DateTime ExpiryDate)> GetIssuedBooksDetails()
        {
            var issuedList = new List<(Book, int, DateTime, DateTime)>();
            foreach (var entry in issuedBooks)
            {
                var book = books.FirstOrDefault(b => b.Id == entry.Key);
                if (book != null)
                {
                    var issueDate = DateTime.Now; // Assuming issued now; replace with actual issue date if stored
                    var expiryDate = issueDate.AddDays(entry.Value.Duration);
                    issuedList.Add((book, entry.Value.StudentId, issueDate, expiryDate));
                }
            }
            return issuedList;
        }
        public bool RemoveBook(int bookId)
        {
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                books.Remove(book);
                issuedBooks.Remove(bookId); // Remove from issued list if exists
                return true;
            }
            return false; // Book not found
        }
        public bool RemoveStudent(int studentId)
        {
            var student = students.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                students.Remove(student);
                return true;
            }
            return false;
        }
    }
}
