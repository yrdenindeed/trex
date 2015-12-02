using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trex.Models
{
    public class Organisation
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailDomain { get; set; }

        public DateTime? Deleted { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        
        public virtual ICollection<PermissionControl> PermissionControls { get; set; }
    }
}