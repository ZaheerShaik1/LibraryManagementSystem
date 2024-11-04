using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LibraryMs.Model;

namespace LibraryMs.Pages
{
    public class BookModel : PageModel
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public int RackId { get; set; }

       
        public IActionResult OnPost()
        {
            int id = LibraryManager.Instance.GetNextBookId();
            string title = Request.Form["title"];
            string author = Request.Form["author"];
            int isbn = Convert.ToInt32(Request.Form["isbn"]);
            
            int rackId = Convert.ToInt32(Request.Form["rackId"]);

            var newBook = new Book(id, title, author, isbn, rackId);
            LibraryManager.Instance.AddBook(newBook);
            return RedirectToPage("/ViewBooks");
        }

    }
}

