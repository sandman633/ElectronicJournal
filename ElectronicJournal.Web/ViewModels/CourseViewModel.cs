using ElectronicJournal.Web.Models;
using System.Collections.Generic;

namespace ElectronicJournal.Web.ViewModels
{
    public class CourseViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Group> Groups { get; set; }

        public ICollection<UserGroup> UserGroups { get; set; }

    }
}
