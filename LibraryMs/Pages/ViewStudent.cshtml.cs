using LibraryMs.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryMs.Pages
{
    public class ViewStudentModel : PageModel
    {
        public List<Student> Students { get; set; } = new List<Student>();

        public void OnGet()
        {
            Students = LibraryManager.Instance.GetAllStudents();
        }

        public IActionResult OnPostRemoveStudent(int studentId)
        {
            var result = LibraryManager.Instance.RemoveStudent(studentId);
            if (result)
            {
                TempData["Message"] = "Student removed successfully.";
            }
            else
            {
                TempData["Error"] = "Failed to remove student.";
            }
            return RedirectToPage(); // Redirect to the same page
        }
    }
}
