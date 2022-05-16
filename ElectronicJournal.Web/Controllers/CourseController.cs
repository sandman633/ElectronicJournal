using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;
using ElectronicJournal.Web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ElectronicJournal.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _repo;
        private readonly IUserCourseRepository _userCourseRepo;
        private readonly ICourseRequestRepository _courseRequestRepo;
        private readonly UserManager<User> _userManager;
        public CourseController(ICourseRepository courseRepo, IUserCourseRepository courseTypeRepo,ICourseRequestRepository courseRequestRepo, UserManager<User> userManager)
        {
            _repo = courseRepo;
            _userCourseRepo = courseTypeRepo;
            _userManager = userManager;
            _courseRequestRepo = courseRequestRepo;
        }

        // GET: Course
        public async Task<IActionResult> Index(int currentPage=1,int pageSize=10, string filter = null)
        {
            IEnumerable<Course> courses;

            if (User.IsInRole("Liner"))
            {
                courses = await _repo.GetAsync();
            }
            else
            {
                var userClaim = User.Claims.Where(e => e.Type == "Id").First();
                Guid userId = new Guid(userClaim.Value);

                courses = await _repo.GetByCriteriaAsync(e => e.UserCourses.Select(x=>x.UserId).Contains(userId));

            }
            ListCoursesViewModel model = new ListCoursesViewModel()
            {
                Courses = courses
                .Where(p =>
                {
                    return (filter == null) || (p.Name.StartsWith(filter));
                })
                .OrderBy(p => p.Id)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize),
                NameFilter = filter,
                PagingInfo = new PagingInfo()
                {
                    ItemsPerPage = pageSize,
                    TotalItems = courses.Count(),
                    CurrentPage = currentPage
                }
            };
            return View(model);
        }

        // GET: Course/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _repo.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        public async Task<IActionResult> CreateRequestAsync()
        {
            var courses = await _repo.GetAsync();
            ViewBag.CourseId = new SelectList(courses, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(CourseRequest courseRequest, string courseType)
        {
            var courses = await _repo.GetAsync();
            ViewBag.CourseId = new SelectList(courses, "Id", "Name");

            if (ModelState.IsValid)
            {
                await CreateNewRequest(courseRequest, courseType);

                return RedirectToAction(nameof(Index));
            }
            return View(courseRequest);
        }

        private async Task CreateNewRequest(CourseRequest courseRequest, string courseType)
        {
            var userid = User.Claims.First().Value;

            var user = await _userManager.FindByIdAsync(userid);

            courseRequest.Id = new Guid();

            courseRequest.PrincipalId = (Guid)user.PrincipalId;

            courseRequest.SenderId = user.Id;

            courseRequest.Created = DateTime.Now;

            courseRequest.TypeId = Int32.Parse(courseType);

            _courseRequestRepo.CreateAsync(courseRequest);
        }



        // GET: Course/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _repo.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Course/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_repo.Update(course);
                    //await _repo.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExistsAsync(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Course/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _repo.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //var course = await _repo.Courses.FindAsync(id);
            //_repo.Courses.Remove(course);
            //await _repo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExistsAsync(Guid id)
        {
            var courses = _repo.GetAsync().Result;
            return courses.Any(e => e.Id == id);
        }
    }
}
