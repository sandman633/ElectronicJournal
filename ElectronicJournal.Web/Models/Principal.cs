using System;
using System.Collections.Generic;

namespace ElectronicJournal.Web.Models
{
    public class Principal : BaseEntity
    {

        public Guid UserId { get; set; }

        public ICollection<User> UsersPrincipal { get; set; }
    }
}
