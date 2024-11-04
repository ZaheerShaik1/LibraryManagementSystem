using LibraryMs.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class ViewBooksModel : PageModel
{
    public List<Book> Books { get; set; } = new List<Book>();
    public List<(Book Book, int StudentId, DateTime IssueDate, DateTime ExpiryDate)> IssuedBooks { get; set; } = new List<(Book, int, DateTime, DateTime)>();

    public void OnGet()
    {
        Books = LibraryManager.Instance.GetAllBooks();
        IssuedBooks = LibraryManager.Instance.GetIssuedBooksDetails(); // Fetch detailed issued books
    }

    public IActionResult OnPostRemoveBook(int bookId)
    {
        if (LibraryManager.Instance.RemoveBook(bookId))
        {
            TempData["Message"] = "Book removed successfully.";
        }
        else
        {
            TempData["Error"] = "Error removing book.";
        }
        return RedirectToPage();
    }
}
