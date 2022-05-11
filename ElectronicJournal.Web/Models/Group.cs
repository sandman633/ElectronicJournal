using System.Collections.Generic;

namespace ElectronicJournal.Web.Models
{
    public class Group : BaseEntity
    {
        
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<UserGroup> UserGroups { get; set; }
    }
}