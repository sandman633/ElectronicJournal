using ElectronicJournal.Web.Models;
using ElectronicJournal.Web.Repositories.Interfaces;
using ElectronicJournal.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectronicJournal.Web.Pages
{
    public class CourseEditModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserCourseRepository _userCourseRepository;

        public CourseEditModel(UserManager<User> userManager,IGroupRepository groupRepository, IUserGroupRepository userGroupRepository, ICourseRepository courseRepository, IUserCourseRepository userCourseRepository)
        {
            _userManager = userManager;
            _groupRepository = groupRepository;
            _userGroupRepository = userGroupRepository;
            _courseRepository = courseRepository;
            _userCourseRepository = userCourseRepository;
        }
        public PagingInfo usersPagingInfo { get; set; }
        public PagingInfo groupsPagingInfo { get; set; }
        public Guid UserIdAdd { get; set; }
        public Course Course { get; set; }
        public IEnumerable<UserCourse> Users { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<User> NotInCourseUsers { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(Guid courseId, int currentPageGroups = 1, int currentPageUsers = 1, int pageSize = 5, string returnUrl = "/")
        {
            PrefillDataAsync(courseId, currentPageUsers: currentPageUsers, pageSize: pageSize);
        }


        public void OnPostUsers(Guid courseId, int currentPageGroups = 1, int currentPageUsers = 1, int pageSize = 5)
        {
            PrefillDataAsync(courseId, currentPageUsers: currentPageUsers, pageSize: pageSize);
        }
        public void OnPostGroups(Guid courseId, int currentPageGroups = 1, int currentPageUsers = 1, int pageSize = 5)
        {
            PrefillDataAsync(courseId, currentPageGroups: currentPageGroups, pageSize: pageSize);
        }

        public async void OnPostRemove(Guid userId, Guid courseId, int currentPageGroups = 1, int currentPageUsers = 1, int pageSize = 5)
        {
            _userCourseRepository.DeleteUser(userId, courseId);
            PrefillDataAsync(courseId, currentPageUsers: currentPageUsers, pageSize: pageSize);
        }
        public async void OnPostAddUserAsync(string username, Guid courseId, int currentPageGroups = 1, int currentPageUsers = 1, int pageSize = 5)
        {
            var user =  _userManager.Users.Where(p=>p.Name == username).First();

            UserCourse userCourse = new UserCourse()
            {
                Id = new Guid(),
                UserId = user.Id,
                CourseId = courseId,
                StatusId = 1,
            };
            _userCourseRepository.CreateAsync(userCourse);

            PrefillDataAsync(courseId, currentPageUsers: currentPageUsers,pageSize: pageSize);
        }
        private void  PrefillDataAsync(Guid courseId, int currentPageGroups=1, int currentPageUsers=1, int pageSize=5, string returnUrl = "/")
        {
            Groups = new List<Group>(10);
            Course = _courseRepository.GetByIdAsync(courseId).Result;
            usersPagingInfo = new PagingInfo()
            {
                CurrentPage = currentPageUsers,
                ItemsPerPage = pageSize,
                TotalItems = Course.UserCourses.Count,
            };

            Users = Course.UserCourses
                .Where(p =>
                {
                    return p.CourseId == courseId;
                })
                .OrderBy(p => p.Id)
                .Skip((currentPageUsers - 1) * pageSize)
                .Take(pageSize);
            var usergroups =  _userGroupRepository.GetAsync().Result;
            var userid = Course.UserCourses
                .Where(p =>
                {
                    return p.CourseId == courseId;
                }).Select(e => e.UserId);
            var groupsid = usergroups.Where(e => userid.Contains(e.UserId)).Select(e => e.Group.Id).Distinct();
            Groups =  _groupRepository.GetByCriteriaAsync(e => groupsid.Contains(e.Id)).Result.OrderBy(p => p.Id)
                .Skip((currentPageGroups - 1) * pageSize)
                .Take(pageSize); 
            groupsPagingInfo = new PagingInfo()
            {
                CurrentPage = currentPageGroups,
                ItemsPerPage = pageSize,
                TotalItems = Groups.Count(),
            };
            ReturnUrl = returnUrl ?? "/";
            var usercoursids = Course.UserCourses.Select(e => e.UserId).ToArray();
            NotInCourseUsers = _userManager.Users.Where(p => !usercoursids.Contains(p.Id)).ToArray();

        }

    }
}
