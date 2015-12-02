using System;
using System.Collections.Generic;

namespace trex.Models
{
    public class SecurityGroup
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime? Deleted { get; set; }

        public Company Company { get; set; }

        public SecurityGroup ParentGroup { get; set; }
        public virtual ICollection<SecurityGroup> ChildGroups { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}