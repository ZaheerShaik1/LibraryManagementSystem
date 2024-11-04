namespace LibraryMs.Model
{
    public class Rack
    {
        public int Id { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
