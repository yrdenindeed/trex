using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trex.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public string PhotoUrl { get; set; }

        public bool IsActive { get; set; }
        public bool IsGloballyIgnored { get; set; }
        public DateTime DateAdded { get; set; }

        public Organisation Organisation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<PermissionControl> PermissionControls { get; set; }

        public DateTime? Deleted { get; set; }

        public int Priority { get; set; }
    }
}