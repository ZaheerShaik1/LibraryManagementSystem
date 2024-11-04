using LibraryMs.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LibraryMs.Pages
{
    public class AddStudentModel : PageModel
    {
        public string Student_Name { get; set; }= string.Empty;

        public string Department { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string mobile { get; set; }
        public IActionResult OnPost()
        {
            int sno = LibraryManager.Instance.GetNextStudentSno();
            string name = Request.Form["name"];
            int sid = Convert.ToInt32(Request.Form["id"]);
            string Department = Request.Form["Dept"];

            string mobile = Request.Form["mobile"];
            string email = Request.Form["email"];   

            var newStudent = new Student(sid, name ,email, mobile, Department);
            LibraryManager.Instance.AddStudent(newStudent);
            return RedirectToPage("/ViewStudent");
        }
    }
}
