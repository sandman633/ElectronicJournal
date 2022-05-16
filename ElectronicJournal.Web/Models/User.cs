using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ElectronicJournal.Web.Models
{
    public class User : IdentityUser<Guid>
    {

        public string Name { get; set; }

        public Guid? PrincipalId { get; set; }

        public User Principal { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }

        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<CourseRequest> UserCourseRequestsPrincipal { get; set; }
        public ICollection<CourseRequest> UserCourseRequestsSender { get; set; }
    }

}
