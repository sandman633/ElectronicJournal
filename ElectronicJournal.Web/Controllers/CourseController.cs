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
        public CourseController(ICourseRepository courseRepo, IUserCourseRepository courseTypeRepo, ICourseRequestRepository courseRequestRepo, UserManager<User> userManager)
        {
            _repo = courseRepo;
            _userCourseRepo = courseTypeRepo;
            _userManager = userManager;
            _courseRequestRepo = courseRequestRepo;
        }


        public async Task<IActionResult> Index(bool showAll, int currentPage = 1, int pageSize = 10, string filter = null)
        {
            IEnumerable<Course> courses;
            var userClaim = User.Claims.Where(e => e.Type == "Id").First();
            Guid userId = new Guid(userClaim.Value);
            if (showAll)
            {
                courses = await _repo.GetAsync();
            }
            else
            {
                

                courses = await _repo.GetByCriteriaAsync(e => e.UserCourses.Select(x => x.UserId).Contains(userId));

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
                ShowAll = showAll,
                UserId = userId,
                PagingInfo = new PagingInfo()
                {
                    ItemsPerPage = pageSize,
                    TotalItems = courses.Count(),
                    CurrentPage = currentPage
                }
            };
            return View(model);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRequestRepo.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(new CourseRequestViewModel { Request = course });
        }

        public async Task<IActionResult> CreateCourse()
        {

            return View(new CourseCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(CourseCreateViewModel courseRequest)
        {
            if (ModelState.IsValid)
            {
                await CreateNewCourse(courseRequest);

                return RedirectToAction(nameof(Index));
            }
            return View(courseRequest);
        }

        private async Task CreateNewCourse(CourseCreateViewModel model)
        {
            var userid = User.Claims.First().Value;
            var user = await _userManager.FindByIdAsync(userid);

            var course = new Course();

            course.Id = new Guid();

            course.Description = model.Description;

            course.Name = model.Name;

            course.Created = DateTime.Now;

            course.Updated = course.Created;


            _repo.CreateAsync(course);
        }


        #region CourseRequest


        public async Task<IActionResult> DetailsRequest(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRequestRepo.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            return View(new CourseRequestViewModel { Request = course });
        }

        public async Task<IActionResult> CreateRequestAsync()
        {
            var userClaim = User.Claims.Where(e => e.Type == "Id").First();
            Guid userId = new Guid(userClaim.Value);

            var courses = await _repo.GetByCriteriaAsync(e => !e.UserCourses.Select(x => x.UserId).Contains(userId));

            ViewBag.Courses = new SelectList(courses, "Id", "Name").Select(e => e);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(CourseRequestCreateViewModel courseRequest)
        {


            if (ModelState.IsValid)
            {
                await CreateNewRequest(courseRequest);

                return RedirectToAction(nameof(RequestList));
            }
            return View(courseRequest);
        }

        private async Task CreateNewRequest(CourseRequestCreateViewModel model)
        {
            var userid = User.Claims.First().Value;
            var user = await _userManager.FindByIdAsync(userid);

            var courseRequest = new CourseRequest();

            courseRequest.Id = new Guid();

            courseRequest.Reason = model.Reason;

            courseRequest.CourseId = model.CourseId;

            courseRequest.PrincipalId = (Guid)user.PrincipalId;

            courseRequest.SenderId = user.Id;

            courseRequest.Created = DateTime.Now;

            courseRequest.Updated = courseRequest.Created;

            courseRequest.RequestStatusId = 1;

            _courseRequestRepo.CreateAsync(courseRequest);
        }


        public async Task<IActionResult> RequestList(int currentPage = 1, int pageSize = 10, string filter = null)
        {
            IEnumerable<CourseRequest> courses;
            var userClaim = User.Claims.Where(e => e.Type == "Id").First();
            Guid userId = new Guid(userClaim.Value);

            if (User.IsInRole("Liner"))
            {
                courses = await _courseRequestRepo.GetByCriteriaAsync(e => e.PrincipalId == userId);
            }
            else
            {
                courses = await _courseRequestRepo.GetByCriteriaAsync(e => e.SenderId == userId);
            }
            ListCourseRequestsViewModel model = new ListCourseRequestsViewModel()
            {
                Requests = courses
                .OrderBy(p => p.Created)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo()
                {
                    ItemsPerPage = pageSize,
                    TotalItems = courses.Count(),
                    CurrentPage = currentPage
                }
            };
            return View(model);
        }


        // GET: Course/Edit/5
        public async Task<IActionResult> EditRequest(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseRequestRepo.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            int statusid;

            if (course.RequestStatusId == 1)
            {
                statusid = 2;
            }
            else
            {
                statusid = course.RequestStatusId;
            }
            var model = new CourseRequestEditViewModel()
            {
                Id = course.Id,
                Reason = course.Reason,
                Comment = course?.Comment,
                Created = course.Created,
                RequestStatus = course.RequestStatus,
                RequestStatusId = statusid,
                CourseId = course.CourseId,
                Course = course.Course,
                Updated = (DateTime)course.Updated,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRequest(Guid? id, CourseRequestEditViewModel model, string action)
        {
            int reqstatus = Int32.Parse(action);

            var request = await _courseRequestRepo.GetByIdAsync(id);

            request.Comment = model?.Comment;
            request.Updated = DateTime.Now;

            request.RequestStatusId = reqstatus;
            request.RequestStatus = null;


            _courseRequestRepo.UpdateAsync(request);

            return RedirectToAction(nameof(RequestList));

        }


        #endregion

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
