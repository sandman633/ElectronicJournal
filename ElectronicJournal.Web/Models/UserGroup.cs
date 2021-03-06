using System;

namespace ElectronicJournal.Web.Models
{
    public class UserGroup : BaseEntity
    {
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
