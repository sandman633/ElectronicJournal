using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectronicJournal.Web.Pages
{
    public class CreateCourseModel : PageModel
    {
        private readonly ICourseRepository _repo;
        public Course CourseRequest;
        public CreateCourseModel(ICourseRepository repo)
        {
            CourseRequest = new Course();
            _repo = repo;
        }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public void OnPost()
        {

        }
    }
}
