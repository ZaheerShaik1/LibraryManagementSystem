namespace LibraryMs.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Department { get; set; }

        // Change this to hold a tuple (Book, Duration)
        public List<(Book Book, int Duration)> IssuedBooks { get; set; } = new List<(Book, int)>();

        public DateTime? StudentAdded { get; set; }

        public Student(int id, string name, string email, string mobile, string department)
        {
            Id = id;
            Name = name;
            Email = email;
            Mobile = mobile;
            Department = department;
            StudentAdded = DateTime.Now;
        }
    }
}
