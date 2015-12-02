using System;
using System.Collections.Generic;

namespace trex.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public Company OwningCompany { get; set; }
        public User DefaultAssignee { get; set; }

        public DateTime? Deleted { get; set; }


        public virtual ICollection<PermissionControl> PermissionControls { get; set; }
    
    }
}