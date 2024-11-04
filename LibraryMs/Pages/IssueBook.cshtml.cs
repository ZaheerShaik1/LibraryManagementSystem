using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Collections.Generic;
using LibraryMs.Model;

namespace LibraryMs.Pages
{
    public class IssueBookModel : PageModel
    {
        public List<Book> AvailableBooks { get; set; }
        public List<Student> AvailableStudents { get; set; }

        public void OnGet()
        {
            AvailableBooks = LibraryManager.Instance.FindAvailableBooks();
            AvailableStudents = LibraryManager.Instance.GetAllStudents();
        }

        public IActionResult OnPost()
        {
            int bookId = Convert.ToInt32(Request.Form["bookid"]);
            int studentId = Convert.ToInt32(Request.Form["id"]);
            int duration = Convert.ToInt32(Request.Form["Duration"]);

            LibraryManager.Instance.IssueBook(bookId, studentId, duration);
            return RedirectToPage("/Index"); // Redirect after issuing the book
        }
    }
}
